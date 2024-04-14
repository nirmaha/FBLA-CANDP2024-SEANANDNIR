using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

using EduPartners.MVVM.View.Controls;


namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Help : Page
    {
        public Help()
        {
            InitializeComponent();
        }

        private void lDashboard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.Load_Page("MVVM/View/Pages/Dashboard.xaml");
        }

        private void lViewPartners_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.Load_Page("MVVM/View/Pages/ViewPartners.xaml");
        }

        private void lAddPartners_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.Load_Page("MVVM/View/Pages/AddPartners.xaml");
        }

        private void lEditPartners_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.Load_Page("MVVM/View/Pages/ViewPartners.xaml");
        }

        private void lProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.Load_Page("MVVM/View/Pages/Profile.xaml");
        }
    }
}
