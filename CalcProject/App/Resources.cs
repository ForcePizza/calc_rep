using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcProject.App
{
    public class Resources
    {
        const String UNSUPPORTED_CULTURE = "Unsupported culture";

        public String[] SupportedCultures = { "en-US", "uk-UA" };

        private String _culture;
        public String Culture
        {
            get => _culture;
            set {
                if(Array.IndexOf(SupportedCultures, value) != -1)
                {
                    _culture = value;
                }
                else
                {
                    throw new ArgumentException(UNSUPPORTED_CULTURE);
                }
            }
        }

        public Resources()
        {
            _culture = SupportedCultures[1];
            
        }

        public String EmptyStringMessage(String culture = null!)
        {
            return culture ?? Culture switch
            {
                "en-US" => "Empty string not allowed",
                "uk-UA" => "Порожній рядок неприпустимий",
                _ => throw new ArgumentException(UNSUPPORTED_CULTURE)
            } ;
        }

        public String InvalidDigitMessage(char digit, String culture = null!)
        {
            return culture ?? Culture switch
            {
                "en-US" => $"Illegal digit '{digit}'",
                "uk-UA" => $"Неприпустима цифра '{digit}'",
                _ => throw new ArgumentException(UNSUPPORTED_CULTURE)
            };
        }

        public String InvalidTypeMessage(String typeName, String culture = null!)
        {
            return culture ?? Culture switch
            {
                "en-US" => $"Invalid argument type '{typeName}'",
                "uk-UA" => $"Тип аргументу не підтримується: '{typeName}'",
                _ => throw new ArgumentException(UNSUPPORTED_CULTURE)
            };
        }

        public String EnterNumberMessage(String culture = null!)
            => culture ?? Culture switch
            {
                "en-US" => "Enter number:",
                "uk-UA" => "Введите число:",
                _ => throw new ArgumentException(UNSUPPORTED_CULTURE)
            };

        public String EnterOperationMessage(String culture = null!)
             => culture ?? Culture switch
             {
                 "en-US" => "Enter operation:",
                 "uk-UA" => "Введите операцию:",
                 _ => throw new ArgumentException(UNSUPPORTED_CULTURE)
             };

        public String ResultMessage(String culture = null!)
             => culture ?? Culture switch
             {
                 "en-US" => "Result:",
                 "uk-UA" => "Результат:",
                 _ => throw new ArgumentException(UNSUPPORTED_CULTURE)
             };

        public String EnterExprMessage(String culture = null!)
             => culture ?? Culture switch
             {
                 "en-US" => "Enter expression( like XI + XL ) : ",
                 "uk-UA" => "Введіть вираз ( як-то XI + XL ) : ",
                 _ => throw new ArgumentException(UNSUPPORTED_CULTURE)
             };
       
    }
}
/* Створити ресурси для UI (консолі):
 * Введіть число / Enter number
 * Введіть операцію / Enter operation
 * Результат: / Result:
 */
