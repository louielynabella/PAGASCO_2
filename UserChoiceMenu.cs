using System.Data.OleDb;
using System.Data;
using System.Runtime.InteropServices;


namespace PAGASCO
{
    public partial class UserChoiceMenu : Form
    {

        public UserChoiceMenu()
        {
            InitializeComponent();
        }





        private void siticonePanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserChoiceExtBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

       

        private void CostumerBtn_Click(object sender, EventArgs e)
        {
            CostumerMenu costumermenu = new CostumerMenu();
            costumermenu.Show();
            this.Hide();
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

        private void CompanyRoleBtn_Click(object sender, EventArgs e)
        {
            CompanyRole companyrole = new CompanyRole();
            companyrole.Show();
            this.Hide();
        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
