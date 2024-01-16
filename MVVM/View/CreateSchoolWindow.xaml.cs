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
using EduPartners.Core;
using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateSchoolWindow.xaml
    /// </summary>
    public partial class CreateSchoolWindow : Window
    {
        private Database db;
        public CreateSchoolWindow()
        {
            InitializeComponent();
            tbSchoolCode.Text = Guid.NewGuid().ToString();
            db = new Database();
        }

        private void btnBack_Clicked(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Owner = null;
            this.Close();
            homePage.Show();
        }

        private void tbSchoolName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSchoolName.Background = Brushes.White;
        }

        private void tbSchoolName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSchoolName.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void tbSchoolName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbSchoolName.Text != "")
            {
                lSchoolName.Visibility = Visibility.Collapsed;
            }
            else
            {
                lSchoolName.Visibility = Visibility.Visible;
            }
        }

        private void CreateSchoolBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.Source is TextBox || e.Source is PasswordBox))
            {
                Keyboard.ClearFocus();
                tbSchoolName.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                tbAddress.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                tbState.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                tbCity.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
                tbZip.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
            }
        }

        private void lSchoolName_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbSchoolName.Focus();
        }

        private void tbAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            tbAddress.Background = Brushes.White;
        }

        private void tbAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            tbAddress.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void tbAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbAddress.Text != "")
            {
                lAddress.Visibility = Visibility.Collapsed;
            }
            else
            {
                lAddress.Visibility = Visibility.Visible;
            }
        }

        private void lAddress_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbAddress.Focus();
        }

        private void tbCity_GotFocus(object sender, RoutedEventArgs e)
        {
            tbCity.Background = Brushes.White;
        }

        private void tbCity_LostFocus(object sender, RoutedEventArgs e)
        {
            tbCity.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void tbCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbCity.Text != "")
            {
                lCity.Visibility = Visibility.Collapsed;
            }
            else
            {
                lCity.Visibility = Visibility.Visible;
            }
        }

        private void lCity_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbCity.Focus();
        }

        private void tbState_GotFocus(object sender, RoutedEventArgs e)
        {
            tbState.Background = Brushes.White;
        }

        private void tbState_LostFocus(object sender, RoutedEventArgs e)
        {
            tbState.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void tbState_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbState.Text != "")
            {
                lState.Visibility = Visibility.Collapsed;
            }
            else
            {
                lState.Visibility = Visibility.Visible;
            }
        }

        private void lState_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbState.Focus();
        }

        private void btnCreateSchool_Clicked(object sender, RoutedEventArgs e)
        {
            School school = new School
            {
                Name = tbSchoolName.Text,
                Address = tbAddress.Text,
                City = tbCity.Text,
                State = tbState.Text,
                Zip = tbZip.Text,
                Code = tbSchoolCode.Text
            };
            db.CreateSchool(school);

            HomePage homePage = new HomePage();
            homePage.Owner = null;
            this.Close();
            homePage.Show();
        }

        private void tbZip_GotFocus(object sender, RoutedEventArgs e)
        {
            tbZip.Background = Brushes.White;
        }

        private void tbZip_LostFocus(object sender, RoutedEventArgs e)
        {
            tbZip.Background = new SolidColorBrush(Color.FromRgb(237, 237, 237));
        }

        private void tbZip_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbZip.Text != "")
            {
                lZip.Visibility = Visibility.Collapsed;
            }
            else
            {
                lZip.Visibility = Visibility.Visible;
            }
        }

        private void lZip_Clicked(object sender, MouseButtonEventArgs e)
        {
            tbZip.Focus();
        }

        private void lCopyCliboard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(tbSchoolCode.Text);
            MessageBox.Show("Successfully copied to clipboard!");
        }

        private void lCodeRefresh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbSchoolCode.Text = Guid.NewGuid().ToString();
        }
    }
}
