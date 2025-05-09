using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PAGASCO
{
    public class SharedUIHelper
    {
        public void ExitApplication(Form currentForm)
        {
            currentForm.Close();
            Application.Exit();
        }

        public void GoBackToLogin(Form currentForm)
        {
            ManagerLogin mlogin = new ManagerLogin();
            mlogin.Show();
            currentForm.Hide();
        }

        // Dragging Logic
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public void EnableDrag(Control dragControl, Form form)
        {
            dragControl.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(form.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            };
        }
    }
}
