using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAGASCO
{
    public partial class OwnerLogin : Form
    {
        public OwnerLogin()
        {
            InitializeComponent();
            // Set password character to dot
            passwordTbx.PasswordChar = '•';
        }

        private void usernameTbx_Click(object sender, EventArgs e)
        {
            // Clear text when clicked
            if (usernameTbx.Text == "Username")
            {
                usernameTbx.Text = "";
            }
        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void passwordTbx_Click(object sender, EventArgs e)
        {
            // Clear text when clicked
            if (passwordTbx.Text == "Password")
            {
                passwordTbx.Text = "";
                passwordTbx.PasswordChar = '•';
            }
        }



        private void loginBtn_Click_1(object sender, EventArgs e)
        {
            string username = usernameTbx.Text;
            string password = passwordTbx.Text;

            if (username == "PAGASCO_Wayen" && password == "abellat")
            {
                // Login successful
                OwnerDashboard ownerDashboard = new OwnerDashboard();
                ownerDashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginBackBtn_Click(object sender, EventArgs e)
        {
            CompanyRole companyrole = new CompanyRole();
            companyrole.Show();
            this.Hide();
        }
    }
}
