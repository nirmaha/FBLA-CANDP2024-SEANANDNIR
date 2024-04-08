using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using BCrypts = BCrypt.Net.BCrypt;

using EduPartners.Core;
using EduPartners.MVVM.Model;
using System.Linq;

namespace EduPartners.MVVM.View.Controls
{
    /// <summary>
    /// Interaction logic for SchoolSelection.xaml
    /// </summary>
    public partial class SchoolSelection : UserControl
    {
        private Database db;

        public SchoolSelection()
        {
            InitializeComponent();
            
            db = App.Current.Properties["Database"] as Database;

            lErrorMessage.Visibility = Visibility.Collapsed;

            this.Loaded += SchoolSelection_Loaded;
        }

        private void SchoolSelection_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateComboBox();
            lErrorMessage.Visibility = Visibility.Collapsed;
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
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("SignUpControl");
        }

        private void btnBack_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("HomePage");
        }
        private async void btnCreateAccount_Clicked(object sender, RoutedEventArgs e)
        {
            if (cbSchool.SelectedItem == null)
            {
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Please select a school.";
                return;
            }

            if (tbSchoolId.Text == "")
            {
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Please enter a school code.";
                return;
            }

            School school = (await db.GetSchoolByName(cbSchool.SelectedItem.ToString())).FirstOrDefault();

            if (tbSchoolId.Text != school.Code)
            {
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Please enter a school code.";
                return;
            }

            User user = new User()
            {
                Name = App.Current.Properties["FirstName"].ToString() + " " + App.Current.Properties["LastName"].ToString(),
                Email = App.Current.Properties["Email"].ToString(),
                Password = BCrypts.HashPassword(App.Current.Properties["Password"].ToString()),
                HomeSchool = school
            };

            await db.CreateUser(user);

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("LoginControl");

        }
    }
}
