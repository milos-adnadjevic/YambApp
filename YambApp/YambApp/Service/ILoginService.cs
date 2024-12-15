using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Service
{
    public interface ILoginService
    {
        bool Authenticate(string username, string password);
    }
}
