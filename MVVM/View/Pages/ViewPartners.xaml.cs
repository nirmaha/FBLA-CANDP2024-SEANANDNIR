using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using System.Xaml;
using EduPartners.Core;
using EduPartners.MVVM.Model;
using EduPartners.MVVM.ViewModel;

namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for ViewPartners.xaml
    /// </summary>
    /// 

    public class EmailToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string emailAddress)
            {
                return new Uri("mailto:" + emailAddress);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WebsiteToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string websiteAddress && websiteAddress != "No Website")
            {
                return new Uri(@"https://www." + websiteAddress);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class ViewPartners : Page
    {
        private PartnerViewModel viewModel;
        private Database db;

        public ViewPartners()
        {
            InitializeComponent();

            db = App.Current.Properties["Database"] as Database;

            viewModel = new PartnerViewModel();

            SyncList();

            PopulateView();

            this.Loaded += ViewPartners_Loaded;

            icViewParnter.ItemsSource = viewModel.Items;
            DataContext = viewModel;
        }

        private void ViewPartners_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private async void SyncList()
        {
            // Retrieve the current school
            School school = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();

            // Retrieve all partners from the database
            List<Partner> allPartners = await db.GetPartners();

            for (int i = 0; i < school.Partners.Value.Count; i++)
            {
                bool foundParnter = false;

                for (int j = 0; j < allPartners.Count; j++)
                {
                    if (school.Partners.Value[i].Id == allPartners[j].Id)
                    { 
                        foundParnter = true;
                        break;
                    }
                }

                if (!foundParnter)
                {
                    school.Partners.Value.RemoveAt(i);
                    i--;
                }
            }

            await db.UpdateSchool(school);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private async void PopulateView()
        {
            School homeSchool = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();

            viewModel.Items.Clear();
            homeSchool.Partners.Value.ForEach(p => viewModel.Items.Add(p));
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

        private async void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel borderParent = (StackPanel)((Border)((Image)sender).Parent).Parent;

            MessageBoxResult deleteMsgBox = MessageBox.Show("Are you sure you want to delete this parnter?", "Delete Partner", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (deleteMsgBox == MessageBoxResult.No)
            {
                return;
            }

            Label partnerName = FindChild<Label>(borderParent.Parent, "lPartnerName");

            Partner partner = (await db.GetPartnerByName(partnerName.Content.ToString())).FirstOrDefault();

            await db.DeletePartner(partner);

            School school = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();

            school.Partners.Value.Remove(partner);

            await db.UpdateSchool(school);

            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();

            user.HomeSchool = school;
            await db.UpdateUser(user);

            viewModel.RemovePartner(partner);

            SyncList();

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

            FilterPartners();

            e.Handled = true;
        }

        private void MainBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            e.Handled = true;
        }

        private void FilterButton_Clicked(object sender, RoutedEventArgs e)
        {
            RadioButton button = e.Source as RadioButton;

            foreach (object stackButton in spFilterButtons.Children)
            {
                if (stackButton is RadioButton checkButton && stackButton != button)
                {
                    checkButton.IsChecked = false;
                }
            }

            FilterPartners(button);
        }

        private async void Serach_MouseDown(object sender, RoutedEventArgs e)
        {
            FilterPartners();
            e.Handled = true;
        }

        private List<string> filters = new List<string>();

        private async void FilterPartners(RadioButton radioButton = null)
        {
            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();
            List<Partner> partners = user.HomeSchool.Partners.Value;
            

            if (radioButton != null)
            { 
                switch (radioButton.Tag.ToString()) 
                {
                    case "AZ":
                        filters.Clear();
                        filters.Add(radioButton.Tag.ToString());
                        break;
                    case "ZA":
                        filters.Clear();
                        filters.Add(radioButton.Tag.ToString());
                        break;
                    case "clear":
                        filters.Clear();
                        filters.Add(radioButton.Tag.ToString());
                        break;
                    default:
                        break;

                }
            }

            if (!string.IsNullOrEmpty(tbSerachBox.Text))
            {
                partners = partners.Where(partner => partner.Name.StartsWith(tbSerachBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            }


            for (int i = 0; i < filters.Count; i++)
            {
                switch (filters[i])
                {
                    case "AZ":
                        partners = partners.OrderBy(partner => partner.Name).ToList();
                        break;
                    case "ZA":
                        partners = partners.OrderByDescending(partner => partner.Name).ToList();
                        break;
                    case "clear":
                        filters.Clear();
                        radioButton.IsChecked = false;
                        i = filters.Count;
                        break;
                    default:
                        break;

                }
            }



            viewModel.Items.Clear();
            partners.ForEach(filter => viewModel.Items.Add(filter));
        }


    }
}
