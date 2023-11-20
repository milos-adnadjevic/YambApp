using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YambApp.Model;

namespace YambApp.Service
{
    public class Score : IScore
    {

        //get score of match at any moment
        IDataGridWrapper dataGridWrapper;
        public Score(IDataGridWrapper dataGridWrapper) 
        {
            this.dataGridWrapper= dataGridWrapper;
        }
        public int ScoreValue(DataGrid ScoreGrid)
        {
            int score = 0;
            for (int i = 0; i < ScoreGrid.Items.Count; i++)
            {
                for (int j = 1; j < ScoreGrid.Columns.Count; j++)
                {
                    TextBlock block = dataGridWrapper.GetCellContent(ScoreGrid,j,i);
                  
                    //exception because of special way to calculate points from this rows
                    if (i == 6 || i == 7)
                    {
                        if (i == 7) continue;
                        TextBlock max = dataGridWrapper.GetCellContent(ScoreGrid, j, 6);
                     

                        TextBlock min = dataGridWrapper.GetCellContent(ScoreGrid, j, 7);
                     
                        TextBlock multiplier = dataGridWrapper.GetCellContent(ScoreGrid, j, 0);
                     
                        if (!string.IsNullOrEmpty(max.Text) && !string.IsNullOrEmpty(min.Text) && !string.IsNullOrEmpty(multiplier.Text))
                        {
                            score += (int.Parse(max.Text) - int.Parse(min.Text)) * int.Parse(multiplier.Text);
                        }
                        else score += 0;

                        continue;

                    }

                    if (!string.IsNullOrEmpty(block.Text))
                    {
                        score += int.Parse(block.Text);
                    }
                    else score += 0;

                }

            }

            return score;


        }
    }
}
