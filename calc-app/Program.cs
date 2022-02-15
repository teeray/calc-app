using calc_app.Util;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace calc_app
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Super Calculator App");
            Console.WriteLine("Enter the string you with to calculate then press enter");
            Console.WriteLine("Press ESCAPE to exit");
            //limited to one string, pull out first index
            while (true)
            {
                var data = Regex.Unescape(Console.ReadLine());
                Sanitize.Setup(new char[] { ',','\n' }, null, true);
                var sanitize = Sanitize.Split(data);
                var sum = 0;
                foreach (var val in sanitize)
                {
                    var clean = Sanitize.SanitizeString(val);
                    sum = Calc.add(sum, clean);
                }
                Console.WriteLine(sum);
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
