using System.Windows.Forms;

namespace PAGASCO
{
    internal interface MyInterface
    {
        void ExitApplication();
        void GoBackToLogin();
        void EnableDrag(Control control); // Use Control to allow panels, forms, etc.
    }
}
