using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;


namespace PAGASCO
{
    public partial class ManagerDashboard : Form
    {
        private Database database; // Declare the database field inside the class
        bool sidebarExpand = true;
        int selectedBranchID = -1;
        private int managerID;
        private int branchID;
        private CartesianChart cartesianChart1;
        private bool isFormReady = false;
        private PieChart fuelPieChart;
        private static bool Topmost;
        private string selectedFuelType = "";

        public ManagerDashboard(int mid)
        {
            InitializeComponent();
            database = new Database();
            branchID = Convert.ToInt32(database.GetBranchByManagerID(mid).Rows[0]["branchID"]);
            managerID = mid;

            isFormReady = true;

        }

        private void ManagerDashboard_Load(object sender, EventArgs e)
        {
            DataTable branchTable = database.GetBranchByManagerID(managerID);

            if (branchTable.Rows.Count > 0)
            {
                int branchID = Convert.ToInt32(branchTable.Rows[0]["branchID"]);

                // Load data into data grids
                FuelStocksDGV.DataSource = database.GetStocksByBranch(branchID);
                FuelStocksDGV.ReadOnly = true;
                FuelPricesDGV.DataSource = database.GetPricesByBranch(branchID);

                CheckStockLevelsAtLogin(branchID);

                // Set total sales label
                decimal totalSalesToday = database.GetTotalSalesForToday(branchID);
                totalSalesLabel.Text = "Today's Total Sales: ₱" + totalSalesToday.ToString("N2");

                // Populate DateSalesCombox (months)
                DataTable monthsTable = database.GetAvailableSalesMonths(branchID);
                DateSalesCombox.Items.Clear();
                foreach (DataRow row in monthsTable.Rows)
                {
                    DateSalesCombox.Items.Add(row["SaleMonth"].ToString());
                }

                // Populate FuelTypeSalesCombobox (if not yet populated)
                FuelTypeSalesCombobox.Items.Clear();
                FuelTypeSalesCombobox.Items.Add("Diesel");
                FuelTypeSalesCombobox.Items.Add("Premium");
                FuelTypeSalesCombobox.Items.Add("Regular");

                // Set default selections AFTER population
                if (FuelTypeSalesCombobox.Items.Count > 0)
                    FuelTypeSalesCombobox.SelectedIndex = 0;

                if (DateSalesCombox.Items.Count > 0)
                    DateSalesCombox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No branch assigned.");
                this.Close();
            }

            // ✅ Create charts BEFORE trying to load data into them
            cartesianChart1 = new CartesianChart
            {
                Width = 663,
                Height = 485,
                Location = new Point(594, 215)
            };
            SalesPanel.Controls.Add(cartesianChart1);
            cartesianChart1.BringToFront();

            fuelPieChart = new PieChart
            {
                Width = 551,
                Height = 285,
                Location = new Point(18, 415),
                LegendPosition = LiveChartsCore.Measure.LegendPosition.Right
            };
            SalesPanel.Controls.Add(fuelPieChart);
            fuelPieChart.BringToFront();

            // ✅ Load charts now that everything is initialized
            LoadSalesChart();
            LoadFuelSalesPieChart();
        }




        //BRANCH PANEL

        private void loginBtn_Click(object sender, EventArgs e) //branchbtn
        {
            BranchPanel.BringToFront();
            sidebar.BringToFront();
            ManagerDashboard.Topmost = true;
        }

        //FuelOfBranchButton
        private void LoadBtn_Click(object sender, EventArgs e)
        {


        }

        private void FuelPricesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void FuelStocksDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (selectedBranchID != -1)
            {
                database.UpdateStock(FuelStocksDGV.Rows[e.RowIndex]);
            }
        }

        private void FuelStocksDGV_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < FuelStocksDGV.Rows.Count)
            {
                DataGridViewRow row = FuelStocksDGV.Rows[e.RowIndex];
                if (row.IsNewRow) return;

                database.UpdateStock(row);
            }
        }

        private void FuelStocksDGV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = FuelStocksDGV.Columns[e.ColumnIndex].Name;
            if (columnName == "Diesel" || columnName == "Premium" || columnName == "Regular")
            {
                if (decimal.TryParse(e.FormattedValue.ToString(), out decimal value))
                {
                    if (value > 20000)
                    {
                        MessageBox.Show("⚠️ Fuel stock cannot exceed 20,000 liters.", "Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
                else
                {
                    MessageBox.Show("❌ Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }



        private void FuelPricesDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (selectedBranchID != -1)
            {
                database.UpdatePrice(FuelPricesDGV.Rows[e.RowIndex]);
            }
        }
        private void FuelStocksDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void CheckStockLevelsAtLogin(int branchID)
        {
            DataTable stocksTable = database.GetStocksByBranch(branchID);
            if (stocksTable.Rows.Count > 0)
            {
                DataRow row = stocksTable.Rows[0];
                StringBuilder warningMessage = new StringBuilder();
                warningMessage.AppendLine("⚠️ TIME TO REFUEL! ⚠️\n");

                decimal diesel = Convert.ToDecimal(row["Diesel"]);
                decimal premium = Convert.ToDecimal(row["Premium"]);
                decimal regular = Convert.ToDecimal(row["Regular"]);

                // Check each fuel type and add to warning message if low
                if (diesel <= 5000)
                {
                    warningMessage.AppendLine($"• Diesel is critically low: {diesel:N2} liters remaining");
                }
                if (premium <= 5000)
                {
                    warningMessage.AppendLine($"• Premium is critically low: {premium:N2} liters remaining");
                }
                if (regular <= 5000)
                {
                    warningMessage.AppendLine($"• Regular is critically low: {regular:N2} liters remaining");
                }

                // Only show the warning if any fuel type is low
                if (warningMessage.Length > 30) // More than just the header
                {
                    MessageBox.Show(warningMessage.ToString(), "Critical Fuel Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Still send individual email notifications
                database.CheckFuelThreshold("Diesel", diesel, branchID);
                database.CheckFuelThreshold("Premium", premium, branchID);
                database.CheckFuelThreshold("Regular", regular, branchID);
            }
        }



        //SALES PANEL
        private void SalesBtn_Click(object sender, EventArgs e)
        {
            SalesPanel.BringToFront();
            sidebar.BringToFront();
        }
        private void DateSalesCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesDGV();
            LoadSalesChart(); // <-- this line
            LoadFuelSalesPieChart(); // new pie chart
            UpdateSalesAndLitersLabels();
        }

        private void LoadSalesDGV()
        {
            if (FuelTypeSalesCombobox.SelectedItem == null || DateSalesCombox.SelectedItem == null)
                return;

            string selectedFuelType = FuelTypeSalesCombobox.SelectedItem.ToString();
            string selectedYearMonth = DateSalesCombox.SelectedItem.ToString();
            int branchID = Convert.ToInt32(database.GetBranchByManagerID(managerID).Rows[0]["branchID"]);

            DataTable salesTable = database.GetSalesByBranchFuelAndMonth(branchID, selectedFuelType, selectedYearMonth);
            salesDVG.DataSource = salesTable;
        }


        //ACCOUNT SETTINGS PANEL
        private void AccSettingsBtn_Click(object sender, EventArgs e)
        {
            AccountSetPanel.BringToFront();
            sidebar.BringToFront();
        }


        //ABOUT US PANEL

        private void AboutPGSCBtn_Click(object sender, EventArgs e)
        {
            AboutUsPanel.BringToFront();
            sidebar.BringToFront();
        }


        //OTHER BUTTONS AND UI


        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 20;
                if (sidebar.Width <= 37)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 20;
                if (sidebar.Width >= 479)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void DashBoardExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void PagascoLabel_Click(object sender, EventArgs e)
        {
        }

        private void siticonePanel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                CompanyRole companyrole = new CompanyRole();
                companyrole.ShowDialog();
                this.Close();
            }
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void BranchBtn_Paint(object sender, PaintEventArgs e)
        {
        }

        private void siticonePanel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void siticoneLabel3_Click(object sender, EventArgs e)
        {
        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {
            sidebar.BringToFront();
        }

        private void SalesPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
        private void label7_Click(object sender, EventArgs e)
        {
        }
        private void AccountSetPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

        }

        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

        }

        private void Panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void branchPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        public class FuelStock
        {
            public int Diesel { get; set; }
            public int Regular { get; set; }
            public int Premium { get; set; }

            public FuelStock(int diesel, int regular, int premium)
            {
                Diesel = diesel;
                Regular = regular;
                Premium = premium;
            }
        }

        private void siticonePanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void salesDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FuelTypeSalesCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesDGV();
            LoadSalesChart();
            UpdateSalesAndLitersLabels();
        }

        private void totalSalesLabel_Click(object sender, EventArgs e)
        {
            if (FuelTypeSalesCombobox.SelectedItem == null || DateSalesCombox.SelectedItem == null)
                return;

            string selectedFuelType = FuelTypeSalesCombobox.SelectedItem.ToString();
            string selectedYearMonth = DateSalesCombox.SelectedItem.ToString(); // format: yyyy-MM
            int branchID = Convert.ToInt32(database.GetBranchByManagerID(managerID).Rows[0]["branchID"]);

            DataTable salesTable = database.GetSalesByBranchFuelAndMonth(branchID, selectedFuelType, selectedYearMonth);

            decimal totalSales = 0;

            foreach (DataRow row in salesTable.Rows)
            {
                if (row["TotalMoney"] != DBNull.Value)
                    totalSales += Convert.ToDecimal(row["TotalMoney"]);
            }

            totalSalesLabel.Text = $"Today's Total Sales: ₱{totalSales:N2}";
        }



        private int GetCurrentBranchID()
        {
            DataTable branchTable = database.GetBranchByManagerID(managerID);
            if (branchTable.Rows.Count > 0)
            {
                return Convert.ToInt32(branchTable.Rows[0]["branchID"]);
            }
            else
            {
                MessageBox.Show("No branch assigned.");
                this.Close();
                return -1;
            }
        }

        private void LoadSalesChart()
        {
            if (!isFormReady || cartesianChart1 == null)
                return;

            if (FuelTypeSalesCombobox.SelectedItem == null || DateSalesCombox.SelectedItem == null)
            {
                // Optionally show a message or silently ignore
                return;
            }

            string selectedFuelType = FuelTypeSalesCombobox.SelectedItem.ToString();
            string selectedYearMonth = DateSalesCombox.SelectedItem.ToString();
            int branchID = GetCurrentBranchID();

            Dictionary<int, decimal> weeklySales = database.GetWeeklySalesByBranchFuelAndMonth(branchID, selectedFuelType, selectedYearMonth);

            // Check if weeklySales is empty
            if (weeklySales == null || !weeklySales.Any())
            {
                // Handle the case where there are no sales data for the selected options.
                // Optionally show a message or return early.
                return;
            }

            var values = weeklySales.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value).ToArray();
            var weekLabels = weeklySales.OrderBy(kvp => kvp.Key).Select(kvp => "Week " + kvp.Key).ToArray();

            cartesianChart1.Series = new ISeries[]
            {
        new LineSeries<decimal>
        {
            Values = values,
            Fill = null,
            Stroke = new SolidColorPaint(SKColors.SkyBlue, 3),
            GeometryFill = new SolidColorPaint(SKColors.SkyBlue),
            GeometryStroke = new SolidColorPaint(SKColors.DodgerBlue, 2)
        }
            };

            cartesianChart1.XAxes = new[]
            {
        new Axis
        {
            Labels = weekLabels,
            LabelsRotation = 15
        }
    };

            double maxSalesValue = weeklySales.Select(x => (double)x.Value).Max();

            double stepSize = Math.Ceiling(maxSalesValue / 5 / 10.0) * 10; // Rounds step to the nearest 10
            double maxLimit = Math.Ceiling((maxSalesValue + 10) / stepSize) * stepSize;

            cartesianChart1.YAxes = new Axis[]
            {
        new Axis
        {
            MinLimit = 0,
            MaxLimit = maxLimit,
            UnitWidth = stepSize,
            SeparatorsPaint = new SolidColorPaint(SKColors.LightGray),
            LabelsPaint = new SolidColorPaint(SKColors.Gray),
            Name = "₱"
        }
            };
        }


        private void LoadFuelSalesPieChart()
        {
            if (!isFormReady || fuelPieChart == null || DateSalesCombox.SelectedItem == null)
                return;

            string selectedYearMonth = DateSalesCombox.SelectedItem.ToString();
            int branchID = GetCurrentBranchID();

            DataTable fuelSalesData = database.GetFuelSalesSummaryByMonth(branchID, selectedYearMonth);

            if (fuelSalesData.Columns.Count == 0)
            {
                MessageBox.Show("No columns returned. Check your SQL query.");
                return;
            }

            if (fuelSalesData.Rows.Count == 0)
            {
                MessageBox.Show("No data found for this branch and month.");
                return;
            }

            List<PieSeries<decimal>> pieSeries = new List<PieSeries<decimal>>();

            foreach (DataRow row in fuelSalesData.Rows)
            {
                string fuelType = row["FuelType"].ToString();
                decimal totalSales = Convert.ToDecimal(row["TotalSales"]);

                pieSeries.Add(new PieSeries<decimal>
                {
                    Values = new decimal[] { totalSales },
                    Name = fuelType,
                    DataLabelsSize = 14,
                    DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer
                });
            }

            fuelPieChart.Series = pieSeries;
        }

        private void LitersSoldLabel_Click(object sender, EventArgs e)
        {
            if (FuelTypeSalesCombobox.SelectedItem == null || DateSalesCombox.SelectedItem == null)
                return;

            string selectedFuelType = FuelTypeSalesCombobox.SelectedItem.ToString();
            string selectedYearMonth = DateSalesCombox.SelectedItem.ToString();
            int branchID = Convert.ToInt32(database.GetBranchByManagerID(managerID).Rows[0]["branchID"]);

            DataTable salesTable = database.GetSalesByBranchFuelAndMonth(branchID, selectedFuelType, selectedYearMonth);

            decimal totalLiters = 0;

            foreach (DataRow row in salesTable.Rows)
            {
                if (row["QuantityLiters"] != DBNull.Value)
                    totalLiters += Convert.ToDecimal(row["QuantityLiters"]);
            }

            LitersSoldLabel.Text = $"Total Liters Sold: {totalLiters:N2} L";
        }

        private void UpdateSalesAndLitersLabels()
        {
            if (FuelTypeSalesCombobox.SelectedItem == null || DateSalesCombox.SelectedItem == null)
                return;

            string selectedFuelType = FuelTypeSalesCombobox.SelectedItem.ToString();
            string selectedMonth = DateSalesCombox.SelectedItem.ToString();
            int branchID = Convert.ToInt32(database.GetBranchByManagerID(managerID).Rows[0]["branchID"]);

            decimal totalSales = database.GetTotalSalesForFuelAndMonth(branchID, selectedFuelType, selectedMonth);
            decimal totalLiters = database.GetTotalLitersSoldForFuelAndMonth(branchID, selectedFuelType, selectedMonth);

            totalSalesLabel.Text = $"Total Sales: ₱{totalSales:N2}";
            LitersSoldLabel.Text = $"Total Liters Sold: {totalLiters:N2} L";
        }

        private void DieselrestockRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (DieselrestockRadioBtn.Checked)
            {
                selectedFuelType = "Diesel";
            }
        }

        private void PremiumrestockRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (PremiumrestockRadioBtn.Checked)
            {
                selectedFuelType = "Premium";
            }
        }

        private void RegularrestockRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (RegularrestockRadioBtn.Checked)
            {
                selectedFuelType = "Regular";
            }
        }

        private void restockTbx_Click(object sender, EventArgs e)
        {
            // Clear the textbox when clicked if it contains the placeholder text
            if (restockTbx.Text == "Restock Here!")
            {
                restockTbx.Text = "";
            }
        }

        private void restockBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFuelType))
            {
                MessageBox.Show("Please select a fuel type first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(restockTbx.Text, out decimal restockAmount))
            {
                MessageBox.Show("Please enter a valid number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (restockAmount <= 0)
            {
                MessageBox.Show("Please enter a positive number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get current stock
            DataTable stocksTable = database.GetStocksByBranch(branchID);
            if (stocksTable.Rows.Count > 0)
            {
                DataRow currentStock = stocksTable.Rows[0];
                decimal currentAmount = Convert.ToDecimal(currentStock[selectedFuelType]);
                decimal newAmount = currentAmount + restockAmount;

                if (newAmount > 20000)
                {
                    MessageBox.Show("⚠️ Total fuel stock cannot exceed 20,000 liters!", "Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update the stock in the DataGridView
                foreach (DataGridViewRow row in FuelStocksDGV.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        row.Cells[selectedFuelType].Value = newAmount;
                        database.UpdateStock(row);
                        break;
                    }
                }

                MessageBox.Show($"Successfully restocked {restockAmount} liters of {selectedFuelType}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                restockTbx.Text = "Restock Here!";
            }
        }

        private void OwnerLoginBtn_Click(object sender, EventArgs e)
        {
            VerifyManager verify = new VerifyManager();
            verify.Show();
            this.Hide();

        }

        private void siticoneShimmerLabel7_Click(object sender, EventArgs e)
        {
            DataTable branchTable = database.GetBranchByManagerID(managerID);
            if (branchTable.Rows.Count > 0)
            {
                string branchName = branchTable.Rows[0]["branchName"].ToString();
                siticoneShimmerLabel7.Text = branchName;
            }
        }

        private void siticonePanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        // Fix for branchRefreshBtn reference


    }
}