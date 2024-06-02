﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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


namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for ViewPartners.xaml
    /// </summary>
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

        private static string localDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EduPartners");

        public ViewPartners()
        {
            InitializeComponent();

            db = App.Current.Properties["Database"] as Database;

            viewModel = new PartnerViewModel();

            PopulateView();

            icViewParnter.ItemsSource = viewModel.Items;
            DataContext = viewModel;
        }


        /// <summary>
        /// This function checks if the Partners inside of School's list still exist inside of the Partner collection.
        /// </summary>
        private async void SyncList()
        {
            // Retrieve the current school
            School school = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();

            // Retrieve all partners from the database
            List<Partner> allPartners = await db.GetPartners();

            // Loops through each partner in the schools list
            for (int i = 0; i < school.Partners.Value.Count; i++)
            {
                bool foundPartner = false;

                // Loops through each partner in the collection
                for (int j = 0; j < allPartners.Count; j++)
                {
                    if (school.Partners.Value[i].Id == allPartners[j].Id)
                    { 
                        foundPartner = true;
                        break;
                    }
                }

                if (!foundPartner)
                {
                    school.Partners.Value.RemoveAt(i);
                    i--;
                }
            }

            await db.UpdateSchool(school);
        }


        /// <summary>
        /// This function populates the ViewModel collection of partners.
        /// </summary>
        private async void PopulateView()
        {
            SyncList();

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

        /// <summary>
        /// This function recursively searches for a child element of a specified type with a given
        /// name within a parent `DependencyObject`.
        /// </summary>
        /// <param name="parent">The partner of the element, passed through the Generic (T).</param>
        /// <param name="childName">The the name of the child element you are looking for within the visual tree hierarchy.</param>
        /// <returns>Returns the element specified in Generic (T) and the <paramref name="childName"/>. If no matching child is found, it returns null.
        /// </returns>
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            // Loops though each child in the parent
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                // If it matches the type and name return the element
                if (child is T typedChild && ((FrameworkElement)child).Name == childName)
                {
                    return typedChild;
                }
                // If it doesn't match it will recursively search for the child
                else
                {
                    T result = FindChild<T>(child, childName);
                    if (result != null)
                        return result;
                }
            }
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

            school.Partners.Value.Remove(partner);

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

            // Makes sure only one button is clicked at a time
            foreach (object stackButton in spFilterButtons.Children)
            {
                if (stackButton is RadioButton checkButton && stackButton != button)
                {
                    checkButton.IsChecked = false;
                }
            }

            FilterPartners(button);
        }

        private void Search_MouseDown(object sender, RoutedEventArgs e)
        {
            FilterPartners();
            e.Handled = true;
        }

        private readonly List<string> filters = new List<string>();

        private async void FilterPartners(RadioButton radioButton = null)
        {
            User user = (await db.GetUserById(App.Current.Properties["CurrentUserId"].ToString())).FirstOrDefault();
            List<Partner> partners = user.HomeSchool.Partners.Value;
            
            // Checks the tag to know what filter it is and adds it to the filter list
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
                    case "$":
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

            // Searches the name starting with the search box
            if (!string.IsNullOrEmpty(tbSerachBox.Text))
            {
                ComboBoxItem searchFilter = (ComboBoxItem)cbSearchFilter.SelectedItem;

                switch (searchFilter.Tag.ToString())
                {
                    case "savings":
                        partners = partners.Where(partner => partner.Savings.ToString().StartsWith(tbSerachBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "industry":
                        partners = partners.Where(partner => partner.Industry.StartsWith(tbSerachBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "address":
                        partners = partners.Where(partner => partner.Address.StartsWith(tbSerachBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    default:
                        partners = partners.Where(partner => partner.Name.StartsWith(tbSerachBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                }

            }

            // Goes through the filter list and applies the filter to the ViewModel
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
                    case "$":
                        partners = partners.OrderByDescending(partner => partner.Savings).ToList();
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

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
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
    }
}
