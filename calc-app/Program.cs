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
              
                Sanitize.Setup(new string[]{ ",","\n" }, null, 1000, true);
                var currString = Sanitize.SetupDeliminator(data);
                var sanitize = Sanitize.Split(currString);
                var sum = 0;
                var printout = string.Empty;
                foreach (var val in sanitize)
                {

                    var clean = Sanitize.SanitizeString(val);
                    if(printout.Length > 0)
                    {
                        printout = printout + "+";
                    }
                    printout = printout + clean.ToString();
                    sum = Calc.add(sum, clean);
                }
                Console.Write($"Calculation:{printout}{Environment.NewLine}");
                Console.WriteLine($"Result:{sum}");
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
