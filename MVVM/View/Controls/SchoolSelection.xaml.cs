﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            this.Loaded += (sender, e) => 
            {
                PopulateComboBox();
                lErrorMessage.Visibility = Visibility.Collapsed;
                cbSchool.SelectedIndex = -1;
                tbSchoolId.Clear();
            };
        }

        private async void PopulateComboBox()
        {
            cbSchool.Items.Clear();
            List<School> schools = (await db.GetSchools()).OrderBy(s => s.Name).ToList();

            schools.ForEach(school => cbSchool.Items.Add(school.Name));
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
        private async void btCreateAccount_Clicked(object sender, RoutedEventArgs e)
        {
            // Checks if a school has been selected
            if (cbSchool.SelectedItem == null)
            {
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Please select a school.";
                return;
            }

            // Checks if a school code has been inputted
            if (tbSchoolId.Text == "")
            {
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Please enter a school code.";
                return;
            }

            School school = (await db.GetSchoolByName(cbSchool.SelectedItem.ToString())).FirstOrDefault();

            // Checks if the correct school code has been inputted
            if (tbSchoolId.Text != school.Code)
            {
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Please enter a valid school code.";
                return;
            }

            // Creates a new user
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
