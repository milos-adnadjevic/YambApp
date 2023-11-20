using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;
using YambApp.Repository;

namespace YambApp.Service
{
    public class PlayerService:IPlayerService
    {
        IPlayerRepository playerRepository;
        public PlayerService() 
        {
            playerRepository= new PlayerRepository();
        }

        public List<Player> GetAll()
        {
           return playerRepository.GetAll();
        }

        public void Create(Player player) 
        {
            playerRepository.Create(player);
        }
    }
}
