using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

using EduPartners.Core;
using EduPartners.MVVM.View.Controls;

/**
 * TODO:
 * Change ini file to JSON
 * **/

namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Dictionary<string, UserControl> userControls;
        
        public MainWindow()
        {
            InitializeComponent();

            App.Current.Properties["Database"] = new Database();

            InitializeUserControls();

            // Default to HomePage if userControl is not set
            if (!userControls.ContainsKey("HomePage"))
            {
                userControls["HomePage"] = new HomePage();
            }

            SetUserControl("HomePage");
        }

        private void InitializeUserControls()
        {
            userControls = new Dictionary<string, UserControl>
            {
                {"HomePage",        new HomePage()},
                {"MainControl",     new MainControl()},
                {"LoginControl",    new LoginControl()},
                {"SignUpControl",   new SignUpControl()},
                {"CreateSchool",    new CreateSchool()},
                {"SchoolSelection", new SchoolSelection()},
            };
        }

        public void SetUserControl(string userControl)
        {
            Main.Children.Clear();
            Main.Children.Add(userControls[userControl]);

            this.Title = "EduPartners - " + userControls[userControl].Name;
            if (userControls[userControl].Name == "Dashboard")
            {
                this.WindowStyle = WindowStyle.None;
                this.Height = userControls[userControl].Height;
                this.Width = userControls[userControl].Width;
            }
            else
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.Height = userControls[userControl].Height + 38;
                this.Width = userControls[userControl].Width;
            }
        }
    }

}
