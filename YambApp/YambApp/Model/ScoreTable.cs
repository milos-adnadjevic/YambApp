using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YambApp.Model
{
    public class ScoreTable
    {
        public int ScoreId { get; set; }
        public string Username { get; set; }
        public int Scores { get; set; }

        public int GameType { get; set; }

        public ScoreTable() { }

        public ScoreTable( string username, int scores, int gameType)
        {
            Username = username;
            Scores = scores;
            GameType = gameType;    
        }
    }
}
