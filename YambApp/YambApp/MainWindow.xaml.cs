using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using YambApp.Controller;
using YambApp.Model;

namespace YambApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<YambCategory> yambCategories { get; set; }

        public static YambController yambController=new YambController();
       
        bool dice1Holded= false;

        bool dice2Holded = false;

        bool dice3Holded = false;

        bool dice4Holded = false;

        bool dice5Holded = false;
        
        private int dicesSum= 0;

        private int turnNo = 0;

        private bool locked = false;

        private bool previewLook = false; 

        private TextBlock lockedTextBlock;

        private DataGridCell lockedCell;

        private Dictionary<int, List<int>> freeFieldsIndexes = new Dictionary<int, List<int>>();

        Dictionary<Dictionary<int, int>, int> indexesValue = new Dictionary<Dictionary<int, int>, int>();

        private bool gameWithBonus = false;


        static double screenHeight = SystemParameters.PrimaryScreenHeight;
        static double screenWidth = SystemParameters.PrimaryScreenWidth;
        public List<Dices> dices = yambController.RollDices();

        
        public MainWindow(bool bonus)
        {
            InitializeComponent();
            DataContext = this;               
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
           //set dimensions of yamb table
            scoreGrid.Width = screenWidth / 2;
           
            
            InitializeScoreSheet();
            scoreGrid.ItemsSource = yambCategories;

           
            //first(initial) rolling dices
            Dice1.Text = dices[0].Value.ToString();
            Dice2.Text = dices[1].Value.ToString();
            Dice3.Text = dices[2].Value.ToString();
            Dice4.Text = dices[3].Value.ToString();
            Dice5.Text = dices[4].Value.ToString();
            dicesSum = SumDices();
           
            //set number of rolling (max is 3)
            turnNo = 1;

            //game type
            gameWithBonus = bonus;


        }


        //make row for yamb table
        private void InitializeScoreSheet()
        {
            yambCategories = new ObservableCollection<YambCategory>(yambController.GetRows());

        }

        private void Roll_Click(object sender, RoutedEventArgs e)
        {
            if (turnNo == 3)
            {
                dice1Holded = false;
                dice2Holded = false;
                dice3Holded = false;
                dice4Holded = false;
                dice5Holded = false;
                return;
            };
            ++turnNo;
            dices = yambController.RollDices();
            if (dice1Holded == false) Dice1.Text = dices[0].Value.ToString();
            if (dice2Holded == false) Dice2.Text = dices[1].Value.ToString();
            if (dice3Holded == false) Dice3.Text = dices[2].Value.ToString();
            if (dice4Holded == false) Dice4.Text = dices[3].Value.ToString();
            if (dice5Holded == false) Dice5.Text = dices[4].Value.ToString();

            if (turnNo > 1)
            {
                //added because algorithm for best choice set optional choices like real value so this erase all optional choice 
                foreach (var dic in indexesValue)
                {
                    foreach (var indexes in dic.Key)
                    {

                        TextBlock style = scoreGrid.Columns[indexes.Value].GetCellContent(scoreGrid.Items[indexes.Key]) as TextBlock;
                        style.Background = Brushes.Transparent;
                        style.Text = string.Empty;




                    }
                }

                freeFieldsIndexes = AllFreeFields();
            }

          
            dicesSum = SumDices();




        }
         
        // logic when you want to hold some dice on that value or you want to unhold it
        private void HoldDice1_Click(object sender, RoutedEventArgs e)
        {
            
            dice1Holded=!dice1Holded;
            if (dice1Holded == true) { Dice1.Background = Brushes.SkyBlue; }
            else Dice1.Background = Brushes.Transparent;
        }

        private void HoldDice2_Click(object sender, RoutedEventArgs e)
        {
            dice2Holded = !dice2Holded;
            if (dice2Holded == true) { Dice2.Background = Brushes.SkyBlue; }
            else Dice2.Background = Brushes.Transparent;
        }

        private void HoldDice3_Click(object sender, RoutedEventArgs e)
        {
            dice3Holded= !dice3Holded;
            if (dice3Holded == true) { Dice3.Background = Brushes.SkyBlue; }
            else Dice3.Background = Brushes.Transparent;
        }

        private void HoldDice4_Click(object sender, RoutedEventArgs e)
        {
            dice4Holded= !dice4Holded;
            if (dice4Holded == true) { Dice4.Background = Brushes.SkyBlue; }
            else Dice4.Background = Brushes.Transparent;
        }

        private void HoldDice5_Click(object sender, RoutedEventArgs e)
        {
            dice5Holded = !dice5Holded;
            if (dice5Holded == true) { Dice5.Background = Brushes.SkyBlue; }
            else Dice5.Background = Brushes.Transparent;
        }


        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            int rowIndex = -1;
            int columnIndex= -1;

            //refresh preview look
            if (previewLook == true && sender is DataGridCell cell2 && e.Source is TextBlock textBlock2)
            {
                DataGridRow r = DataGridRow.GetRowContainingElement(cell2);
                int rowIndex1 = r.GetIndex();
                int columnIndex1 = cell2.Column.DisplayIndex;
                Dictionary<int,int> key = new Dictionary<int,int>();
                key.Add(rowIndex1, columnIndex1);
                indexesValue.Remove(key);
                foreach (var dic in indexesValue)
                {
                    foreach (var indexes in dic.Key)
                    {

                        TextBlock style = scoreGrid.Columns[indexes.Value].GetCellContent(scoreGrid.Items[indexes.Key]) as TextBlock;
                        style.Background = Brushes.Transparent;
                        style.Text = string.Empty;
                        previewLook = false;



                    }
                }

            }
            //check if some cell locked
            if (locked == true &&  sender is DataGridCell cell1 && e.Source is TextBlock textBlock1 )
            {
                if (!cell1.Column.Header.ToString().Equals("Lock")) return;
                if (cell1 != lockedCell) return; //check if right cell is clicked               
                DataGridRow r = DataGridRow.GetRowContainingElement(cell1);
                int rowIndex1 = r.GetIndex();
                int columnIndex1 = cell1.Column.DisplayIndex;


                if (rowIndex1 == 0) textBlock1.Text = yambController.SumOnes(GetDices()).ToString();  //write sum of dices with value 1 on appropriate row
                if (rowIndex1 == 1) textBlock1.Text = yambController.SumTwos(GetDices()).ToString();  //write sum of dices with value 2 on appropriate row
                if (rowIndex1 == 2) textBlock1.Text = yambController.SumThrees(GetDices()).ToString();//write sum of dices with value 3 on appropriate row
                if (rowIndex1 == 3) textBlock1.Text = yambController.SumFours(GetDices()).ToString(); //write sum of dices with value 4 on appropriate row
                if (rowIndex1 == 4) textBlock1.Text = yambController.SumFives(GetDices()).ToString(); //write sum of dices with value 5 on appropriate row
                if (rowIndex1 == 5) textBlock1.Text = yambController.SumSixes(GetDices()).ToString(); //write sum of dices with value 6 on appropriate row

                if (rowIndex1 == 6 || rowIndex1 == 7) textBlock1.Text = dicesSum.ToString();            //write sum of dices on row max or min

                if (rowIndex1 == 8) textBlock1.Text = yambController.SumStraight(GetDices(),gameWithBonus).ToString(); //write sum of dices (with bonus optional) if you have straight
                if (rowIndex1 == 9) textBlock1.Text = yambController.SumFullHouse(GetDices(), gameWithBonus).ToString();//write sum of dices (with bonus optional) if you have full house
                if (rowIndex1 == 10) textBlock1.Text = yambController.SumPocker(GetDices(), gameWithBonus).ToString();  //write sum of dices (with bonus optional) if you have pocker
                if (rowIndex1 == 11) textBlock1.Text = yambController.SumYamb(GetDices(), gameWithBonus).ToString();    //write sum of dices (with bonus optional) if you have straight

                //unlock field
                textBlock1.Background = Brushes.Transparent;
                locked = false;

                //unhold all dices because turn is over 
                dice1Holded = false;
                dice2Holded = false;
                dice3Holded = false;
                dice4Holded = false;
                dice5Holded = false;

                Dice1.Background = Brushes.Transparent;
                Dice2.Background = Brushes.Transparent;
                Dice3.Background = Brushes.Transparent;
                Dice4.Background = Brushes.Transparent;
                Dice5.Background = Brushes.Transparent;

                //first roll on next turn
                dices = yambController.RollDices();
                Dice1.Text = dices[0].Value.ToString();
                Dice2.Text = dices[1].Value.ToString();
                Dice3.Text = dices[2].Value.ToString();
                Dice4.Text = dices[3].Value.ToString();
                Dice5.Text = dices[4].Value.ToString();
                turnNo = 1;
                dicesSum = SumDices();

                Scores.Text = Score().ToString();

                freeFieldsIndexes = AllFreeFields();
                textBlock1.Background= Brushes.Transparent;

                return;



            }
            if (sender is DataGridCell cell)
            {
                // Get the row and column index              
                DataGridRow row = FindVisualParent<DataGridRow>(cell);
                 rowIndex = scoreGrid.ItemContainerGenerator.IndexFromContainer(row);
                 columnIndex = cell.Column.DisplayIndex;
                

                //rule for down column
                if (cell.Column.Header.ToString() == "Down")
                {
                    
                    
                    if (rowIndex > 0)
                    {

                        TextBlock x = scoreGrid.Columns[columnIndex].GetCellContent(scoreGrid.Items[rowIndex - 1]) as TextBlock;
                        if (string.IsNullOrEmpty(x.Text))
                        {
                            MessageBox.Show("Cell above is empty");
                            freeFieldsIndexes = AllFreeFields();
                            e.Handled = true;
                            return;
                        }

                    }
                }

                // rule for Up column
                if (cell.Column.Header.ToString() == "Up" )
                {
                   
                    if (rowIndex < scoreGrid.Items.Count-1)
                    {

                        TextBlock x = scoreGrid.Columns[columnIndex].GetCellContent(scoreGrid.Items[rowIndex + 1]) as TextBlock;
                        if (string.IsNullOrEmpty(x.Text))
                        {
                            MessageBox.Show("Cell under is empty");
                            freeFieldsIndexes = AllFreeFields();
                            e.Handled = true;
                            return;
                        }

                    }
                }

                //lock cell you want
                if (cell.Column.Header.ToString() == "Lock" )
                {

                    if (turnNo > 1) { freeFieldsIndexes = AllFreeFields(); return; }                                 
                    lockedTextBlock = scoreGrid.Columns[columnIndex].GetCellContent(scoreGrid.Items[rowIndex]) as TextBlock;
                    if (!string.IsNullOrEmpty(lockedTextBlock.Text)) { freeFieldsIndexes = AllFreeFields(); return; }
                    lockedTextBlock.Background = Brushes.DimGray;
                    lockedCell = cell;
                    locked = true;
                    MessageBox.Show("Cell is locked");
                    freeFieldsIndexes = AllFreeFields();


                    return;
                }
            }
           


            //if field on yamb table properly selected write appropriate value
            if (e.Source is TextBlock textBlock && string.IsNullOrEmpty(textBlock.Text))
            {     
                
               

                if (rowIndex == 0 ) textBlock.Text = yambController.SumOnes(GetDices()).ToString();  //write sum of dices with value 1 on appropriate row
                if (rowIndex == 1 ) textBlock.Text = yambController.SumTwos(GetDices()).ToString();  //write sum of dices with value 2 on appropriate row
                if (rowIndex == 2 ) textBlock.Text = yambController.SumThrees(GetDices()).ToString();//write sum of dices with value 3 on appropriate row
                if (rowIndex == 3 ) textBlock.Text = yambController.SumFours(GetDices()).ToString(); //write sum of dices with value 4 on appropriate row
                if (rowIndex == 4 ) textBlock.Text = yambController.SumFives(GetDices()).ToString(); //write sum of dices with value 5 on appropriate row
                if (rowIndex == 5 ) textBlock.Text = yambController.SumSixes(GetDices()).ToString(); //write sum of dices with value 6 on appropriate row

                if (rowIndex == 6 || rowIndex == 7 ) textBlock.Text= dicesSum.ToString();            //write sum of dices on row max or min

                if (rowIndex == 8) textBlock.Text = yambController.SumStraight(GetDices(), gameWithBonus).ToString(); //write sum of dices (with bonus optional) if you have straight
                if (rowIndex == 9) textBlock.Text = yambController.SumFullHouse(GetDices(), gameWithBonus).ToString();//write sum of dices (with bonus optional) if you have full house
                if (rowIndex == 10) textBlock.Text = yambController.SumPocker(GetDices(), gameWithBonus).ToString();  //write sum of dices (with bonus optional) if you have pocker
                if (rowIndex == 11) textBlock.Text = yambController.SumYamb(GetDices(), gameWithBonus).ToString();    //write sum of dices (with bonus optional) if you have straight

                //unhold all dices because turn is over 
                dice1Holded = false;
                dice2Holded = false;
                dice3Holded = false;
                dice4Holded = false;
                dice5Holded = false;

                Dice1.Background = Brushes.Transparent;
                Dice2.Background = Brushes.Transparent;
                Dice3.Background = Brushes.Transparent;
                Dice4.Background = Brushes.Transparent;
                Dice5.Background = Brushes.Transparent;

                //first roll on next turn
                dices = yambController.RollDices();
                Dice1.Text = dices[0].Value.ToString();
                Dice2.Text = dices[1].Value.ToString();
                Dice3.Text = dices[2].Value.ToString();
                Dice4.Text = dices[3].Value.ToString();
                Dice5.Text = dices[4].Value.ToString();
                turnNo = 1;
                dicesSum = SumDices();

                Scores.Text = Score().ToString();


                freeFieldsIndexes = AllFreeFields();
                textBlock.Background = Brushes.Transparent;

            }
        }

        //get dices combination on table
        private List<int> GetDices()
        {
            List<int> dices = new List<int>(5);
            dices.Add(int.Parse(Dice1.Text));
            dices.Add(int.Parse(Dice2.Text));
            dices.Add(int.Parse(Dice3.Text));
            dices.Add(int.Parse(Dice4.Text));
            dices.Add(int.Parse(Dice5.Text));
            dices.Sort();
            return dices;
        }

        //sum all dices for min and max field
        //maybe I should put that in service
        private int SumDices()
        {
            dicesSum = int.Parse(Dice1.Text) + int.Parse(Dice2.Text) + int.Parse(Dice3.Text) + int.Parse(Dice4.Text) + int.Parse(Dice5.Text);
            return dicesSum;

        }


        private T FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }

        //get score of match at any moment
        private int Score()
        {
            int score = 0;
            for (int i = 0; i < scoreGrid.Items.Count; i++)
            {
                for (int j = 1; j < scoreGrid.Columns.Count; j++)
                {
                    TextBlock block = scoreGrid.Columns[j].GetCellContent(scoreGrid.Items[i]) as TextBlock;
                    //exception because of special way to calculate points from this rows
                    if (i == 6 || i == 7)
                    {
                        if (i == 7) continue;
                        TextBlock max = scoreGrid.Columns[j].GetCellContent(scoreGrid.Items[6]) as TextBlock;
                        TextBlock min = scoreGrid.Columns[j].GetCellContent(scoreGrid.Items[7]) as TextBlock;
                        TextBlock multiplier = scoreGrid.Columns[j].GetCellContent(scoreGrid.Items[0]) as TextBlock;
                        if (!string.IsNullOrEmpty(max.Text) && !string.IsNullOrEmpty(min.Text) && !string.IsNullOrEmpty(multiplier.Text) )
                        {
                            score += (int.Parse(max.Text) - int.Parse(min.Text)) * int.Parse(multiplier.Text);
                        }
                        else score += 0;

                        continue;
                       
                    }
                    //
                    if (!string.IsNullOrEmpty(block.Text))
                    {
                        score += int.Parse(block.Text);
                    }
                    else score += 0;
                  
                }

            }
         
            return score;


        }

        private Dictionary<int, List<int>> AllFreeFields()
        {
            int column = -1;
            int row = -1;
           
           freeFieldsIndexes = new Dictionary<int,List< int>>();
            
            for (int i = 0; i < scoreGrid.Items.Count; i++)
            {
                List<int> columnsInRow = new List<int>();
                freeFieldsIndexes.Add(i, columnsInRow);
                for (int j = 1; j < scoreGrid.Columns.Count; j++)
                {
                    
                    TextBlock freeField = scoreGrid.Columns[j].GetCellContent(scoreGrid.Items[i]) as TextBlock;

                    DataGridCell cell = scoreGrid.Columns[j].GetCellContent(i) as DataGridCell; //find another way to get cell
                    if (j==1 && string.IsNullOrEmpty(freeField.Text) && i>0)
                    {
                        TextBlock aboveField = scoreGrid.Columns[j].GetCellContent(scoreGrid.Items[i-1]) as TextBlock;
                        if (string.IsNullOrEmpty(aboveField.Text)) continue;
                        columnsInRow.Add(j);
                        freeFieldsIndexes[i]= columnsInRow;
                        freeField.Background = Brushes.Aqua;
                        continue;
                    }

                     if (j == 3 && string.IsNullOrEmpty(freeField.Text) && i < scoreGrid.Items.Count - 1)
                     {

                        TextBlock underField = scoreGrid.Columns[j].GetCellContent(scoreGrid.Items[i+1]) as TextBlock;
                        if (string.IsNullOrEmpty(underField.Text)) continue;
                        columnsInRow.Add(j);
                        freeFieldsIndexes[i] = columnsInRow;
                        continue;
                     }

                    if (j == 4 && string.IsNullOrEmpty(freeField.Text) && turnNo>1 ) continue; 


                    if (string.IsNullOrEmpty(freeField.Text))
                    {
                        columnsInRow.Add(j);
                        freeFieldsIndexes[i] = columnsInRow;                     
                    }


                }


            }
          

            StyleOfFreeFields();

            //check indexes in dictionary
            foreach (var j in freeFieldsIndexes)
            {

                Console.Write("Row:" + j.Key + " " + "Column:");

                foreach (int value in j.Value)
                {
                    Console.Write(value + " ");
                }
                Console.WriteLine();

            }
            Console.WriteLine();
            return freeFieldsIndexes;
        }

        private void StyleOfFreeFields()
        {
            //if locked release all free fields because u lock one
            if (locked == true)
            {

                foreach (var dic in freeFieldsIndexes)
                {
                    foreach (int col in dic.Value)
                    {

                        TextBlock style = scoreGrid.Columns[col].GetCellContent(scoreGrid.Items[dic.Key]) as TextBlock;
                        DataGridRow r = DataGridRow.GetRowContainingElement(lockedCell);
                        int lockedrowIndex = r.GetIndex();
                        int lockedColumnIndex = lockedCell.Column.DisplayIndex;
                        if (col != lockedColumnIndex || dic.Key != lockedrowIndex)
                        {
                            style.Background = Brushes.Transparent;
                            
                        }

                    }
                }

                freeFieldsIndexes = new Dictionary<int, List<int>>();
                return;
            }

            //colored free fields and preview value
            indexesValue=yambController.BestChoice(freeFieldsIndexes, GetDices(), gameWithBonus);
            foreach (var dic in indexesValue)
            {
                foreach(var indexes in dic.Key)
                {

                    TextBlock style = scoreGrid.Columns[indexes.Value].GetCellContent(scoreGrid.Items[indexes.Key]) as TextBlock;
                    style.Background = Brushes.Aquamarine;
                    style.Text = dic.Value.ToString();
                    previewLook = true;
                    
                    

                }
            }
            //release free fields in Lock columns after first turn
            if(turnNo>1)
            {


                foreach (var dic1  in freeFieldsIndexes )
                {

                   TextBlock style = scoreGrid.Columns[4].GetCellContent(scoreGrid.Items[dic1.Key]) as TextBlock;
                   style.Background = Brushes.Transparent;
                                                                
                }
                
            }
           
        }

        private void scoreGrid_Loaded(object sender, RoutedEventArgs e)
        {
            freeFieldsIndexes = AllFreeFields();
        }
    }
}




    


