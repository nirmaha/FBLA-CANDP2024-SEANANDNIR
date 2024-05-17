using EduPartners.Core;
using EduPartners.MVVM.Model;
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
    /// Interaction logic for IndividualPartnerReport.xaml
    /// </summary>
    public partial class IndividualPartnerReport : Page
    {
        Database db;
        public IndividualPartnerReport()
        {
            InitializeComponent();
            db = App.Current.Properties["Database"] as Database;
            Partner partner = App.Current.Properties["SelectedPartner"] as Partner;
            DataContext = partner;
            InceptionDate.Text = partner.StartDate.ToString("MM/dd/yyyy");
            GenerationDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            Savings.Text = $"${partner.Savings.ToString()}";
        }
    }
}
