using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YambApp.Model;

namespace YambApp.Service
{
    public class EndOfGameCheck: IEndOfGameCheck
    {
        IBestChoiceAlgorithams bestChoiceAlgorithams;
        ISumRules sumRules;
        IDataGridWrapper dataGridWrapper;
        public EndOfGameCheck() 
        { 
            dataGridWrapper=new DataGridWrapper();
            sumRules=new SumRules();
            bestChoiceAlgorithams = new BestChoiceAlgorithams(dataGridWrapper,sumRules);
        }

        public bool EndOfGame(DataGrid scoreGrid, Dictionary<Dictionary<int, int>, int> dictionary)
        {
            int column = -1;
            int row = -1;
            int counter = 0;

           

            for (int i = 0; i < scoreGrid.Items.Count; i++)
            {
               
              
                for (int j = 1; j < scoreGrid.Columns.Count; j++)
                {

                    TextBlock freeField = scoreGrid.Columns[j].GetCellContent(scoreGrid.Items[i]) as TextBlock;

                    if (freeField == null)
                    {
                        YambCategory items = (YambCategory)scoreGrid.Items[i];
                        List<string> itemsAsList = new List<string>();
                        itemsAsList.Add(items.CategoryName);
                        itemsAsList.Add(items.Up);
                        itemsAsList.Add(items.Free);
                        itemsAsList.Add(items.Down);
                        itemsAsList.Add(items.Lock);


                        string text = itemsAsList[j];
                        freeField = new TextBlock();
                        freeField.Text = text;
                    }

                    if (string.IsNullOrEmpty(freeField.Text))
                    {
                        return false;
                    }

                }
            }

            if (dictionary.Count == 0) return true;
            else return false;

           

           
        }
    }
}
