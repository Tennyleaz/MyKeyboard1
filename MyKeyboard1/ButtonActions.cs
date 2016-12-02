using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyKeyboard1
{
    public partial class MainWindow
    {
        private void SpaceClick(object sender, RoutedEventArgs e)
        {
            SendKeyAndRecord(" ");
        }

        private void CtrlClicked(object sender, RoutedEventArgs e)
        {
            if (!ctrlIsDown)
            {
                Style btnDowntyle = (Style)FindResource("RoundCornerPushed");
                ctrlIsDown = true;
                btnCtrl.Style = btnDowntyle;
            }
            else
            {
                Style btnStyle = (Style)FindResource("RoundCorner");
                ctrlIsDown = false;
                btnCtrl.Style = btnStyle;
            }
        }
        private void AltClicked(object sender, RoutedEventArgs e)
        {
            if (!altIsDown)
            {
                Style btnDowntyle = (Style)FindResource("RoundCornerPushed");
                altIsDown = true;
                btnAlt.Style = btnDowntyle;
            }
            else
            {
                Style btnStyle = (Style)FindResource("RoundCorner");
                altIsDown = false;
                btnAlt.Style = btnStyle;
            }
        }

        private void ShiftClicked(object sender, RoutedEventArgs e)
        {
            if (!shiftIsDown)
            {
                Style btnDowntyle = (Style)FindResource("RoundCornerPushed");
                shiftIsDown = true;
                btnShift.Style = btnDowntyle;
            }
            else
            {
                Style btnStyle = (Style)FindResource("RoundCorner");
                shiftIsDown = false;
                btnShift.Style = btnStyle;
            }
            myButtonList.FlipAll();
        }
        private void EnterClicked(object sender, RoutedEventArgs e)
        {
            SendKeyAndRecord("\n");
        }

        private void BackspaceClicked(object sender, RoutedEventArgs e)
        {
            SendKeyAndRecord("{BACKSPACE}");
        }

        private void RegularButtonClick(object sender, RoutedEventArgs e)
        {
            string s;            

            //get the char from button bounded data
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            if (btn != null)
                s = btn.Content.ToString();
            else
                s = "";

            SendKeyAndRecord(s);
        }

        private void AboutDialog(object sender, RoutedEventArgs e)
        {
            Topmost = false;
            int i = StartDialog("what", 4);
            Console.WriteLine(i);
            Topmost = true;
        }

        private void RegularButtonRelease(object sender, MouseButtonEventArgs e)
        {
            //stop the timer and clean button
            myTimer.Interval = 1000;
            myTimer.Enabled = false;
            currentPressedButton = "";
        }

        private void SendKeyAndRecord(string s)
        {
            // Start the timer
            myTimer.Enabled = true;

            //get the top window
            targetWindowTitle = GetActiveWindowTitle();
            IntPtr calculatorHandle = FindWindow(null, targetWindowTitle);
            bool success = SetForegroundWindow(calculatorHandle);
            if (success)
            {
                //special sign %, ^, (, ) need to be enclose it within braces {}.
                if (s == "%")
                    s = "{%}";
                else if(s == "^")
                    s = "{^}";
                else if (s == "(")
                    s = "{(}";
                else if (s == ")")
                    s = "{)}";

                //send cutrrent key to target window
                currentPressedButton = s;

                //doing with ctrl/shift/alt
                /*if (shiftIsDown)
                {
                    s = s.ToLower();
                    s = "+(" + s + ")";
                }*/
                

                if (ctrlIsDown)
                {
                    s = "^(" + s + ")";
                }

                if (altIsDown)
                {
                    s = "%(" + s + ")";
                }

                currentPressedButton = s;
                Console.WriteLine("sending key " + s + " to " + targetWindowTitle);
                SendKeys.SendWait(s);
            }
            else
                Console.WriteLine("SetForegroundWindow failed");
        }
    }
}
