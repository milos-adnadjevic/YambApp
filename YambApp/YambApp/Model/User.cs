using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Model
{
    public class User
    {
        public int    UserId   { get; set; }
        public string Username { get; set; }    
        public string Email    { get; set; }
        public string Password { get; set; }



        public User() { }

        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
