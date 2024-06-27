using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using EduPartners.Core;
using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.View.Controls
{
    /// <summary>
    /// Interaction logic for SignUpControl.xaml
    /// </summary>
    public partial class SignUpControl : UserControl
    {
        private Database db;

        public SignUpControl()
        {
            InitializeComponent();

            db = App.Current.Properties["Database"] as Database;

            lErrorMessage.Visibility = Visibility.Collapsed;

            this.Loaded += (sender, e) => 
            {
                lErrorMessage.Visibility = Visibility.Collapsed;

                // Fills in saved data
                tbFirstName.Text = App.Current.Properties["FirstName"]?.ToString();
                tbLastName.Text = App.Current.Properties["LastName"]?.ToString();
                tbEmail.Text = App.Current.Properties["Email"]?.ToString();
                pbPassword.Clear();
                pbConfirmPassword.Clear();
                cbTerms.IsChecked = false;
            };

        }

        private void SignUpBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Resets textboxes backgrounds if the border is clicked
            if (!(e.Source is TextBox || e.Source is PasswordBox))
            {
                Keyboard.ClearFocus();

                SolidColorBrush newBackground = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                tbFirstName.Background = newBackground;
                tbLastName.Background = newBackground;
                tbEmail.Background = newBackground;
                pbPassword.Background = newBackground;
                pbConfirmPassword.Background = newBackground;
            }
        }

        private void tbEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            tbEmail.Background = Brushes.White;
        }

        private void tbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            tbEmail.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void tbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            lEmail.Visibility = string.IsNullOrEmpty(tbEmail.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void lEmail_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbEmail.Focus();
        }

        private void pbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pbPassword.Background = Brushes.White;
        }

        private void pbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            pbPassword.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lPassword.Visibility = string.IsNullOrEmpty(pbPassword.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void lPassword_Clicked(object sender, MouseButtonEventArgs e)
        {
            pbPassword.Focus();
        }

        private void tbFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbFirstName.Background = Brushes.White;
        }

        private void tbFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbFirstName.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void tbFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lFirstName.Visibility = string.IsNullOrEmpty(tbFirstName.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void lFirstName_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbFirstName.Focus();
        }

        private void tbLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbLastName.Background = Brushes.White;
        }

        private void tbLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbLastName.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void tbLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lLastName.Visibility = string.IsNullOrEmpty(tbLastName.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void lLastName_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbLastName.Focus();
        }

        private void pbConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pbConfirmPassword.Background = Brushes.White;
        }

        private void pbConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            pbConfirmPassword.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void pbConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lConfirmPassword.Visibility = string.IsNullOrEmpty(pbConfirmPassword.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void lConfirmPassword_Clicked(object sender, MouseButtonEventArgs e)
        {
            pbConfirmPassword.Focus();
        }

        private void LoginRedirect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("LoginControl");
        }

        private void btnBack_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("HomePage");
        }

        private async void btNext_Clicked(object sender, RoutedEventArgs e)
        {
            // Checks if any textbox is empty
            if (string.IsNullOrWhiteSpace(tbFirstName.Text) || string.IsNullOrWhiteSpace(tbLastName.Text) || string.IsNullOrWhiteSpace(tbEmail.Text) || string.IsNullOrWhiteSpace(pbPassword.Password) || string.IsNullOrWhiteSpace(pbConfirmPassword.Password))
            {
                ShowErrorMessage("Please fill in all the fields.", 18);
                return;
            }

            List<User> users = await db.GetUsers();
           
            // Checks if the email doesn't already exist
            if (users.Any(user => user.Email == tbEmail.Text))
            {
                ShowErrorMessage("Cannot create account due to the email already existing.", 15);
                return;
            }
            

            if (pbPassword.Password != pbConfirmPassword.Password)
            {
                ShowErrorMessage("Confirmed password does not match given password.", 15);
                return;
            }

            // Makes sure that the terms check box has been clicked
            if (cbTerms.IsChecked == false)
            {
                ShowErrorMessage("Please agree to the terms and conditions.", 18);
                return;
            }

            // Checks if a valid email has been inputted
            if (!Regex.IsMatch(tbEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                ShowErrorMessage("Please enter a valid email.", 18);
                return;
            }

            // Stores inputted data for user creation
            App.Current.Properties["FirstName"] = tbFirstName.Text;
            App.Current.Properties["LastName"] = tbLastName.Text;
            App.Current.Properties["Email"] = tbEmail.Text;
            App.Current.Properties["Password"] = pbPassword.Password;

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("SchoolSelection");
        }

        private void ShowErrorMessage(string message, int fontSize)
        { 
            lErrorMessage.FontSize = fontSize;
            lErrorMessage.Visibility = Visibility.Visible;
            lErrorMessage.Content = message;
        }
    }
}
