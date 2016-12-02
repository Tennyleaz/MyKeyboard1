using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKeyboard1
{    
    class Chars : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string myPrivateString;
        private bool isUpperCase;
        public string myChar
        {
            get
            { return myPrivateString; }
            set
            {
                myPrivateString = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("myChar"));
                    //If you leave the name null then it is assumed that all of the objects properties have changed.
                    //this event equals to:
                    //textBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                }
            }
        }
        public Chars(string c)
        {
            myChar = c;
            isUpperCase = false;
        }
        public void Flip()
        {
            //a~z || A~Z
            if (myPrivateString[0] >= 'a' && myPrivateString[0] <= 'z' || myPrivateString[0] >= 'A' && myPrivateString[0] <= 'Z')  
            {
                if (!isUpperCase)
                {
                    myChar = myChar.ToUpper();
                    isUpperCase = true;
                }
                else
                {
                    myChar = myChar.ToLower();
                    isUpperCase = false;
                }
            }
            else if (myPrivateString[0] >= '0' && myPrivateString[0] <= '9')  //0~9
            {
                switch (myChar[0])
                {
                    case '0':
                        myChar = ")";
                        break;
                    case '1':
                        myChar = "!";
                        break;
                    case '2':
                        myChar = "@";
                        break;
                    case '3':
                        myChar = "#";
                        break;
                    case '4':
                        myChar = "$";
                        break;
                    case '5':
                        myChar = "%";
                        break;
                    case '6':
                        myChar = "^";
                        break;
                    case '7':
                        myChar = "&";
                        break;
                    case '8':
                        myChar = "*";
                        break;
                    case '9':
                        myChar = "(";
                        break;
                }
                isUpperCase = true;
            }
            else  //symbols
            {
                if (!isUpperCase)
                {
                    switch (myChar[0])
                    {
                        case ',':
                            myChar = "<";
                            break;
                        case '.':
                            myChar = ">";
                            break;
                        case '/':
                            myChar = "?";
                            break;
                        case ';':
                            myChar = ":";
                            break;
                        case '\'':
                            myChar = "\"";
                            break;
                        case '\\':
                            myChar = "|";
                            break;
                    }
                    isUpperCase = true;
                }
                else
                {
                    switch (myChar[0])
                    {
                        case '<':
                            myChar = ",";
                            break;
                        case '>':
                            myChar = ".";
                            break;
                        case '?':
                            myChar = "/";
                            break;
                        case ':':
                            myChar = ";";
                            break;
                        case '\"':
                            myChar = "\'";
                            break;
                        case '|':
                            myChar = "\\";
                            break;
                        case ')':
                            myChar = "0";
                            break;
                        case '!':
                            myChar = "1";
                            break;
                        case '@':
                            myChar = "2";
                            break;
                        case '#':
                            myChar = "3";
                            break;
                        case '$':
                            myChar = "4";
                            break;
                        case '%':
                            myChar = "5";
                            break;
                        case '^':
                            myChar = "6";
                            break;
                        case '&':
                            myChar = "7";
                            break;
                        case '*':
                            myChar = "8";
                            break;
                        case '(':
                            myChar = "9";
                            break;
                    }
                    isUpperCase = false;
                }
            }
        }        
    }
}
