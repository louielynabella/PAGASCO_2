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
using System.Data.OleDb;

namespace PAGASCO
{
    public partial class ManagerDashboard : Form
    {
        private Database database; // Declare the database field inside the class

        bool sidebarExpand = true;

        public ManagerDashboard()
        {
            InitializeComponent();
            database = new Database(); // Initialize the database object in the constructor
        }

        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 37)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
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

        private void loginBtn_Click(object sender, EventArgs e) //branchbtn
        {
            branchPanel.BringToFront();
            sidebar.BringToFront();
        }

        private void branchesComBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected branch name
                string selectedBranch = branchesComBx.SelectedItem.ToString();

                // Fetch the branchID for the selected branch
                int branchID = database.GetBranchID(selectedBranch);

                if (branchID != -1) // Ensure a valid branchID was found
                {
                    // Fetch fuel stocks for the selected branch using the Database class
                    DataTable fuelStocks = database.GetFuelStocks(branchID);

                    // Bind the DataTable to the DataGridView
                    FuelStocksDGV.DataSource = fuelStocks;
                }
                else
                {
                    MessageBox.Show("Branch ID not found for the selected branch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading fuel stocks: " + ex.Message);
            }
        }

        private void AddFuelTbx_Click(object sender, EventArgs e)
        {
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
        }

        private void FuelStocksDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DashBoardExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void iconButton1_Click(object sender, EventArgs e)  //addstockBtn
        {
        }

        private void addbranchBtn_Click(object sender, EventArgs e)
        {
        }

        private void SaveStockBtn_Click(object sender, EventArgs e)
        {
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
                ManagerLogin managerlogin = new ManagerLogin();
                managerlogin.ShowDialog();
                this.Close();
            }
        }

        private void SalesBtn_Click(object sender, EventArgs e)
        {
            SalesPanel.BringToFront();
            sidebar.BringToFront();
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

        private void DateSalesCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void AboutPGSCBtn_Click(object sender, EventArgs e)
        {
            AboutUsPanel.BringToFront();
            sidebar.BringToFront();
        }

        private void AccountSetPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void AccSettingsBtn_Click(object sender, EventArgs e)
        {
            AccountSetPanel.BringToFront();
            sidebar.BringToFront();
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

        private void usernameTbx_Click(object sender, EventArgs e)
        {
        }

        private void ManagerDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                // Fetch branch names using the Database class
                List<string> branchNames = database.GetBranchNames();

                // Populate the ComboBox with branch names
                foreach (string branchName in branchNames)
                {
                    branchesComBx.Items.Add(branchName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading branches: " + ex.Message);
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
}