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
using System.Windows.Shapes;

namespace EduPartners.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateSchoolWindow.xaml
    /// </summary>
    public partial class CreateSchoolWindow : Window
    {
        public CreateSchoolWindow()
        {
            InitializeComponent();
        }

        private void btnBack_Clicked(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Owner = null;
            this.Close();
            homePage.Show();
        }

        private void tbSchoolName_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tbSchoolName_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tbSchoolName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
