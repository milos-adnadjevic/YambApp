using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Repository;

namespace YambApp.Service
{
    public class LoginService:ILoginService
    {
        IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool Authenticate(string username, string password)
        {
            return _userRepository.Authenticate(username, password);
        }
    }
}
