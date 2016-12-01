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
            for (int i=0; i<26; i++)
            {
                int c = 97 + i;
                string temp = "";
                temp += (char)c;
                Chars myChars = new Chars(temp);
                theList.Add(myChars);
            }
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
