using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YambApp.Model;

namespace YambApp.ViewModel
{
    public class CustomMessageBoxViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand OkCommand { get; }

        private string _informationMessage;
        public string InformationMessage
        {
            get => _informationMessage;
            set
            {
                _informationMessage = value;
                OnPropertyChanged();
            }
        }
        public CustomMessageBoxViewModel(string infoMess)
        {
            InformationMessage = infoMess;
            OkCommand= new RelayCommand(Execute, CanExecute);
        }


        private void Execute(object parameter)
        {
          
          Application.Current.Windows.OfType<CustomMessageBox>().FirstOrDefault().Close();

        }

        public bool CanExecute(object parameter)
        {
            return true;

        }




        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
