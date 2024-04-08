using System;
using System.IO;
using System.Collections.Generic;
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

using BCrypts = BCrypt.Net.BCrypt;

using EduPartners.Core;
using EduPartners.MVVM.Model;
using Microsoft.Win32;

namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private Database db;
        private User currentUser;
        private IniFile iniFile;

        private static string localDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EduPartners");
        private string filePath = Path.Combine(localDataPath, "EduPartners.ini");

        private bool isUpdating = false;

        public Profile()
        {
            InitializeComponent();
            db = App.Current.Properties["Database"] as Database;
            iniFile = new IniFile(filePath, localDataPath);

            this.Loaded += async (sender, e) => 
            {
                currentUser = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();
                imgProfile.Source = new BitmapImage(new Uri($"{currentUser.ProfileImage}", UriKind.RelativeOrAbsolute));

                DataContext = currentUser;
            };
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            isUpdating = true;
            
            btUpdate.Visibility = Visibility.Collapsed;
            spChangePwrd.Visibility = Visibility.Visible;
            btCancel.Visibility = Visibility.Visible;
            btSave.Visibility = Visibility.Visible;

            tbAbout.IsReadOnly = false;
            tbAbout.BorderThickness = new Thickness(1);
            tbAbout.HorizontalContentAlignment = HorizontalAlignment.Center;

            tbEmail.IsReadOnly = false;
            tbName.IsReadOnly = false;
            tbPhoneNumber.IsReadOnly = false;

            overlayEllipse.Opacity = 0.5;
            imgEdit.Visibility = Visibility.Visible;
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelBase();
        }

        private async void CancelBase()
        {
            isUpdating = false;

            currentUser = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();
            DataContext = currentUser;

            btUpdate.Visibility = Visibility.Visible;
            spChangePwrd.Visibility = Visibility.Collapsed;
            btCancel.Visibility = Visibility.Collapsed;
            btSave.Visibility = Visibility.Collapsed;

            tbAbout.IsReadOnly = true;
            tbAbout.BorderThickness = new Thickness(0);
            tbAbout.HorizontalContentAlignment = HorizontalAlignment.Center;

            lErrorMsg.Visibility = Visibility.Collapsed;

            tbEmail.IsReadOnly = true;
            tbEmail.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            tbName.IsReadOnly = true;
            tbName.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            tbPhoneNumber.IsReadOnly = true;

            tbCurrentPwrd.Clear();
            tbCurrentPwrd.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            tbConfirmPwrd.Clear();
            tbConfirmPwrd.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            tbChangedPwrd.Clear();
            tbChangedPwrd.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            overlayEllipse.Opacity = 0;
            imgEdit.Visibility = Visibility.Hidden;

        }

        private async void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                tbEmail.BorderBrush = Brushes.Red;
                lErrorMsg.Visibility = Visibility.Visible;
                return;
            }

            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                tbEmail.BorderBrush = Brushes.Red;
                lErrorMsg.Visibility = Visibility.Visible;
                return;
            }

            bool emailMatch = Regex.IsMatch(tbEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            bool phoneNumberMatch = tbPhoneNumber.Text.Length == 14;

            if (!emailMatch)
            {
                tbEmail.BorderBrush = Brushes.Red;
                lErrorMsg.Visibility = Visibility.Visible;
                return;
            }
            if (!string.IsNullOrEmpty(tbPhoneNumber.Text) && !phoneNumberMatch)
            {
                tbPhoneNumber.BorderBrush = Brushes.Red;
                lErrorMsg.Visibility = Visibility.Visible;
                return;
            }

            if (!string.IsNullOrEmpty(tbCurrentPwrd.Text) || !string.IsNullOrEmpty(tbChangedPwrd.Text) || !string.IsNullOrEmpty(tbConfirmPwrd.Text))
            {
                if (!BCrypts.Verify(tbCurrentPwrd.Text, currentUser.Password))
                {
                    tbCurrentPwrd.BorderBrush = Brushes.Red; 
                    lErrorMsg.Visibility = Visibility.Visible;
                    return;
                }

                if (tbChangedPwrd.Text != tbConfirmPwrd.Text)
                {
                    tbConfirmPwrd.BorderBrush = Brushes.Red;
                    lErrorMsg.Visibility = Visibility.Visible;
                    return;
                }

                if (iniFile.GetValue("SECURITY", "PASSWORDLOGIN") != "")
                {
                    iniFile.SetValue("SECURITY", "PASSWORDLOGIN", "null");
                    iniFile.Save();
                }
            }
      
            User user = new User()
            {
                Id = currentUser.Id,
                Name = tbName.Text,
                Email = tbEmail.Text,
                About = tbAbout.Text,
                PhoneNumber = tbPhoneNumber.Text,
                Password = string.IsNullOrEmpty(tbChangedPwrd.Text) ? currentUser.Password : BCrypts.HashPassword(tbChangedPwrd.Text),
                ProfileImage = currentUser.ProfileImage,
                HomeSchool = currentUser.HomeSchool
            };

            await db.UpdateUser(user);

            CancelBase();
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

        private async void ProfileCircle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isUpdating)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "Images (*.png, *.jpg, *gif, *.bmp) | *.png;*.jpg;*.gif;*.bmp",
                    InitialDirectory = "C:\\",
                    CheckFileExists = true,
                    CheckPathExists = true
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    if (!File.Exists(Path.Combine(localDataPath, Path.GetFileName(openFileDialog.FileName))))
                    {
                        File.Copy(openFileDialog.FileName, Path.Combine(localDataPath, Path.GetFileName(openFileDialog.FileName)));
                    }

                    currentUser.ProfileImage = Path.Combine(localDataPath, Path.GetFileName(openFileDialog.FileName));
                    imgProfile.Source = new BitmapImage(new Uri($"{Path.Combine(localDataPath, Path.GetFileName(openFileDialog.FileName))}", UriKind.RelativeOrAbsolute));

                    await db.UpdateUser(currentUser);
                }
            }
        }
    }
}
