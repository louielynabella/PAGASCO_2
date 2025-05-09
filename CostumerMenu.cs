using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;


namespace PAGASCO
{

    public partial class CostumerMenu : Form
    {
        private Database database; // Declare the database field inside the class
        int selectedBranchID = -1;
        private string selectedFuelType = ""; // "Diesel", "Regular", or "Premium"
    

        public CostumerMenu()
        {
            InitializeComponent();
            database = new Database();
        }
        private void CostumerMenu_Load(object sender, EventArgs e)
        {
            LoadBranches();
        }
        private void LoadBranches()
        {
            DataTable dt = database.GetBranches();
            branchesComBx.DataSource = dt;
            branchesComBx.DisplayMember = "branchName";
            branchesComBx.ValueMember = "branchID";
        }

        private void CostumerExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void costumerBackBtn_Click(object sender, EventArgs e)
        {
            UserChoiceMenu choiceMenu = new UserChoiceMenu();
            choiceMenu.Show();
            this.Hide();
        }

        private void siticoneLabel1_Click(object sender, EventArgs e)
        {

        }

        private void usernameTbx_Click(object sender, EventArgs e)
        {

        }

        private void FuelCostumeracahoicesPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LitterBtn_Click(object sender, EventArgs e)
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

        private void panell_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void branchesComBx_SelectedIndexChanged(object sender, EventArgs e) //Costumerbranchesmenu
        {
            if (branchesComBx.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)branchesComBx.SelectedItem;
                selectedBranchID = Convert.ToInt32(selectedRow["branchID"]);

                // Load prices into the DataGridView
                CostumerFuelandPricesDGV.DataSource = database.GetPricesByBranchCostumer(selectedBranchID);


                // Make the DataGridView read-only
                CostumerFuelandPricesDGV.ReadOnly = true;

                // Optional: prevent user from adding or deleting rows
                CostumerFuelandPricesDGV.AllowUserToAddRows = false;
                CostumerFuelandPricesDGV.AllowUserToDeleteRows = false;

                // Optional: auto size columns for better display
                CostumerFuelandPricesDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        }


        private void LiterTotalPriceTbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void LitersTbx_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(LitersTbx.Text, out decimal liters) && liters > 0 && selectedBranchID != -1 && !string.IsNullOrEmpty(selectedFuelType))
            {
                decimal pricePerLiter = database.GetFuelPrice(selectedBranchID, selectedFuelType);
                decimal total = liters * pricePerLiter;
                LiterTotalPriceTbx.Text = "₱" + total.ToString("N2");
            }
            else
            {
                LiterTotalPriceTbx.Text = "₱0.00";
            }
        }


        private void AppendToLitersTextBox(string value)
        {
            LitersTbx.Text += value;
        }

        private void ZeroBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("0");
        }

        private void oneBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("1");
        }

        private void twoBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("2");
        }

        private void threeBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("3");
        }

        private void fourBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("4");
        }

        private void fiveBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("5");
        }

        private void sixBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("6");
        }

        private void sevenBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("7");
        }

        private void eightBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("8");
        }

        private void nineBtn_Click(object sender, EventArgs e)
        {
            AppendToLitersTextBox("9");
        }

        private void dotBtn_Click(object sender, EventArgs e)
        {
            // Optional: prevent more than one dot
            if (!LitersTbx.Text.Contains("."))
            {
                AppendToLitersTextBox(".");
            }
        }

        private void backspacedeleteBtn_Click(object sender, EventArgs e)
        {
            if (LitersTbx.Text.Length > 0)
            {
                LitersTbx.Text = LitersTbx.Text.Substring(0, LitersTbx.Text.Length - 1);
            }
        }


        private void OrderBtn_Click(object sender, EventArgs e)
        {
            if (selectedBranchID == -1 || string.IsNullOrEmpty(selectedFuelType))
            {
                MessageBox.Show("Please select a branch and a fuel type.");
                return;
            }

            if (!decimal.TryParse(LitersTbx.Text, out decimal litersOrdered) || litersOrdered <= 0)
            {
                MessageBox.Show("Please enter a valid number of liters.");
                return;
            }

            // Get the price from database
            decimal pricePerLiter = database.GetFuelPrice(selectedBranchID, selectedFuelType);
            decimal totalCost = litersOrdered * pricePerLiter;

            // Open payment form with these actual values
            CostumerPayment paymentForm = new CostumerPayment(selectedFuelType, litersOrdered, totalCost, selectedBranchID);
            this.Hide(); // Hide the CostumerMenu form
            paymentForm.ShowDialog();
            this.Show(); // Show the CostumerMenu form again when payment form is closed

            // Optional: Refresh the DataGridView with updated stock
            CostumerFuelandPricesDGV.DataSource = database.GetPricesByBranchCostumer(selectedBranchID);
        }



        private void siticonePanel1_Paint(object sender, EventArgs e)
        {

        }

        //RADIO BUTTONS
        private void radioButton1_CheckedChanged(object sender, EventArgs e)//DieselRadioButton
        {
            if (((RadioButton)sender).Checked)
                selectedFuelType = "Diesel";
        }
        private void RegularRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                selectedFuelType = "Regular";
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) //PremiumRadioButton
        {
            if (((RadioButton)sender).Checked)
                selectedFuelType = "Premium";

        }
        private void selectFuelBtn_Click(object sender, EventArgs e)
        {
            if (selectedBranchID == -1 || string.IsNullOrEmpty(selectedFuelType))
            {
                MessageBox.Show("Please select a branch and a fuel type.");
                return;
            }

            if (!decimal.TryParse(LitersTbx.Text, out decimal liters) || liters <= 0)
            {
                MessageBox.Show("Enter a valid number of liters.");
                return;
            }

            // Get price per liter of selected fuel at this branch
            decimal pricePerLiter = database.GetFuelPrice(selectedBranchID, selectedFuelType);
            decimal totalCost = pricePerLiter * liters;

            // Try deduct stock
            bool success = database.DeductFuelStock(selectedBranchID, selectedFuelType, liters);

            if (success)
            {
                // Record sale
                database.InsertSale(selectedBranchID, selectedFuelType, liters, totalCost, DateTime.Now);

                MessageBox.Show($"{liters}L of {selectedFuelType} sold for ₱{totalCost:N2}.");

                // Refresh DataGridView
                CostumerFuelandPricesDGV.DataSource = database.GetPricesByBranchCostumer(selectedBranchID);
            }
            else
            {
                MessageBox.Show("Not enough stock available.");
            }
        }



        private void CostumerFuelandPricesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
