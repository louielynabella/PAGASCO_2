namespace PAGASCO
{
    partial class CostumerReceipt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CostumerReceipt));
            pictureBox1 = new PictureBox();
            PagascoLabel = new SiticoneNetCoreUI.SiticoneShimmerLabel();
            siticoneLabel1 = new SiticoneNetCoreUI.SiticoneLabel();
            siticoneLabel2 = new SiticoneNetCoreUI.SiticoneLabel();
            siticoneLabel3 = new SiticoneNetCoreUI.SiticoneLabel();
            siticoneLabel4 = new SiticoneNetCoreUI.SiticoneLabel();
            siticoneLabel5 = new SiticoneNetCoreUI.SiticoneLabel();
            siticoneLabel6 = new SiticoneNetCoreUI.SiticoneLabel();
            siticonePanel2 = new SiticoneNetCoreUI.SiticonePanel();
            RChangeTbx = new TextBox();
            RPaymentTbx = new TextBox();
            RTotalCostTbx = new TextBox();
            RPricePerLiterTbx = new TextBox();
            RLiterPurchasedTbx = new TextBox();
            RFuelTypeTbx = new TextBox();
            RBranchTbx = new TextBox();
            siticoneLabel9 = new SiticoneNetCoreUI.SiticoneLabel();
            siticoneLabel8 = new SiticoneNetCoreUI.SiticoneLabel();
            siticoneLabel7 = new SiticoneNetCoreUI.SiticoneLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            siticonePanel2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(488, 577);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // PagascoLabel
            // 
            PagascoLabel.AutoReverse = false;
            PagascoLabel.BackColor = Color.FromArgb(244, 241, 222);
            PagascoLabel.BaseColor = Color.FromArgb(38, 70, 83);
            PagascoLabel.Direction = SiticoneNetCoreUI.ShimmerDirection.LeftToRight;
            PagascoLabel.Font = new Font("Impact", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PagascoLabel.IsAnimating = true;
            PagascoLabel.IsPaused = false;
            PagascoLabel.Location = new Point(192, 23);
            PagascoLabel.Margin = new Padding(4);
            PagascoLabel.Name = "PagascoLabel";
            PagascoLabel.PauseDuration = 0;
            PagascoLabel.ShimmerColor = Color.FromArgb(231, 111, 81);
            PagascoLabel.ShimmerOpacity = 1F;
            PagascoLabel.ShimmerSpeed = 30;
            PagascoLabel.ShimmerWidth = 0.5F;
            PagascoLabel.Size = new Size(379, 110);
            PagascoLabel.TabIndex = 14;
            PagascoLabel.Text = "PAGAS.CO";
            PagascoLabel.ToolTipText = "";
            PagascoLabel.Click += PagascoLabel_Click;
            // 
            // siticoneLabel1
            // 
            siticoneLabel1.BackColor = Color.Transparent;
            siticoneLabel1.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneLabel1.ForeColor = Color.FromArgb(231, 111, 81);
            siticoneLabel1.Location = new Point(188, 132);
            siticoneLabel1.Margin = new Padding(4, 0, 4, 0);
            siticoneLabel1.Name = "siticoneLabel1";
            siticoneLabel1.Size = new Size(426, 36);
            siticoneLabel1.TabIndex = 15;
            siticoneLabel1.Text = "Salamat, Balik-Balik Lang!";
            // 
            // siticoneLabel2
            // 
            siticoneLabel2.BackColor = Color.Transparent;
            siticoneLabel2.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneLabel2.ForeColor = Color.FromArgb(38, 70, 83);
            siticoneLabel2.Location = new Point(164, 183);
            siticoneLabel2.Margin = new Padding(4, 0, 4, 0);
            siticoneLabel2.Name = "siticoneLabel2";
            siticoneLabel2.Size = new Size(145, 31);
            siticoneLabel2.TabIndex = 16;
            siticoneLabel2.Text = "RECEIPT";
            // 
            // siticoneLabel3
            // 
            siticoneLabel3.BackColor = Color.Transparent;
            siticoneLabel3.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneLabel3.ForeColor = Color.FromArgb(38, 70, 83);
            siticoneLabel3.Location = new Point(392, 200);
            siticoneLabel3.Margin = new Padding(4, 0, 4, 0);
            siticoneLabel3.Name = "siticoneLabel3";
            siticoneLabel3.Size = new Size(96, 31);
            siticoneLabel3.TabIndex = 17;
            siticoneLabel3.Text = "Branch: ";
            // 
            // siticoneLabel4
            // 
            siticoneLabel4.BackColor = Color.Transparent;
            siticoneLabel4.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneLabel4.ForeColor = Color.FromArgb(38, 70, 83);
            siticoneLabel4.Location = new Point(361, 244);
            siticoneLabel4.Margin = new Padding(4, 0, 4, 0);
            siticoneLabel4.Name = "siticoneLabel4";
            siticoneLabel4.Size = new Size(127, 31);
            siticoneLabel4.TabIndex = 18;
            siticoneLabel4.Text = "Fuel Type:";
            // 
            // siticoneLabel5
            // 
            siticoneLabel5.BackColor = Color.Transparent;
            siticoneLabel5.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneLabel5.ForeColor = Color.FromArgb(38, 70, 83);
            siticoneLabel5.Location = new Point(290, 286);
            siticoneLabel5.Margin = new Padding(4, 0, 4, 0);
            siticoneLabel5.Name = "siticoneLabel5";
            siticoneLabel5.Size = new Size(198, 31);
            siticoneLabel5.TabIndex = 19;
            siticoneLabel5.Text = "Liters Purchased:";
            // 
            // siticoneLabel6
            // 
            siticoneLabel6.BackColor = Color.Transparent;
            siticoneLabel6.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneLabel6.ForeColor = Color.FromArgb(38, 70, 83);
            siticoneLabel6.Location = new Point(313, 325);
            siticoneLabel6.Margin = new Padding(4, 0, 4, 0);
            siticoneLabel6.Name = "siticoneLabel6";
            siticoneLabel6.Size = new Size(175, 31);
            siticoneLabel6.TabIndex = 20;
            siticoneLabel6.Text = "Price Per Liter:";
            // 
            // siticonePanel2
            // 
            siticonePanel2.AcrylicTintColor = Color.FromArgb(128, 255, 255, 255);
            siticonePanel2.BackColor = Color.Transparent;
            siticonePanel2.BorderAlignment = System.Drawing.Drawing2D.PenAlignment.Center;
            siticonePanel2.BorderDashPattern = null;
            siticonePanel2.BorderGradientEndColor = Color.Purple;
            siticonePanel2.BorderGradientStartColor = Color.Blue;
            siticonePanel2.BorderThickness = 2F;
            siticonePanel2.Controls.Add(RChangeTbx);
            siticonePanel2.Controls.Add(RPaymentTbx);
            siticonePanel2.Controls.Add(RTotalCostTbx);
            siticonePanel2.Controls.Add(RPricePerLiterTbx);
            siticonePanel2.Controls.Add(RLiterPurchasedTbx);
            siticonePanel2.Controls.Add(RFuelTypeTbx);
            siticonePanel2.Controls.Add(RBranchTbx);
            siticonePanel2.Controls.Add(siticoneLabel9);
            siticonePanel2.Controls.Add(siticoneLabel8);
            siticonePanel2.Controls.Add(siticoneLabel7);
            siticonePanel2.Controls.Add(siticoneLabel6);
            siticonePanel2.Controls.Add(siticoneLabel5);
            siticonePanel2.Controls.Add(siticoneLabel4);
            siticonePanel2.Controls.Add(siticoneLabel3);
            siticonePanel2.Controls.Add(siticoneLabel2);
            siticonePanel2.Controls.Add(siticoneLabel1);
            siticonePanel2.Controls.Add(PagascoLabel);
            siticonePanel2.Controls.Add(pictureBox1);
            siticonePanel2.CornerRadiusBottomLeft = 0F;
            siticonePanel2.CornerRadiusBottomRight = 0F;
            siticonePanel2.CornerRadiusTopLeft = 0F;
            siticonePanel2.CornerRadiusTopRight = 0F;
            siticonePanel2.Dock = DockStyle.Fill;
            siticonePanel2.EnableAcrylicEffect = false;
            siticonePanel2.EnableMicaEffect = false;
            siticonePanel2.EnableRippleEffect = false;
            siticonePanel2.FillColor = Color.FromArgb(244, 241, 222);
            siticonePanel2.GradientColors = new Color[]
    {
    Color.White,
    Color.LightGray,
    Color.Gray
    };
            siticonePanel2.GradientPositions = new float[]
    {
    0F,
    0.5F,
    1F
    };
            siticonePanel2.Location = new Point(0, 0);
            siticonePanel2.Margin = new Padding(4);
            siticonePanel2.Name = "siticonePanel2";
            siticonePanel2.PatternStyle = System.Drawing.Drawing2D.HatchStyle.Max;
            siticonePanel2.RippleAlpha = 50;
            siticonePanel2.RippleAlphaDecrement = 3;
            siticonePanel2.RippleColor = Color.FromArgb(50, 255, 255, 255);
            siticonePanel2.RippleMaxSize = 600F;
            siticonePanel2.RippleSpeed = 15F;
            siticonePanel2.ShowBorder = true;
            siticonePanel2.Size = new Size(844, 578);
            siticonePanel2.TabIndex = 6;
            siticonePanel2.TabStop = true;
            siticonePanel2.UseBorderGradient = false;
            siticonePanel2.UseMultiGradient = false;
            siticonePanel2.UsePatternTexture = false;
            siticonePanel2.UseRadialGradient = false;
            siticonePanel2.Paint += siticonePanel2_Paint;
            // 
            // RChangeTbx
            // 
            RChangeTbx.Location = new Point(495, 454);
            RChangeTbx.Name = "RChangeTbx";
            RChangeTbx.Size = new Size(150, 31);
            RChangeTbx.TabIndex = 30;
            RChangeTbx.TextChanged += RChangeTbx_TextChanged;
            // 
            // RPaymentTbx
            // 
            RPaymentTbx.Location = new Point(495, 408);
            RPaymentTbx.Name = "RPaymentTbx";
            RPaymentTbx.Size = new Size(150, 31);
            RPaymentTbx.TabIndex = 29;
            RPaymentTbx.TextChanged += RPaymentTbx_TextChanged;
            // 
            // RTotalCostTbx
            // 
            RTotalCostTbx.Location = new Point(495, 368);
            RTotalCostTbx.Name = "RTotalCostTbx";
            RTotalCostTbx.Size = new Size(150, 31);
            RTotalCostTbx.TabIndex = 28;
            RTotalCostTbx.TextChanged += RTotalCostTbx_TextChanged;
            // 
            // RPricePerLiterTbx
            // 
            RPricePerLiterTbx.Location = new Point(495, 323);
            RPricePerLiterTbx.Name = "RPricePerLiterTbx";
            RPricePerLiterTbx.Size = new Size(150, 31);
            RPricePerLiterTbx.TabIndex = 27;
            RPricePerLiterTbx.TextChanged += RPricePerLiterTbx_TextChanged;
            // 
            // RLiterPurchasedTbx
            // 
            RLiterPurchasedTbx.Location = new Point(495, 286);
            RLiterPurchasedTbx.Name = "RLiterPurchasedTbx";
            RLiterPurchasedTbx.Size = new Size(150, 31);
            RLiterPurchasedTbx.TabIndex = 26;
            RLiterPurchasedTbx.TextChanged += RLiterPurchasedTbx_TextChanged;
            // 
            // RFuelTypeTbx
            // 
            RFuelTypeTbx.Location = new Point(495, 244);
            RFuelTypeTbx.Name = "RFuelTypeTbx";
            RFuelTypeTbx.Size = new Size(150, 31);
            RFuelTypeTbx.TabIndex = 25;
            RFuelTypeTbx.TextChanged += RFuelTypeTbx_TextChanged;
            // 
            // RBranchTbx
            // 
            RBranchTbx.Location = new Point(495, 200);
            RBranchTbx.Name = "RBranchTbx";
            RBranchTbx.Size = new Size(150, 31);
            RBranchTbx.TabIndex = 24;
            RBranchTbx.TextChanged += RBranchTbx_TextChanged;
            // 
            // siticoneLabel9
            // 
            siticoneLabel9.BackColor = Color.Transparent;
            siticoneLabel9.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneLabel9.ForeColor = Color.FromArgb(38, 70, 83);
            siticoneLabel9.Location = new Point(384, 454);
            siticoneLabel9.Margin = new Padding(4, 0, 4, 0);
            siticoneLabel9.Name = "siticoneLabel9";
            siticoneLabel9.Size = new Size(104, 31);
            siticoneLabel9.TabIndex = 23;
            siticoneLabel9.Text = "Change:";
            siticoneLabel9.Click += siticoneLabel9_Click;
            // 
            // siticoneLabel8
            // 
            siticoneLabel8.BackColor = Color.Transparent;
            siticoneLabel8.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneLabel8.ForeColor = Color.FromArgb(38, 70, 83);
            siticoneLabel8.Location = new Point(374, 410);
            siticoneLabel8.Margin = new Padding(4, 0, 4, 0);
            siticoneLabel8.Name = "siticoneLabel8";
            siticoneLabel8.Size = new Size(114, 31);
            siticoneLabel8.TabIndex = 22;
            siticoneLabel8.Text = "Payment:";
            siticoneLabel8.Click += siticoneLabel8_Click;
            // 
            // siticoneLabel7
            // 
            siticoneLabel7.BackColor = Color.Transparent;
            siticoneLabel7.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneLabel7.ForeColor = Color.FromArgb(38, 70, 83);
            siticoneLabel7.Location = new Point(356, 368);
            siticoneLabel7.Margin = new Padding(4, 0, 4, 0);
            siticoneLabel7.Name = "siticoneLabel7";
            siticoneLabel7.Size = new Size(132, 31);
            siticoneLabel7.TabIndex = 21;
            siticoneLabel7.Text = "Total Cost:";
            // 
            // CostumerReceipt
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 578);
            Controls.Add(siticonePanel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CostumerReceipt";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CostumerReceipt";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            siticonePanel2.ResumeLayout(false);
            siticonePanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private SiticoneNetCoreUI.SiticoneShimmerLabel PagascoLabel;
        private SiticoneNetCoreUI.SiticoneLabel siticoneLabel1;
        private SiticoneNetCoreUI.SiticoneLabel siticoneLabel2;
        private SiticoneNetCoreUI.SiticoneLabel siticoneLabel3;
        private SiticoneNetCoreUI.SiticoneLabel siticoneLabel4;
        private SiticoneNetCoreUI.SiticoneLabel siticoneLabel5;
        private SiticoneNetCoreUI.SiticoneLabel siticoneLabel6;
        private SiticoneNetCoreUI.SiticonePanel siticonePanel2;
        private SiticoneNetCoreUI.SiticoneLabel siticoneLabel9;
        private SiticoneNetCoreUI.SiticoneLabel siticoneLabel8;
        private SiticoneNetCoreUI.SiticoneLabel siticoneLabel7;
        private TextBox RPricePerLiterTbx;
        private TextBox RLiterPurchasedTbx;
        private TextBox RFuelTypeTbx;
        private TextBox RBranchTbx;
        private TextBox RChangeTbx;
        private TextBox RPaymentTbx;
        private TextBox RTotalCostTbx;
    }
}