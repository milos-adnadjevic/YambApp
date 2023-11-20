using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace YambApp.Service
{
    public class StyleOfBestChoiceAlgorithams:IStyleOfBestChoiceAlgorithams
    {
        private static IBestChoiceAlgorithams bestChoiceAlgorithams;
        ISumRules sumRules;
        IDataGridWrapper dataGridWrapper;
        public StyleOfBestChoiceAlgorithams()
        {
            dataGridWrapper = new DataGridWrapper();
            sumRules = new SumRules();
            bestChoiceAlgorithams = new BestChoiceAlgorithams(dataGridWrapper,sumRules);
        }
        public Dictionary<Dictionary<int, int>, int> StyleOfFreeFields(DataGrid ScoreGrid, int turnNo, bool locked, DataGridCell lockedCell, List<int> dices, bool gameWithBonus)
        {

            Dictionary<int, List<int>> freeFieldsIndexes = bestChoiceAlgorithams.AllFreeFields(ScoreGrid, turnNo);

            //if locked release all free fields because u lock one
            if (locked)
            {

                foreach (var dic in freeFieldsIndexes)
                {
                    foreach (int col in dic.Value)
                    {

                        TextBlock style = ScoreGrid.Columns[col].GetCellContent(ScoreGrid.Items[dic.Key]) as TextBlock;
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

                return new Dictionary<Dictionary<int, int>, int>();
            }

            //colored free fields and type preview value                      
            Dictionary<Dictionary<int, int>, int>
                indexesValue = bestChoiceAlgorithams.BestChoice(freeFieldsIndexes, dices, gameWithBonus);

            foreach (var dic in indexesValue)
            {
                foreach (var indexes in dic.Key)
                {

                    TextBlock style = ScoreGrid.Columns[indexes.Value].GetCellContent(ScoreGrid.Items[indexes.Key]) as TextBlock;
                    style.Background = Brushes.Aquamarine;
                    style.Text = dic.Value.ToString();
                    //previewLook = true;



                }
            }
            //release free fields in Lock columns after first turn
            if (turnNo > 1)
            {


                foreach (var dic1 in freeFieldsIndexes)
                {

                    TextBlock style = ScoreGrid.Columns[4].GetCellContent(ScoreGrid.Items[dic1.Key]) as TextBlock;
                    style.Background = Brushes.Transparent;

                }

            }

            return indexesValue;

        }
    }
}
