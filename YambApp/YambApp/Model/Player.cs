using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Model
{
    public class Player
    {
        public string Name { get; set; }    
        public int Score { get; set; }

        public Player() { }

        public Player(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}
