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
    public partial class VerifyManager : Form
    {
        private string currentUsername = "";

        public VerifyManager()
        {
            InitializeComponent();
        }

        private void codeBtn_Click(object sender, EventArgs e)
        {
            string username = registerdEmailTbx.Text.Trim(); // TextBox for username input
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter your username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database dbHelper = new Database();
            bool sent = dbHelper.StartVerification(username);
            if (sent)
            {
                currentUsername = username;
                MessageBox.Show("Verification code sent to your registered email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void registerdEmailTbx_Click(object sender, EventArgs e)
        {
            // Optional: You can leave this empty
        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {
            // Optional: You can leave this empty
        }

        private void CodeTbx_Click_1(object sender, EventArgs e)
        {
            // Do nothing here, logic will be on the buttons
        }



        private void verifyBtn_Click(object sender, EventArgs e)
        {
            string code = CodeTbx.Text.Trim(); // TextBox for code input

            if (string.IsNullOrEmpty(currentUsername))
            {
                MessageBox.Show("Please request a verification code first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database dbHelper = new Database();
            bool verified = dbHelper.VerifyCode(currentUsername, code);
            if (verified)
            {
                // Proceed to reset password
                MessageBox.Show("Code verified! You can now reset your password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Pass username to ForgotPassword
                ForgotPassword resetForm = new ForgotPassword(currentUsername);
                resetForm.Show();
                this.Hide();
            }
        }


        private void VerifyExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void verifyBackBtn_Click(object sender, EventArgs e)
        {
            ManagerLogin mlogin = new ManagerLogin();
            mlogin.Show();
            this.Hide();
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
