
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using YambApp.Model;
using YambApp.Repository;
using YambApp.Service;
using NAudio.Wave;


namespace YambApp.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        

        private static IBestChoiceAlgorithams bestChoiceAlgorithams;
        private static IYambTableGenerator yambTableGenerator;
        private static IRandomDiceGenerator randomDiceGenerator;
        private static IScore score;
        private static ISumRules sumRules;
        private static IStyleOfBestChoiceAlgorithams styleOfBestChoiceAlgorithams;
        private static IScoreService scoreService;
        private static IEndOfGameCheck endOfGameCheck;
        private static INavigationService navigationService;

        IDataGridWrapper dataGridWrapper;
        public ObservableCollection<YambCategory> YambCategories { get; set; }
        public int Scores { get; set; }
        public List<Dices> Dices { get; set; }
        public ICommand RollCommand { get; set; }
        public ICommand HoldDiceCommand { get; set; } 
        public ICommand CellClickCommand { get; private set; }

        public ICommand GridLoadedCommand { get; set; }
        public ICommand NavigateToHomeCommand { get; set; }
        public ICommand LogOutCommand {  get; set; }


        private bool dice1Holded = false;
        private bool dice2Holded = false;
        private bool dice3Holded = false;
        private bool dice4Holded = false;
        private bool dice5Holded = false;
       
        private int turnNo;
        private bool locked = false;
        private bool previewLook = false;
        private TextBlock lockedTextBlock;
        private DataGridCell lockedCell;
     
       
        private bool gameWithBonus = false;
        private bool validInput = true;
        private bool onlyLockColum = false;

        private string username;

        public DataGrid ScoreGrid { get; set; }

        Dictionary<int, List<int>> freeFieldsIndexes =new Dictionary<int, List<int>>();
        Dictionary<Dictionary<int, int>, int> indexesValue = new Dictionary<Dictionary<int, int>, int>();




        static double screenWidth = SystemParameters.PrimaryScreenWidth;

        private Func<List<int>, int>[] actions = new Func<List<int>, int>[8];
        private Func<List<int>, bool, int>[] actionsWithBoolean = new Func<List<int>, bool, int>[12];

        private Action[] imageSource = new Action[7];

        private string imageSourceString;

        private readonly Random _random;
        private readonly DispatcherTimer _rollTimer;
        private int _finalDiceValue;
        private int _currentRollingValue;


        public MainWindowViewModel(bool bonus,DataGrid scoreGrid)
        {
            username=UserSession.Instance.Username;
            
            randomDiceGenerator = new RandomDiceGenerator();           
            randomDiceGenerator= new RandomDiceGenerator();
            dataGridWrapper = new DataGridWrapper();
            sumRules = new SumRules();
            bestChoiceAlgorithams = new BestChoiceAlgorithams(dataGridWrapper, sumRules);
            yambTableGenerator = new YambTableGenerator();
            score = new Score(dataGridWrapper);
           
            styleOfBestChoiceAlgorithams = new StyleOfBestChoiceAlgorithams();
            scoreService= new ScoreService();
            endOfGameCheck= new EndOfGameCheck();
            navigationService = new NavigationService();

            YambCategories = new ObservableCollection<YambCategory>(yambTableGenerator.GetRows());

            

            Scores = 0;

           


            ScoreGrid = scoreGrid;
            ScoreGrid.ItemsSource = YambCategories;
            ScoreGrid.Width = screenWidth / 2;

            RollCommand = new RelayCommand(Roll);
            HoldDiceCommand = new RelayCommand(HoldDice);
            CellClickCommand = new RelayCommandMouseArg<EventTuple>(ClickCell);
            GridLoadedCommand = new RelayCommand(GridLoaded);
            NavigateToHomeCommand = new RelayCommand(NavigateToHome);
            LogOutCommand = new RelayCommand(LogOut);


            gameWithBonus = bonus;

            Func<List<int>, int> sumOnes = sumRules.SumOnes;
            Func<List<int>, int> sumTwos = sumRules.SumTwos;
            Func<List<int>, int> sumThrees = sumRules.SumThrees;
            Func<List<int>, int> sumFours = sumRules.SumFours;
            Func<List<int>, int> sumFives = sumRules.SumFives;
            Func<List<int>, int> sumSixes = sumRules.SumSixes;
            Func<List<int>, int> sumMax = sumRules.SumDices;
            Func<List<int>, int> sumMin = sumRules.SumDices;

            Func<List<int>, bool, int> sumStraight = sumRules.SumStraight;
            Func<List<int>, bool, int> sumFullHouse = sumRules.SumFullHouse;
            Func<List<int>, bool, int> sumPocker = sumRules.SumPocker;
            Func<List<int>, bool, int> sumYamb = sumRules.SumYamb;


            actions[0] = sumOnes;  //write sum of dices with value 1 on appropriate row
            actions[1] = sumTwos;  //write sum of dices with value 2 on appropriate row
            actions[2] = sumThrees;  //write sum of dices with value 3 on appropriate row
            actions[3] = sumFours; //write sum of dices with value 4 on appropriate row
            actions[4] = sumFives; //write sum of dices with value 5 on appropriate row
            actions[5] = sumSixes; //write sum of dices with value 6 on appropriate row
            actions[6] = sumMax; //write sum of dices on row max 
            actions[7] = sumMin;//write sum of dices on row min 
            actionsWithBoolean[8] = sumStraight;  //write sum of dices (with bonus optional) if you have straight
            actionsWithBoolean[9] = sumFullHouse; //write sum of dices (with bonus optional) if you have full house
            actionsWithBoolean[10] = sumPocker; //write sum of dices (with bonus optional) if you have pocker
            actionsWithBoolean[11] = sumYamb; //write sum of dices (with bonus optional) if you have straight


            imageSource[1] = () => imageSourceString = "/YambApp;component/Image/diceOne.png";
            imageSource[2] = () => imageSourceString = "/YambApp;component/Image/diceTwo.png";
            imageSource[3] = () => imageSourceString = "/YambApp;component/Image/diceThree.png";
            imageSource[4] = () => imageSourceString = "/YambApp;component/Image/diceFour.png";
            imageSource[5] = () => imageSourceString = "/YambApp;component/Image/diceFive.png";
            imageSource[6] = () => imageSourceString = "/YambApp;component/Image/diceSix.png";

            turnNo = 1;
            CurrentRoll = turnNo;

            Dices = randomDiceGenerator.RollDices();
            Dice1Value = Dices[0].Value.ToString();
            imageSource[Dices[0].Value].Invoke();
            FirstDiceImage = imageSourceString;
            Dice2Value = Dices[1].Value.ToString();
            imageSource[Dices[1].Value].Invoke();
            SecondDiceImage = imageSourceString;
            Dice3Value = Dices[2].Value.ToString();
            imageSource[Dices[2].Value].Invoke();
            ThirdDiceImage = imageSourceString;
            Dice4Value = Dices[3].Value.ToString();
            imageSource[Dices[3].Value].Invoke();
            FourthDiceImage = imageSourceString;
            Dice5Value = Dices[4].Value.ToString();
            imageSource[Dices[4].Value].Invoke();
            FifthDiceImage = imageSourceString;
            Dice1Background =  Brushes.Black;
            Dice2Background =  Brushes.Black;
            Dice3Background =  Brushes.Black;
            Dice4Background =  Brushes.Black;
            Dice5Background =  Brushes.Black;

            _random = new Random();


            using (var audioFile = new AudioFileReader("Music/diceRolling.mp3"))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Wait until music playback finishes
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Task.Delay(100).Wait(); // Prevent tight loop
                }
            }




        }


       



        private string dice1Value;
        public string Dice1Value
        {
            get { return dice1Value; }
            set
            {
                dice1Value = value;
                OnPropertyChanged(nameof(Dice1Value));
            }
        }
        private string dice2Value;
        public string Dice2Value
        {
            get { return dice2Value; }
            set
            {
                dice2Value = value;
                OnPropertyChanged(nameof(Dice2Value));
            }
        }
        private string dice3Value;
        public string Dice3Value
        {
            get { return dice3Value; }
            set
            {
                dice3Value = value;
                OnPropertyChanged(nameof(Dice3Value));
            }
        }
        private string dice4Value;
        public string Dice4Value
        {
            get { return dice4Value; }
            set
            {
                dice4Value = value;
                OnPropertyChanged(nameof(Dice4Value));
            }
        }
        private string dice5Value;
        public string Dice5Value
        {
            get { return dice5Value; }
            set
            {
                dice5Value = value;
                OnPropertyChanged(nameof(Dice5Value));
            }
        }



        private Brush dice1BackGround;
        public Brush Dice1Background
        {
            get { return dice1BackGround; }
            set
            {
                dice1BackGround = value;
                OnPropertyChanged(nameof(Dice1Background));
            }
        }


        private Brush dice2BackGround;
        public Brush Dice2Background
        {
            get { return dice2BackGround; }
            set
            {
                dice2BackGround = value;
                OnPropertyChanged(nameof(Dice2Background));
            }
        }


        private Brush dice3BackGround;
        public Brush Dice3Background
        {
            get { return dice3BackGround; }
            set
            {
                dice3BackGround = value;
                OnPropertyChanged(nameof(Dice3Background));
            }
        }


        private Brush dice4BackGround;
        public Brush Dice4Background
        {
            get { return dice4BackGround; }
            set
            {
                dice4BackGround = value;
                OnPropertyChanged(nameof(Dice4Background));
            }
        }


        private Brush dice5BackGround;
        public Brush Dice5Background
        {
            get { return dice5BackGround; }
            set
            {
                dice5BackGround = value;
                OnPropertyChanged(nameof(Dice5Background));
            }
        }
        
        private string scoreValue;
        public string ScoreValue
        {
            get { return scoreValue; }
            set
            {
                scoreValue = value;
                OnPropertyChanged(nameof(ScoreValue));
            }
        }


        private string firsDiceImage;
        public string FirstDiceImage
        {
            get { return firsDiceImage; }
            set
            {
                firsDiceImage = value;
                OnPropertyChanged(nameof(FirstDiceImage));
            }
        }

        private string secondDiceImage;
        public string SecondDiceImage
        {
            get { return secondDiceImage; }
            set
            {
                secondDiceImage = value;
                OnPropertyChanged(nameof(SecondDiceImage));
            }
        }

        private string thirdDiceImage;
        public string ThirdDiceImage
        {
            get { return thirdDiceImage; }
            set
            {
                thirdDiceImage = value;
                OnPropertyChanged(nameof(ThirdDiceImage));
            }
        }

        private string fourthiceImage;
        public string FourthDiceImage
        {
            get { return fourthiceImage; }
            set
            {
                fourthiceImage = value;
                OnPropertyChanged(nameof(FourthDiceImage));
            }
        }

        private string fifthDiceImage;
        public string FifthDiceImage
        {
            get { return fifthDiceImage; }
            set
            {
                fifthDiceImage = value;
                OnPropertyChanged(nameof(FifthDiceImage));
            }
        }

        private int _currentRoll=0; // Start from the first roll
        public int CurrentRoll
        {
            get => _currentRoll;
            set
            {
                _currentRoll = value;
                OnPropertyChanged(nameof(CurrentRoll)); // Notify UI of updates
            }
        }
        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
        private void LogOut(object obj)
        {
            UserSession.Instance.ClearSession();
            navigationService.NavigateTo("Login", Application.Current.Windows.OfType<MainWindow>().FirstOrDefault());
        }
        private void NavigateToHome(object obj)
        {
            navigationService.NavigateTo("Home", Application.Current.Windows.OfType<MainWindow>().FirstOrDefault());
            
        }

        private async Task RollingDiceSimulation()
        {
            var musicTask = Task.Run(() =>
            {
                using (var audioFile = new AudioFileReader("Music/diceRolling.mp3"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    // Wait until music playback finishes
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Task.Delay(100).Wait(); // Prevent tight loop
                    }
                }
            });

            for (int i = 0; i <= 7; i++)
            {
                _currentRollingValue = _random.Next(1, 7);

                // Simulate an asynchronous operation, e.g., delay for visualization
                await Task.Delay(20); // Wait for 500 milliseconds

                imageSource[_random.Next(1, 7)].Invoke();
                FirstDiceImage = imageSourceString;
                imageSource[_random.Next(1, 7)].Invoke();
                SecondDiceImage = imageSourceString;
                imageSource[_random.Next(1, 7)].Invoke();
                ThirdDiceImage = imageSourceString;
                imageSource[_random.Next(1, 7)].Invoke();
                FourthDiceImage = imageSourceString;
                imageSource[_random.Next(1, 7)].Invoke();
                FifthDiceImage = imageSourceString;
               

            }

           
        }

        private async void Roll(object obj)
        {
            if (endOfGameCheck.EndOfGame(ScoreGrid, indexesValue)) return;

            if (onlyLockColum && !locked && CurrentRoll==1) { StatusMessage = "You must lock field first!"; return; }
            StatusMessage = null;

            if (CurrentRoll == 3)
            {
                StatusMessage = "You have reached the maximum number of rolls!";
            }
            if (turnNo == 3)
            {
                dice1Holded = false;
                dice2Holded = false;
                dice3Holded = false;
                dice4Holded = false;
                dice5Holded = false;

                return;
            }



            ++turnNo;
            CurrentRoll = turnNo;
            await RollingDiceSimulation();
            Dices = randomDiceGenerator.RollDices();

            if (!dice1Holded)
            {
                Dice1Value = Dices[0].Value.ToString();
                imageSource[Dices[0].Value].Invoke();
                FirstDiceImage = imageSourceString; 

            }
            if (!dice2Holded)
            {
                Dice2Value = Dices[1].Value.ToString();
                imageSource[Dices[1].Value].Invoke();
                SecondDiceImage = imageSourceString;
            }
            if (!dice3Holded)
            {
                Dice3Value = Dices[2].Value.ToString();
                imageSource[Dices[2].Value].Invoke();
                ThirdDiceImage = imageSourceString;
            }
            if (!dice4Holded)
            {
                Dice4Value = Dices[3].Value.ToString();
                imageSource[Dices[3].Value].Invoke();
                FourthDiceImage = imageSourceString;
            }
            if (!dice5Holded)
            {
                Dice5Value = Dices[4].Value.ToString();
                imageSource[Dices[4].Value].Invoke();
                FifthDiceImage = imageSourceString;
            }

            //  this was making a problem when we roll the dice after locking cell
            if (turnNo > 1 && locked == false)
            {

                foreach (var dic in indexesValue)
                {
                    foreach (var indexes in dic.Key)
                    {

                        TextBlock style = ScoreGrid.Columns[indexes.Value].GetCellContent(ScoreGrid.Items[indexes.Key]) as TextBlock;
                        style.Background = Brushes.Transparent;
                        style.Text = string.Empty;
                    }
                }


            }

            freeFieldsIndexes=bestChoiceAlgorithams.AllFreeFields(ScoreGrid, turnNo);
            onlyLockColum = true;
            foreach (var dic in freeFieldsIndexes)
            {
                if(dic.Value.Any()) onlyLockColum=false;
            }

            styleOfBestChoiceAlgorithams.StyleOfFreeFields(ScoreGrid, turnNo, locked, lockedCell, GetDices(), gameWithBonus);


        
        }
        
        

        private void HoldDice(object obj)
        {
            int diceNumber = int.Parse(obj.ToString());

            
            switch (diceNumber)
            {
                case 1:
                    dice1Holded = !dice1Holded;
                    Dice1Background = dice1Holded ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3B5998")) : Brushes.Black;                    
                    break;
                case 2:
                    dice2Holded = !dice2Holded;
                    Dice2Background = dice2Holded ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3B5998")) : Brushes.Black;
                    break;
                case 3:
                    dice3Holded = !dice3Holded;
                    Dice3Background = dice3Holded ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3B5998")) : Brushes.Black;
                    break;
                case 4:
                    dice4Holded = !dice4Holded;
                    Dice4Background = dice4Holded ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3B5998")) : Brushes.Black;
                    break;
                case 5:
                    dice5Holded = !dice5Holded;
                    Dice5Background = dice5Holded ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3B5998")) : Brushes.Black;
                    break;
            }
        }

        



        private void ClickCell(EventTuple T)
        {
            validInput = true; //initial value for validation
            int rowIndex = -1;
            int columnIndex = -1;

            if (T.Sender is DataGridCell cell && T.MouseEventArgs.Source is TextBlock textBlock)
            {


                    //get index of field
                    DataGridRow r = DataGridRow.GetRowContainingElement(cell);
                    rowIndex = r.GetIndex();
                    columnIndex = cell.Column.DisplayIndex;
                    Dictionary <int,int> key = new Dictionary<int,int>();

                   if (textBlock.Background != Brushes.Aquamarine && textBlock.Background != Brushes.DimGray) return;
               
                    indexesValue.Remove(key);
                    foreach (var dic in indexesValue)
                    {
                        foreach (var indexes in dic.Key)
                        {
                            //remove all proposal for input 
                            TextBlock style = ScoreGrid.Columns[indexes.Value].GetCellContent(ScoreGrid.Items[indexes.Key]) as TextBlock;
                            style.Background = Brushes.Transparent;
                            style.Text = string.Empty;
                       
                        }
                    }


                    //if field is locked
                    if (locked)
                    {
                        if (!cell.Column.Header.ToString().Equals("Lock")) return; //check if right column is clicked 
                        if (cell != lockedCell) return; //check if right cell is clicked               
                                      
                       textBlock.Text = (rowIndex >= 0 && rowIndex <= 7)? actions[rowIndex].Invoke(GetDices()).ToString() : actionsWithBoolean[rowIndex].Invoke(GetDices(), gameWithBonus).ToString();

                        //unlock field
                        textBlock.Background = Brushes.Transparent;
                        locked = false;

                        BackDicesToDefault();

                        FirstRoleOfTurn();

                        ScoreValue = score.ScoreValue(ScoreGrid).ToString();

                        indexesValue= styleOfBestChoiceAlgorithams.StyleOfFreeFields(ScoreGrid, turnNo, locked, lockedCell, GetDices(), gameWithBonus);

                        EndOfGame();


                        return;

                    }
                            

                    //rule for down column
                    if (cell.Column.Header.ToString() == "Down")
                    {
                        RuleForDownColumn(rowIndex, columnIndex);                                               
                    }

                    // rule for Up column
                    if (cell.Column.Header.ToString() == "Up")
                    {
                        RuleForUpColumn(rowIndex, columnIndex);                     
                    }

                    //lock cell you want
                    if (cell.Column.Header.ToString() == "Lock")
                    {
                        RuleForLockColumn(rowIndex, columnIndex, cell);
                        return;                        
                    }

                     
                    //if field on yamb table properly selected write appropriate value
                    if (string.IsNullOrEmpty(textBlock.Text) && validInput)
                    {

                        textBlock.Text = (rowIndex >= 0 && rowIndex <= 7)? actions[rowIndex].Invoke(GetDices()).ToString() : actionsWithBoolean[rowIndex].Invoke(GetDices(), gameWithBonus).ToString();

                        BackDicesToDefault();

                        FirstRoleOfTurn();

                        ScoreValue = score.ScoreValue(ScoreGrid).ToString();


                        indexesValue= styleOfBestChoiceAlgorithams.StyleOfFreeFields(ScoreGrid, turnNo, locked, lockedCell, GetDices(), gameWithBonus);

                        EndOfGame();

                        textBlock.Background = Brushes.Transparent;

                    }               
            }
        }

        //get value of all dices in list and sort it 
        private List<int> GetDices()
        {
            List<int> dices = new List<int>(5);
            dices.Add(int.Parse(Dice1Value));
            dices.Add(int.Parse(Dice2Value));
            dices.Add(int.Parse(Dice3Value));
            dices.Add(int.Parse(Dice4Value));
            dices.Add(int.Parse(Dice5Value));

            dices.Sort();



            return dices;
        }

        private void BackDicesToDefault()
        {
            //unhold all dices because turn is over 
            dice1Holded = false;
            dice2Holded = false;
            dice3Holded = false;
            dice4Holded = false;
            dice5Holded = false;

            Dice1Background = Brushes.Black;
            Dice2Background = Brushes.Black;
            Dice3Background = Brushes.Black;
            Dice4Background = Brushes.Black;
            Dice5Background = Brushes.Black;

           
           
        }

        private async void FirstRoleOfTurn()
        {
            //first roll on next turn
            await RollingDiceSimulation();
            Dices = randomDiceGenerator.RollDices();
            Dice1Value = Dices[0].Value.ToString();
            Dice2Value = Dices[1].Value.ToString();
            Dice3Value = Dices[2].Value.ToString();
            Dice4Value = Dices[3].Value.ToString();
            Dice5Value = Dices[4].Value.ToString();
            imageSource[Dices[0].Value].Invoke();
            FirstDiceImage = imageSourceString;
            imageSource[Dices[1].Value].Invoke();
            SecondDiceImage = imageSourceString;
            imageSource[Dices[2].Value].Invoke();
            ThirdDiceImage = imageSourceString;
            imageSource[Dices[3].Value].Invoke();
            FourthDiceImage = imageSourceString;
            imageSource[Dices[4].Value].Invoke();
            FifthDiceImage = imageSourceString;
            turnNo = 1;
            CurrentRoll = turnNo;
            StatusMessage = null;
            freeFieldsIndexes = bestChoiceAlgorithams.AllFreeFields(ScoreGrid, turnNo);
            onlyLockColum = true;
            bool breakOuterLoop = false;
            foreach (var dic in freeFieldsIndexes)
            {
                if (dic.Value.Any())
                {
                    foreach (var values in dic.Value)
                    {
                        if (values != 4)
                        {
                            onlyLockColum = false;
                            breakOuterLoop = true;
                            break; // Exit the inner loop
                        }
                    }
                    if (breakOuterLoop)
                        break; // Exit the outer loop
                }

            }
        }


       


        private void RuleForDownColumn(int rowIndex,int columnIndex) 
        {
            if (rowIndex > 0)
            {
                
                TextBlock x = ScoreGrid.Columns[columnIndex].GetCellContent(ScoreGrid.Items[rowIndex - 1]) as TextBlock;
                if (string.IsNullOrEmpty(x.Text))
                {
                    MessageBox.Show("Cell above is empty");
                    indexesValue = styleOfBestChoiceAlgorithams.StyleOfFreeFields(ScoreGrid, turnNo, locked, lockedCell, GetDices(), gameWithBonus);
                    validInput = false;
                }

            }
        }
        private void RuleForUpColumn(int rowIndex, int columnIndex) 
        {
            if (rowIndex < ScoreGrid.Items.Count - 1)
            {
                
                TextBlock x = ScoreGrid.Columns[columnIndex].GetCellContent(ScoreGrid.Items[rowIndex + 1]) as TextBlock;
                if (string.IsNullOrEmpty(x.Text))
                {
                    MessageBox.Show("Cell under is empty");
                    indexesValue = styleOfBestChoiceAlgorithams.StyleOfFreeFields(ScoreGrid, turnNo, locked, lockedCell, GetDices(), gameWithBonus);
                    validInput = false;
                }

            }
        }
        private void RuleForLockColumn(int rowIndex, int columnIndex, DataGridCell cell)
        {
            if (turnNo > 1) { indexesValue = styleOfBestChoiceAlgorithams.StyleOfFreeFields(ScoreGrid, turnNo, locked, lockedCell, GetDices(), gameWithBonus); return; }
            lockedTextBlock = ScoreGrid.Columns[columnIndex].GetCellContent(ScoreGrid.Items[rowIndex]) as TextBlock;
            if (!string.IsNullOrEmpty(lockedTextBlock.Text)) { indexesValue = styleOfBestChoiceAlgorithams.StyleOfFreeFields(ScoreGrid, turnNo, locked, lockedCell, GetDices(), gameWithBonus); return; }
            lockedTextBlock.Background = Brushes.DimGray;
            lockedCell = cell;
            locked = true;
            CustomMessageBox customMessageBox = new CustomMessageBox("Cell is locked");
            customMessageBox.ShowDialog();
           
            return;
        }

        private void EndOfGame()
        {
            if (endOfGameCheck.EndOfGame(ScoreGrid,indexesValue))
            {
                Random random = new Random();
                int randomNO = random.Next(1,1000000);
                int scoreValueInt = score.ScoreValue(ScoreGrid);
                int gameType = gameWithBonus ? 1 : 0; 
                ScoreTable scores = new ScoreTable (username, scoreValueInt, gameType);
                scoreService.Create(scores);

                PlayersScoresWindow allPLayers = new PlayersScoresWindow();
                
                allPLayers.Show();
            }
        }

        private void GridLoaded(object obj)
        {
           indexesValue = styleOfBestChoiceAlgorithams.StyleOfFreeFields(ScoreGrid,turnNo,locked,lockedCell,GetDices(),gameWithBonus);        

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
