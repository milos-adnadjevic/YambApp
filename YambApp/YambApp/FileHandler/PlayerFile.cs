using NUnit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.FileHandler
{
    public class PlayerFile:IPlayerFile
    {

        private string path = @"..\..\Data\PlayersScores.txt";
        public List<Player> Read()
        {
            if (!System.IO.File.Exists(path))
            {
                return new List<Player>();
            }

            string playerSerialized = System.IO.File.ReadAllText(path);
            List<Player> players = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Player>>(playerSerialized);
            return players;
        }

        public void Save(List<Player> players)
        {
            string playerSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(players);
            System.IO.File.WriteAllText(path, playerSerialized);
        }
    }
}
