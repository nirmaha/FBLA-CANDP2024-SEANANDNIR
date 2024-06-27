using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;

using EduPartners.Core;
using EduPartners.MVVM.Model;
using EduPartners.MVVM.View.Controls;
using EduPartners.MVVM.ViewModel;

# pragma warning disable CA1416 // Validate platform compatibility

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
        private readonly PartnerViewModel viewModel;
        private readonly Database db;

        private readonly string localDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EduPartners");

        public ViewPartners()
        {
            InitializeComponent();

            db = App.Current.Properties["Database"] as Database;

            viewModel = new PartnerViewModel();

            PopulateView();

            icViewParnter.ItemsSource = viewModel.Items;
            DataContext = viewModel;

            if (App.Current.Properties["PreFilteredIndustry"] != null)
            {
                PreFilter_Industry();
                App.Current.Properties["PreFilteredIndustry"] = null;
            }
        }

        /// <summary>
        /// Asynchronously synchronizes the list of partners for the current school.
        /// </summary>
        private async void SyncList()
        {
            // Retrieve the current school using the current school ID
            School school = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();

            // Retrieve all partners from the database
            List<Partner> allPartners = await db.GetPartners();

            // Create a HashSet of partner IDs for efficient lookup
            HashSet<string> partnerIds = new(allPartners.Select(p => p.Id));

            // Filter out partners that do not exist in the partner collection
            school.Partners.Value.RemoveAll(partner => !partnerIds.Contains(partner.Id));

            // Update the school in the database
            await db.UpdateSchool(school);
        }


        /// <summary>
        /// This function asynchronously populates the view with partners from the home school.
        /// </summary>
        private async void PopulateView()
        {
            // Asynchronously run the following code
            await Task.Run(() =>
            {
                // Sync the list of partners
                SyncList();

                // Retrieve the current school using the school ID stored in the application properties
                School homeSchool = (db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString()).Result).FirstOrDefault();

                // Update the UI on the main thread
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Clear the existing items in the ViewModel
                    viewModel.Items.Clear();

                    // Add each partner from the home school to the ViewModel
                    homeSchool.Partners.Value.ForEach(p => viewModel.Items.Add(p));
                });
            });
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
                gMoreInfo.Visibility = (gMoreInfo.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Recursively finds a child element of type T by name in the visual tree starting from the given parent element.
        /// </summary>
        /// <typeparam name="T">The type of the child element to search for.</typeparam>
        /// <param name="parent">The parent element to start the search from.</param>
        /// <param name="childName">The name of the child element to find.</param>
        /// <returns>The child element of type T with the specified name if found, otherwise null.</returns>
        private static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // If the parent is null, return null as there are no children to search
            if (parent == null)
                return null;

            // Get the total number of children elements in the parent
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            // Loop through each child in the parent
            for (int i = 0; i < childrenCount; i++)
            {
                // Get the current child element
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                // Check if the child is of the specified type T and has the matching name
                if (child is T typedChild && ((FrameworkElement)child).Name == childName)
                {
                    // If the child matches the type and name, return the element
                    return typedChild;
                }

                // Recursively call FindChild on the current child to search deeper in the visual tree
                T result = FindChild<T>(child, childName);

                // If a matching child element is found in the recursive call, return the result
                if (result != null)
                    return result;
            }

            // If no matching child element is found, return null
            return null;
        }


        private async void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel borderParent = (StackPanel)((Border)sender).Parent;
            Label partnerId = FindChild<Label>(borderParent.Parent, "lPartnerId");

            Partner partner = (await db.GetPartnerById(partnerId.Content.ToString())).FirstOrDefault();

            App.Current.Properties["SelectedPartner"] = partner;

            MainControl mainControl = App.Current.Properties["MainControl"] as MainControl;
            mainControl.Load_Page("MVVM/View/Pages/EditPartners.xaml");

            e.Handled = true;
        }

        private async void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel borderParent = (StackPanel)((Border)sender).Parent;

            MessageBoxResult deleteMsgBox = MessageBox.Show("Are you sure you want to delete this partner?", "Delete Partner", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (deleteMsgBox == MessageBoxResult.No)
            {
                return;
            }


            // Finds the label with the ID in it
            Label partnerId = FindChild<Label>(borderParent.Parent, "lPartnerId");

            // Deletes the partner
            Partner partner = (await db.GetPartnerById(partnerId.Content.ToString())).FirstOrDefault();

            await db.DeletePartner(partner);

            // Removes the partner for the school list
            School school = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();

            school.Partners.Value.RemoveAll(p => p.Id == partner.Id);

            await db.UpdateSchool(school);

            // Sync the school list with the user school list
            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();

            user.HomeSchool = school;
            await db.UpdateUser(user);

            // Remove from the view model
            viewModel.RemovePartner(partner);

            SyncList();

            e.Handled = true;
        }

        private void lSearchLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbSerachBox.Focus();
            e.Handled = true;
        }

        private void tbSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lSearchLabel.Visibility = string.IsNullOrEmpty(tbSerachBox.Text) ? Visibility.Visible : Visibility.Collapsed;

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

            // Makes sure only one button is clicked at a time
            foreach (RadioButton child in spFilterButtons.Children.OfType<RadioButton>())
            {
                if (child != button)
                {
                    child.IsChecked = false;
                }
            }

            FilterPartners(button);
        }

        private void Search_MouseDown(object sender, RoutedEventArgs e)
        {
            FilterPartners();
            e.Handled = true;
        }

        private readonly List<string> filters = [];

        /// <summary>
        /// Filters the list of partners based on user input and applies filters to the ViewModel.
        /// </summary>
        /// <param name="radioButton">The radio button selected as a filter, can be null.</param>
        private async void FilterPartners(RadioButton radioButton = null)
        {
            // Get the current user
            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();
            List<Partner> partners = user.HomeSchool.Partners.Value;

            // Check if a radio button is selected and update the filter list
            if (radioButton != null)
            {
                filters.Clear();
                filters.Add(radioButton.Tag.ToString());
            }

            // Filter partners based on the search box input
            if (!string.IsNullOrEmpty(tbSerachBox.Text))
            {
                ComboBoxItem searchFilter = (ComboBoxItem)cbSearchFilter.SelectedItem;

                string searchText = tbSerachBox.Text;

                partners = partners.Where(partner =>
                    (searchFilter.Tag.ToString() == "savings" && partner.Savings.ToString().StartsWith(searchText, StringComparison.OrdinalIgnoreCase)) ||
                    (searchFilter.Tag.ToString() == "industry" && partner.Industry.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)) ||
                    (searchFilter.Tag.ToString() == "address" && partner.Address.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)) ||
                    (searchFilter.Tag.ToString() != "savings" && searchFilter.Tag.ToString() != "industry" && searchFilter.Tag.ToString() != "address" && partner.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
                ).ToList();

            }

            // Apply filters to the partners list
            for (int i = 0; i < filters.Count; i++)
            {
                string filter = filters[i];

                switch (filter)
                {
                    case "AZ":
                        partners = partners.OrderBy(partner => partner.Name).ToList();
                        break;
                    case "ZA":
                        partners = partners.OrderByDescending(partner => partner.Name).ToList();
                        break;
                    case "$":
                        partners = partners.OrderByDescending(partner => partner.Savings).ToList();
                        break;
                    case "clear":
                        filters.Clear();
                        radioButton.IsChecked = false;
                        i = 0;
                        break;
                    default:
                        break;

                }
            }

            // Clear current items in the ViewModel and add filtered partners
            viewModel.Items.Clear();
            foreach (Partner partner in partners)
            {
                viewModel.Items.Add(partner);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo() 
            { 
                FileName = e.Uri.AbsoluteUri, 
                UseShellExecute = true 
            });

            e.Handled = true;
        }

        private async void Print_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel borderParent = (StackPanel)((Border)sender).Parent;
            Label partnerId = FindChild<Label>(borderParent.Parent, "lPartnerId");
            Partner partner = (await db.GetPartnerById(partnerId.Content.ToString())).FirstOrDefault();
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                App.Current.Properties["SelectedPartner"] = partner;
                IndividualPartnerReport individualPartnerReport = new IndividualPartnerReport();

                // Set page size to match printable area
                individualPartnerReport.Width = printDialog.PrintableAreaWidth;
                individualPartnerReport.Height = printDialog.PrintableAreaHeight;
                individualPartnerReport.Measure(new Size(individualPartnerReport.Width, individualPartnerReport.Height));
                individualPartnerReport.Arrange(new Rect(new Size(individualPartnerReport.Width, individualPartnerReport.Height)));
                individualPartnerReport.UpdateLayout();

                // Adjust position
                individualPartnerReport.RenderTransform = new TranslateTransform(0, -10);

                printDialog.PrintVisual(individualPartnerReport, "Partner Report");
            }
        }

        private async void SaveAll_Clicked(object sender, RoutedEventArgs e)
        {
            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();
            List<Partner> partners = user.HomeSchool.Partners.Value;

            string tempPDFImage = Path.Combine(localDataPath, "TempPDFImage");

            if (!Directory.Exists(tempPDFImage))
            {
                Directory.CreateDirectory(tempPDFImage);
            }


            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = true;

            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                foreach (Partner partner in partners)
                {
                    App.Current.Properties["SelectedPartner"] = partner;
                    IndividualPartnerReport individualPartnerReport = new IndividualPartnerReport();

                    // Force layout update
                    individualPartnerReport.Measure(new Size(individualPartnerReport.Width, individualPartnerReport.Height));
                    individualPartnerReport.Arrange(new Rect(new Size(individualPartnerReport.Width, individualPartnerReport.Height)));
                    individualPartnerReport.UpdateLayout();

                    // Wait for rendering to complete
                    individualPartnerReport.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() => { }));

                    // Render the page to a bitmap
                    RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)individualPartnerReport.Width, (int)individualPartnerReport.Height, 92, 100, PixelFormats.Pbgra32);
                    renderTargetBitmap.Render(individualPartnerReport);

                    string imgPath = Path.Combine(tempPDFImage, $"{partner.Name}.png");

                    // Save the image to a file
                    using (FileStream fileStream = new FileStream(imgPath, FileMode.Create))
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                        encoder.Save(fileStream);
                    }

                    // Generate PDF with the rendered image
                    using (PdfWriter pdfWriter = new PdfWriter(Path.Combine(folderDialog.SelectedPath, $"{partner.Name}.pdf")))
                    using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
                    using (Document document = new Document(pdfDocument))
                    {
                        // Add the rendered image to the PDF
                        iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory.Create(imgPath));

                        // Set the position of the image to move it to the left
                        float xPosition = 12;
                        float yPosition = pdfDocument.GetDefaultPageSize().GetHeight() - 450f;

                        // Set fixed position and size
                        img.SetFixedPosition(xPosition, yPosition);

                        img.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());

                        document.Add(img);
                    }
                }

                MessageBox.Show("Successfully downloaded all partner reports", "Partners Download Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                string[] files = Directory.GetFiles(tempPDFImage);

                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }

            RadioButton radioButton = (RadioButton)sender;
            radioButton.IsChecked = false;
        }

        private void PreFilter_Industry()
        {
            tbSerachBox.Text = App.Current.Properties["PreFilteredIndustry"].ToString();
            cbSearchFilter.SelectedIndex = 1;
            FilterPartners();
        }
    }
}
