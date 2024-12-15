using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.Service
{
    public interface IScoreService
    {
        List<ScoreTable> GetAllByType(int gameType);
        void Create(ScoreTable score);
        int GetAverageScoreByUser(string username, int gameType);
        int GetAverageScore(int gameType);
        int GetBestScore(int gameType);
        List<ScoreTable> GetAllByUser(string username, int gameType);
        int GetBestScoreByUser(string username, int gameType);
    }
}
