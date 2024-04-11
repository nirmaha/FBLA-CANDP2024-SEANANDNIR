using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using EduPartners.Core;
using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    /// 

    public partial class Dashboard : Page
    {
        private Database db;
        private Dictionary<string, int> IndustryPopulation = new Dictionary<string, int>();
        private Dictionary<string, double> PartnerToSavings = new Dictionary<string, double>();
        private Dictionary<int, int> YearToParnters = new Dictionary<int, int>();
        private Dictionary<string, double> IndustryToSavings = new Dictionary<string, double>();

        public Dashboard()
        {
            InitializeComponent();
            db = App.Current.Properties["Database"] as Database;

            this.Loaded += async (sender, e) =>
            {
                School school = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();

                List<Partner> partners = school.Partners.Value;
                partners = partners.OrderBy(p => p.StartDate.Year).ToList();

                
                foreach (Partner partner in partners) 
                {
                    // Pie Chart
                    if (!IndustryPopulation.ContainsKey(partner.Industry))
                    {
                        IndustryPopulation.Add(partner.Industry, 1);
                    }
                    else
                    { 
                        IndustryPopulation[partner.Industry]++;
                    }

                    // Verical Bar Graph
                    //if (!PartnerToSavings.ContainsKey(partner.Name))
                    //{ 
                    //    PartnerToSavings.Add(partner.Name, partner.Savings);
                    //}

                    // Line Graph
                    if (!YearToParnters.ContainsKey(partner.StartDate.Year))
                    {
                        YearToParnters.Add(partner.StartDate.Year, 1);
                    }
                    else
                    {
                        YearToParnters[partner.StartDate.Year]++;
                    }

                    // Horizontal Bar Graphs
                    //if (!IndustryToSavings.ContainsKey(partner.Industry))
                    //{
                    //    IndustryToSavings.Add(partner.Industry, partner.Savings);
                    //}
                    //else
                    //{
                    //    IndustryToSavings[partner.Industry] += partner.Savings;
                    //}

                }
                ((PieSeries)pieIndustry.Series[0]).ItemsSource = IndustryPopulation;
                // ((ColumnSeries)barSavings.Series[0]).ItemsSource = PartnerToSavings;
                ((LineSeries)lineNumofPartners.Series[0]).ItemsSource = YearToParnters;
                // ((BarSeries)barIndustry.Series[0]).ItemsSource = IndustryToSavings;
                
            };
        }
    }
}
