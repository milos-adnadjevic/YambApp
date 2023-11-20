using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace YambApp.ViewModel
{
    public class StartGameWindowViewModel : INotifyPropertyChanged
    {
        private bool withBonus = false;

        public ICommand ClickWithBonusCommand { get; set; }

        public ICommand ClickWithOutBonusCommand { get; set; }

     

        public StartGameWindowViewModel() 
        {
           
            ClickWithBonusCommand = new RelayCommand(ClickWithBonus);
            ClickWithOutBonusCommand = new RelayCommand(ClickWithOutBonus);
        }


        private void ClickWithBonus(object obj) 
        {
            withBonus = true;
            MainWindow main = new MainWindow(withBonus);
            Application.Current.MainWindow.Close();
            main.Show();



        }


        private void ClickWithOutBonus(object obj)
        {
            withBonus = false;
            MainWindow main = new MainWindow(withBonus);
            Application.Current.MainWindow.Close();
            main.Show();
           
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
