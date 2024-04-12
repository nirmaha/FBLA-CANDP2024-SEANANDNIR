using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using EduPartners.Core;
using EduPartners.MVVM.Model;
using Microsoft.Win32;

namespace EduPartners.MVVM.View.Controls
{
    /// <summary>
    /// Interaction logic for CreateScool.xaml
    /// </summary>
    public partial class CreateSchool : UserControl
    {
        private Database db;

        public CreateSchool()
        {
            InitializeComponent();
           
            tbSchoolCode.Text = Guid.NewGuid().ToString();
            db = App.Current.Properties["Database"] as Database;

            this.Loaded += CreateSchool_Loaded;
        }

        private void CreateSchool_Loaded(object sender, RoutedEventArgs e)
        {
            lErrorMessage.Visibility = Visibility.Collapsed;
        }

        private void btnBack_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("HomePage");
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
            if (tbSchoolName.Text != "")
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
            // Returns every textbox to normal color when border is clicked
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

        private async void btnCreateSchool_Clicked(object sender, RoutedEventArgs e)
        {
            // Checks all fields for emptiness
            if (tbSchoolName.Text == "" || tbAddress.Text == "" || tbCity.Text == "" || tbState.Text == "" || tbZip.Text == "")
            {
                lErrorMessage.Visibility = Visibility.Visible;
                lErrorMessage.Content = "Please fill out all require felids.";
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog() 
            {
                Title = "Save School Code",
                InitialDirectory = @"C:\Downloads",
                Filter = "Text File (*.txt) | *.txt",
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, tbSchoolCode.Text);
            }
            else
            {
                return;
            }

            // Creates a new school
            School school = new School
            {
                Name = tbSchoolName.Text,
                Address = tbAddress.Text,
                City = tbCity.Text,
                State = tbState.Text,
                Zip = tbZip.Text,
                Code = tbSchoolCode.Text,
                Partners = new List<Partner>()
            };
            await db.CreateSchool(school);

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("HomePage");
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
            if (tbZip.Text != "")
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
