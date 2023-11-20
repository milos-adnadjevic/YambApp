using System.Windows.Controls;

namespace YambApp.Service
{
    public interface IDataGridWrapper
    {
        TextBlock GetCellContent(DataGrid dataGrid, int columnIndex, int rowIndex);
    }
}