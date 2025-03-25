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
    public partial class ManagerLogin : Form
    {
        public ManagerLogin()
        {
            InitializeComponent();
        }

        private void siticoneLabel2_Click(object sender, EventArgs e)
        {

        }

        private void siticoneTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ManagerLogin_Load(object sender, EventArgs e)
        {

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

            bool loginSuccess = dbHelper.ValidateLogin(username, password);

            if (loginSuccess)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ManagerDashboard dashboard = new ManagerDashboard();
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void siticoneLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void ManagerLabel_Click(object sender, EventArgs e)
        {

        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void createaccBtn_Click(object sender, EventArgs e)
        {
            CreateAcc createacc = new CreateAcc();
            createacc.Show();
            this.Hide();
        }

        private void LoginExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void LoginBackBtn_Click(object sender, EventArgs e)
        {
            UserChoiceMenu choiceMenu = new UserChoiceMenu();
            choiceMenu.Show();
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
    }
}
