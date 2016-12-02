using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKeyboard1
{
    class CharsList
    {
        public List<Chars> theList;
        public CharsList()
        {
            theList = new List<Chars>();

            //add a~z buttons
            for (int i=0; i<26; i++)
            {
                int c = 97 + i;
                string temp = "";
                temp += (char)c;
                Chars myChars = new Chars(temp);
                theList.Add(myChars);
            }

            //add 0~9 buttons
            for (int i=0; i<=9; i++)
            {
                string temp = "";
                temp += (char)(i + '0');
                Chars myChars = new Chars(temp);
                theList.Add(myChars);
            }

            //add rest symbols
            Chars c1 = new Chars("\"");
            theList.Add(c1);

            Chars c2 = new Chars("\\");
            theList.Add(c2);

            Chars c3 = new Chars(",");
            theList.Add(c3);

            Chars c4 = new Chars(".");
            theList.Add(c4);

            Chars c5 = new Chars("/");
            theList.Add(c5);

            Chars c6 = new Chars(":");
            theList.Add(c6);
        }
        public void FlipAll()
        {
            foreach (Chars it in theList)
            {
                it.Flip();
            }
        }
    }
}
