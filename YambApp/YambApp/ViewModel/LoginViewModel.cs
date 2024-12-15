using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using YambApp.Model;
using YambApp.Service;

namespace YambApp.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ILoginService _loginService;
        private readonly INavigationService _navigationService;

        private string _usernameOrEmail;
        public string UsernameOrEmail
        {
            get => _usernameOrEmail;
            set
            {
                _usernameOrEmail = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

 
        private bool _canExecuteButton= false;
        public bool CanExecuteButton
        {
            get => _canExecuteButton;
            set
            {
                _canExecuteButton = value;
                OnPropertyChanged(nameof(CanExecuteButton));
            }
        }
        public ICommand LoginCommand { get; }

        public LoginViewModel(ILoginService loginService,INavigationService navigationService)
        {
            CanExecuteButton=false;
            _loginService = loginService;
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private void ExecuteLogin(object parameter)
        {
            StatusMessage = "Logging in...";
            bool isLoggedIn = _loginService.Authenticate(UsernameOrEmail, Password);
            if (!isLoggedIn)
            {
                StatusMessage = "Invalid username/email or password.";

            }
            else if(isLoggedIn)
            {
                StatusMessage = "Successfull login!";

                // Populate session details
                UserSession.Instance.Username = UsernameOrEmail;

                _navigationService.NavigateTo("Home",Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault());



            }

        }

        public bool CanExecuteLogin(object parameter)
        {
            if( !string.IsNullOrWhiteSpace(UsernameOrEmail) && !string.IsNullOrWhiteSpace(Password))
            {
                CanExecuteButton = true;
                return true;
            }
            else
            {
                CanExecuteButton= false; 
                return false;
            }

        }




        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
