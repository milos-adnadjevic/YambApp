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
using YambApp.ViewModel;

namespace YambApp
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            this.DataContext = new RegistrationViewModel();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.Password = passwordBox.Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.ConfirmPassword = passwordBox.Password;
            }
        }


        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (OriginalPassword.Visibility == Visibility.Visible)
            {
                // Show TextBox (visible password)
                OriginalPassword.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordTextBox.Text = OriginalPassword.Password;

                // Toggle Eye Icon
                EyeIcon.Visibility = Visibility.Collapsed;
                EyeSlashIcon.Visibility = Visibility.Visible;
            }
            else
            {
                // Hide TextBox and show PasswordBox
                OriginalPassword.Visibility = Visibility.Visible;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                OriginalPassword.Password = PasswordTextBox.Text;

                // Toggle Eye Icon
                EyeIcon.Visibility = Visibility.Visible;
                EyeSlashIcon.Visibility = Visibility.Collapsed;
            }
        }

        private void TogglePasswordVisibility_Click2(object sender, RoutedEventArgs e)
        {
            if (ConfirmPassword.Visibility == Visibility.Visible)
            {
                // Show TextBox (visible password)
                ConfirmPassword.Visibility = Visibility.Collapsed;
                PasswordTextBox2.Visibility = Visibility.Visible;
                PasswordTextBox2.Text = ConfirmPassword.Password;

                // Toggle Eye Icon
                EyeIcon2.Visibility = Visibility.Collapsed;
                EyeSlashIcon2.Visibility = Visibility.Visible;
            }
            else
            {
                // Hide TextBox and show PasswordBox
                ConfirmPassword.Visibility = Visibility.Visible;
                PasswordTextBox2.Visibility = Visibility.Collapsed;
                ConfirmPassword.Password = PasswordTextBox2.Text;

                // Toggle Eye Icon
                EyeIcon2.Visibility = Visibility.Visible;
                EyeSlashIcon2.Visibility = Visibility.Collapsed;
            }
        }
    }
}
