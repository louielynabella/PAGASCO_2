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
    public partial class ForgotPassword : Form
    {
        private string currentUsername;

        public ForgotPassword(string username = "")
        {
            InitializeComponent();
            currentUsername = username;
        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NewPassTbx_Click(object sender, EventArgs e)
        {

        }

        private void UpdatePassForgotBtn_Click(object sender, EventArgs e)
        {
            string newPassword = NewPassTbx.Text.Trim();
            string confirmPassword = confirmPassTbx.Text.Trim();

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all password fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(currentUsername))
            {
                MessageBox.Show("Username is not set. Please verify your account first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database dbHelper = new Database();
            bool updated = dbHelper.UpdatePassword(currentUsername, newPassword);

            if (updated)
            {
                MessageBox.Show("Password successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // After successful password change, go back to login form
                ManagerLogin loginForm = new ManagerLogin();
                loginForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to update password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void confirmPassTbx_Click(object sender, EventArgs e)
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

        private void ForgotPassBackBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
