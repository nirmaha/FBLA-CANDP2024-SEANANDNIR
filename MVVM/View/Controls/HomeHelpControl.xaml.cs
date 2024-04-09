using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
