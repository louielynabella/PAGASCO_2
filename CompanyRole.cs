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
    public partial class CompanyRole : Form
    {
        public CompanyRole()
        {
            InitializeComponent();
        }

        private void OwnerLoginBtn_Click(object sender, EventArgs e)
        {
            OwnerLogin ownerlogin = new OwnerLogin();
            ownerlogin.Show();
            this.Hide();
        }

        private void ManagerLoginBtn_Click(object sender, EventArgs e)
        {
            ManagerLogin managerlogin = new ManagerLogin();
            managerlogin.Show();
            this.Hide();
        }

        private void costumerBackBtn_Click(object sender, EventArgs e)
        {
           UserChoiceMenu usermenu = new UserChoiceMenu();
           usermenu.Show();
            this.Hide();
        }
    }
}
