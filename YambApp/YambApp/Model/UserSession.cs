using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Model
{
    public class UserSession
    {
        private static UserSession _instance;
        private static readonly object _lock = new object();
        public static UserSession Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserSession();
                    }
                    return _instance;
                }
            }
        }

        public string Username { get; set; }
       
        public bool IsLoggedIn => !string.IsNullOrEmpty(Username);

        private UserSession()
        {
            
        }

        public void ClearSession()
        {
            Username = null;
            
        }
    }

}
