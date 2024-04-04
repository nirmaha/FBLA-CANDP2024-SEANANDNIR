using EduPartners.Core;
using EduPartners.MVVM.Model;
using EduPartners.MVVM.View.Controls;
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


            List<School> schools = await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString());
            School school = schools.FirstOrDefault();
            
            school.Partners.Value.Add(partner);

            await db.UpdateSchool(school);

            List<User> users = await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString());
            User user = users.FirstOrDefault();

            user.HomeSchool = school;
            await db.UpdateUser(user);
            
            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.Load_Page("MVVM/View/Pages/ViewPartners.xaml");
        }
    }
}
