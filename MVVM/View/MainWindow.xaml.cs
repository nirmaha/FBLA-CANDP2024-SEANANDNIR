using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private static UserControl userControl;

        public MainWindow()
        {
            InitializeComponent();

            // Default to HomePage if userControl is not set
            if (userControl == null)
            {
                userControl = new HomePage();
            }

            UpdateUIForCurrentUserControl();
        }

        private void UpdateUIForCurrentUserControl()
        {
            Main.Children.Clear();
            Main.Children.Add(userControl);

            this.Title = userControl.Name;
            if (this.Title == "Dashboard")
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
