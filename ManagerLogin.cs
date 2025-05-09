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

namespace PAGASCO
{
    public partial class ManagerLogin : Form, MyInterface
    {
        private SharedUIHelper uiHelper = new SharedUIHelper();

        public ManagerLogin()
        {
            InitializeComponent();
        }
        private void siticoneTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ManagerLogin_Load(object sender, EventArgs e)
        {
            try
            {
                Database db = new Database();
                var managersWithBranches = db.GetManagersWithBranches();
                
                // Build the display text with manager names and their branches
                var displayLines = managersWithBranches.Select(m => $"{m.managerName} - {m.branchName}");
                ManagerNamesRegisteredLabel.Text = "Available Managers:\n" + string.Join("\n", displayLines);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading manager list: " + ex.Message);
            }
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            string username = usernameTbx.Text.Trim(); // Your username TextBox
            string password = passwordTbx.Text.Trim(); // Your password TextBox

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database dbHelper = new Database();

            int managerID = dbHelper.ValidateLogin(username, password);

            if (managerID != -1)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ManagerDashboard dashboard = new ManagerDashboard(managerID);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void LoginExtBtn_Click(object sender, EventArgs e)
        {
            ExitApplication(); // Now uses interface method
        }

        private void LoginBackBtn_Click(object sender, EventArgs e)
        {
           CompanyRole companyrole = new CompanyRole();
            companyrole.Show();
            this.Hide();
        }

        private void ForgotpasBtn_Click(object sender, EventArgs e)
        {
            VerifyManager verifyManagerForm = new VerifyManager();
            verifyManagerForm.Show();
            this.Hide(); // Optional: hide current form if you don't want both open
        }

        private void passwordTbx_Click(object sender, EventArgs e)
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

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ManagerNamesRegisteredLabel_Click(object sender, EventArgs e)
        {

        }

        private void MANAGERS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Interface Methods
        public void ExitApplication()
        {
            uiHelper.ExitApplication(this);
        }

        public void GoBackToLogin()
        {
            uiHelper.GoBackToLogin(this);
        }

        public void EnableDrag(Control control)
        {
            uiHelper.EnableDrag(control, this);
        }
    }
}
