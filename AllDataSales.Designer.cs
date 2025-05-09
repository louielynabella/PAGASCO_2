namespace PAGASCO
{
    partial class AllDataSales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllDataSales));
            siticonePanel1 = new SiticoneNetCoreUI.SiticonePanel();
            siticoneShimmerLabel1 = new SiticoneNetCoreUI.SiticoneShimmerLabel();
            LoginBackBtn = new FontAwesome.Sharp.IconButton();
            totalSalesLabel = new SiticoneNetCoreUI.SiticoneLabel();
            AllDataSalesYearCombobox = new ComboBox();
            pieChart1 = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            pictureBox2 = new PictureBox();
            cartesianChart1 = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            pictureBox1 = new PictureBox();
            siticonePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // siticonePanel1
            // 
            siticonePanel1.AcrylicTintColor = Color.FromArgb(128, 255, 255, 255);
            siticonePanel1.BackColor = Color.Transparent;
            siticonePanel1.BorderAlignment = System.Drawing.Drawing2D.PenAlignment.Center;
            siticonePanel1.BorderDashPattern = null;
            siticonePanel1.BorderGradientEndColor = Color.Purple;
            siticonePanel1.BorderGradientStartColor = Color.Blue;
            siticonePanel1.BorderThickness = 2F;
            siticonePanel1.Controls.Add(siticoneShimmerLabel1);
            siticonePanel1.Controls.Add(LoginBackBtn);
            siticonePanel1.Controls.Add(totalSalesLabel);
            siticonePanel1.Controls.Add(AllDataSalesYearCombobox);
            siticonePanel1.Controls.Add(pieChart1);
            siticonePanel1.Controls.Add(pictureBox2);
            siticonePanel1.Controls.Add(cartesianChart1);
            siticonePanel1.Controls.Add(pictureBox1);
            siticonePanel1.CornerRadiusBottomLeft = 0F;
            siticonePanel1.CornerRadiusBottomRight = 0F;
            siticonePanel1.CornerRadiusTopLeft = 0F;
            siticonePanel1.CornerRadiusTopRight = 0F;
            siticonePanel1.Dock = DockStyle.Fill;
            siticonePanel1.EnableAcrylicEffect = false;
            siticonePanel1.EnableMicaEffect = false;
            siticonePanel1.EnableRippleEffect = false;
            siticonePanel1.FillColor = Color.White;
            siticonePanel1.GradientColors = new Color[]
    {
    Color.FromArgb(244, 241, 222),
    Color.FromArgb(231, 111, 81),
    Color.FromArgb(38, 70, 83)
    };
            siticonePanel1.GradientPositions = new float[]
    {
    0F,
    0.5F,
    1F
    };
            siticonePanel1.Location = new Point(0, 0);
            siticonePanel1.Name = "siticonePanel1";
            siticonePanel1.PatternStyle = System.Drawing.Drawing2D.HatchStyle.Max;
            siticonePanel1.RippleAlpha = 50;
            siticonePanel1.RippleAlphaDecrement = 3;
            siticonePanel1.RippleColor = Color.FromArgb(50, 255, 255, 255);
            siticonePanel1.RippleMaxSize = 600F;
            siticonePanel1.RippleSpeed = 15F;
            siticonePanel1.ShowBorder = true;
            siticonePanel1.Size = new Size(1276, 721);
            siticonePanel1.TabIndex = 0;
            siticonePanel1.TabStop = true;
            siticonePanel1.UseBorderGradient = true;
            siticonePanel1.UseMultiGradient = true;
            siticonePanel1.UsePatternTexture = false;
            siticonePanel1.UseRadialGradient = false;
            // 
            // siticoneShimmerLabel1
            // 
            siticoneShimmerLabel1.AutoReverse = false;
            siticoneShimmerLabel1.BackColor = Color.FromArgb(244, 241, 222);
            siticoneShimmerLabel1.BaseColor = Color.FromArgb(38, 70, 83);
            siticoneShimmerLabel1.Direction = SiticoneNetCoreUI.ShimmerDirection.LeftToRight;
            siticoneShimmerLabel1.Font = new Font("Impact", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siticoneShimmerLabel1.IsAnimating = true;
            siticoneShimmerLabel1.IsPaused = false;
            siticoneShimmerLabel1.Location = new Point(364, 20);
            siticoneShimmerLabel1.Margin = new Padding(4);
            siticoneShimmerLabel1.Name = "siticoneShimmerLabel1";
            siticoneShimmerLabel1.PauseDuration = 0;
            siticoneShimmerLabel1.ShimmerColor = Color.FromArgb(231, 111, 81);
            siticoneShimmerLabel1.ShimmerOpacity = 1F;
            siticoneShimmerLabel1.ShimmerSpeed = 30;
            siticoneShimmerLabel1.ShimmerWidth = 0.9F;
            siticoneShimmerLabel1.Size = new Size(515, 87);
            siticoneShimmerLabel1.TabIndex = 16;
            siticoneShimmerLabel1.Text = "BRANCHES DATA";
            siticoneShimmerLabel1.ToolTipText = "";
            // 
            // LoginBackBtn
            // 
            LoginBackBtn.BackColor = Color.Transparent;
            LoginBackBtn.FlatStyle = FlatStyle.Popup;
            LoginBackBtn.IconChar = FontAwesome.Sharp.IconChar.Reply;
            LoginBackBtn.IconColor = Color.FromArgb(231, 111, 81);
            LoginBackBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            LoginBackBtn.IconSize = 40;
            LoginBackBtn.Location = new Point(4, 656);
            LoginBackBtn.Margin = new Padding(4);
            LoginBackBtn.Name = "LoginBackBtn";
            LoginBackBtn.Size = new Size(66, 61);
            LoginBackBtn.TabIndex = 15;
            LoginBackBtn.UseVisualStyleBackColor = false;
            LoginBackBtn.Click += LoginBackBtn_Click;
            // 
            // totalSalesLabel
            // 
            totalSalesLabel.BackColor = Color.Transparent;
            totalSalesLabel.Font = new Font("MS Reference Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            totalSalesLabel.ForeColor = Color.FromArgb(231, 111, 81);
            totalSalesLabel.Location = new Point(23, 20);
            totalSalesLabel.Margin = new Padding(4, 0, 4, 0);
            totalSalesLabel.Name = "totalSalesLabel";
            totalSalesLabel.Size = new Size(305, 36);
            totalSalesLabel.TabIndex = 10;
            totalSalesLabel.Text = "YEAR ";
            // 
            // AllDataSalesYearCombobox
            // 
            AllDataSalesYearCombobox.BackColor = Color.FromArgb(244, 241, 222);
            AllDataSalesYearCombobox.FormattingEnabled = true;
            AllDataSalesYearCombobox.Location = new Point(23, 54);
            AllDataSalesYearCombobox.Name = "AllDataSalesYearCombobox";
            AllDataSalesYearCombobox.Size = new Size(220, 33);
            AllDataSalesYearCombobox.TabIndex = 4;
            AllDataSalesYearCombobox.SelectedIndexChanged += AllDataSalesYearCombobox_SelectedIndexChanged;
            // 
            // pieChart1
            // 
            pieChart1.BackColor = Color.FromArgb(244, 241, 222);
            pieChart1.InitialRotation = 0D;
            pieChart1.IsClockwise = true;
            pieChart1.Location = new Point(896, 114);
            pieChart1.MaxAngle = 360D;
            pieChart1.MaxValue = double.NaN;
            pieChart1.MinValue = 0D;
            pieChart1.Name = "pieChart1";
            pieChart1.Size = new Size(265, 416);
            pieChart1.TabIndex = 3;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Center;
            pictureBox2.Location = new Point(1138, -37);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(138, 509);
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // cartesianChart1
            // 
            cartesianChart1.BackColor = Color.FromArgb(244, 241, 222);
            cartesianChart1.Location = new Point(23, 114);
            cartesianChart1.MatchAxesScreenDataRatio = false;
            cartesianChart1.Name = "cartesianChart1";
            cartesianChart1.Size = new Size(867, 535);
            cartesianChart1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            pictureBox1.Location = new Point(1024, 469);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(252, 249);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // AllDataSales
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1276, 721);
            Controls.Add(siticonePanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AllDataSales";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AllDataSales";
            siticonePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SiticoneNetCoreUI.SiticonePanel siticonePanel1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChart1;
        private ComboBox AllDataSalesYearCombobox;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieChart1;
        private SiticoneNetCoreUI.SiticoneLabel totalSalesLabel;
        private FontAwesome.Sharp.IconButton LoginBackBtn;
        private SiticoneNetCoreUI.SiticoneShimmerLabel siticoneShimmerLabel1;
    }
}