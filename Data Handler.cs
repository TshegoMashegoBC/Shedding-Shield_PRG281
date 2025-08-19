using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;


namespace Shedding_Shield
{
    public class Data_Handler
    { // saving the data of the user using the decrypted password
        public void SaveUserData(List<User> users)
        {
            // Here you would implement the logic to save the user data securely
            Console.WriteLine("Saving data for user...");
            try
            {
                List<User> users = new List<User>();
                while (true)
                {
                    Console.WriteLine("\nEnter user details (or type 'exit' to save and quit):");

                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    if (name.ToLower() == "exit")
                        break;

                    Console.Write("Email: ");
                    string email = Console.ReadLine();

                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    // Add user to the list
                    users.Add(new User { Name = name, Email = email, Password = password });

                    Console.WriteLine("User added successfully!");
                }

                if (users.Count > 0)
                {
                    string jsonData = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                    string filePath = "UserInfo.json";
                    File.WriteAllText(filePath, jsonData);

                    Console.WriteLine($"\nData saved to {filePath} successfully!");
                    Console.WriteLine("JSON Output:");
                    Console.WriteLine(jsonData);
                }
                else
                {
                    Console.WriteLine("\nNo data was entered.");
                }
            }

            catch (FormatException ex)
            {
                Console.WriteLine($"Error: Invalid input format. {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }
        static void SaveToUserPDF(List<User> users)
        {
            string pdfPath = "UserData.pdf";
            using (PdfWriter writer = new PdfWriter(pdfPath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);
                    document.Add(new Paragraph("User  Data"));

                    foreach (var user in users)
                    {
                        document.Add(new Paragraph($"Name: {user.Name}, Email: {user.Email}, Password: {user.Password}")); ;
                    }

                    document.Close();
                }
            }
            Console.WriteLine($"Data saved to {pdfPath} successfully!");
        }
        public void SaveSchedule(List<Schedule> UserScheule)
        {
            
                // Here you would implement the logic to save the user data securely
                Console.WriteLine("Saving data for user...");
                try
                {
                    List<User> users = new List<User>();
                    while (true)
                    {
                        Console.WriteLine("\nEnter user details (or type 'exit' to save and quit):");

                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        if (name.ToLower() == "exit")
                            break;

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        // Add user to the list
                        users.Add(new User { Name = name, Email = email });

                        Console.WriteLine("User added successfully!");
                    }

                    if (users.Count > 0)
                    {
                        string jsonData = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                        string filePath = "UserInfo.json";
                        File.WriteAllText(filePath, jsonData);

                        Console.WriteLine($"\nData saved to {filePath} successfully!");
                        Console.WriteLine("JSON Output:");
                        Console.WriteLine(jsonData);
                    }
                    else
                    {
                        Console.WriteLine("\nNo data was entered.");
                    }
                }

                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: Invalid input format. {ex.Message}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error saving data: {ex.Message}");
                }
           }
            static void SaveToSchedulePDF()
            {
                string pdfPath = "Schedule.pdf";
                using (PdfWriter writer = new PdfWriter(pdfPath))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);
                        document.Add(new Paragraph("Schedule"));

                        foreach (var user in users)
                        {
                            document.Add(new Paragraph($"Name: {user.Name}, Email: {user.Email}, Password: {user.Password}"));
                        }

                        document.Close();
                    }
                }
                Console.WriteLine($"Data saved to {pdfPath} successfully!");
            }
        }
    }
}
       
  
