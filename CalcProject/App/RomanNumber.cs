using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject.App
{
    public delegate RomanNumber Operation(RomanNumber rn1, RomanNumber rn2);

    public record RomanNumber
    {
        public static Resources Resources { get; set; }

        public static Dictionary<String, Operation> Operations = new()
        {
            { "+", Add },
            { "-", Sub },
            { "%", Mod },
        };

        private int _value;
        public int Value 
        { 
            get { return _value; } 
            set { _value = value; } 
        }

        public RomanNumber(int value)
        {
            _value = value;
        }

        public override string ToString()
        {
            if(this._value == 0)
            {
                return "N";
            }
            String[] parts = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            
            int n = Math.Abs(this._value);  // модуль числа (без знаку)
            String res = this._value < 0 ? "-" : "";

            while (n > 0)
            {
                for (int i = 0; i < parts.Length; i++)
                {
                    while (n >= values[i])
                    {
                        n -= values[i];
                        res += parts[i];
                    }
                }
            }
            return res;
        }

        public static int Parse(String str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }
            if (str == "N")
            {
                return 0;  // Zero digit could be only itself
            }

            bool isNegative = str.StartsWith('-');
            if (isNegative)
            {
                str = str[1..];  // підрядок з 1го символа (прибрали "-")
            }

            if (str.Length == 0)
            {
                throw new ArgumentException(Resources.EmptyStringMessage());
            }            
            char[] digits = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            int[] digitValues = { 1, 5, 10, 50, 100, 500, 1000 };

            int pos = str.Length - 1;  // позиція останньої цифри у str
            int ind = Array.IndexOf(digits, str[pos]);  // індекс цифри у масиві
            if(ind == -1)  // цифри немає у масиві
            {
                throw new ArgumentException(
                    Resources.InvalidDigitMessage(str[pos])
                );
            }
            int nextDigitValue = digitValues[ind];
            int res = nextDigitValue;  // остання цифра завжди з +

            while (pos > 0)
            {
                pos -= 1;
                ind = Array.IndexOf(digits, str[pos]);  // передостання цифра
                if (ind == -1)  // цифри немає у масиві
                {
                    throw new ArgumentException(
                        Resources.InvalidDigitMessage(str[pos])
                    );
                }
                if (digitValues[ind] < nextDigitValue) res -= digitValues[ind];
                else res += digitValues[ind];
                nextDigitValue = digitValues[ind];
            }
            return isNegative ? -res : res;
        }

        /* Рефакторинг - усунення дублювання
        public RomanNumber Add(RomanNumber other)
        {
            if(other == null)
            {
                throw (new ArgumentNullException(nameof(other)));
            }
            return new(this.Value + other.Value);
        }

        public RomanNumber Add(int right)
        {
            return new(this.Value + right);
        }

        public RomanNumber Add(String roman)
        {             
            if (roman == null) 
                throw new ArgumentNullException(nameof(roman));

            return new(this.Value + Parse(roman));
        }
        */
        public RomanNumber Add(RomanNumber other)
        {
            return Add(this, other);
        }
        public RomanNumber Add(int right)
        {
            return this.Add(new RomanNumber(right));
        }
        public RomanNumber Add(String roman)
        {
            return this.Add(new RomanNumber(Parse(roman)));
        }
        /*
        // Статичні методи - коли немає жодного з RomanNumber
        public static RomanNumber Add(int value1, int value2)
        {
            return new RomanNumber(value1).Add(value2);
        }
        // Статичний метод - для можливості заміни на оператор
        public static RomanNumber Add(RomanNumber rn1, RomanNumber rn2)
        {
            
            //if (rn1 is null) throw new ArgumentNullException(nameof(rn1));            
            //if (rn2 is null) throw new ArgumentNullException(nameof(rn2));
            
            if (rn1 is null || rn2 is null)
            {
                throw new ArgumentNullException(
                    rn1 is null ? nameof(rn1) : nameof(rn2) );
            }
            return new(rn1.Value + rn2.Value);
        }*/

        /// <summary>
        /// Універсальний метод додавання. Приймає типи 
        /// Int32, String, RomanNumber
        /// </summary>
        /// <param name="rn1">Int32, String, RomanNumber</param>
        /// <param name="rn2">Int32, String, RomanNumber</param>
        /// <returns>RomanNumber - сума аргументів</returns>
        public static RomanNumber Add(object obj1, object obj2)
        {
            // "Фабричний" метод - для довільних аргументів
            // "+" - один метод, немає великої кількості
            // "-" - помилка під час виконання, а не компіляції
            //       велика роль документації (коментарів)
            RomanNumber rn1, rn2;
            // obj1 is RomanNumber val1 ==> if(obj1 is RomanNumber)
            //                           var val1 = obj1 as RomanNumber
            if (obj1 is RomanNumber val1) rn1 = val1;
            else rn1 = new RomanNumber(obj1);

            if (obj2 is RomanNumber val2) rn2 = val2;
            else rn2 = new RomanNumber(obj2);

            return new RomanNumber(rn1.Value + rn2.Value);
        }

        private RomanNumber(object value)
        {
            if (value is null) 
                throw new ArgumentNullException(nameof(value));
            else if (value is RomanNumber rn) _value = rn.Value;
            else if (value is int int_val) _value = int_val;
            else if (value is string s_val) _value = Parse(s_val);
            else throw new ArgumentException(
                Resources.InvalidTypeMessage(value.GetType().Name)
            );
        }

        public RomanNumber Sub(RomanNumber other)
        {
            if(other is null) throw new ArgumentNullException(nameof(other));
            return this.Add(other with { Value = -other.Value });
        }

        public static RomanNumber Sub(object obj1, object obj2)
        {
            var rn1 = (obj1 is RomanNumber val1) ? val1 : new RomanNumber(obj1);
            var rn2 = (obj2 is RomanNumber val2) ? val2 : new RomanNumber(obj2);

            return new RomanNumber(rn1.Value - rn2.Value);
        }

        public static RomanNumber Mod(object obj1, object obj2)
        {
            var rn1 = (obj1 is RomanNumber val1) ? val1 : new RomanNumber(obj1);
            var rn2 = (obj2 is RomanNumber val2) ? val2 : new RomanNumber(obj2);

            return new RomanNumber(rn1.Value % rn2.Value);
        }
    }
}
/*
 Римські числа
Складаються з римських цифр:
 I - 1
 V - 5
 X - 10
 L - 50
 C - 100
 D - 500
 M - 1000
За правилом: якщо після цифри наступна цифра є більшою,
 то вона віднімається від результату
 інакше - додається до результату
Наприклад
 II +1+1
 IV -1+5
 XL -10+50
 XV +10+5
 MCM +1000-100+1000 = 1900
 */