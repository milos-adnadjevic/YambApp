using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using YambApp.Service;
using NUnit.Core;
using YambApp.Model;

namespace YambApp.ViewModel
{
        public class RegistrationViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            IUserService _userService;
            INavigationService _navigationService;
           // private readonly Window _registrationWindow;

            private string _username;
            private string _email;
            private string _password;
            private string _confirmPassword;

            public string Username
            {
                get => _username;
                set
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }

            public string Email
            {
                get => _email;
                set
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }

            public string Password
            {
                get => _password;
                set
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }

            public string ConfirmPassword
            {
                get => _confirmPassword;
                set
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                }
            }
            private string _statusMessageUsername;
            public string StatusMessageUsername
            {
                get => _statusMessageUsername;
                set
                {
                    _statusMessageUsername = value;
                    OnPropertyChanged(nameof(StatusMessageUsername));
                }
            }
            private string _statusMessageEmail;
            public string StatusMessageEmail
            {
                get => _statusMessageEmail;
                set
                {
                    _statusMessageEmail = value;
                    OnPropertyChanged(nameof(StatusMessageEmail));
                }
            }
            private string _statusMessagePassword;
            public string StatusMessagePassword
            {
                get => _statusMessagePassword;
                set
                {
                    _statusMessagePassword = value;
                    OnPropertyChanged(nameof(StatusMessagePassword));
                }
            }

            private bool _canExecuteButton = false;
            public bool CanExecuteButton
            {
                get => _canExecuteButton;
                set
                {
                    _canExecuteButton = value;
                    OnPropertyChanged(nameof(CanExecuteButton));
                }
            }
        public RelayCommand SubmitCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

            public RegistrationViewModel()
            {
                _userService = new UserService();
                _navigationService = new NavigationService();
            //_registrationWindow = window;
                 CanExecuteButton = false;

            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);
            NavigateToLoginCommand = new RelayCommand (NavigateToLogin);

            }
            private void NavigateToLogin(object obj)
            {
                _navigationService.NavigateTo("Login",Application.Current.Windows.OfType<RegistrationWindow>().FirstOrDefault()); 
            }
            private void NavigateToLogin()
            {
                _navigationService.NavigateTo("Login", Application.Current.Windows.OfType<RegistrationWindow>().FirstOrDefault());
            }




        private bool CanSubmit(object parameter)
        {
                
                CanExecuteButton= !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);
                return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);
        }


            private void OnSubmit(object parameter)
            {
            // Your submit logic here
                StatusMessagePassword = null;
                StatusMessageUsername = null;
                StatusMessageEmail = null;
                bool passwordPass = true;
                bool usernamePass = true;
                bool emailPass = true;
            if (Password != ConfirmPassword)
                {
                    StatusMessagePassword = "Passwords do not match."; 
                    passwordPass=false;
                }
                if (_userService.UsernameExist(Username))
                {
                    StatusMessageUsername = "Username already exist.";
                    usernamePass=false;
                }
                if (_userService.EmailExist(Email))
                {
                    StatusMessageEmail = "E-mail already exist.";
                    emailPass=false;
                }
                if(!_userService.IsValidEmail(Email)) 
                {
                    StatusMessageEmail = "E-mail form is not correct.";
                    emailPass = false;
                }
                if(!passwordPass || !usernamePass || !emailPass) { return; }

                User user = new User
                {
                    Username = Username,
                    Email = Email,
                    Password = Password,

                };
                _userService.Create(user);
            
                
                NavigateToLogin();

                CustomMessageBox customMessageBox = new CustomMessageBox("Registration Successful");
                customMessageBox.ShowDialog();
              
             
        }

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
}

