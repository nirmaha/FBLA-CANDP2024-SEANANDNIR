using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using EduPartners.Core;
using EduPartners.MVVM.Model;
using EduPartners.MVVM.View.Controls;

namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for EditPartners.xaml
    /// </summary>
    public partial class EditPartners : Page
    {
        private Database db;
        private Partner partner;
        private List<string> industries = new List<string>() { "IT", "Architecture", "Educational Services", "Emergency Services", "Food Services", "Arts, Entertainment and Recreation", "Administration Service", "Business Support", "Construction", "Finance and Insurance", "Healthcare", "Information", "Real Estate and Rental and Leasing", "Transportation", "Utilities", "Technology" };

        public EditPartners()
        {
            InitializeComponent();
            db = App.Current.Properties["Database"] as Database;

            industries.Sort();
            industries.Add("Other");

            foreach (string industry in industries)
            {
                cbType.Items.Add(industry);
            }

            this.Loaded += async (sender, e) =>
            {
                partner = (await db.GetPartnerById(((Partner)App.Current.Properties["SelectedPartner"]).Id.ToString())).FirstOrDefault();

                DataContext = partner;
                tbRepresentativePhoneNumber.Text = partner.RepresentativePhoneNumber == "No Phone Number" ? "" : partner.RepresentativePhoneNumber;
                tbWebsite.Text = partner.Website == "No Website" ? "" : partner.Website;
                tbAddress.Text = partner.Address == "No Address" ? "" : partner.Address;

            };

        }

        private async void UpdatePartner_Clicked(object sender, RoutedEventArgs e)
        {
            bool isEmpty = false;

            foreach (UIElement uIElement in spMain.Children)
            {
                if (uIElement is TextBox textbox && ((textbox.Tag != null && textbox.Tag.ToString() == "required") && string.IsNullOrEmpty(textbox.Text)))
                {
                    textbox.BorderBrush = Brushes.Red;
                    textbox.BorderThickness = new Thickness(2);
                    lErrorMsg.Visibility = Visibility.Visible;
                    isEmpty = true;
                }
                else if (uIElement is DatePicker datePicker && ((datePicker.Tag != null && datePicker.Tag.ToString() == "required") && datePicker.SelectedDate == null))
                {
                    // Customize the appearance or behavior for the DatePicker control
                    datePicker.BorderBrush = Brushes.Red;
                    datePicker.BorderThickness = new Thickness(2);
                    lErrorMsg.Visibility = Visibility.Visible;
                    isEmpty = true;
                }
                else if (uIElement is Border comboBox && ((comboBox.Tag != null && comboBox.Tag.ToString() == "required") && cbType.SelectedItem == null))
                {
                    // Customize the appearance or behavior for the ComboBox control
                    comboBox.BorderBrush = Brushes.Red;
                    comboBox.BorderThickness = new Thickness(2);
                    lErrorMsg.Visibility = Visibility.Visible;
                    isEmpty = true;
                }
            }

            if (isEmpty)
            {
                isEmpty = false;
                return;
            }

            if (tbSavings.Text.StartsWith("$"))
            {
                tbSavings.Text = tbSavings.Text.Substring(1);
            }

            if (tbWebsite.Text.StartsWith(@"https://"))
            {
                tbWebsite.Text = tbWebsite.Text.Substring(8);
            }
            else if (tbWebsite.Text.StartsWith(@"http://"))
            {
                tbWebsite.Text = tbWebsite.Text.Substring(7);
            }
            if (tbWebsite.Text.StartsWith("www."))
            {
                tbWebsite.Text = tbWebsite.Text.Substring(4);
            }

            bool emailMatch = Regex.IsMatch(tbRepresentativeEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            bool phoneNumberMatch = tbRepresentativePhoneNumber.Text.Length == 14;
            bool savingsMatch = Regex.IsMatch(tbSavings.Text, @"^[0-9,.]+$");

            if (!emailMatch)
            {
                tbRepresentativeEmail.BorderBrush = Brushes.Red;
                tbRepresentativeEmail.BorderThickness = new Thickness(2);
                lErrorMsg.Visibility = Visibility.Visible;
                return;
            }
            if (!string.IsNullOrEmpty(tbRepresentativePhoneNumber.Text) && !phoneNumberMatch)
            {
                tbRepresentativePhoneNumber.BorderBrush = Brushes.Red;
                tbRepresentativePhoneNumber.BorderThickness = new Thickness(2);
                lErrorMsg.Visibility = Visibility.Visible;
                return;
            }
            if (!savingsMatch)
            {
                tbSavings.BorderBrush = Brushes.Red;
                tbSavings.BorderThickness = new Thickness(2);
                lErrorMsg.Visibility = Visibility.Visible;
                return;
            }


            Partner updatePartner = new Partner()
            {
                Id = partner.Id,
                Name = tbName.Text,
                Description = tbDescription.Text,
                ResourcesAvailable = tbResources.Text,
                Industry = cbType.Text,
                StartDate = dpStartDate.DisplayDate.Date,
                RepresentativeName = tbRepresentativeName.Text,
                RepresentativeEmail = tbRepresentativeEmail.Text,
                RepresentativePhoneNumber = string.IsNullOrEmpty(tbRepresentativePhoneNumber.Text) ? "No Phone Number" : tbRepresentativePhoneNumber.Text,
                Website = string.IsNullOrEmpty(tbWebsite.Text) ? "No Website" : tbWebsite.Text,
                Address = string.IsNullOrEmpty(tbAddress.Text) ? "No Address" : tbAddress.Text,
                Savings = tbSavings.Text,
            };
            await db.UpdatePartner(partner);

            School school = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();

            foreach (Partner searchPartner in school.Partners.Value)
            {
                if (searchPartner.Id == updatePartner.Id)
                { 
                    school.Partners.Value.Remove(searchPartner);
                    break;
                }
            }

            school.Partners.Value.Add(updatePartner);

            await db.UpdateSchool(school);

            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();

            user.HomeSchool = school;
            await db.UpdateUser(user);

            lErrorMsg.Visibility = Visibility.Collapsed;

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

        private void btnBack_Clicked(object sender, RoutedEventArgs e)
        {
            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.Load_Page("MVVM/View/Pages/ViewPartners.xaml");
        }
    } // Class
} // Namespace
