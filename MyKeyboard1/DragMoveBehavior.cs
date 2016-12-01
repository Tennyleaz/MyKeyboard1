using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyKeyboard1
{
    public partial class MainWindow
    {
        private void TitleMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            {
                //ReleaseCapture();
                IntPtr calculatorHandle = FindWindow(null, "MainWindow");
                ResizeMode = System.Windows.ResizeMode.NoResize;
                SendMessage(calculatorHandle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void TitleMouseUp(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
        }
    }
}
