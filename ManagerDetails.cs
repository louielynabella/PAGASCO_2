using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PAGASCO
{
    public partial class ManagerDetails : Form
    {
        private Database database;
        private int managerID;

        public ManagerDetails(int mid)
        {
            InitializeComponent();
            database = new Database();

            managerID = mid;
            LoadManagerDetails(managerID);
        }

        private void LoadManagerDetails(int mid)
        {
            try
            {
                // Get details from ManagerQuery table first
                var queryDetails = database.GetManagerQueryDetails(mid);
                if (queryDetails == null)
                {
                    MessageBox.Show($"No manager found with ID: {mid}");
                    return;
                }

                // Get details from ManagerDetails table
                var details = database.GetManagerDetailsFromTable(mid);

                // Get branch details if branchID exists
                Dictionary<string, object> branchDetails = null;
                if (queryDetails.ContainsKey("branchID") && queryDetails["branchID"] != DBNull.Value)
                {
                    int branchID = Convert.ToInt32(queryDetails["branchID"]);
                    branchDetails = database.GetBranchDetails(branchID);
                }

                // Set basic information from ManagerQuery
                ManagerNameLabel.Text = "Manager Name: " + (queryDetails.TryGetValue("ManagerName", out object nameValue)
                    ? nameValue?.ToString() ?? "N/A" : "N/A");
                EmailLabel.Text = "Email: " + (queryDetails.TryGetValue("Email", out object emailValue)
                    ? emailValue?.ToString() ?? "N/A" : "N/A");

                // Set detailed information from ManagerDetails if available
                if (details != null)
                {
                    BirthDateLabel.Text = "Birth Date: " + (
                        details.TryGetValue("BirthOfDate", out object birthValue) && birthValue != DBNull.Value
                        ? Convert.ToDateTime(birthValue).ToShortDateString() : "N/A"
                    );
                    GenderLabel.Text = "Gender: " + (details.TryGetValue("Gender", out object genderValue)
                        ? genderValue?.ToString() ?? "N/A" : "N/A");
                    NationalityLabel.Text = "Nationality: " + (details.TryGetValue("Nationality", out object nationValue)
                        ? nationValue?.ToString() ?? "N/A" : "N/A");
                    AddressLabel.Text = "Address: " + (details.TryGetValue("Adress", out object addrValue)
                        ? addrValue?.ToString() ?? "N/A" : "N/A");
                    EducationBackgroundLabel.Text = "Education Background: " + (details.TryGetValue("EducationBackGround", out object eduValue)
                        ? eduValue?.ToString() ?? "N/A" : "N/A");
                    DegreeLabel.Text = "Degree: " + (details.TryGetValue("Degree", out object degreeValue)
                        ? degreeValue?.ToString() ?? "N/A" : "N/A");
                    InstituitionUniversityLabel.Text = "Institution/University: " + (details.TryGetValue("Institution/Univeresity", out object instValue)
                        ? instValue?.ToString() ?? "N/A" : "N/A");
                    ContactNumberLabel.Text = "Contact Number: " + (details.TryGetValue("ContactNumber", out object contactValue)
                        ? contactValue?.ToString() ?? "N/A" : "N/A");
                }
                else
                {
                    BirthDateLabel.Text = "Birth Date: N/A";
                    GenderLabel.Text = "Gender: N/A";
                    NationalityLabel.Text = "Nationality: N/A";
                    AddressLabel.Text = "Address: N/A";
                    EducationBackgroundLabel.Text = "Education Background: N/A";
                    DegreeLabel.Text = "Degree: N/A";
                    InstituitionUniversityLabel.Text = "Institution/University: N/A";
                    ContactNumberLabel.Text = "Contact Number: N/A";
                }

                // Set branch information
                BranchLabel.Text = "Branch: " + (branchDetails != null && branchDetails.TryGetValue("BranchName", out object branchValue)
                    ? branchValue?.ToString() ?? "No Branch Assigned" : "No Branch Assigned");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading manager details: {ex.Message}\n{ex.StackTrace}");
            }
        }


        private void siticoneLabel6_Click(object sender, EventArgs e) //contactnumber
        {

        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ManagerNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void EmailLabel_Click(object sender, EventArgs e)
        {

        }

        private void BirthDateLabel_Click(object sender, EventArgs e)
        {

        }

        private void GenderLabel_Click(object sender, EventArgs e)
        {

        }

        private void NationalityLabel_Click(object sender, EventArgs e)
        {

        }

        private void AddressLabel_Click(object sender, EventArgs e)
        {

        }

        private void EducationBackgroundLabel_Click(object sender, EventArgs e)
        {

        }

        private void DegreeLabel_Click(object sender, EventArgs e)
        {

        }

        private void InstituitionUniversityLabel_Click(object sender, EventArgs e)
        {

        }

        private void BranchLabel_Click(object sender, EventArgs e)
        {

        }

        private void LoginBackBtn_Click(object sender, EventArgs e)
        {
            OwnerDashboard ownerdashboard = new OwnerDashboard();
           ownerdashboard.Show();
            this.Hide();
        }
    }
}
