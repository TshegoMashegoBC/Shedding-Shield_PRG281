using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Shedding_Shield
{
    
   public class Program
    {
        static List<User> users = new List<User>();
        static  User currentUser;
        static Schedule schedule = new Schedule();
         static Notification notification = new Notification();
        static Countdown countdown;

        public static void Main(string[] args)
        {
            countdown = new Countdown(notification);
            notification.OnNotify += (message) => Console.WriteLine($"[NOTIFICATION] {message}");

            while (true)
            {
                try
                {
                    if (currentUser == null || !currentUser.IsLoggedIn)
                    {
                        DisplayLoginMenu();
                    }
                    else
                    {
                        DisplayMainMenu();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void DisplayLoginMenu()
        {
            Console.WriteLine("\n1. Create Account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void CreateAccount()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = ReadPassword();

            try
            {
                var user = new User(name, email, password);
                users.Add(user);
                users.SaveUserData(user); // Save user data securely(data handler
                Console.WriteLine("Account created successfully!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void Login()
        {
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = ReadPassword();

            var user = users.FirstOrDefault(u => u.Email == email);
            if (user != null && user.Login(email, password))
            {
                currentUser = user;
                Console.WriteLine($"Welcome, {user.Name}!");
            }
            else
            {
                Console.WriteLine("Invalid email or password.");
            }
        }

        private static string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\n1. Select Region");
            Console.WriteLine("2. Add Task");
            Console.WriteLine("3. View Tasks");
            Console.WriteLine("4. Complete Task");
            Console.WriteLine("5. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SelectRegion();
                    break;
                case "2":
                    AddTask();
                    break;
                case "3":
                    ViewTasks();
                    break;
                case "4":
                    CompleteTask();
                    break;
                case "5":
                    currentUser.Logout();
                    countdown.StopCountdown();
                    currentUser = null;
                    Console.WriteLine("Logged out successfully.");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void SelectRegion()
        {
            Console.WriteLine("\nAvailable regions:");
            foreach (var area in schedule.GetRegions())
            {
                var (start, duration) = schedule.GetSchedule(area);
                Console.WriteLine($"{area}: {start} - {start + duration}");
            }
            Console.Write("Select a region: ");
            string region = Console.ReadLine();

            try
            {
                var (start, _) = schedule.GetSchedule(region);
                DateTime loadSheddingStart = DateTime.Today + start;
                if (loadSheddingStart < DateTime.Now)
                    loadSheddingStart = loadSheddingStart.AddDays(1);
                countdown.StartCountdown(loadSheddingStart);
                Console.WriteLine($"Countdown started for {region} load shedding at {loadSheddingStart:HH:mm}.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void AddTask()
        {
            try
            {
                Console.Write("Enter task description: ");
                string desc = Console.ReadLine();
                Console.Write("Enter planned time (HH:mm): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime time))
                    throw new ArgumentException("Invalid time format.");
                Console.Write("Enter duration in minutes: ");
                if (!int.TryParse(Console.ReadLine(), out int minutes) || minutes <= 0)
                    throw new ArgumentException("Invalid duration.");

                var task = new Task(desc, DateTime.Today + time.TimeOfDay, TimeSpan.FromMinutes(minutes));
                task.Plan();
                countdown.AddTask(task);
                Console.WriteLine("Task added successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ViewTasks()
        {
            var tasks = countdown.GetPendingTasks();
            if (!tasks.Any())
            {
                Console.WriteLine("No pending tasks.");
                return;
            }

            Console.WriteLine("\nPending Tasks:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task: {task.Description}, Planned: {task.PlannedTime:HH:mm}, Duration: {task.Duration.TotalMinutes} mins");
            }
        }

        static void CompleteTask()
        {
            Console.Write("Enter task description to complete: ");
            string desc = Console.ReadLine();

            try
            {
                countdown.RemoveTask(desc);
                Console.WriteLine("Task marked as completed.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}