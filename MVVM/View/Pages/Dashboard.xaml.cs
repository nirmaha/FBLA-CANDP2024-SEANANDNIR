using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;

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

                // Populates Dashbord Graphs
                foreach (Partner partner in partners) 
                {
                    // Industy Pie Chart
                    if (!IndustryPopulation.ContainsKey(partner.Industry))
                    {
                        IndustryPopulation.Add(partner.Industry, 1);
                    }
                    else
                    { 
                        IndustryPopulation[partner.Industry]++;
                    }

                    // Line Graph
                    if (!YearToParnters.ContainsKey(partner.StartDate.Year))
                    {
                        YearToParnters.Add(partner.StartDate.Year, 1);
                    }
                    else
                    {
                        YearToParnters[partner.StartDate.Year]++;
                    }

                    // Industry Savings Pie Chart
                    if (!IndustryToSavings.ContainsKey(partner.Industry))
                    {
                        IndustryToSavings.Add(partner.Industry, partner.Savings);
                    }
                    else
                    {
                        IndustryToSavings[partner.Industry] += partner.Savings;
                    }

                }

                ((PieSeries)pieIndustry.Series[0]).ItemsSource = IndustryPopulation;
                ((LineSeries)lineNumofPartners.Series[0]).ItemsSource = YearToParnters;
                ((PieSeries)pieIndustrySavings.Series[0]).ItemsSource = IndustryToSavings;
                
            };
        }
    }
}
