using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.Repository
{
    public interface IUserRepository
    {
        List<User> GetAll();
        void Create(User user);
        bool EmailExist(string email);
        bool UsernameExist(string username);
        bool Authenticate(string usernameEmail, string password);
    }
}
