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

using YambApp.Model;
using YambApp.ViewModel;

namespace YambApp
{
    public partial class MainWindow : Window
    {
        private bool gameWithBonus = false;

       
        
        public MainWindow(bool bonus)
        {
            InitializeComponent();   
            
            gameWithBonus = bonus;


            DataContext = new MainWindowViewModel(gameWithBonus,scoreGrid);   
            
            WindowStartupLocation = WindowStartupLocation.CenterScreen;                                                                       
           
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(gameWithBonus, scoreGrid);

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                EventTuple eventTuple = new EventTuple();
                eventTuple.Sender = sender;
                eventTuple.MouseEventArgs = e;
               
                viewModel.CellClickCommand.Execute(eventTuple);
            }
        }


    }
}




    


