using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject.App
{
    public class RomanNumber
    {
        public static int Parse (String str)
        {
            char[] digits = { 'I','V','X','L','C','D','M'};
            int[] digitValues = { 1, 5, 10, 50, 100, 500, 1000 };

            int pos = 1;
            int ind = Array.IndexOf(digits, str[pos]); // индекс цифры у масиви
            if (ind == -1)
            {
                throw new ArgumentException($"Invalid digit ' {str[pos]}'");
            }
            int res = digitValues[ind]; // остання цифра завжди з +


            ind = Array.IndexOf(digits, str[pos - 1]);
            if (ind == -1)
            {
                throw new ArgumentException($"Invalid digit '{str[pos-1]}");
            }
            if (digitValues[ind] < res)
            {
                res -= digitValues[ind];
            }

            else
            {
                res += digitValues[ind];
            }
            return res;
        }
    }
}
