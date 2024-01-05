using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void pbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pbPassword.Background = Brushes.White;
        }

        private void tbEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            tbEmail.Background = Brushes.White;
        }

        private void tbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            tbEmail.Background = new SolidColorBrush(Color.FromRgb(237,237,237));
        }

        private void pbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            pbPassword.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void lEmail_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbEmail.Focus();
        }

        private void lPassword_Clicked(object sender, MouseButtonEventArgs e)
        {
            pbPassword.Focus();
        }

        private void tbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbEmail.Text != "")
            {
                lEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                lEmail.Visibility = Visibility.Visible;
            }
        }

        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbPassword.Password != "")
            {
                lPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                lPassword.Visibility = Visibility.Visible;
            }
        }

        private void LoginBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.Source is TextBox || e.Source is PasswordBox))
            {
                Keyboard.ClearFocus();
                pbPassword.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                tbEmail.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
            }
        }

        private void btnLogin_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Owner = null;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
            mainWindow.Show();
        }

        private void SignUpRedirect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Owner = null;
            signUpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
            signUpWindow.Show();
        }
    }
}
