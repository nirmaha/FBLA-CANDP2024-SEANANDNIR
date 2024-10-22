﻿using System;
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
using EduPartners.MVVM.View.Controls;

# pragma warning disable CA1416 // Validate platform compatibility

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
        private readonly Database db;
        private readonly MainControl mainControl;
        

        public ObservableCollection<CustomLegendItem> LegendItems { get; set; }

        private readonly Dictionary<string, double> IndustryToSavings = [];

        public string[] BarChartLabels { get; set; }
        public Func<double, string> CurrencyFormatter { get; set; }


        public Dashboard()
        {
            InitializeComponent();
            db = App.Current.Properties["Database"] as Database;
            mainControl = App.Current.Properties["MainControl"] as MainControl;
            DataContext = this;

            // Event handler for when the Dashboard page is loaded
            this.Loaded += async (sender, e) =>
            {
                // Retrieve the school information
                School school = (await db.GetSchoolById(App.Current.Properties["CurrentSchoolId"].ToString())).FirstOrDefault();

                // Get the list of partners from the school and order them by start date
                List<Partner> partners = school.Partners.Value;
                partners = partners.OrderBy(p => p.StartDate.Year).ToList();

                List<int> dates = new List<int>();
                List<string> industries = new List<string>();

                // Populates the dates and industries lists for Dashboard Graphs
                foreach (Partner partner in partners)
                {
                    dates.Add(partner.StartDate.Year);
                    industries.Add(partner.Industry);

                    // Calculate industry savings for the Pie Chart
                    if (!IndustryToSavings.ContainsKey(partner.Industry))
                    {
                        IndustryToSavings.Add(partner.Industry, partner.Savings);
                    }
                    else
                    {
                        IndustryToSavings[partner.Industry] += partner.Savings;
                    }
                }

                // Generate data for the Line Graph
                Dictionary<int, int> yearCounts = dates
                          .GroupBy(year => year)
                          .ToDictionary(group => group.Key, group => group.Count());

                ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();

                foreach (KeyValuePair<int, int> year in yearCounts)
                {
                    values.Add(new ObservablePoint(year.Key, year.Value));
                }

                // Create the Line Series for the Line Graph
                LineSeries lineSeries = new LineSeries
                {
                    Title = "Partners",
                    Values = values,
                    PointGeometry = DefaultGeometries.Circle,
                };

                lineNumofPartners.Series = new SeriesCollection { lineSeries };

                // Generate data for the Industry Pie Chart
                Dictionary<string, int> industryCount = industries
                          .GroupBy(industry => industry)
                          .ToDictionary(group => group.Key, group => group.Count());

                SeriesCollection pieSeries = new SeriesCollection();

                int colorIndex = 0;

                foreach (KeyValuePair<string, int> industry in industryCount)
                {
                    pieSeries.Add
                    (
                        new PieSeries
                        {
                            Title = industry.Key,
                            Values = new ChartValues<int> { industry.Value },
                            Fill = mainControl.savedColors[colorIndex]
                        }
                    );
                    colorIndex++;
                }

                List<CustomLegendItem> LegendItems = new List<CustomLegendItem>();
                foreach (Series series in pieSeries)
                {
                    LegendItems.Add(new CustomLegendItem { Title = series.Title, Color = series.Fill });
                }

                pieIndustry.Series = pieSeries;
                lbPieLegend.ItemsSource = LegendItems;

                ChartValues<double> savings = new ChartValues<double>();

                foreach (double saving in IndustryToSavings.Values)
                {
                    savings.Add(saving);
                }

                // Generate data for the Bar Graph
                ColumnSeries columnSeries = new ColumnSeries
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

                SeriesCollection barChartCollection = new SeriesCollection { columnSeries };
                barIndustrySavings.Series = barChartCollection;
            };
        }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            App.Current.Properties["PreFilteredIndustry"] = chartPoint.SeriesView.Title;
            mainControl.Load_Page("MVVM/View/Pages/ViewPartners.xaml");
        }

        private void BarGraph_DataClick(object sender, ChartPoint chartPoint)
        {
            App.Current.Properties["PreFilteredIndustry"] = barIndustrySavings.AxisX[0].Labels[(int)chartPoint.X];
            mainControl.Load_Page("MVVM/View/Pages/ViewPartners.xaml");
        }

        private void lbPieLegend_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbPieLegend.SelectedItem is not CustomLegendItem item)
            {
                return;
            }

            App.Current.Properties["PreFilteredIndustry"] = item.Title;
            mainControl.Load_Page("MVVM/View/Pages/ViewPartners.xaml");
        }
    }
}
