using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static UserControl userControl { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Default to HomePage if userControl is not set
            if (userControl == null)
            {
                userControl = new HomePage();
            }
            App.Current.Properties["Database"] = new Database();

            UpdateUIForCurrentUserControl();
        }

        private void UpdateUIForCurrentUserControl()
        {
            Main.Children.Clear();
            Main.Children.Add(userControl);

            this.Title = "EduPartners - " + userControl.Name;
            if (userControl.Name == "Dashboard")
            {
                this.WindowStyle = WindowStyle.None;
                this.Height = userControl.Height;
                this.Width = userControl.Width;
            }
            else
            { 
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.Height = userControl.Height + 38;
                this.Width = userControl.Width;
            }
            
        }

        public void SetUserControl(UserControl newControl)
        {
            userControl = newControl;
            UpdateUIForCurrentUserControl();
        }
    }

}
