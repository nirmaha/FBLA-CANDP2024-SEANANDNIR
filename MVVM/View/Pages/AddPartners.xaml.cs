﻿using EduPartners.Core;
using EduPartners.MVVM.Model;
using EduPartners.MVVM.View.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for AddPartners.xaml
    /// </summary>
    public partial class AddPartners : Page
    {
        private Database db;
        public AddPartners()
        {
            InitializeComponent();
            db = App.Current.Properties["Database"] as Database;
            cbType.Items.Add("IT");
            cbType.Items.Add("Architecture");
        }

        private async void AddPartner_Cliked(object sender, RoutedEventArgs e)
        {
            bool emailMatch = Regex.IsMatch(tbRepresentativeEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            bool phoneNumberMatch = tbRepresentativePhoneNumber.Text.Length == 14;
            if (!emailMatch)
            {
                tbRepresentativeEmail.BorderBrush = Brushes.Red;
                tbRepresentativeEmail.BorderThickness = new Thickness(2);
                return;
            }
            if (!phoneNumberMatch)
            {
                tbRepresentativePhoneNumber.BorderBrush = Brushes.Red;
                tbRepresentativePhoneNumber.BorderThickness = new Thickness(2);
                return;
            }
            Partner partner = new Partner()
            { 
                Name = tbName.Text,
                Description = tbDescription.Text,
                ResourcesAvailable = tbResources.Text,
                Industry = cbType.Text,
                StartDate = dpStartDate.DisplayDate.Date,
                RepresentativeName = tbRepresentativeName.Text,
                RepresentativeEmail = tbRepresentativeEmail.Text,
                RepresentativePhoneNumber = tbRepresentativePhoneNumber.Text,
                Website = tbWebsite.Text,
                Address = tbAddress.Text,
                Savings = tbSavings.Text,
            };
            await db.CreatePartner(partner);

            School school = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();
            
            school.Partners.Value.Add(partner);

            await db.UpdateSchool(school);

            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();

            user.HomeSchool = school;
            await db.UpdateUser(user);
            
            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.Load_Page("MVVM/View/Pages/ViewPartners.xaml");
        }

        private void PhoneNumber_Changed(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Use a regular expression to format the phone number
                if (Regex.IsMatch(textBox.Text, @"^\d{10}$"))
                {
                    textBox.Text = Regex.Replace(textBox.Text, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                }
            }
        }
    }
}
