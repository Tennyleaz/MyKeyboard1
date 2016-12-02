using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MyKeyboard1
{
    public partial class MainWindow
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SendMessage(
               IntPtr hWnd,　　　// handle to destination window
               uint Msg,　　　 // message
               int wParam,　  // first message parameter
               int lParam     // second message parameter
         );
        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string strclassName, string strWindowName);
        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32", SetLastError = true)]
        private extern static int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewValue);
        [DllImport("user32", SetLastError = true)]
        private extern static int GetWindowLong(IntPtr hwnd, int nIndex);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //function from my dll file
        [DllImport("KeyboardDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartDialog(string str, int length);
    }
}
