using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.Service
{
    public interface IPlayerService
    {
        List<Player> GetAll();
        void Create(Player player);
    }
}
