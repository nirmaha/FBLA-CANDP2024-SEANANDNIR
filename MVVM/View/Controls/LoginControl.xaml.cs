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

        // Path to the local AppData folder and to EduPartners folder
        private static string localDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EduPartners");
        private string filePath = Path.Combine(localDataPath, "EduPartners.ini");

        public LoginControl()
        {
            InitializeComponent();
            
            db = App.Current.Properties["Database"] as Database;
            
            this.Loaded += LoginControl_Loaded;
        }

        private void LoginControl_Loaded(object sender, RoutedEventArgs e)
        {
            lErrorMessage.Visibility = Visibility.Collapsed;

            // Clears User information propteries
            App.Current.Properties["FirstName"] = "";
            App.Current.Properties["LastName"] = "";
            App.Current.Properties["Email"] = "";
            App.Current.Properties["Password"] = "";

            IniFile iniFile = new IniFile(filePath, localDataPath);

            // Keeps Remeber me on if there is cookies
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

            // If Password cookie is empty password box to nothing
            if (iniFile.GetValue("SECURITY", "PASSWORDLOGIN") == "")
            {
                cbRememberMe.IsChecked = true;
                pbPassword.Password = "";
            }
          
            // Checks if it has been 30 days (or more) then it sets the password field
            if (iniFile.GetValue("SECURITY", "PASSWORDLOGIN") != "" && iniFile.GetValue("SECURITY", "DATELOGINSAVED") != "" && !HasPassedDays(Convert.ToDateTime(iniFile.GetValue("SECURITY", "DATELOGINSAVED")), 30))
            {
                // Has not been 30 days
                cbRememberMe.IsChecked = true;
                pbPassword.Password = iniFile.GetValue("SECURITY", "PASSWORDLOGIN");
            }
            else if (iniFile.GetValue("SECURITY", "DATELOGINSAVED") != "" && HasPassedDays(Convert.ToDateTime(iniFile.GetValue("SECURITY", "DATELOGINSAVED")), 30))
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
            // Calculate the current date
            DateTime currentDate = DateTime.Now;

            // Calculate the difference in days
            int differenceInDays = (int)(currentDate - previousDate).TotalDays;

            // Check if the difference is greater than or equal to the specified number of days
            return differenceInDays >= days;
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
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Please enter a username and a password.";
                return;
            }

            User user = (await db.GetUserByEmail(tbEmail.Text)).FirstOrDefault();
            User logingInUser = null;
            IniFile iniFile = new IniFile(filePath, localDataPath);

            // Checks if the user inputted the correct email
            if (user == null)
            {
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Username / Password is incorrect.";
                return;
            }

            // Checks if the correct password was inputted
            if (BCrypts.Verify(pbPassword.Password, user.Password))
            {
                logingInUser = user;
                App.Current.Properties["User"] = logingInUser.Id;
            }
            else
            {
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Username / Password is incorrect.";
                return;
            }

            // Fills in cookies if Remember me is checked
            if (cbRememberMe.IsChecked == true)
            {
                iniFile.SetValue("SECURITY", "EMAILLOGIN", tbEmail.Text);
                iniFile.SetValue("SECURITY", "PASSWORDLOGIN", pbPassword.Password);
                iniFile.SetValue("SECURITY", "DATELOGINSAVED", DateTime.Now.ToString());
                iniFile.Save();
            }
            // Clears in cookies if Remember me is not checked
            else
            {
                iniFile.SetValue("SECURITY", "EMAILLOGIN", "");
                iniFile.SetValue("SECURITY", "PASSWORDLOGIN", "");
                iniFile.SetValue("SECURITY", "DATELOGINSAVED", "");
                iniFile.Save();
            }

            App.Current.Properties["CurrentSchoolId"] = logingInUser.HomeSchool.Id;
            App.Current.Properties["CurrentUserId"] = logingInUser.Id;

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("MainControl");
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
