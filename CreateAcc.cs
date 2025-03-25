using Microsoft.Win32;
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
    public partial class CreateAcc : Form
    {
        public CreateAcc()
        {
            InitializeComponent();

        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreateExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void createBackBtn_Click(object sender, EventArgs e)
        {
            ManagerLogin mlogin = new ManagerLogin();
            mlogin.Show();
            this.Hide();
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            string managerName = managerNameBtn.Text.Trim();
            string username = usernameTbx.Text.Trim();
            string password = passwordTbx.Text;
            string email = emailTbx.Text.Trim();

            if (string.IsNullOrWhiteSpace(managerName) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database dbHelper = new Database();

            bool success = dbHelper.RegisterManager(managerName, username, password, email);

            if (success)
            {
                MessageBox.Show("Manager Registered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Optionally clear fields:
                managerNameBtn.Clear();
                usernameTbx.Clear();
                passwordTbx.Clear();
                emailTbx.Clear();
            }
        }


        private void managerNameBtn_Click(object sender, EventArgs e)
        {

        }

        private void siticoneTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void usernameTbx_Click(object sender, EventArgs e)
        {

        }

        private void siticoneTextBox1_Click_1(object sender, EventArgs e)
        {

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
    }
}
