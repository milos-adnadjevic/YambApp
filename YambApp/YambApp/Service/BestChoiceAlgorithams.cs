using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using YambApp.Model;

namespace YambApp.Service
{
    public class BestChoiceAlgorithams:IBestChoiceAlgorithams
    {
       
        ISumRules sumRules;

        IDataGridWrapper dataGridWrapper;
        int value = 0;
        private Func<List<int>,int>[] actions = new Func<List<int>, int>[8];
        private Func<List<int>,bool, int>[] actionsWithBoolean = new Func<List<int>,bool, int>[12];
        public BestChoiceAlgorithams(IDataGridWrapper dataGridWrapper,ISumRules sumRules) 
        {
            this.sumRules = sumRules;

            Func<List<int>, int> sumOnes = sumRules.SumOnes;
            Func<List<int>, int> sumTwos = sumRules.SumTwos;
            Func<List<int>, int> sumThrees = sumRules.SumThrees;
            Func<List<int>, int> sumFours = sumRules.SumFours;
            Func<List<int>, int> sumFives = sumRules.SumFives;
            Func<List<int>, int> sumSixes = sumRules.SumSixes;
            Func<List<int>, int> sumMax = sumRules.SumDices;
            Func<List<int>, int> sumMin = sumRules.SumDices;

            Func<List<int>,bool, int> sumStraight = sumRules.SumStraight;
            Func<List<int>,bool, int> sumFullHouse = sumRules.SumFullHouse;
            Func<List<int>,bool ,int> sumPocker = sumRules.SumPocker;
            Func<List<int>,bool, int> sumYamb = sumRules.SumYamb;


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

            this.dataGridWrapper = dataGridWrapper;


        }




            //Algoritham for finding best chocice to play
            public Dictionary<Dictionary<int, int>, int> BestChoice(Dictionary<int, List<int>> freeFieldsIndexes, List<int> dices, bool gameWithBonus)
            {
                Dictionary<Dictionary<int, int>, int> indexesValue = new Dictionary<Dictionary<int, int>, int>();
                Dictionary<int, int> indexes = new Dictionary<int, int>();
            

                foreach (var row in freeFieldsIndexes)
                {


                    foreach (int col in row.Value)
                    {
                        indexes = new Dictionary<int, int>
                        {
                            { row.Key, col }
                        };
                        value = (row.Key >= 0 && row.Key <= 7)? actions[row.Key].Invoke(dices) : actionsWithBoolean[row.Key].Invoke(dices, gameWithBonus);
                

                        indexesValue.Add(indexes, value);
                    }
                }
                Console.WriteLine();
                foreach (var j in indexesValue)
                {
                    Console.WriteLine();
                    foreach (var ind in j.Key)
                    {
                        
                        Console.Write("Row:" + ind.Key + " " + "Column:" + ind.Value + " " + "Value:" + j.Value);
                        Console.WriteLine();
                    }
                }

                return indexesValue;
            }


        public Dictionary<int, List<int>> AllFreeFields(DataGrid ScoreGrid,int turnNo)
        {
            int column = -1;
            int row = -1;

           Dictionary<int, List<int>> freeFieldsIndexes = new Dictionary<int, List<int>>();

            for (int i = 0; i < ScoreGrid.Items.Count; i++)
            {
                List<int> columnsInRow = new List<int>();
                freeFieldsIndexes.Add(i, columnsInRow);
                for (int j = 1; j < ScoreGrid.Columns.Count; j++)
                {

                    TextBlock freeField = dataGridWrapper.GetCellContent(ScoreGrid,j,i);


                    //DataGridCell cell = ScoreGrid.Columns[j].GetCellContent(i) as DataGridCell; //find another way to get cell
                    if (j == 1 && string.IsNullOrEmpty(freeField.Text) && i > 0)
                    {
                        TextBlock aboveField = dataGridWrapper.GetCellContent(ScoreGrid, j, i-1); ;

                        if (string.IsNullOrEmpty(aboveField.Text)) continue;
                        columnsInRow.Add(j);
                        freeFieldsIndexes[i] = columnsInRow;
                        //freeField.Background = Brushes.Aqua;
                        continue;
                    }

                    if (j == 3 && string.IsNullOrEmpty(freeField.Text) && i < ScoreGrid.Items.Count - 1)
                    {

                        TextBlock underField = dataGridWrapper.GetCellContent(ScoreGrid, j, i+1); ;
          
                        if (string.IsNullOrEmpty(underField.Text)) continue;
                        columnsInRow.Add(j);
                        freeFieldsIndexes[i] = columnsInRow;
                        continue;
                    }

                    if (j == 4 && string.IsNullOrEmpty(freeField.Text) && turnNo > 1) continue;


                    if (string.IsNullOrEmpty(freeField.Text))
                    {
                        columnsInRow.Add(j);
                        freeFieldsIndexes[i] = columnsInRow;
                    }


                }


            }
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





    }
}

