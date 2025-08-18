using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shedding_Shield
{

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        private string Password { get; set; }
        public bool IsLoggedIn { get; private set; }

        public User(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Name, email, and password cannot be empty.");
            if (!email.Contains("@")) throw new ArgumentException("Invalid email format.");

            Name = name;
            Email = email;
            Password = password;
            IsLoggedIn = false;
        }

        public bool Login(string email, string password)
        {
            if (Email == email && Password == password)
            {
                IsLoggedIn = true;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            IsLoggedIn = false;
        }


    }
}