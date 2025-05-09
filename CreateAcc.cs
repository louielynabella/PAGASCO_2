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
    public partial class CreateAcc : Form, MyInterface
    {
        private Database dbHelper;
        private Label managerNamesRegisteredLabel;
        SharedUIHelper uiHelper = new SharedUIHelper();


        public CreateAcc()
        {
            InitializeComponent();
            dbHelper = new Database();

            managerNamesRegisteredLabel = this.Controls.Find("managerNamesRegisteredLabel", true).FirstOrDefault() as Label;

            LoadManagerNames();

            // Apply shared dragging feature
            EnableDrag(this); // or a specific panel if you want
        }

        private OwnerDashboard ownerDashboard;

        public CreateAcc(OwnerDashboard dashboard)
        {
            InitializeComponent();
            this.ownerDashboard = dashboard;
            dbHelper = new Database();

            managerNamesRegisteredLabel = this.Controls.Find("managerNamesRegisteredLabel", true).FirstOrDefault() as Label;

            LoadManagerNames();

            // Apply shared dragging feature
            EnableDrag(this);
        }


        public void ExitApplication()
        {
            this.Close();
            Application.Exit();
        }

        public void GoBackToLogin()
        {
            ManagerLogin mlogin = new ManagerLogin();
            mlogin.Show();
            this.Hide();
        }

        public void EnableDrag(Control control)
        {
            control.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    SharedUIHelper.ReleaseCapture();
                    SharedUIHelper.SendMessage(this.Handle, SharedUIHelper.WM_NCLBUTTONDOWN, SharedUIHelper.HT_CAPTION, 0);
                }
            };
        }

        private Label ManagerNamesRegisteredLabel;

        private void LoadManagerNames()
        {
            try
            {
                List<string> managerNames = dbHelper.GetAllManagerNames();
                if (managerNamesRegisteredLabel != null)
                {
                    managerNamesRegisteredLabel.Text = "Registered Managers:\n" + string.Join("\n", managerNames);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading managers: " + ex.Message);
            }
        }

        private void UpdateManagerNamesLabel()
        {
            List<string> managerNames = dbHelper.GetAllManagerNames();
            if (managerNamesRegisteredLabel != null)
            {
                managerNamesRegisteredLabel.Text = "Registered Managers:\n" + string.Join("\n", managerNames);
            }
        }



        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreateExtBtn_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void createBackBtn_Click(object sender, EventArgs e)
        {
            OwnerDashboard ownerDashboard = new OwnerDashboard();
            ownerDashboard.Show();
            this.Hide();
        }


        private void createBtn_Click(object sender, EventArgs e)
        {
            // Validate all required fields
            string managerName = managerNameBtn.Text.Trim();
            string username = usernameTbx.Text.Trim();
            string password = passwordTbx.Text;
            string email = emailTbx.Text.Trim();
            string contactNumber = ContactNumberTbx.Text.Trim();
            string address = AdressTbx.Text.Trim();
            string nationality = NationalityTbx.Text.Trim();
            string degree = ManagerDegreeTbx.Text.Trim();
            string institution = ManagerSchoolNameTbx.Text.Trim();

            // Check basic manager info
            if (string.IsNullOrWhiteSpace(managerName) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all basic manager information (Name, Username, Password, Email)!", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check manager details
            if (string.IsNullOrWhiteSpace(contactNumber) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(nationality) || string.IsNullOrWhiteSpace(degree) ||
                string.IsNullOrWhiteSpace(institution))
            {
                MessageBox.Show("Please fill in all manager details (Contact Number, Address, Nationality, Degree, Institution)!", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check gender selection
            if (!MaleRbtn.Checked && !FemaleRbtn.Checked && !OthersRbtn.Checked)
            {
                MessageBox.Show("Please select a gender!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check education background
            if (EducationBackgroundCombobox.SelectedItem == null)
            {
                MessageBox.Show("Please select an education background!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check date of birth
            DateTime birthOfDate;
            if (!DateTime.TryParse(DateOfBirth.Text, out birthOfDate))
            {
                MessageBox.Show("Invalid birth date format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If all validations pass, proceed with registration
            bool success = dbHelper.RegisterManager(managerName, username, password, email);

            if (success)
            {
                // Get the MID of the newly registered manager
                int mid = dbHelper.GetManagerID(username);
                if (mid == -1)
                {
                    MessageBox.Show("Error: Could not retrieve manager ID after registration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Send the password to the manager's email
                string emailSubject = "Your Manager Login Password";
                string emailBody = $"Hello {managerName},\n\nYour temporary password is: {password}\nPlease log in and change it immediately.";

                dbHelper.SendCustomEmail(email, emailSubject, emailBody);

                string gender = MaleRbtn.Checked ? "Male" : (FemaleRbtn.Checked ? "Female" : "Others");
                string educationBackground = EducationBackgroundCombobox.SelectedItem.ToString();

                // Register manager details with the same MID
                bool detailsSuccess = dbHelper.RegisterManagerDetails(
                    mid, managerName, email, birthOfDate, gender, contactNumber, address, nationality, 
                    educationBackground, degree, institution
                );

                if (detailsSuccess)
                {
                    MessageBox.Show("Manager Registered with details!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear all fields
                    managerNameBtn.Clear();
                    usernameTbx.Clear();
                    passwordTbx.Clear();
                    emailTbx.Clear();
                    ContactNumberTbx.Clear();
                    AdressTbx.Clear();
                    NationalityTbx.Clear();
                    EducationBackgroundCombobox.SelectedIndex = -1;
                    ManagerDegreeTbx.Clear();
                    ManagerSchoolNameTbx.Clear();
                    MaleRbtn.Checked = FemaleRbtn.Checked = OthersRbtn.Checked = false;
                    DateOfBirth.Text = DateTime.Today.ToShortDateString();

                    UpdateManagerNamesLabel();

                    if (ownerDashboard != null)
                    {
                        ownerDashboard.LoadManagerList();
                    }
                }
            }
        }

        private void managerNameBtn_Click(object sender, EventArgs e)
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

        private void siticoneLabel5_Click(object sender, EventArgs e)
        {

        }

        //ManagerDetails

        private void MaleRbtn_Click(object sender, EventArgs e)
        {

        }

        private void FemaleRbtn_Click(object sender, EventArgs e)
        {

        }

        private void OthersRbtn_Click(object sender, EventArgs e)
        {

        }

        private void siticoneGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void ContactNumberTbx_Click(object sender, EventArgs e)
        {

        }

        private void AdressTbx_Click(object sender, EventArgs e)
        {

        }

        private void NationalityTbx_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Remove the message box since it's not needed
        }


        private void ManagerDegreeTbx_Click(object sender, EventArgs e)
        {

        }

        private void ManagerSchoolNameTbx_Click(object sender, EventArgs e)
        {

        }

        private void DateOfBirth_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
