using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shedding_Shield
{ // using threads
    public class Countdown
    {
        private readonly Notification notification;
        private readonly List<Task> tasks;
        private bool isRunning;

        public Countdown(Notification notification)
        {
            this.notification = notification;
            tasks = new List<Task>();
            isRunning = false;
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void RemoveTask(string description)
        {
            var task = tasks.FirstOrDefault(t => t.Description == description && !t.IsCompleted);
            if (task != null)
            {
                task.IsCompleted = true;
            }
            else
            {
                throw new InvalidOperationException("Task not found or already completed.");
            }
        }

        public IEnumerable<Task> GetPendingTasks()
        {
            return tasks.Where(t => !t.IsCompleted).OrderBy(t => t);
        }

        public void StartCountdown(DateTime loadSheddingStart)
        {
            if (isRunning) return;
            isRunning = true;

            Thread timerThread = new Thread(() =>
            {
                try
                {
                    while (isRunning && DateTime.Now < loadSheddingStart)
                    {
                        TimeSpan timeLeft = loadSheddingStart - DateTime.Now;
                        if (timeLeft.TotalSeconds <= 0)
                        {
                            notification.Notify("Load shedding has started! All tasks have been cleared.");
                            tasks.Clear();
                            isRunning = false;
                            break;
                        }

                        if (DateTime.Now.Minute == 0) // Notify every minute for simplicity
                        {
                            string tasksList = string.Join(", ", GetPendingTasks().Select(t => t.Description));
                            notification.Notify($"Time left until load shedding: {timeLeft.Hours}h {timeLeft.Minutes}m. Pending tasks: {tasksList}");
                        }

                        Thread.Sleep(1000); // Check every second
                    }
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Countdown interrupted.");
                }
            });

            timerThread.IsBackground = true;
            timerThread.Start();
        }

        public void StopCountdown()
        {
            isRunning = false;
        }
    }
}
