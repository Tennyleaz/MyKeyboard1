using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Security.Permissions;
using System.Windows.Shell;

namespace MyKeyboard1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static bool runHandler;
        public static IntPtr targetWindowPtr;
        private static string targetWindowTitle;
        CharsList myButtonList;
        private bool shiftIsDown;
        private bool ctrlIsDown;
        private bool altIsDown;
        private string currentPressedButton;
        private System.Timers.Timer myTimer;
        //WinEventDelegate dele = null;
        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        
        // Constants from winuser.h
        const uint EVENT_SYSTEM_FOREGROUND = 3;
        const uint WINEVENT_OUTOFCONTEXT = 0;
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        const int WS_EX_NOACTIVATE = 0x08000000;
        const int GWL_EXSTYLE = -20;

        // Need to ensure delegate is not collected while we're using it,
        // storing it in a class field is simplest way to do this.
        /*static WinEventDelegate procDelegate = new WinEventDelegate(WinEventProc);*/

        public MainWindow()
        {
            //runHandler = false;
            targetWindowPtr = IntPtr.Zero;
            shiftIsDown = false;
            ctrlIsDown = false;
            altIsDown = false;

            InitializeComponent();

            //dele = new WinEventDelegate(WinEventProc);
            //IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
            //UnhookWinEvent(hhook);
        }

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return "Cannot get window!";
        }

        /*private static void OnFocusChangedHandler(object src, AutomationFocusChangedEventArgs args)
        {
            if (!MainWindow.runHandler)
                return;

            Console.WriteLine("New focus is:");
            AutomationElement element = src as AutomationElement;
            if (element != null)
            {                
                string name = element.Current.Name;
                string id = element.Current.AutomationId;
                int processId = element.Current.ProcessId;
                using (Process process = Process.GetProcessById(processId))
                {
                    Console.WriteLine("  Name: {0}, Id: {1}, Process: {2}", name, id, process.ProcessName);
                    System.Console.WriteLine(element.Current.AutomationId);
                }
            }
        }*/

        /*private static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (GetActiveWindowTitle().Equals("MainWindow"))
                return;

            targetWindowTitle = GetActiveWindowTitle();
            System.Console.WriteLine(targetWindowTitle);
        }*/

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
            WindowInteropHelper wih = new WindowInteropHelper(this);
            int exstyle = GetWindowLong(wih.Handle, GWL_EXSTYLE);
            exstyle |= WS_EX_NOACTIVATE;
            SetWindowLong(wih.Handle, GWL_EXSTYLE, exstyle);

            BindButtonText();            

            //will this be faster? I don't really feel it...
            RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;

            myTimer = new System.Timers.Timer();
            myTimer.Interval = 1000;
            myTimer.Elapsed += OnTimedEvent;
            myTimer.AutoReset = true;

            //WindowChrome.SetWindowChrome(this, new WindowChrome());
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
            for (int i = 0; i < 26; i++)
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
        }

        //event for timer over 1000 each time
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);

            targetWindowTitle = GetActiveWindowTitle();
            IntPtr calculatorHandle = FindWindow(null, targetWindowTitle);
            bool success = SetForegroundWindow(calculatorHandle);
            if (success)
            {
                Console.WriteLine("OnTimedEvent sending key " + currentPressedButton + " to " + targetWindowTitle);
                SendKeys.SendWait(currentPressedButton);
            }
            else
                Console.WriteLine("SetForegroundWindow failed");
        }

    }
}
