using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan_
{
    static class expansion
    {
        public static string Title(this String str)
        {
            String emptyString = "";
            int counter = 0;
            foreach (char item in str)
            {
                if (counter == 0) emptyString += item.ToString().ToUpper();
                else emptyString += item;
                counter++;
            }
            return emptyString;
        }
    }
}
