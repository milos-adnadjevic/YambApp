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
        IPlayerService playerService;
        public PlayersScoresViewModel() 
        {
            playerService = new PlayerService();

            AllPlayers = playerService.GetAll();


        }

        private List<Player> allPlayers;
        public List<Player> AllPlayers
        {
            get { return allPlayers; }
            set
            {
                allPlayers = value;
                OnPropertyChanged(nameof(AllPlayers));
            }
        }






        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
