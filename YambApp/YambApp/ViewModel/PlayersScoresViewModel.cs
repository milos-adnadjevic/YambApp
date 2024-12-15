using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;
using YambApp.Service;

namespace YambApp.ViewModel
{
    public class PlayersScoresViewModel : INotifyPropertyChanged
    {
        IScoreService scoreService;
        public PlayersScoresViewModel() 
        {
            scoreService = new ScoreService();

            AllScores = scoreService.GetAllByType(1);


        }

        private List<ScoreTable> allScores;
        public List<ScoreTable> AllScores
        {
            get { return allScores; }
            set
            {
                allScores = value;
                OnPropertyChanged(nameof(AllScores));
            }
        }






        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
