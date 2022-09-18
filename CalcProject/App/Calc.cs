using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject.App
{
    // Головний клас - запуск програми
    public class Calc
    {
        private readonly Resources _resources;

        public Calc(Resources resources)
        {
            _resources = resources;
            RomanNumber.Resources = resources;
        }

        public RomanNumber EvalExpression(String expression)
        {
            if (expression is null) throw new ArgumentException("System error");

            String[] parts = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) throw new ArgumentException("Invalid expression");

            if(!RomanNumber.Operations.ContainsKey(parts[1]))
                throw new ArgumentException("Invalid operation");

            RomanNumber rn1 = new(RomanNumber.Parse(parts[0]));
            RomanNumber rn2 = new(RomanNumber.Parse(parts[2]));
            return RomanNumber.Operations[parts[1]](rn1, rn2);
        }

        private void SelectCulture()
        {
            Console.WriteLine("Select culture:");
            for (int i = 0; i < _resources.SupportedCultures.Length; i++)
            {
                Console.WriteLine($"{i+1} {_resources.SupportedCultures[i]}");
            }
            int selection = Convert.ToInt32(Console.ReadLine());
            _resources.Culture = _resources.SupportedCultures[selection - 1];
        }

        public void Run()
        {
            SelectCulture();

            String expression;
            Console.Write(_resources.EnterExprMessage());
            expression = Console.ReadLine()!;
            try
            {
                Console.WriteLine($"{expression} = {EvalExpression(expression)}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        

        public void RunOld()
        {
            String? inp;
            RomanNumber rn1 = null!;
            do
            {
                Console.WriteLine(_resources.EnterNumberMessage());
                inp = Console.ReadLine();
                try
                {
                    rn1 = new RomanNumber(RomanNumber.Parse(inp!));
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Console Error. Program terminated");
                    return;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (rn1 == null);
            // Завдання: організувати цикл введення числа доки воно не буде валідним
            
            // Завдання: організувати цикл введення другого числа 
            RomanNumber rn2 = null!;
            do
            {
                Console.WriteLine(_resources.EnterNumberMessage());
                inp = Console.ReadLine();
                try
                {
                    rn2 = new RomanNumber(RomanNumber.Parse(inp!));
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Console Error. Program terminated");
                    return;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (rn2 == null);
            //  вивести суму  RN1 + RN2 = RES
            Console.WriteLine($"{rn1} + {rn2} = {rn1.Add(rn2)}");

            /* Д.З. Провести рефакторинг коду методу Calc.Run:
             *  - Винести повідомлення (текст) у ресурси
             *  - Усунути дублювання коду
             *  - За потреби розширити клас ресурсів, додати повідомлення UI
             */
        }
    }
}
/* Завдання: доробити проєкт "Калькулятор"
 * - На початку роботи вибір мови інтерфейсу (культуру)
 * - Реалізувати діалого введення виразу за зразком
 *     ОПЕРАНД ОПЕРАЦІЯ ОПЕРАНД (XI + IV    CD - L)
 *   За умови помилки обчислення виразу діалог повторюється  
 * - Забезпечити покриття тестами як операцій, так і 
 *     засоби обчислення виразів
 * -* Реалізувати "еластичність" - при додаванні а) культури
 *     б) операції  програма автоматично перелаштовується на 
 *     роботу з ними
 */