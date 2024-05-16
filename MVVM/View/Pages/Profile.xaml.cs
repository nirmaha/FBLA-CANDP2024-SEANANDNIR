using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

using BCrypts = BCrypt.Net.BCrypt;

using EduPartners.Core;
using EduPartners.MVVM.Model;
using EduPartners.MVVM.View.Controls;
using System.Windows.Documents.Serialization;
using System.Drawing.Printing;
using System.Printing;
using System.Drawing.Imaging;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;


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

        private string newProfileName = "";

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
                lSchoolAddress.Content = $"{currentUser.HomeSchool.Address}, {currentUser.HomeSchool.City}, {currentUser.HomeSchool.State} {currentUser.HomeSchool.Zip}";

                try
                {
                    // Checks if the profile image exists
                    if (currentUser.ProfileImage.ImageData != null && currentUser.ProfileImage.ImageName != null)
                    {
                        byte[] imageData = currentUser.ProfileImage.ImageData.AsByteArray;
                        string imageName = currentUser.ProfileImage.ImageName;

                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                            if (!File.Exists(Path.Combine(localDataPath, imageName)))
                            {
                                image.Save(Path.Combine(localDataPath, imageName));
                            }

                            if (currentUser.ProfileImage == null || !File.Exists(Path.Combine(localDataPath, imageName)))
                            {
                                imgProfile.Source = new BitmapImage(new Uri("/EduPartners;component/Resources/defaultProfile.png", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                imgProfile.Source = new BitmapImage(new Uri($"{Path.Combine(localDataPath, imageName)}", UriKind.RelativeOrAbsolute));
                            }
                        }
                    }
                    else
                    {
                        imgProfile.Source = new BitmapImage(new Uri("/EduPartners;component/Resources/defaultProfile.png", UriKind.RelativeOrAbsolute));
                    }
                }
                catch
                {
                    MessageBox.Show("There was an error while loading your profile image.", "Profile Image Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                DataContext = currentUser;
            };
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            isUpdating = true;
            
            // Changes which buttons are visible
            btUpdate.Visibility = Visibility.Collapsed;
            spChangePwrd.Visibility = Visibility.Visible;
            btCancel.Visibility = Visibility.Visible;
            btSave.Visibility = Visibility.Visible;
            
            // Make textboxes editable 
            tbAbout.IsReadOnly = false;
            tbAbout.BorderThickness = new Thickness(1);
            tbAbout.HorizontalContentAlignment = HorizontalAlignment.Left;

            tbEmail.IsReadOnly = false;
            tbName.IsReadOnly = false;
            tbPhoneNumber.IsReadOnly = false;

            // Show Profile edit elements
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

            // Resets data to before it was changed or sets new data if user was updated
            currentUser = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();
            DataContext = currentUser;

            // Changes the button back to only show the Update button
            btUpdate.Visibility = Visibility.Visible;
            btCancel.Visibility = Visibility.Collapsed;
            btSave.Visibility = Visibility.Collapsed;

            // Hide change password fields
            spChangePwrd.Visibility = Visibility.Collapsed;

            // Turn the textboxes back to readonly
            tbAbout.IsReadOnly = true;
            tbAbout.BorderThickness = new Thickness(0);
            tbAbout.HorizontalContentAlignment = HorizontalAlignment.Center;

            lErrorMsg.Visibility = Visibility.Collapsed;

            tbEmail.IsReadOnly = true;
            tbEmail.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            tbName.IsReadOnly = true;
            tbName.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            tbPhoneNumber.IsReadOnly = true;

            // Clear change password boxes
            tbCurrentPwrd.Clear();
            tbCurrentPwrd.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            tbConfirmPwrd.Clear();
            tbConfirmPwrd.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            tbChangedPwrd.Clear();
            tbChangedPwrd.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            // Hide profile edit elements
            overlayEllipse.Opacity = 0;
            imgEdit.Visibility = Visibility.Hidden;

        }

        private async void btSave_Click(object sender, RoutedEventArgs e)
        {
            // Checks if required fields are empty
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

            // Checks if email is valid
            if (!emailMatch)
            {
                tbEmail.BorderBrush = Brushes.Red;
                lErrorMsg.Visibility = Visibility.Visible;
                return;
            }

            // Checks if phonenumber is valid
            if (!string.IsNullOrEmpty(tbPhoneNumber.Text) && !phoneNumberMatch)
            {
                tbPhoneNumber.BorderBrush = Brushes.Red;
                lErrorMsg.Visibility = Visibility.Visible;
                return;
            }

            // Checks if the password fields have been filled out
            if (!string.IsNullOrEmpty(tbCurrentPwrd.Text) || !string.IsNullOrEmpty(tbChangedPwrd.Text) || !string.IsNullOrEmpty(tbConfirmPwrd.Text))
            {
                // Checks if current password was not the correct password
                if (!BCrypts.Verify(tbCurrentPwrd.Text, currentUser.Password))
                {
                    tbCurrentPwrd.BorderBrush = Brushes.Red; 
                    lErrorMsg.Visibility = Visibility.Visible;
                    return;
                }

                // Checks in the changed and confirm password are not the same
                if (tbChangedPwrd.Text != tbConfirmPwrd.Text)
                {
                    tbConfirmPwrd.BorderBrush = Brushes.Red;
                    lErrorMsg.Visibility = Visibility.Visible;
                    return;
                }

                // Clears the password cookie
                if (iniFile.GetValue("SECURITY", "PASSWORDLOGIN") != "")
                {
                    iniFile.SetValue("SECURITY", "PASSWORDLOGIN", "");
                    iniFile.Save();
                }
            }

            // Updates user
            User user = new User()
            {
                Id = currentUser.Id,
                Name = tbName.Text,
                Email = tbEmail.Text,
                About = tbAbout.Text,
                PhoneNumber = tbPhoneNumber.Text,
                Password = string.IsNullOrEmpty(tbChangedPwrd.Text) ? currentUser.Password : BCrypts.HashPassword(tbChangedPwrd.Text),
                ProfileImage = new ProfileImageInfo
                {  
                    ImageData = File.ReadAllBytes(newProfileName),
                    ImageName = Path.GetFileName(newProfileName)
                },
                HomeSchool = currentUser.HomeSchool
            };

            await db.UpdateUser(user);

            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.UpdateProfileImage();

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

        private void ProfileCircle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isUpdating)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Title = "Selete a Profile Image",
                    Filter = "Images (*.png, *.jpg, *gif, *.bmp) | *.png;*.jpg;*.gif;*.bmp",
                    InitialDirectory = "C:\\",
                    CheckFileExists = true,
                    CheckPathExists = true
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    // Update the profile image visually
                    newProfileName = openFileDialog.FileName;
                    imgProfile.Source = new BitmapImage(new Uri($"{openFileDialog.FileName}", UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
}
