using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAGASCO
{
    public partial class CostumerPayment : Form
    {
        public CostumerPayment()
        {
            InitializeComponent();
        }

        private string fuelType;
        private decimal liters { get; set; }
        private decimal totalCost;
        private int branchID;
      
        private System.Windows.Forms.TextBox LitersTbx;





        public CostumerPayment(string fuelType, decimal liters, decimal totalCost, int branchID)
        {
            InitializeComponent();

            this.fuelType = fuelType;
            this.liters = liters;
            this.totalCost = totalCost;
            this.branchID = branchID;

            
       
       
            TotalCostTbx.Text = totalCost.ToString("C");
        }

        private void siticoneLabel2_Click(object sender, EventArgs e)
        {

        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TotalCostTbx_Click(object sender, EventArgs e)
        {

        }

        private void PaymentTbx_Click(object sender, EventArgs e)
        {

        }

        private void PaymentTbx_Click_1(object sender, EventArgs e)
        {

        }

        private void PayBtn_Click(object sender, EventArgs e)
        {
            decimal paymentAmount;
            if (!decimal.TryParse(PaymentTbx.Text, out paymentAmount))
            {
                MessageBox.Show("Invalid payment amount.");
                return;
            }

            if (paymentAmount < totalCost)
            {
                MessageBox.Show("Insufficient payment.");
                return;
            }

            decimal change = paymentAmount - totalCost;

            decimal litersToDeduct = liters;
            // Deduct fuel from file/database (you can replace this with real DB logic)
            //DeductFuelFromDatabase(fuelType, liters, branchID);
            Database db = new Database();
            db.DeductFuelStock(branchID, fuelType, litersToDeduct);
            db.InsertSale(branchID, fuelType, liters, totalCost, DateTime.Now);

            // Proceed to receipt
            CostumerReceipt receiptForm = new CostumerReceipt(
                fuelType,
                liters,
                totalCost,
                paymentAmount,
                change,
                branchID
            );

            this.Hide(); // Hide the payment form
            receiptForm.ShowDialog();
            this.Close(); // Close payment form after receipt is closed
        }

      /*  private void DeductFuelFromDatabase(string fuelType, decimal litersUsed, int branchId)
        {
            // Example placeholder
            // In a real system, you'd update the fuel stock in your file or database
            MessageBox.Show($"Fuel '{fuelType}' deducted by {litersUsed} liters for Branch {branchId}");
        }*/



        private void CancelPurchaseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
