using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

using EduPartners.Core;
using EduPartners.MVVM.View.Controls;


namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Interaction Logic for MainWindow.xaml
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

        /// <summary>
        /// This function pre-loads all user controls inside of a Dictionary.
        /// </summary>
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
                {"HomeHelpControl", new HomeHelpControl()},
            };
        }

        /// <summary>
        /// This function sets dock panel to the specified <paramref name="userControl"/>.
        /// </summary>
        /// <param name="userControl">The name of the user control that is going to be switched to.</param>
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
                // Sets the window dimensions to fir the user control
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.Height = userControls[userControl].Height + 38;
                this.Width = userControls[userControl].Width;
            }
        }
    }

}
