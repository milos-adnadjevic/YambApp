using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;
using YambApp.Repository;

namespace YambApp.Service
{
    public class ScoreService:IScoreService
    {
        IScoreRepository scoreRepository;
        public ScoreService() 
        {
            scoreRepository = new ScoreRepository();
        }

        public List<ScoreTable> GetAllByType(int gameType)
        {
           return scoreRepository.GetAllByType(gameType);
        }

        public void Create(ScoreTable score) 
        {
            scoreRepository.Create(score);
        }

        public int GetAverageScoreByUser(string username, int gameType)
        {
            return scoreRepository.GetAverageScoreByUser(username, gameType);
        }
        public int GetAverageScore(int gameType)
        {
            return scoreRepository.GetAverageScore(gameType);
        }
        public int GetBestScore(int gameType)
        {
            return scoreRepository.GetBestScore(gameType);
        }
        public List<ScoreTable> GetAllByUser(string username, int gameType)
        {
            return scoreRepository.GetAllByUser(username,gameType);
        }

        public int GetBestScoreByUser(string username, int gameType)
        {
            return scoreRepository.GetBestScoreByUser(username, gameType);
        }


    }
}
