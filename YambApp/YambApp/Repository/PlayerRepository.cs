using NUnit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.FileHandler;
using YambApp.Model;

namespace YambApp.Repository
{
    public class PlayerRepository:IPlayerRepository
    {
        IPlayerFile playerFile;

       public  PlayerRepository()
        {
            playerFile = new PlayerFile();
        }


        public List<Player> GetAll()
        {
            return playerFile.Read();
        }

        public void Create(Player player)
        {
            List<Player> players = GetAll();
            if (players == null)
            {
                players = new List<Player>();
            }
            players.Add(player);
            playerFile.Save(players);
        }





    }
}
