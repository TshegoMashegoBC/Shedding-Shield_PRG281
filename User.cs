using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shedding_Shield
{
    internal class User_Class
    {
        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public DateTime CreatedAt { get; set; }
            public User(string username, string password, string email)
            {
                Username = username;
                Password = password;
                Email = email;
                CreatedAt = DateTime.Now;
            }
            public void UpdatePassword(string newPassword)
            {
                Password = newPassword;
            }
            public void UpdateEmail(string newEmail)
            {
                Email = newEmail;
            }
        }
    }
}
