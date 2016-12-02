using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Forms;
using System.Windows.Interop;


namespace MyKeyboard1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string targetWindowTitle;
        CharsList myButtonList;
        private bool shiftIsDown;
        private bool ctrlIsDown;
        private bool altIsDown;
        private string currentPressedButton;
        private System.Timers.Timer myTimer;
        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        
        // Constants from winuser.h
        const uint EVENT_SYSTEM_FOREGROUND = 3;
        const uint WINEVENT_OUTOFCONTEXT = 0;
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        const int WS_EX_NOACTIVATE = 0x08000000;
        const int GWL_EXSTYLE = -20;

        public MainWindow()
        {
            shiftIsDown = false;
            ctrlIsDown = false;
            altIsDown = false;

            InitializeComponent();
        }

        private static string GetActiveWindowTitle()
        {
            //prepare buffer
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            //get target window title and to a string
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return "Cannot get window!";
        }


        /*protected override void OnSourceInitialized(EventArgs e)
        {
            HwndSource hwndSource = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            hwndSource.AddHook(WndProcHook);
            base.OnSourceInitialized(e);
        }*/
        /*private IntPtr WndProcHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handeled)
        {
            if (msg == 0x0010) //WM_CLOSE
            {
                //do something
                Console.WriteLine("WM_CLOSE");
            }

            if (msg == 0x0002)
            {

                System.Windows.MessageBox.Show("I saw a WM_DESTROY!");

                handeled = true;

            }
            return IntPtr.Zero;
        }*/

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            //make my window that cannot be activated
            //this help when I click on buttons
            WindowInteropHelper wih = new WindowInteropHelper(this);
            int exstyle = GetWindowLong(wih.Handle, GWL_EXSTYLE);
            exstyle |= WS_EX_NOACTIVATE;
            SetWindowLong(wih.Handle, GWL_EXSTYLE, exstyle);

            //bind Chars object
            BindButtonText();

            //will this be faster? I don't really feel it...
            RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;

            //RenderOptions.SetBitmapScalingMode(Grid1, BitmapScalingMode.LowQuality);

            //create a timer to calculate key long press
            myTimer = new System.Timers.Timer();
            myTimer.Interval = 1000;
            myTimer.Elapsed += OnTimedEvent;  //event for key long press
            myTimer.AutoReset = true;
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void BindButtonText()
        {
            //the object used for data binding
            myButtonList = new CharsList();

            //bind all a~z buttons
            List<System.Windows.Data.Binding> myBindingList = new List<System.Windows.Data.Binding>();
            for (int i = 0; i < 42; i++)
            {
                System.Windows.Data.Binding mb = new System.Windows.Data.Binding("myChar");
                mb.Source = myButtonList.theList[i];
                mb.Mode = BindingMode.OneWay;
                myBindingList.Add(mb);
            }

            btnA.SetBinding(ContentProperty, myBindingList[0]);
            btnB.SetBinding(ContentProperty, myBindingList[1]);
            btnC.SetBinding(ContentProperty, myBindingList[2]);
            btnD.SetBinding(ContentProperty, myBindingList[3]);
            btnE.SetBinding(ContentProperty, myBindingList[4]);
            btnF.SetBinding(ContentProperty, myBindingList[5]);
            btnG.SetBinding(ContentProperty, myBindingList[6]);
            btnH.SetBinding(ContentProperty, myBindingList[7]);
            btnI.SetBinding(ContentProperty, myBindingList[8]);
            btnJ.SetBinding(ContentProperty, myBindingList[9]);

            btnK.SetBinding(ContentProperty, myBindingList[10]);
            btnL.SetBinding(ContentProperty, myBindingList[11]);
            btnM.SetBinding(ContentProperty, myBindingList[12]);
            btnN.SetBinding(ContentProperty, myBindingList[13]);
            btnO.SetBinding(ContentProperty, myBindingList[14]);
            btnP.SetBinding(ContentProperty, myBindingList[15]);
            btnQ.SetBinding(ContentProperty, myBindingList[16]);
            btnR.SetBinding(ContentProperty, myBindingList[17]);
            btnS.SetBinding(ContentProperty, myBindingList[18]);
            btnT.SetBinding(ContentProperty, myBindingList[19]);

            btnU.SetBinding(ContentProperty, myBindingList[20]);
            btnV.SetBinding(ContentProperty, myBindingList[21]);
            btnW.SetBinding(ContentProperty, myBindingList[22]);
            btnX.SetBinding(ContentProperty, myBindingList[23]);
            btnY.SetBinding(ContentProperty, myBindingList[24]);
            btnZ.SetBinding(ContentProperty, myBindingList[25]);

            btn0.SetBinding(ContentProperty, myBindingList[26]);
            btn1.SetBinding(ContentProperty, myBindingList[27]);
            btn2.SetBinding(ContentProperty, myBindingList[28]);
            btn3.SetBinding(ContentProperty, myBindingList[29]);
            btn4.SetBinding(ContentProperty, myBindingList[30]);
            btn5.SetBinding(ContentProperty, myBindingList[31]);
            btn6.SetBinding(ContentProperty, myBindingList[32]);
            btn7.SetBinding(ContentProperty, myBindingList[33]);
            btn8.SetBinding(ContentProperty, myBindingList[34]);
            btn9.SetBinding(ContentProperty, myBindingList[35]);            

            btnBarclate.SetBinding(ContentProperty, myBindingList[36]);
            btnNot.SetBinding(ContentProperty, myBindingList[37]);
            btnCom.SetBinding(ContentProperty, myBindingList[38]);
            btnDot.SetBinding(ContentProperty, myBindingList[39]);
            btnQue.SetBinding(ContentProperty, myBindingList[40]);
            btnSemi.SetBinding(ContentProperty, myBindingList[41]);
        }

        //event for timer over 1000 each time
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            //if this is fired, set time interval to 300 ms
            myTimer.Interval = 100;

            //find the target window
            targetWindowTitle = GetActiveWindowTitle();
            IntPtr calculatorHandle = FindWindow(null, targetWindowTitle);
            bool success = SetForegroundWindow(calculatorHandle);
            if (success)
            {
                Console.WriteLine("OnTimedEvent sending key " + currentPressedButton + " to " + targetWindowTitle + " at " + e.SignalTime);
                SendKeys.SendWait(currentPressedButton);
            }
            else
                Console.WriteLine("SetForegroundWindow failed");
        }

    }
}
