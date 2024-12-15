using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YambApp.Model;
using YambApp.Repository;

namespace YambApp.Service
{
    public class UserService:IUserService
    {
        IUserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public void Create(User user)
        {
            user.Password=BCrypt.Net.BCrypt.HashPassword(user.Password);
            userRepository.Create(user);
        }

        public bool UsernameExist(string username)
        {
            return userRepository.UsernameExist(username);
        }

        public bool EmailExist(string email)
        {
            return userRepository.EmailExist(email);
        }

        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,63}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
