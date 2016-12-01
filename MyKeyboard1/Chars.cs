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
    }
}
