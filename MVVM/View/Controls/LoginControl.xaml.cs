using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using BCrypts = BCrypt.Net.BCrypt;

using EduPartners.Core;
using EduPartners.MVVM.Model;


namespace EduPartners.MVVM.View.Controls
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        private Database db;
        private IniFile iniFile;

        // Path to the local AppData folder and to EduPartners folder
        private static string localDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EduPartners");
        private string filePath = Path.Combine(localDataPath, "EduPartners.ini");

        public LoginControl()
        {
            InitializeComponent();
            
            db = App.Current.Properties["Database"] as Database;
            iniFile = new IniFile(filePath, localDataPath);

            this.Loaded += LoginControl_Loaded;
        }

        private void LoginControl_Loaded(object sender, RoutedEventArgs e)
        {
            lErrorMessage.Visibility = Visibility.Collapsed;

            // Clears User information properties
            App.Current.Properties["FirstName"] = "";
            App.Current.Properties["LastName"] = "";
            App.Current.Properties["Email"] = "";
            App.Current.Properties["Password"] = "";

            // Keeps Remember me on if there are cookies
            string emailLogin = iniFile.GetValue("SECURITY", "EMAILLOGIN");
            string passwordLogin = iniFile.GetValue("SECURITY", "PASSWORDLOGIN");
            string dateLoginSaved = iniFile.GetValue("SECURITY", "DATELOGINSAVED");


            // Keeps Remember me on if there is cookies
            if (!string.IsNullOrEmpty(emailLogin))
            {
                cbRememberMe.IsChecked = true;
                tbEmail.Text = emailLogin;
                pbPassword.Focus();
            }
            else
            {
                tbEmail.Focus();
            }

            // If Password cookie is empty password box to nothing
            if (string.IsNullOrEmpty(passwordLogin))
            {
                cbRememberMe.IsChecked = true;
                pbPassword.Password = "";
            }
            // Checks if it has been 30 days (or more) then it sets the password field
            else if (!string.IsNullOrEmpty(passwordLogin) && !string.IsNullOrEmpty(dateLoginSaved) && !HasPassedDays(Convert.ToDateTime(dateLoginSaved), 30))
            {
                // Has not been 30 days
                cbRememberMe.IsChecked = true;
                pbPassword.Password = passwordLogin;
            }
            else if (!string.IsNullOrEmpty(passwordLogin) && HasPassedDays(Convert.ToDateTime(dateLoginSaved), 30))
            {
                // Has been 30 days -- clears the password cookie
                pbPassword.Password = "";
                iniFile.SetValue("SECURITY", "PASSWORDLOGIN", "");
                iniFile.Save();
            }

        }

        /// <summary>
        /// This function checks if it has been <paramref name="days"/> since <paramref name="previousDate"/>.
        /// </summary>
        /// <param name="previousDate">The previous date that it has been.</param>
        /// <param name="days">Number of days to check.</param>
        /// <returns>If it has or has not been <paramref name="days"/>.</returns>
        private bool HasPassedDays(DateTime previousDate, int days)
        {
           return (int)previousDate.Subtract(DateTime.Now).TotalDays >= days;
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
            tbEmail.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
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
           lEmail.Visibility = string.IsNullOrEmpty(tbEmail.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lPassword.Visibility = string.IsNullOrEmpty(pbPassword.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LoginBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Resets textboxes background if the login border was clicked.
            if (!(e.Source is TextBox || e.Source is PasswordBox))
            {
                Keyboard.ClearFocus();
                pbPassword.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                tbEmail.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
            }
        }

        private async void btLogin_Clicked(object sender, RoutedEventArgs e)
        {
            // Checks for emptiness
            if (tbEmail.Text == "" || pbPassword.Password == "")
            {
                ShowErrorMessage("Please enter a username and a password.");
                return;
            }

            User user = (await db.GetUserByEmail(tbEmail.Text)).FirstOrDefault();
            if (user == null || !BCrypts.Verify(pbPassword.Password, user.Password))
            {
                ShowErrorMessage("Username / Password is incorrect.");
                return;
            }

            // Fills in cookies if Remember me is checked
            if (cbRememberMe.IsChecked == true)
            {
                SetRememberMeCookies(tbEmail.Text, pbPassword.Password);
            }
            // Clears in cookies if Remember me is not checked
            else
            {
                ClearRememberMeCookies();
            }

            SetUserProperties(user);

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("MainControl");
        }

        private void ShowErrorMessage(string message)
        { 
            lErrorMessage.Visibility = Visibility.Visible;
            lErrorMessage.Content = message;
        }

        private void SetRememberMeCookies(string email, string password)
        {
            iniFile.SetValue("SECURITY", "EMAILLOGIN", email);
            iniFile.SetValue("SECURITY", "PASSWORDLOGIN", password);
            iniFile.SetValue("SECURITY", "DATELOGINSAVED", DateTime.Now.ToString());
            iniFile.Save();
        }

        private void ClearRememberMeCookies()
        {
            iniFile.SetValue("SECURITY", "EMAILLOGIN", "");
            iniFile.SetValue("SECURITY", "PASSWORDLOGIN", "");
            iniFile.SetValue("SECURITY", "DATELOGINSAVED", "");
            iniFile.Save();
        }

        private void SetUserProperties(User user)
        {
            App.Current.Properties["CurrentSchoolId"] = user.HomeSchool.Id;
            App.Current.Properties["CurrentUserId"] = user.Id;
        }

        private void SignUpRedirect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("SignUpControl");
        }

        private void btBack_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("HomePage");
        }
    }
}
