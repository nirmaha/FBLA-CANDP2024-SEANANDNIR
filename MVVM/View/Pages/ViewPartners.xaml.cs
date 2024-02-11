using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using EduPartners.MVVM.ViewModel;

namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for ViewPartners.xaml
    /// </summary>
    public partial class ViewPartners : Page
    {
        private PartnerViewModel viewModel;

        public ViewPartners()
        {
            InitializeComponent();

            viewModel = new PartnerViewModel();

            DataContext = viewModel;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (object item in icViewParnter.Items)
            {
                FrameworkElement container = icViewParnter.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                Grid grid = container.FindName("gMoreInfo") as Grid;

                if (grid.Visibility == Visibility.Collapsed)
                {
                    grid.Visibility = Visibility.Visible;
                }
                else 
                {
                    grid.Visibility = Visibility.Collapsed;
                }
            }
            e.Handled = true;
        }

        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
