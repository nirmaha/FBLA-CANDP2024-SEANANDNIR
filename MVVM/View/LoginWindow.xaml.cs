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
using EduPartners.Core;
using EduPartners.MVVM.Model;
using BCrypts = BCrypt.Net.BCrypt;

namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Database db;
        public LoginWindow()
        {
            InitializeComponent();

            db = new Database();
            IniFile iniFile = new IniFile("EduPartners.ini");
            if (iniFile.GetValue("SECURITY", "EMAILLOGIN") != "")
            {
                cbRememberMe.IsChecked = true;
                tbEmail.Text = iniFile.GetValue("SECURITY", "EMAILLOGIN");
                pbPassword.Focus();
            }
            else 
            {
                tbEmail.Focus();
            }
            
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

        private async void btnLogin_Clicked(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "" || pbPassword.Password == "")
            {
                MessageBox.Show("Please enter a username and a password.");
                return;
            }

            List<User> users = await db.GetUsers();
            User user = null;

            IniFile iniFile = new IniFile("EduPartners.ini");

            foreach (User tempUser in users)
            {
                if (tbEmail.Text == tempUser.Email && BCrypts.Verify(pbPassword.Password, tempUser.Password))
                { 
                    user = tempUser;
                    App.Current.Properties["User"] = user.Id;
                    break;
                }
            }

            if (user == null)
            {
                MessageBox.Show("Username / Password is incorrect.");
                return;
            }

            if (cbRememberMe.IsChecked == true)
            {
                iniFile.SetValue("SECURITY", "EMAILLOGIN", tbEmail.Text);
                iniFile.Save();
            }
            else
            {
                iniFile.SetValue("SECURITY", "EMAILLOGIN", "");
                iniFile.Save();
            }

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

        private void btnBack_Clicked(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Owner = null;
            this.Close();
            homePage.Show();
        }
    }
}
