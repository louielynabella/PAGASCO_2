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
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Globalization;


namespace PAGASCO
{
    public partial class OwnerDashboard : Form
    {
        private Database database;
        private string selectedFuel;
        private bool isFormReady = false;
        private int selectedBranchID = -1;
        private string selectedDateFilter = ""; // e.g., "2024", or "2024-04"
        private bool isYearlyView = true;
        private string selectedFuelType = "";

        private CartesianChart lineChart;

        private int managerRowClickCount = 0;
        private int lastManagerRowIndex = -1;

        public OwnerDashboard()
        {
            InitializeComponent();

            database = new Database();
            isFormReady = true;

        }


        private void LoadBranches()
        {
            Database database = new Database();
            DataTable dt = database.GetBranches();
            branchesComBx.DataSource = dt;
            branchesComBx.DisplayMember = "branchName";
            branchesComBx.ValueMember = "branchID";
            branchesDGV.DataSource = database.GetAllBranches();
        }

        private void OwnerDashboard_Load(object sender, EventArgs e)
        {
            LoadBranches();
            LoadManagerList();

            // Initialize the line chart
            lineChart = new CartesianChart
            {
                Location = new Point(11, 337),
                Size = new Size(879, 393),
                LegendPosition = LiveChartsCore.Measure.LegendPosition.Right,
                BackColor = Color.White // Add background color to make it visible
            };

            // Add the charts to the panel
            OwnerSalesPanel.Controls.Add(lineChart);

            // Make sure the charts are visible and brought to front
            lineChart.Visible = true;
            lineChart.BringToFront();
        }

        private void siticonePanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void branchesComBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedRow = (DataRowView)branchesComBx.SelectedItem;
            selectedBranchID = Convert.ToInt32(selectedRow["branchID"]);

            branchesDGV.DataSource = database.GetStocksByBranch(selectedBranchID);

        }

        private void addbranchBtn_Click(object sender, EventArgs e)
        {
            string newBranch = addbranchTbx.Text.Trim(); // Get branch name from TextBox
            if (!string.IsNullOrWhiteSpace(newBranch))
            {
                database.AddBranch(newBranch);
                LoadBranches(); // Refresh combo box
                addbranchTbx.Clear(); // Clear TextBox after adding
            }
            else
            {
                MessageBox.Show("Please enter a branch name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void siticoneButton1_Click(object sender, EventArgs e) //managemanagerBtn
        {
            ManageManagerPanel.BringToFront();
        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BrnchBtn_Click_1(object sender, EventArgs e)
        {
            ManageBranchPanel.BringToFront();
        }

        private void LogOutOwnerBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                UserChoiceMenu userchoicemenu = new UserChoiceMenu();
                userchoicemenu.ShowDialog();
                this.Close();
            }
        }

        public void LoadManagerList()
        {
            Database db = new Database();
            DataTable managerTable = db.GetAllManagers();
            AddedManagerDGV.DataSource = managerTable;
        }

        private void AddManagerBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateAcc createacc = new CreateAcc(this);
            createacc.ShowDialog();
            this.Close();
        }

        private void OwnerSalesPanel_Paint(object sender, PaintEventArgs e)
        {



        }

        private void SalesOwnerBtn_Click(object sender, EventArgs e)
        {
            OwnerSalesPanel.BringToFront();
            lineChart.Visible = true;
            lineChart.BringToFront();

            // If we have data already selected, refresh the chart
            if (selectedBranchID != -1 && !string.IsNullOrEmpty(selectedDateFilter))
            {
                LoadOwnerSalesData();
            }
        }

        private void AddedManagerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // If a different row is clicked, reset the counter
                if (lastManagerRowIndex != e.RowIndex)
                {
                    managerRowClickCount = 0;
                    lastManagerRowIndex = e.RowIndex;
                }

                managerRowClickCount++;

                if (managerRowClickCount == 3)
                {
                    AddedManagerDGV.ClearSelection();
                    AddedManagerDGV.Rows[e.RowIndex].Selected = true;

                    DataGridViewRow row = AddedManagerDGV.Rows[e.RowIndex];
                    string managerName = row.Cells["ManagerName"].Value.ToString();
                    MessageBox.Show("Selected Manager: " + managerName);

                    // Open ManagerDetails directly
                    if (row.Cells["MID"].Value != null)
                    {
                        int managerID = Convert.ToInt32(row.Cells["MID"].Value);
                        ManagerDetails details = new ManagerDetails(managerID);
                        details.Show();
                    }

                    // Reset click count after opening
                    managerRowClickCount = 0;
                }
            }
        }

        private void AddedManagerDGV_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (AddedManagerDGV.IsCurrentCellDirty)
            {
                AddedManagerDGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void AddedManagerDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && AddedManagerDGV.Columns[e.ColumnIndex].Name == "branchID")
            {
                DataGridViewRow row = AddedManagerDGV.Rows[e.RowIndex];
                if (row.Cells["Mid"].Value != DBNull.Value && row.Cells["branchID"].Value != DBNull.Value)
                {
                    int mid = Convert.ToInt32(row.Cells["Mid"].Value);
                    int newBranchID = Convert.ToInt32(row.Cells["branchID"].Value);

                    database.UpdateManagerBranch(mid, newBranchID);
                    MessageBox.Show($"Assigned Manager ID {mid} to Branch {newBranchID}");
                }
            }
        }

        private void AddedManagerDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = AddedManagerDGV.Rows[e.RowIndex];
                if (row.Cells["MID"].Value != DBNull.Value)
                {
                    int managerID = Convert.ToInt32(row.Cells["MID"].Value);
                    ManagerDetails details = new ManagerDetails(managerID);
                    details.Show();
                }
            }
        }

        private void AddedManagerDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // This method can be removed or kept empty if not needed
        }

        private void FuelStocksDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (branchesDGV.Columns[e.ColumnIndex].Name == "MID")
            {
                int branchID = Convert.ToInt32(branchesDGV.Rows[e.RowIndex].Cells["branchID"].Value);
                int newMID = Convert.ToInt32(branchesDGV.Rows[e.RowIndex].Cells["MID"].Value);

                Database database = new Database();
                database.UpdateBranchManager(branchID, newMID);

                MessageBox.Show("Manager assigned to branch successfully!");
            }
        }

        //SALES
        private void branchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable branches = database.GetAllBranches(); // You should create this method if you haven't already.

            branchesToolStripMenuItem.DropDownItems.Clear();
            foreach (DataRow row in branches.Rows)
            {
                string branchName = row["BranchName"].ToString();
                int branchID = Convert.ToInt32(row["BranchID"]);

                ToolStripMenuItem item = new ToolStripMenuItem(branchName);
                item.Tag = branchID;
                item.Click += BranchItem_Click;
                item.MouseDown += BranchItem_MouseDown;


                branchesToolStripMenuItem.DropDownItems.Add(item);
            }

        }
        private void BranchItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            selectedBranchID = (int)item.Tag;

            siticoneShimmerLabel3.Text = item.Text;

            LoadOwnerSalesData();
        }

        private void BranchItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ToolStripMenuItem branchItem = sender as ToolStripMenuItem;
                int branchID = (int)branchItem.Tag;

                // Create context menu with fuel types
                ContextMenuStrip fuelMenu = new ContextMenuStrip();

                string[] fuels = { "Diesel", "Premium", "Regular" };
                foreach (var fuel in fuels)
                {
                    ToolStripMenuItem fuelItem = new ToolStripMenuItem(fuel);
                    fuelItem.Click += (s, ev) =>
                    {
                        selectedBranchID = branchID;
                        selectedFuelType = fuel;
                        LoadFilteredFuelSales();
                    };
                    fuelMenu.Items.Add(fuelItem);
                }

                // Show the context menu at the cursor position
                fuelMenu.Show(Cursor.Position);
            }
        }

        private void LoadFilteredFuelSales()
        {
            if (selectedBranchID == -1)
                return;

            DataTable salesData = database.GetSalesByBranchAndFuel(selectedBranchID, selectedFuelType);
            OwnersalesDVG.DataSource = salesData;
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void allDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllDataSales alldata = new AllDataSales();
            alldata.Show();
        }

        private void monthlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedBranchID == -1)
            {
                MessageBox.Show("Please select a branch first.");
                return;
            }

            ToolStripMenuItem monthlyMenu = sender as ToolStripMenuItem;
            monthlyMenu.DropDownItems.Clear();

            if (string.IsNullOrEmpty(selectedDateFilter) || selectedDateFilter.Length < 4)
            {
                MessageBox.Show("Invalid date format for selectedDateFilter.");
                return;
            }

            var year = selectedDateFilter.Substring(0, 4); // Extract year (yyyy)

            try
            {
                var monthsTable = database.OwnerGetAvailableSalesMonths(selectedBranchID, year); 

                foreach (DataRow row in monthsTable.Rows)
                {
                    string month = row["SaleMonth"].ToString();
                    ToolStripMenuItem monthItem = new ToolStripMenuItem(month);
                    monthItem.Click += (s, ev) =>
                    {
                        selectedDateFilter = month;
                        isYearlyView = false;
                        LoadOwnerSalesData();
                    };
                    monthlyMenu.DropDownItems.Add(monthItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadOwnerSalesData()
        {
            if (selectedBranchID == -1 || string.IsNullOrEmpty(selectedDateFilter))
                return;

            DataTable salesData;
            if (isYearlyView)
            {
                salesData = database.GetSalesByYearForBranch(selectedBranchID, selectedDateFilter);
            }
            else
            {
                var year = selectedDateFilter.Substring(0, 4);
                var month = selectedDateFilter.Substring(5, 2);
                salesData = database.GetSalesByMonthForBranch(selectedBranchID, year, month);
            }

            OwnersalesDVG.DataSource = salesData;

            // Show line chart
            LoadLineChart();

            // Show pie chart
            LoadLitersPieChart(salesData);
        }

        private void LoadLitersPieChart(DataTable salesData)
        {
            if (salesData == null || salesData.Rows.Count == 0)
            {
                litersPieChart.Series = new List<ISeries>();
                return;
            }

            var fuelSummary = salesData.AsEnumerable()
                .GroupBy(row => row.Field<string>("FuelType"))
                .Select(group => new
                {
                    FuelType = group.Key,
                    TotalLiters = group.Sum(row => Convert.ToDouble(row["QuantityLiters"]))
                })
                .ToList();

            var colors = new Dictionary<string, SKColor>
            {
                { "Diesel", SKColors.Red },
                { "Premium", SKColors.LightGreen },
                { "Regular", SKColors.LightBlue }
            };

            var series = new List<ISeries>();
            foreach (var fuel in fuelSummary.OrderByDescending(f => f.TotalLiters))
            {
                var color = colors.ContainsKey(fuel.FuelType) ? colors[fuel.FuelType] : SKColors.Gray;
                series.Add(new PieSeries<double>
                {
                    Name = fuel.FuelType,
                    Values = new[] { fuel.TotalLiters },
                    Fill = new SolidColorPaint(color),
                    Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 2 },
                    DataLabelsSize = 14,
                    DataLabelsPaint = new SolidColorPaint(SKColors.Black)
                });
            }
            litersPieChart.Series = series;
            litersPieChart.Invalidate();
        }

        private void OwnersalesDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void yearlyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (selectedBranchID == -1)
            {
                MessageBox.Show("Please select a branch first.");
                return;
            }

            ToolStripMenuItem yearlyMenu = sender as ToolStripMenuItem;
            yearlyMenu.DropDownItems.Clear();

            var yearsTable = database.OwnerGetAvailableSalesYears(selectedBranchID); // Use method to get available years
            foreach (DataRow row in yearsTable.Rows)
            {
                string year = row["SaleYear"].ToString();
                ToolStripMenuItem yearItem = new ToolStripMenuItem(year);
                yearItem.Click += (s, ev) =>
                {
                    selectedDateFilter = year;
                    isYearlyView = true;
                    LoadOwnerSalesData();
                };
                yearlyMenu.DropDownItems.Add(yearItem);
            }
        }
        private void OwnerDashboardmenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void LoadLineChart()
        {
            if (selectedBranchID == -1 || string.IsNullOrEmpty(selectedDateFilter))
            {
                MessageBox.Show("Cannot load chart: No branch or date selected");
                return;
            }

            DataTable salesData;
            if (isYearlyView)
            {
                salesData = database.GetSalesByYearForBranch(selectedBranchID, selectedDateFilter);
            }
            else
            {
                var year = selectedDateFilter.Substring(0, 4);
                var month = selectedDateFilter.Substring(5, 2);
                salesData = database.GetSalesByMonthForBranch(selectedBranchID, year, month);
            }

            if (salesData.Rows.Count == 0)
            {
                MessageBox.Show("No sales data available for the selected period");
                return;
            }

            var groupedData = salesData.AsEnumerable()
                .GroupBy(row => row.Field<string>("FuelType"))
                .ToDictionary(
                    g => g.Key,
                    g => g.GroupBy(row =>
                    {
                        DateTime date = row.Field<DateTime>("SaleDate");
                        if (isYearlyView)
                        {
                            return date.ToString("MMM");
                        }
                        else
                        {
                            CultureInfo ci = CultureInfo.CurrentCulture;
                            return $"Week {ci.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday)}";
                        }
                    })
                    .ToDictionary(
                        grp => grp.Key,
                        grp => grp.Sum(r => Convert.ToDouble(r["TotalMoney"]))
                    )
                );

            var colors = new Dictionary<string, SKColor>
            {
                { "Diesel", SKColors.Blue },
                { "Premium", SKColors.Green },
                { "Regular", SKColors.Orange }
            };

            var labels = groupedData
                .SelectMany(f => f.Value.Keys)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            var series = new List<LineSeries<double>>();
            foreach (var fuelType in groupedData)
            {
                var values = labels.Select(label => fuelType.Value.ContainsKey(label) ? fuelType.Value[label] : 0).ToArray();
                var color = colors.ContainsKey(fuelType.Key) ? colors[fuelType.Key] : SKColors.Gray;

                series.Add(new LineSeries<double>
                {
                    Name = fuelType.Key,
                    Values = values,
                    GeometrySize = 10,
                    Stroke = new SolidColorPaint(color, 2),
                    Fill = null
                });
            }

            try
            {
                lineChart.Series = series;
                lineChart.XAxes = new[]
                {
        new Axis
        {
            Labels = labels,
            Name = isYearlyView ? "Month" : "Week",
            LabelsRotation = 15,
            TextSize = 14
        }
    };
                lineChart.YAxes = new[]
                {
        new Axis
        {
            Name = "Sales Amount (₱)",
            TextSize = 14
        }
    };

                // Force a refresh of the chart
                lineChart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating chart: {ex.Message}");
            }
        }

        private void ManageManagerPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ForgotpasBtn_Click(object sender, EventArgs e)
        {
            if (AddedManagerDGV.CurrentRow != null && AddedManagerDGV.CurrentRow.Cells["MID"].Value != null)
            {
                int managerID = Convert.ToInt32(AddedManagerDGV.CurrentRow.Cells["MID"].Value);
                MessageBox.Show("Selected ManagerID: " + managerID);

                ManagerDetails details = new ManagerDetails(managerID);
                details.Show();
            }
            else
            {
                MessageBox.Show("Please select a manager first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteManagerBtn_Click(object sender, EventArgs e)
        {
            if (AddedManagerDGV.CurrentRow == null)
            {
                MessageBox.Show("Please select a manager to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = AddedManagerDGV.CurrentRow;
            if (selectedRow.Cells["MID"].Value == null)
            {
                MessageBox.Show("Invalid manager selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int managerID = Convert.ToInt32(selectedRow.Cells["MID"].Value);
            string managerName = selectedRow.Cells["ManagerName"].Value?.ToString() ?? "Unknown Manager";

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete manager '{managerName}'? This action cannot be undone.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // First, get the branch ID if assigned
                    int? branchID = null;
                    if (selectedRow.Cells["branchID"].Value != null && selectedRow.Cells["branchID"].Value != DBNull.Value)
                    {
                        branchID = Convert.ToInt32(selectedRow.Cells["branchID"].Value);
                    }

                    // Delete from ManagerDetails table
                    using (OleDbConnection conn = new OleDbConnection(database.ConnectionString))
                    {
                        conn.Open();

                        // Delete from ManagerDetails
                        string deleteDetailsQuery = "DELETE FROM ManagerDetails WHERE MID = ?";
                        using (OleDbCommand cmd = new OleDbCommand(deleteDetailsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("?", managerID);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete from ManagerQuerry
                        string deleteQueryQuery = "DELETE FROM ManagerQuerry WHERE MID = ?";
                        using (OleDbCommand cmd = new OleDbCommand(deleteQueryQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("?", managerID);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete from Manager
                        string deleteManagerQuery = "DELETE FROM Manager WHERE MID = ?";
                        using (OleDbCommand cmd = new OleDbCommand(deleteManagerQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("?", managerID);
                            cmd.ExecuteNonQuery();
                        }

                        // If manager was assigned to a branch, update the branch to remove the manager
                        if (branchID.HasValue)
                        {
                            string updateBranchQuery = "UPDATE Branch SET MID = NULL WHERE branchID = ?";
                            using (OleDbCommand cmd = new OleDbCommand(updateBranchQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("?", branchID.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Manager deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadManagerList(); // Refresh the manager list
                }
                catch (Exception ex)
                {
                   
                    // MessageBox.Show($"Error deleting manager: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void siticoneShimmerLabel3_Click(object sender, EventArgs e)
        {
            if (selectedBranchID != -1)
            {
                DataTable branchData = database.GetAllBranches();
                var selectedBranch = branchData.AsEnumerable()
                    .FirstOrDefault(row => Convert.ToInt32(row["branchID"]) == selectedBranchID);

                if (selectedBranch != null)
                {
                    siticoneShimmerLabel3.Text = selectedBranch["branchName"].ToString();
                }
            }
        }

        private void DashBoardExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}

