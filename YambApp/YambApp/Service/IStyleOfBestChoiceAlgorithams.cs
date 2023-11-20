using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YambApp.Service
{
    public interface IStyleOfBestChoiceAlgorithams
    {
        Dictionary<Dictionary<int, int>, int> StyleOfFreeFields(DataGrid ScoreGrid, int turnNo, bool locked, DataGridCell lockedCell, List<int> dices, bool gameWithBonus);
    }
}
