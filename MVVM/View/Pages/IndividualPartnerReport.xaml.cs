using System;
using System.Collections.Generic;
using System.Windows.Controls;

using EduPartners.MVVM.Model;


namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for IndividualPartnerReport.xaml
    /// </summary>
    public partial class IndividualPartnerReport : Page
    {
        public IndividualPartnerReport()
        {
            InitializeComponent();
            Partner partner = App.Current.Properties["SelectedPartner"] as Partner;
            DataContext = partner;
            GenerationDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }
    }
}
