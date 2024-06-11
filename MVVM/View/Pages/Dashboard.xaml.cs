using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

using EduPartners.Core;
using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.View.Pages
{
    public class CustomLegendItem
    {
        public string Title { get; set; }
        public Brush Color { get; set; }
    }

    /// <summary>
    /// Interaction Logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private Database db;
        private Random rnd = new Random();

        public ObservableCollection<CustomLegendItem> LegendItems { get; set; }

        private Dictionary<string, double> IndustryToSavings = new Dictionary<string, double>();

        public string[] BarChartLabels { get; set; }
        public Func<double, string> CurrencyFormatter { get; set; }

        public Dashboard()
        {
            InitializeComponent();
            db = App.Current.Properties["Database"] as Database;
            DataContext = this;

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
                    SolidColorBrush randomColor = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));

                    pieSeries.Add
                    (
                        new PieSeries() 
                        { 
                            Title = industry.Key, 
                            Values = new ChartValues<int> { industry.Value },
                            Fill = randomColor
                        }
                    );
                }

                LegendItems = new ObservableCollection<CustomLegendItem>();
                foreach (Series series in pieSeries)
                {
                    LegendItems.Add(new CustomLegendItem { Title = series.Title, Color = series.Fill });
                }

                pieIndustry.Series = pieSeries;
                legendListBox.ItemsSource = LegendItems;

                ChartValues<double> savings = new ChartValues<double>();

                foreach (double saving in IndustryToSavings.Values)
                {
                    savings.Add(saving);
                }

                // Bar Graph
                ColumnSeries columnSeries = new ColumnSeries()
                {
                    Title = "Partners",
                    Values = savings,
                };

                List<string> listLabels = new List<string>();
                foreach (string industry in IndustryToSavings.Keys)
                { 
                    listLabels.Add(industry);
                }

                BarChartLabels = listLabels.ToArray();
                CurrencyFormatter = value => value.ToString("C");

                SeriesCollection barChartCollection = new SeriesCollection() { columnSeries };
                barIndustrySavings.Series = barChartCollection;
            };
        }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            PieChart chart = (PieChart)chartPoint.ChartView;

            foreach (PieSeries series in chart.Series)
            {
                series.PushOut = 0;
            }

            PieSeries selectedSeries = (PieSeries)chartPoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
