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

namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for ViewPartners.xaml
    /// </summary>
    public partial class ViewPartners : Page
    {
        public ViewPartners()
        {
            InitializeComponent();
        }

        private void btnEdit_MouseEnter(object sender, MouseEventArgs e)
        {
            //miEdit.IndicatorBrush = Brushes.White;
        }

        private void btnEdit_MouseLeave(object sender, MouseEventArgs e)
        {
            //miEdit.IndicatorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3772FF"));
        }

        private void btnEdit_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
