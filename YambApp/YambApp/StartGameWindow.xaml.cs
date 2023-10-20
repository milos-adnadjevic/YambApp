using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YambApp
{
    /// <summary>
    /// Interaction logic for StartGameWindow.xaml
    /// </summary>
    
    public partial class StartGameWindow : Window
    {
        private bool withBonus = false;

       
        public StartGameWindow()
        {
            InitializeComponent();
            DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
          
        }

        private void WithBonus_Click(object sender, RoutedEventArgs e)
        {
            withBonus = true;
            MainWindow main = new MainWindow(withBonus);
            main.Show();
            this.Close();

            
        }

        private void WithOutBonus_Click(object sender, RoutedEventArgs e)
        {
            withBonus = false;
            MainWindow main = new MainWindow(withBonus);
            main.Show();
            this.Close();
        }

  
    }
}
