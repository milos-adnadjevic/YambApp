using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using YambApp.Model;
using YambApp.Service;

namespace YambApp.ViewModel
{
    public class StartGameWindowViewModel : INotifyPropertyChanged
    {
        private bool withBonus = false;

        IScoreService _scoreService;
        INavigationService _navigationService;

        public ICommand StartGameCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        private string username { get; set; }




        public StartGameWindowViewModel() 
        {
            _scoreService = new ScoreService();
        
            username = UserSession.Instance.Username;
            _navigationService = new Service.NavigationService();



            BestScoreGameWithBonus = _scoreService.GetBestScore(1).ToString();
            BestScoreGameWithoutBonus = _scoreService.GetBestScore(2).ToString();
            PersonalBestScoreGameWithoutBonus = _scoreService.GetBestScoreByUser(username, 2).ToString();
            PersonalBestScoreGameWithBonus = _scoreService.GetBestScoreByUser(username, 1).ToString();

            AllScoresWithBonus = _scoreService.GetAllByType(1);
            AllScoresWithoutBonus= _scoreService.GetAllByType(2);

            PersonalScoresWithBonus = _scoreService.GetAllByUser(username, 1);
            PersonalScoresWithoutBonus=_scoreService.GetAllByUser(username, 2);

            PersonalAverageScoreGameWithoutBonus=_scoreService.GetAverageScoreByUser(username, 2).ToString();
            PersonalAverageScoreGameWithBonus= _scoreService.GetAverageScoreByUser(username, 1).ToString();

            AverageScoreGameWithoutBonus = _scoreService.GetAverageScore(2).ToString();
            AverageScoreGameWithBonus= _scoreService.GetAverageScore(1).ToString();

            WelcomeMessage = "Welcome " + username;
            StartGameCommand = new RelayCommand(StartGame);
            LogOutCommand = new RelayCommand(LogOut);

        }

        private void StartGame(object paramtetar)
        {
            Window currentWindow = Application.Current.Windows.OfType<StartGameWindow>().FirstOrDefault();
            MainWindow mainWindow = new MainWindow(isGameWithBonus);
            mainWindow.Show();
            currentWindow.Close();
        }

        private void LogOut(object obj)
        {
             UserSession.Instance.ClearSession();
            _navigationService.NavigateTo("Login", Application.Current.Windows.OfType<StartGameWindow>().FirstOrDefault());
        }


        private bool isGameWithoutBonus;
        public bool IsGameWithoutBonus
        {
            get { return isGameWithoutBonus; }
            set
            {
                isGameWithoutBonus = value;
                OnPropertyChanged(nameof(IsGameWithoutBonus));
            }
        }

        private bool isGameWithBonus = true;
        public bool IsGameWithBonus
        {
            get { return isGameWithBonus; }
            set
            {
                isGameWithBonus = value;
                OnPropertyChanged(nameof(IsGameWithBonus));
            }
        }

        private List<ScoreTable> personalScoresWithBonus;
        public List<ScoreTable> PersonalScoresWithBonus
        {
            get { return personalScoresWithBonus; }
            set
            {
                personalScoresWithBonus = value;
                OnPropertyChanged(nameof(PersonalScoresWithBonus));
            }
        }

        private List<ScoreTable> personalScoresWithoutBonus;
        public List<ScoreTable> PersonalScoresWithoutBonus
        {
            get { return personalScoresWithoutBonus; }
            set
            {
                personalScoresWithoutBonus = value;
                OnPropertyChanged(nameof(PersonalScoresWithoutBonus));
            }
        }


        private List<ScoreTable> allScoresWithBonus;
        public List<ScoreTable> AllScoresWithBonus
        {
            get { return allScoresWithBonus; }
            set
            {
                allScoresWithBonus = value;
                OnPropertyChanged(nameof(AllScoresWithBonus));
            }
        }
        private List<ScoreTable> allScoresWithoutBonus;
        public List<ScoreTable> AllScoresWithoutBonus
        {
            get { return allScoresWithoutBonus; }
            set
            {
                allScoresWithoutBonus = value;
                OnPropertyChanged(nameof(AllScoresWithoutBonus));
            }
        }

        private string welcomeMessage;
        public string WelcomeMessage
        {
            get { return welcomeMessage; }
             set
             {
               welcomeMessage = value;
               OnPropertyChanged(nameof(WelcomeMessage));
             }
        }

        private string bestScoreGameWithBonus;
        public string BestScoreGameWithBonus
        {
            get { return bestScoreGameWithBonus; }
            set
            {
                bestScoreGameWithBonus = value;
                OnPropertyChanged(nameof(BestScoreGameWithBonus));
            }
        }

        private string bestScoreGameWithoutBonus;
        public string BestScoreGameWithoutBonus
        {
            get { return bestScoreGameWithoutBonus; }
            set
            {
                bestScoreGameWithoutBonus = value;
                OnPropertyChanged(nameof(BestScoreGameWithoutBonus));
            }
        }
        private string personalBestScoreGameWithBonus;
        public string PersonalBestScoreGameWithBonus
        {
            get { return personalBestScoreGameWithBonus; }
            set
            {
                personalBestScoreGameWithBonus = value;
                OnPropertyChanged(nameof(PersonalBestScoreGameWithBonus));
            }
        }

        private string personalBestScoreGameWithoutBonus;
        public string PersonalBestScoreGameWithoutBonus
        {
            get { return personalBestScoreGameWithoutBonus; }
            set
            {
                personalBestScoreGameWithoutBonus = value;
                OnPropertyChanged(nameof(PersonalBestScoreGameWithoutBonus));
            }
        }
        private string averageScoreGameWithBonus;
        public string AverageScoreGameWithBonus
        {
            get { return averageScoreGameWithBonus; }
            set
            {
                averageScoreGameWithBonus = value;
                OnPropertyChanged(nameof(AverageScoreGameWithBonus));
            }
        }

        private string averageScoreGameWithoutBonus;
        public string AverageScoreGameWithoutBonus
        {
            get { return averageScoreGameWithoutBonus; }
            set
            {
                averageScoreGameWithoutBonus = value;
                OnPropertyChanged(nameof(AverageScoreGameWithoutBonus));
            }
        }

        private string personalAverageScoreGameWithBonus;
        public string PersonalAverageScoreGameWithBonus
        {
            get { return personalAverageScoreGameWithBonus; }
            set
            {
                personalAverageScoreGameWithBonus = value;
                OnPropertyChanged(nameof(PersonalAverageScoreGameWithBonus));
            }
        }

        private string personalAverageScoreGameWithoutBonus;
        public string PersonalAverageScoreGameWithoutBonus
        {
            get { return personalAverageScoreGameWithoutBonus; }
            set
            {
                personalAverageScoreGameWithoutBonus = value;
                OnPropertyChanged(nameof(PersonalAverageScoreGameWithoutBonus));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
