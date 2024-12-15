using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.Service
{
    public interface IUserService
    {
        List<User> GetAll();
        void Create(User user);
        bool UsernameExist(string username);
        bool EmailExist(string email);
        bool IsValidEmail(string email);
    }
}
