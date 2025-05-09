namespace PAGASCO
{
    partial class ManagerDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerDetails));
            siticonePanel1 = new SiticoneNetCoreUI.SiticonePanel();
            LoginBackBtn = new FontAwesome.Sharp.IconButton();
            BranchLabel = new SiticoneNetCoreUI.SiticoneShimmerLabel();
            pictureBox2 = new PictureBox();
            siticonePanel2 = new SiticoneNetCoreUI.SiticonePanel();
            InstituitionUniversityLabel = new SiticoneNetCoreUI.SiticoneLabel();
            DegreeLabel = new SiticoneNetCoreUI.SiticoneLabel();
            EducationBackgroundLabel = new SiticoneNetCoreUI.SiticoneLabel();
            NationalityLabel = new SiticoneNetCoreUI.SiticoneLabel();
            AddressLabel = new SiticoneNetCoreUI.SiticoneLabel();
            ContactNumberLabel = new SiticoneNetCoreUI.SiticoneLabel();
            GenderLabel = new SiticoneNetCoreUI.SiticoneLabel();
            BirthDateLabel = new SiticoneNetCoreUI.SiticoneLabel();
            EmailLabel = new SiticoneNetCoreUI.SiticoneLabel();
            ManagerNameLabel = new SiticoneNetCoreUI.SiticoneLabel();
            pictureBox1 = new PictureBox();
            siticonePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            siticonePanel2.SuspendLayout();
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
            siticonePanel1.Controls.Add(LoginBackBtn);
            siticonePanel1.Controls.Add(BranchLabel);
            siticonePanel1.Controls.Add(pictureBox2);
            siticonePanel1.Controls.Add(siticonePanel2);
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
            siticonePanel1.UseBorderGradient = false;
            siticonePanel1.UseMultiGradient = true;
            siticonePanel1.UsePatternTexture = false;
            siticonePanel1.UseRadialGradient = false;
            siticonePanel1.Paint += siticonePanel1_Paint;
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
            LoginBackBtn.TabIndex = 16;
            LoginBackBtn.UseVisualStyleBackColor = false;
            LoginBackBtn.Click += LoginBackBtn_Click;
            // 
            // BranchLabel
            // 
            BranchLabel.AutoReverse = false;
            BranchLabel.BackColor = Color.FromArgb(38, 70, 83);
            BranchLabel.BaseColor = Color.FromArgb(38, 70, 83);
            BranchLabel.Direction = SiticoneNetCoreUI.ShimmerDirection.LeftToRight;
            BranchLabel.Font = new Font("Impact", 28F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BranchLabel.IsAnimating = true;
            BranchLabel.IsPaused = false;
            BranchLabel.Location = new Point(13, 13);
            BranchLabel.Margin = new Padding(4);
            BranchLabel.Name = "BranchLabel";
            BranchLabel.PauseDuration = 0;
            BranchLabel.ShimmerColor = Color.FromArgb(231, 111, 81);
            BranchLabel.ShimmerOpacity = 1F;
            BranchLabel.ShimmerSpeed = 30;
            BranchLabel.ShimmerWidth = 0.5F;
            BranchLabel.Size = new Size(455, 87);
            BranchLabel.TabIndex = 3;
            BranchLabel.ToolTipText = "";
            BranchLabel.Click += BranchLabel_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(-188, 98);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(403, 551);
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // siticonePanel2
            // 
            siticonePanel2.AcrylicTintColor = Color.FromArgb(128, 255, 255, 255);
            siticonePanel2.BackColor = Color.FromArgb(244, 241, 222);
            siticonePanel2.BorderAlignment = System.Drawing.Drawing2D.PenAlignment.Center;
            siticonePanel2.BorderDashPattern = null;
            siticonePanel2.BorderGradientEndColor = Color.Purple;
            siticonePanel2.BorderGradientStartColor = Color.Blue;
            siticonePanel2.BorderThickness = 2F;
            siticonePanel2.Controls.Add(InstituitionUniversityLabel);
            siticonePanel2.Controls.Add(DegreeLabel);
            siticonePanel2.Controls.Add(EducationBackgroundLabel);
            siticonePanel2.Controls.Add(NationalityLabel);
            siticonePanel2.Controls.Add(AddressLabel);
            siticonePanel2.Controls.Add(ContactNumberLabel);
            siticonePanel2.Controls.Add(GenderLabel);
            siticonePanel2.Controls.Add(BirthDateLabel);
            siticonePanel2.Controls.Add(EmailLabel);
            siticonePanel2.Controls.Add(ManagerNameLabel);
            siticonePanel2.CornerRadiusBottomLeft = 10F;
            siticonePanel2.CornerRadiusBottomRight = 10F;
            siticonePanel2.CornerRadiusTopLeft = 10F;
            siticonePanel2.CornerRadiusTopRight = 10F;
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
            siticonePanel2.Location = new Point(485, 15);
            siticonePanel2.Name = "siticonePanel2";
            siticonePanel2.PatternStyle = System.Drawing.Drawing2D.HatchStyle.Max;
            siticonePanel2.RippleAlpha = 50;
            siticonePanel2.RippleAlphaDecrement = 3;
            siticonePanel2.RippleColor = Color.FromArgb(50, 255, 255, 255);
            siticonePanel2.RippleMaxSize = 600F;
            siticonePanel2.RippleSpeed = 15F;
            siticonePanel2.ShowBorder = true;
            siticonePanel2.Size = new Size(775, 691);
            siticonePanel2.TabIndex = 0;
            siticonePanel2.TabStop = true;
            siticonePanel2.UseBorderGradient = false;
            siticonePanel2.UseMultiGradient = false;
            siticonePanel2.UsePatternTexture = false;
            siticonePanel2.UseRadialGradient = false;
            // 
            // InstituitionUniversityLabel
            // 
            InstituitionUniversityLabel.BackColor = Color.Transparent;
            InstituitionUniversityLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InstituitionUniversityLabel.ForeColor = Color.FromArgb(38, 70, 83);
            InstituitionUniversityLabel.Location = new Point(14, 589);
            InstituitionUniversityLabel.Margin = new Padding(4, 0, 4, 0);
            InstituitionUniversityLabel.Name = "InstituitionUniversityLabel";
            InstituitionUniversityLabel.Size = new Size(744, 87);
            InstituitionUniversityLabel.TabIndex = 12;
            InstituitionUniversityLabel.Text = "Instituition/University: ";
            InstituitionUniversityLabel.Click += InstituitionUniversityLabel_Click;
            // 
            // DegreeLabel
            // 
            DegreeLabel.BackColor = Color.Transparent;
            DegreeLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DegreeLabel.ForeColor = Color.FromArgb(38, 70, 83);
            DegreeLabel.Location = new Point(14, 529);
            DegreeLabel.Margin = new Padding(4, 0, 4, 0);
            DegreeLabel.Name = "DegreeLabel";
            DegreeLabel.Size = new Size(734, 36);
            DegreeLabel.TabIndex = 11;
            DegreeLabel.Text = "Degree: ";
            DegreeLabel.Click += DegreeLabel_Click;
            // 
            // EducationBackgroundLabel
            // 
            EducationBackgroundLabel.BackColor = Color.Transparent;
            EducationBackgroundLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EducationBackgroundLabel.ForeColor = Color.FromArgb(38, 70, 83);
            EducationBackgroundLabel.Location = new Point(14, 468);
            EducationBackgroundLabel.Margin = new Padding(4, 0, 4, 0);
            EducationBackgroundLabel.Name = "EducationBackgroundLabel";
            EducationBackgroundLabel.Size = new Size(734, 36);
            EducationBackgroundLabel.TabIndex = 10;
            EducationBackgroundLabel.Text = "Education Background:";
            EducationBackgroundLabel.Click += EducationBackgroundLabel_Click;
            // 
            // NationalityLabel
            // 
            NationalityLabel.BackColor = Color.Transparent;
            NationalityLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NationalityLabel.ForeColor = Color.FromArgb(38, 70, 83);
            NationalityLabel.Location = new Point(14, 326);
            NationalityLabel.Margin = new Padding(4, 0, 4, 0);
            NationalityLabel.Name = "NationalityLabel";
            NationalityLabel.Size = new Size(734, 36);
            NationalityLabel.TabIndex = 9;
            NationalityLabel.Text = "Nationality:";
            NationalityLabel.Click += NationalityLabel_Click;
            // 
            // AddressLabel
            // 
            AddressLabel.BackColor = Color.Transparent;
            AddressLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddressLabel.ForeColor = Color.FromArgb(38, 70, 83);
            AddressLabel.Location = new Point(14, 398);
            AddressLabel.Margin = new Padding(4, 0, 4, 0);
            AddressLabel.Name = "AddressLabel";
            AddressLabel.Size = new Size(734, 36);
            AddressLabel.TabIndex = 8;
            AddressLabel.Text = "Address:";
            AddressLabel.Click += AddressLabel_Click;
            // 
            // ContactNumberLabel
            // 
            ContactNumberLabel.BackColor = Color.Transparent;
            ContactNumberLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ContactNumberLabel.ForeColor = Color.FromArgb(38, 70, 83);
            ContactNumberLabel.Location = new Point(14, 140);
            ContactNumberLabel.Margin = new Padding(4, 0, 4, 0);
            ContactNumberLabel.Name = "ContactNumberLabel";
            ContactNumberLabel.Size = new Size(734, 36);
            ContactNumberLabel.TabIndex = 7;
            ContactNumberLabel.Text = "Contact Number:";
            ContactNumberLabel.Click += siticoneLabel6_Click;
            // 
            // GenderLabel
            // 
            GenderLabel.BackColor = Color.Transparent;
            GenderLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            GenderLabel.ForeColor = Color.FromArgb(38, 70, 83);
            GenderLabel.Location = new Point(14, 260);
            GenderLabel.Margin = new Padding(4, 0, 4, 0);
            GenderLabel.Name = "GenderLabel";
            GenderLabel.Size = new Size(734, 36);
            GenderLabel.TabIndex = 6;
            GenderLabel.Text = "Gender:";
            GenderLabel.Click += GenderLabel_Click;
            // 
            // BirthDateLabel
            // 
            BirthDateLabel.BackColor = Color.Transparent;
            BirthDateLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BirthDateLabel.ForeColor = Color.FromArgb(38, 70, 83);
            BirthDateLabel.Location = new Point(14, 200);
            BirthDateLabel.Margin = new Padding(4, 0, 4, 0);
            BirthDateLabel.Name = "BirthDateLabel";
            BirthDateLabel.Size = new Size(734, 36);
            BirthDateLabel.TabIndex = 5;
            BirthDateLabel.Text = "Birth Date:";
            BirthDateLabel.Click += BirthDateLabel_Click;
            // 
            // EmailLabel
            // 
            EmailLabel.BackColor = Color.Transparent;
            EmailLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EmailLabel.ForeColor = Color.FromArgb(38, 70, 83);
            EmailLabel.Location = new Point(14, 83);
            EmailLabel.Margin = new Padding(4, 0, 4, 0);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(734, 36);
            EmailLabel.TabIndex = 3;
            EmailLabel.Text = "Email:";
            EmailLabel.Click += EmailLabel_Click;
            // 
            // ManagerNameLabel
            // 
            ManagerNameLabel.BackColor = Color.Transparent;
            ManagerNameLabel.Font = new Font("MS Reference Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ManagerNameLabel.ForeColor = Color.FromArgb(38, 70, 83);
            ManagerNameLabel.Location = new Point(14, 27);
            ManagerNameLabel.Margin = new Padding(4, 0, 4, 0);
            ManagerNameLabel.Name = "ManagerNameLabel";
            ManagerNameLabel.Size = new Size(734, 36);
            ManagerNameLabel.TabIndex = 2;
            ManagerNameLabel.Text = "Manager Name:";
            ManagerNameLabel.Click += ManagerNameLabel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(254, 172);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(403, 569);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // ManagerDetails
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1276, 721);
            Controls.Add(siticonePanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ManagerDetails";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManagerDetails";
            siticonePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            siticonePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SiticoneNetCoreUI.SiticonePanel siticonePanel1;
        private SiticoneNetCoreUI.SiticonePanel siticonePanel2;
        private SiticoneNetCoreUI.SiticoneLabel EmailLabel;
        private SiticoneNetCoreUI.SiticoneLabel ManagerNameLabel;
        private SiticoneNetCoreUI.SiticoneLabel InstituitionUniversityLabel;
        private SiticoneNetCoreUI.SiticoneLabel DegreeLabel;
        private SiticoneNetCoreUI.SiticoneLabel EducationBackgroundLabel;
        private SiticoneNetCoreUI.SiticoneLabel NationalityLabel;
        private SiticoneNetCoreUI.SiticoneLabel AddressLabel;
        private SiticoneNetCoreUI.SiticoneLabel ContactNumberLabel;
        private SiticoneNetCoreUI.SiticoneLabel GenderLabel;
        private SiticoneNetCoreUI.SiticoneLabel BirthDateLabel;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private SiticoneNetCoreUI.SiticoneShimmerLabel BranchLabel;
        private FontAwesome.Sharp.IconButton LoginBackBtn;
    }
}