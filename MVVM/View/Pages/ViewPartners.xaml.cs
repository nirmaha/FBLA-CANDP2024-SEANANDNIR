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

using EduPartners.Core;
using EduPartners.MVVM.Model;
using EduPartners.MVVM.ViewModel;

namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for ViewPartners.xaml
    /// </summary>
    public partial class ViewPartners : Page
    {
        private PartnerViewModel viewModel;
        private Database db; 

        public ViewPartners()
        {
            InitializeComponent();

            db = App.Current.Properties["Database"] as Database;

            viewModel = new PartnerViewModel();

            PopulateView();

            DataContext = viewModel;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private async void PopulateView()
        {
            List<Partner> parnters = await db.GetPartners();
            parnters.ForEach(p => viewModel.Items.Add(p));
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Assuming sender is the Border clicked
            Border border = (Border)sender;

            // Find the named Grid within the Border
            Grid gMoreInfo = FindChild<Grid>(border, "gMoreInfo");

            // Now you have access to the gMoreInfo Grid
            if (gMoreInfo != null)
            {
                // Do something with gMoreInfo
                if (gMoreInfo.Visibility == Visibility.Collapsed)
                {
                    gMoreInfo.Visibility = Visibility.Visible;
                }
                else
                {
                    gMoreInfo.Visibility = Visibility.Collapsed;
                }
            }
        }

        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild && ((FrameworkElement)child).Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T result = FindChild<T>(child, childName);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }


        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void lSearchLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbSerachBox.Focus();
            e.Handled = true;
        }

        private void tbSerachBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSerachBox.Text != "")
            {
                lSearchLabel.Visibility = Visibility.Collapsed;
            }
            else
            { 
                lSearchLabel.Visibility = Visibility.Visible;
            }
            e.Handled = true;
        }

        private void MainBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            e.Handled = true;
        }

        private void Serach_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void FilterButton_Clicked(object sender, RoutedEventArgs e)
        {
            RadioButton button = e.Source as RadioButton;
            Debug.WriteLine(button.Tag);

            foreach (object stackButton in spFilterButtons.Children)
            {
                if (stackButton is RadioButton checkButton && stackButton != button)
                {
                    checkButton.IsChecked = false;
                }
            }
        }
    }
}
