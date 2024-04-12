using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace EduPartners.MVVM.View.Controls
{
    /// <summary>
    /// Interaction logic for HomeHelpControl.xaml
    /// </summary>
    public partial class HomeHelpControl : UserControl
    {
        public HomeHelpControl()
        {
            InitializeComponent();
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetUserControl("HomePage");
        }
    }
}
