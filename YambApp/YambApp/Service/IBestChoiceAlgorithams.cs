using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YambApp.Service
{
    public interface  IBestChoiceAlgorithams
    {
        

        Dictionary<Dictionary<int, int>, int> BestChoice(Dictionary<int, List<int>> freeFieldsIndexes, List<int> dices, bool gameWithBonus);

        Dictionary<int, List<int>> AllFreeFields(DataGrid ScoreGrid, int turnNo);

        
    }
}
