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
    public partial class CostumerReceipt : Form
    {
        public CostumerReceipt()
        {
            InitializeComponent();
        }

        public CostumerReceipt(string fuelType, decimal liters, decimal totalCost, decimal payment, decimal change, int branchId)
        {
            InitializeComponent();

            // Get branch name from database
            string branchName = $"Branch {branchId}";
            try
            {
                var db = new Database();
                var branchDetails = db.GetBranchDetails(branchId);
                if (branchDetails != null && branchDetails.ContainsKey("BranchName"))
                {
                    branchName = branchDetails["BranchName"].ToString();
                }
            }
            catch { /* fallback to default if error */ }

            RBranchTbx.Text = branchName;
            RFuelTypeTbx.Text = fuelType;
            RLiterPurchasedTbx.Text = liters.ToString("N2");
            RPricePerLiterTbx.Text = (totalCost / liters).ToString("C"); // calculate price per liter
            RTotalCostTbx.Text = totalCost.ToString("C");
            RPaymentTbx.Text = payment.ToString("C");
            RChangeTbx.Text = change.ToString("C");
        }




        private void siticonePanel2_Paint(object sender, PaintEventArgs e)
        {

        }



        private void siticoneLabel9_Click(object sender, EventArgs e)
        {

        }

        private void siticoneLabel8_Click(object sender, EventArgs e)
        {

        }

        private void RBranchTbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void RFuelTypeTbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void RLiterPurchasedTbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void RPricePerLiterTbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void RTotalCostTbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void RPaymentTbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void RChangeTbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void PagascoLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
