using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.Drawing;
using SkiaSharp;
using System.Data.OleDb;
using System.Globalization;

namespace PAGASCO
{
    public partial class AllDataSales : Form
    {
        private Database database;
        private CartesianChart salesChart;
        private PieChart fuelPieChart;
        private string currentYear;
        private Label titleLabel;
        private Label pieChartLabel;

        public AllDataSales()
        {
            InitializeComponent();
            database = new Database();

            InitializeCharts();

            // Add labels
            titleLabel = new Label
            {
                Text = "Branch Sales Comparison",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(23, 84)
            };
            this.Controls.Add(titleLabel);

            pieChartLabel = new Label
            {
                Text = "Total Fuel Sales Distribution",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(896, 84)
            };
            this.Controls.Add(pieChartLabel);

            LoadYearsAndSetMostRecent();
        }

        private void InitializeCharts()
        {
            try
            {
                // Initialize line chart
                salesChart = new CartesianChart
                {
                    Width = 867,
                    Height = 535,
                    Location = new Point(23, 114),
                    Series = new ISeries[] { },
                    XAxes = new Axis[]
                    {
                        new Axis
                        {
                            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" },
                            LabelsRotation = 0,
                            TextSize = 12,
                            SeparatorsPaint = new SolidColorPaint(SKColors.LightGray) { StrokeThickness = 1 }
                        }
                    },
                    YAxes = new Axis[]
                    {
                        new Axis
                        {
                            Name = "Sales (₱)",
                            TextSize = 12,
                            Labeler = (value) => $"₱{value:N0}",
                            SeparatorsPaint = new SolidColorPaint(SKColors.LightGray) { StrokeThickness = 1 }
                        }
                    },
                    LegendPosition = LiveChartsCore.Measure.LegendPosition.Right,
                    LegendTextSize = 12,
                    LegendTextPaint = new SolidColorPaint(SKColors.Black),
                    BackColor = Color.White
                };

                // Initialize pie chart
                fuelPieChart = new PieChart
                {
                    Width = 265,
                    Height = 416,
                    Location = new Point(896, 114),
                    Series = new ISeries[] { },
                    LegendPosition = LiveChartsCore.Measure.LegendPosition.Bottom,
                    LegendTextSize = 12,
                    IsClockwise = true,
                    InitialRotation = -90,
                    MaxAngle = 360,
                    LegendTextPaint = new SolidColorPaint(SKColors.Black),
                    BackColor = Color.White
                };

                this.Controls.Add(salesChart);
                this.Controls.Add(fuelPieChart);

                salesChart.BringToFront();
                fuelPieChart.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing charts: {ex.Message}");
            }
        }

        private void LoadYearsAndSetMostRecent()
        {
            try
            {
                var yearsTable = database.GetAllSalesYears();
                if (yearsTable.Rows.Count > 0)
                {
                    AllDataSalesYearCombobox.DataSource = yearsTable;
                    AllDataSalesYearCombobox.DisplayMember = "SaleYear";
                    AllDataSalesYearCombobox.ValueMember = "SaleYear";

                    // Get the most recent year
                    currentYear = yearsTable.Rows[yearsTable.Rows.Count - 1]["SaleYear"].ToString();
                    AllDataSalesYearCombobox.SelectedValue = currentYear;

                    // Load data for the most recent year
                    LoadBranchSalesData(currentYear);
                }
                else
                {
                    MessageBox.Show("No sales data available.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading years: {ex.Message}");
            }
        }

        private void AllDataSalesYearCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AllDataSalesYearCombobox.SelectedValue != null)
            {
                // Convert the selected value to string properly
                string selectedYear = AllDataSalesYearCombobox.SelectedValue.ToString();
                if (int.TryParse(selectedYear, out int year))
                {
                    LoadBranchSalesData(year.ToString());
                }
            }
        }

        private void LoadBranchSalesData(string year)
        {
            try
            {
                var branchSalesData = database.GetBranchSalesByYear(year);
                UpdateChart(branchSalesData, year);
                UpdatePieChart(branchSalesData, year);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales data: {ex.Message}");
            }
        }

        private void UpdateChart(DataTable salesData, string year)
        {
            try
            {
                // Group data by branch and month, summing the TotalMoney
                var branchData = salesData.AsEnumerable()
                    .GroupBy(row => new
                    {
                        BranchID = row.Field<int>("branchID"),
                        BranchName = row.Field<string>("BranchName")
                    })
                    .ToDictionary(
                        g => g.Key,
                        g => g.GroupBy(row => row.Field<DateTime>("SaleDate").Month)
                            .ToDictionary(
                                m => m.Key,
                                m => m.Sum(r => Convert.ToDouble(r["TotalMoney"]))
                            )
                    );

                var series = new List<ISeries>();
                var colors = new[]
                {
                    SKColors.Blue,
                    SKColors.Green,
                    SKColors.Orange,
                    SKColors.Red,
                    SKColors.Purple,
                    SKColors.Brown,
                    SKColors.Teal,
                    SKColors.Navy
                };
                int colorIndex = 0;

                foreach (var branch in branchData)
                {
                    var monthlyValues = Enumerable.Range(1, 12)
                        .Select(month => branch.Value.ContainsKey(month) ? branch.Value[month] : 0)
                        .ToArray();

                    series.Add(new LineSeries<double>
                    {
                        Name = branch.Key.BranchName,
                        Values = monthlyValues,
                        GeometrySize = 10,
                        Stroke = new SolidColorPaint(colors[colorIndex % colors.Length], 2),
                        GeometryStroke = new SolidColorPaint(colors[colorIndex % colors.Length], 2),
                        Fill = null,
                        LineSmoothness = 0.5
                    });

                    colorIndex++;
                }

                titleLabel.Text = $"Branch Sales Comparison for {year}";
                salesChart.Series = series;
                salesChart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating chart: {ex.Message}");
            }
        }

        private void UpdatePieChart(DataTable salesData, string year)
        {
            try
            {
                // Group data by fuel type and sum the quantities
                var fuelSummary = salesData.AsEnumerable()
                    .GroupBy(row => row.Field<string>("FuelType"))
                    .Select(group => new
                    {
                        FuelType = group.Key,
                        TotalLiters = group.Sum(row => Convert.ToDouble(row["QuantityLiters"]))
                    })
                    .ToList();

                var series = new List<ISeries>();
                var colors = new Dictionary<string, SKColor>
                {
                    { "Diesel", SKColors.Red },
                    { "Premium", SKColors.LightGreen },
                    { "Regular", SKColors.LightBlue }
                };

                foreach (var fuel in fuelSummary.OrderByDescending(f => f.TotalLiters))
                {
                    var color = colors.ContainsKey(fuel.FuelType) ? colors[fuel.FuelType] : SKColors.Gray;

                    series.Add(new PieSeries<double>
                    {
                        Name = $"{fuel.FuelType} ({fuel.TotalLiters:N0}L)",
                        Values = new[] { fuel.TotalLiters },
                        Fill = new SolidColorPaint(color),
                        Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 2 },
                        DataLabelsSize = 11,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black)
                    });
                }

                fuelPieChart.Series = series;
                pieChartLabel.Text = $"Total Fuel Sales Distribution ({year})";
                fuelPieChart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating pie chart: {ex.Message}");
            }
        }

        private void LoginBackBtn_Click(object sender, EventArgs e)
        {
           OwnerDashboard ownerdashboard = new OwnerDashboard();
            ownerdashboard.Show();
            this.Hide();
        }
    }
}
