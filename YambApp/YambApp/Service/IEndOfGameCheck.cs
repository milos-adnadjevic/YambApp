using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YambApp.Service
{
    public interface IEndOfGameCheck
    {
        bool EndOfGame(DataGrid scoreGrid, Dictionary<Dictionary<int, int>, int> dictionary);
    }
}
