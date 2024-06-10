using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

using EduPartners.Core;
using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.View.Pages
{
    /// <summary>
    /// Interaction Logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private Database db;

        private Dictionary<string, int> IndustryPopulation = new Dictionary<string, int>();
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

                List<int> dates = new List<int>();
                List<string> industries = new List<string>();

                // Populates Dashboard Graphs
                foreach (Partner partner in partners) 
                {
                    dates.Add(partner.StartDate.Year);
                    industries.Add(partner.Industry);

                    // Industry Pie Chart
                    if (!IndustryPopulation.ContainsKey(partner.Industry))
                    {
                        IndustryPopulation.Add(partner.Industry, 1);
                    }
                    else
                    {
                        IndustryPopulation[partner.Industry]++;
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

                // Line Graph
                Dictionary<int, int> yearCounts = dates
                          .GroupBy(year => year)
                          .ToDictionary(group => group.Key, group => group.Count());

                ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();

                foreach (KeyValuePair<int, int> year in yearCounts)
                {
                    values.Add(new ObservablePoint(year.Key, year.Value));
                }


                LineSeries lineSeries = new LineSeries()
                {
                    Title = "Partners",
                    Values = values,
                    PointGeometry = DefaultGeometries.Circle,
                    
                };

                lineNumofPartners.Series = new SeriesCollection() { lineSeries };

                // Industry Pie Chart
                Dictionary<string, int> industryCount = industries
                          .GroupBy(industry => industry)
                          .ToDictionary(group => group.Key, group => group.Count());

                SeriesCollection pieSeries = new SeriesCollection();

                foreach (KeyValuePair<string, int> industry in industryCount)
                {
                    pieSeries.Add(new PieSeries() { Title = industry.Key, Values = new ChartValues<int> { industry.Value } });
                }

                pieIndustry.Series = pieSeries;

            };
        }
    }
}
