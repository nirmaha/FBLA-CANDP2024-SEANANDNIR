using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using EduPartners.Core;
using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private Database db;
        public SignUpWindow()
        {
            InitializeComponent();
            db = new Database();

            if (!(string.IsNullOrEmpty(App.Current.Properties["FirstName"]?.ToString())))
            {
                tbFirstName.Text = App.Current.Properties["FirstName"].ToString();
            }
            if (!(string.IsNullOrEmpty(App.Current.Properties["LastName"]?.ToString())))
            {
                tbLastName.Text = App.Current.Properties["LastName"]?.ToString();
            }
            if (!(string.IsNullOrEmpty(App.Current.Properties["Email"]?.ToString())))
            {
                tbEmail.Text = App.Current.Properties["Email"].ToString();
            }
        }

        private void SignUpBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.Source is TextBox || e.Source is PasswordBox))
            {
                Keyboard.ClearFocus();
                tbFirstName.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                tbLastName.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                tbEmail.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                pbPassword.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                pbConfirmPassword.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
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
            if (tbEmail.Text != "")
            {
                lEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                lEmail.Visibility = Visibility.Visible;
            }
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
            if (pbPassword.Password != "")
            {
                lPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                lPassword.Visibility = Visibility.Visible;
            }
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
            if (tbFirstName.Text != "")
            {
                lFirstName.Visibility = Visibility.Collapsed;
            }
            else
            {
                lFirstName.Visibility = Visibility.Visible;
            }
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
            if (tbLastName.Text != "")
            {
                lLastName.Visibility = Visibility.Collapsed;
            }
            else
            {
                lLastName.Visibility = Visibility.Visible;
            }
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
            if (pbConfirmPassword.Password != "")
            {
                lConfirmPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                lConfirmPassword.Visibility= Visibility.Visible;
            }
        }

        private void lConfirmPassword_Clicked(object sender, MouseButtonEventArgs e)
        {
            pbConfirmPassword.Focus();
        }


        private void LoginRedirect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = null;
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
            loginWindow.Show();
        }

        private void btnBack_Clicked(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Owner = null;
            this.Close();
            homePage.Show();
        }

        private async void btnNext_Clicked(object sender, RoutedEventArgs e)
        {
            if (tbFirstName.Text == "" || tbLastName.Text == "" || tbEmail.Text == "" || pbPassword.Password == "" || pbConfirmPassword.Password == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            List<User> users = await db.GetUsers();

            foreach (User user in users)
            {
                if (tbEmail.Text == user.Email)
                {
                    MessageBox.Show("Cannot create account due to the email already existing.");
                    return;
                }
            }

            if (pbPassword.Password != pbConfirmPassword.Password)
            {
                MessageBox.Show("Confirmed password does not match given password.");
                return;
            }

            if (cbTerms.IsChecked == false) 
            {
                MessageBox.Show("Please agree to the terms and conditions.");
                return;
            }

            if (!tbEmail.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email");
                return;
            }

            App.Current.Properties["FirstName"] = tbFirstName.Text;
            App.Current.Properties["LastName"] = tbLastName.Text;
            App.Current.Properties["Email"] = tbEmail.Text;
            App.Current.Properties["Password"] = pbPassword.Password;

            SchoolSelection schoolSelection = new SchoolSelection();
            schoolSelection.Owner = null;
            schoolSelection.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
            schoolSelection.Show();
        }
    }
}
