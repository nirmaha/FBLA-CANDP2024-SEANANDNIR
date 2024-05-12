using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using EduPartners.Core;
using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.View.Controls
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        private Button curentMenuItem;
        private Database db;

        private static string localDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EduPartners");

        public MainControl()
        {
            InitializeComponent();

            db = App.Current.Properties["Database"] as Database;

            this.Loaded += MainControl_Loaded;
            App.Current.Properties["MainControl"] = this;
        }

        private void MainControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            // Resizes window to fit the user control
            mainWindow.WindowState = WindowState.Normal;
            this.Height = 650;
            this.Width = 1000;

            // Sets the profile image on navgation bar
            UpdateProfileImage();

            // Selects Dashboard as the default menuitem
            DashboardMenuItem.InternalMenu.IsChecked = true;
            btDashboard.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        /// <summary>
        /// This function navigates to a specified <paramref name="PpgeUri"/> and updates the corresponding
        /// buttons and menu items accordingly.
        /// </summary>
        /// <param name="pageUri">The URI of the page that needs to be loaded in the application. It is used to
        /// determine which page to navigate to within the application based on the provided URI.</param>
        public void Load_Page(string pageUri)
        {
            if (pageUri == @"MVVM/View/Pages/EditPartners.xaml")
            {
                fContainer.Navigate(new Uri(pageUri, UriKind.RelativeOrAbsolute));
                return;
            }

            Dictionary<string, Button> buttons = new Dictionary<string, Button>()
            {
                {"MVVM/View/Pages/ViewPartners.xaml",btView },
                {"MVVM/View/Pages/AddPartners.xaml",btAdd },
                {"MVVM/View/Pages/Help.xaml",btHelp },
                {"MVVM/View/Pages/Dashboard.xaml",btDashboard },
                {"MVVM/View/Pages/Profile.xaml",btProfile },

            };            
            
            Dictionary<string, MenuItem> pages = new Dictionary<string, MenuItem>()
            {
                {"MVVM/View/Pages/ViewPartners.xaml",ViewMenuItem },
                {"MVVM/View/Pages/AddPartners.xaml",AddMenuItem },
                {"MVVM/View/Pages/Help.xaml", HelpItem },
                {"MVVM/View/Pages/Dashboard.xaml",DashboardMenuItem },
                {"MVVM/View/Pages/Profile.xaml",ProfileMenuItem },
            };

            // Navigates to the page
            fContainer.Navigate(new Uri(pageUri, UriKind.RelativeOrAbsolute));

            // Updates the buttons
            pages[pageUri].InternalMenu.IsChecked = true;
            buttons[pageUri].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        /// <summary>
        /// The UpdateProfileImage function retrieves the user's profile image and displays it, using a
        /// default image if the user does not have one.
        /// </summary>
        public async void UpdateProfileImage()
        {
            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();
            try
            {
                // Checks if the profile image exists
                if (user.ProfileImage.ImageData != null && user.ProfileImage.ImageName != null)
                {
                    byte[] imageData = user.ProfileImage.ImageData.AsByteArray;
                    string imageName = user.ProfileImage.ImageName;

                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                        if (!File.Exists(Path.Combine(localDataPath, imageName)))
                        {
                            image.Save(Path.Combine(localDataPath, imageName));
                        }

                        if (user.ProfileImage == null || !File.Exists(Path.Combine(localDataPath, imageName)))
                        {
                            imgProfile.ImageSource = new BitmapImage(new Uri("/EduPartners;component/Resources/defaultProfile.png", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            imgProfile.ImageSource = new BitmapImage(new Uri($"{Path.Combine(localDataPath, imageName)}", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
                else
                {
                    imgProfile.ImageSource = new BitmapImage(new Uri("/EduPartners;component/Resources/defaultProfile.png", UriKind.RelativeOrAbsolute));
                }
            }
            catch
            {
                MessageBox.Show("There was an error while loading your profile image.", "Profile Image Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Start: MenuLeft PopupButton //

        // Dashboard Menu Button Events
        private void btDashboard_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btDashboard;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Dashboard";
            }
        }

        private void btDashboard_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btDashboard_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/Dashboard.xaml", UriKind.RelativeOrAbsolute));
        }

        // View Menu Button Events
        private void btView_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btView;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "View Partners";
            }
        }

        private void btView_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btView_Clicked(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/ViewPartners.xaml", UriKind.RelativeOrAbsolute));
            curentMenuItem = (Button)sender;
        }

        // Add Menu Button Events
        private void btAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btAdd;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Add Partner";
            }
        }

        private void btAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btAdd_Clicked(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/AddPartners.xaml", UriKind.RelativeOrAbsolute));
            curentMenuItem = (Button)sender;
        }

        // Help Menu Button Events
        private void btHelp_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btHelp;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Help";
            }
        }

        private void btHelp_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btHelp_Clicked(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/Help.xaml", UriKind.RelativeOrAbsolute));
            curentMenuItem = (Button)sender;
        }

        // Profile Menu Button Events
        private void btProfile_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            { 
                Popup.PlacementTarget = btProfile;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Profile";
            }
        }

        private void btProfile_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btProfile_Clicked(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/Profile.xaml", UriKind.RelativeOrAbsolute));
            curentMenuItem = (Button)sender;
        }

        // Logout Menu Button Events
        private void btLogOut_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btLogOut;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Logout";
            }
        }

        private void btLogOut_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btLogOut_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            // Retruns user to the home page
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                App.Current.Properties["User"] = "";

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.SetUserControl("HomePage");
                return;
            }

            if (curentMenuItem.Name == "btProfile")
            {
                // Gets the MenuItem inside of the button
                foreach (object child in ((Grid)curentMenuItem.Content).Children)
                {
                    if (child is MenuItem menuItem)
                    { 
                        menuItem.InternalMenu.IsChecked = true;
                        menuItem.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        return;
                    }
                }
            }

            // Sets user back to the page they were on
            ((MenuItem)curentMenuItem.Content).InternalMenu.IsChecked = true;
            curentMenuItem.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
        // End: MenuLeft PopupButton //

        // Start: Button Close | Restore | Minimize 
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Close();
        }

        private void btRestore_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow.WindowState == WindowState.Normal)
            {
                mainWindow.WindowState = WindowState.Maximized;
                this.Height = mainWindow.Height;
                this.Width = mainWindow.Width;
            }
            else
            {
                mainWindow.WindowState = WindowState.Normal;
                this.Height = 650;
                this.Width = 1000;
            }
        }

        private void btMinimize_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.WindowState = WindowState.Minimized;
        }
        // End: Button Close | Restore | Minimize

        private void Top_Clicked(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (e.LeftButton == MouseButtonState.Pressed) mainWindow.DragMove();
        }
    }
}
