using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using BCrypts = BCrypt.Net.BCrypt;

using EduPartners.Core;
using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Interaction logic for SchoolSelection.xaml
    /// </summary>
    public partial class SchoolSelection : Window
    {
        private Database db;
        public SchoolSelection()
        {
            InitializeComponent();
            db = new Database();
            
            PopulateComboBox();

        }

        private async void PopulateComboBox()
        {
            List<School> schools = await db.GetSchools();

            foreach (School school in schools)
            { 
                cbSchool.Items.Add(school.Name);
            }
        }

        private void SchoolSelectionBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.Source is TextBox || e.Source is PasswordBox))
            {
                Keyboard.ClearFocus();
                tbSchoolId.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
            }
        }

        private void tbSchoolId_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSchoolId.Background = Brushes.White;
        }

        private void tbSchoolId_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSchoolId.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void tbSchoolId_PasswordChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSchoolId.Text != "")
            {
                lSchoolId.Visibility = Visibility.Collapsed;
            }
            else
            {
                lSchoolId.Visibility = Visibility.Visible;
            }
        }

        private void lSchoolId_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbSchoolId.Focus();
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
        private async void btnCreateAccount_Clicked(object sender, RoutedEventArgs e)
        {
            if (cbSchool.SelectedItem == null)
            {
                MessageBox.Show("Please select a school.");
                return;
            }

            if (tbSchoolId.Text == "")
            {
                MessageBox.Show("Please enter a school code.");
                return;
            }

            List<School> school = await db.GetSchoolByName(cbSchool.SelectedItem.ToString());

            if (tbSchoolId.Text != school[0].Code)
            {
                MessageBox.Show("Please enter a valid code.");
                return;
            }
            
            User user = new User()
            {
                Name = App.Current.Properties["FirstName"].ToString() + " " + App.Current.Properties["LastName"].ToString(),
                Email = App.Current.Properties["Email"].ToString(),
                Password = BCrypts.HashPassword(App.Current.Properties["Password"].ToString()),
                HomeSchool = school[0]
            };

            await db.CreateUser(user);

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = null;
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
            loginWindow.Show();
        }
    }
}
