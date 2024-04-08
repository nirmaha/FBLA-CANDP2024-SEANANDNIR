using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
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

        private async void MainControl_Loaded(object sender, RoutedEventArgs e)
        {
            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();

         /*   if (!File.Exists(Path.Combine(localDataPath, user.ProfileImage)))
            {
                ProfileMenuItem.ImageSource = null;
            }
            else
            {
                ProfileMenuItem.ImageSource = new BitmapImage(new Uri(Path.Combine(localDataPath, user.ProfileImage), UriKind.RelativeOrAbsolute)) ?? new BitmapImage(new Uri(null, UriKind.RelativeOrAbsolute));
            }*/

            DashboardMenuItem.InternalMenu.IsChecked = true;
            btnDashboard.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        public void Load_Page(string PageUri)
        {
            if (PageUri == @"MVVM/View/Pages/EditPartners.xaml")
            {
                fContainer.Navigate(new Uri(PageUri, UriKind.RelativeOrAbsolute));
                return;
            }

            Dictionary<string, Button> buttons = new Dictionary<string, Button>()
            {
                {"MVVM/View/Pages/ViewPartners.xaml",btnView },
                {"MVVM/View/Pages/AddPartners.xaml",btnAdd },
                {"MVVM/View/Pages/Help.xaml",btnHelp },
                {"MVVM/View/Pages/Dashboard.xaml",btnDashboard },
                {"MVVM/View/Pages/Profile.xaml",btnProfile },

            };            
            
            Dictionary<string, MenuItem> pages = new Dictionary<string, MenuItem>()
            {
                {"MVVM/View/Pages/ViewPartners.xaml",ViewMenuItem },
                {"MVVM/View/Pages/AddPartners.xaml",AddMenuItem },
                {"MVVM/View/Pages/Help.xaml", HelpItem },
                {"MVVM/View/Pages/Dashboard.xaml",DashboardMenuItem },
                {"MVVM/View/Pages/Profile.xaml",ProfileMenuItem },
            };

            fContainer.Navigate(new Uri(PageUri, UriKind.RelativeOrAbsolute));
            pages[PageUri].InternalMenu.IsChecked = true;
            buttons[PageUri].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        // Start: MenuLeft PopupButton //
        private void btnDashboard_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnDashboard;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Dashboard";
            }
        }

        private void btnDashboard_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnView_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnView;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "View Partners";
            }
        }

        private void btnView_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnView_Clicked(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/ViewPartners.xaml", UriKind.RelativeOrAbsolute));
            curentMenuItem = (Button)sender;
        }

        private void btnAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnAdd;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Add Partner";
            }
        }

        private void btnAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnAdd_Clicked(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/AddPartners.xaml", UriKind.RelativeOrAbsolute));
            curentMenuItem = (Button)sender;
        }

        private void btnHelp_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnHelp;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Help";
            }
        }

        private void btnHelp_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnHelp_Clicked(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/Help.xaml", UriKind.RelativeOrAbsolute));
            curentMenuItem = (Button)sender;
        }

        private void btnProfile_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            { 
                Popup.PlacementTarget = btnProfile;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Profile";
            }
        }

        private void btnProfile_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnProfile_Clicked(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/Profile.xaml", UriKind.RelativeOrAbsolute));
            curentMenuItem = (Button)sender;
        }

        private void btnLogOut_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnLogOut;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Logout";
            }
        }

        private void btnLogOut_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        // End: MenuLeft PopupButton //

        // Start: Button Close | Restore | Minimize 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.WindowState = WindowState.Minimized;
        }
        // End: Button Close | Restore | Minimize

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/Home.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new Uri("MVVM/View/Pages/Dashboard.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Top_Clicked(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (e.LeftButton == MouseButtonState.Pressed) mainWindow.DragMove();
        }

        private void btnLogOut_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                App.Current.Properties["User"] = "";
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.SetUserControl("HomePage");
                return;
            }

            ((MenuItem)curentMenuItem.Content).InternalMenu.IsChecked = true;
            curentMenuItem.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
    }
}
