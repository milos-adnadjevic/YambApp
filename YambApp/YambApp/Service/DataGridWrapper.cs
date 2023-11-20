using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YambApp.Service
{
    public class DataGridWrapper:IDataGridWrapper

    {

        public TextBlock GetCellContent (DataGrid dataGrid,int columnIndex, int rowIndex)
        {
           return dataGrid.Columns[columnIndex].GetCellContent(dataGrid.Items[rowIndex]) as TextBlock;
        }
    }
}
