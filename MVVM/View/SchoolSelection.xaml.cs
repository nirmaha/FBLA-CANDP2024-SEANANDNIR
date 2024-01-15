using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Interaction logic for SchoolSelection.xaml
    /// </summary>
    public partial class SchoolSelection : Window
    {
        public SchoolSelection()
        {
            InitializeComponent();
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

        private void btnCreateAccount_Clicked(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = null;
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
            loginWindow.Show();
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
    }
}
