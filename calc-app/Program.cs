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
            Console.WriteLine("Add /*/... to access additional properties (upper bound, upper###, remove negatives, neg, and change math type [sub,mult,div])");
            Console.WriteLine("Ex, \"/*/neg,upper605,sub\", this will toggle negative values to be set to 0 and the upper bound to be set at 605 while subtracting");
            Console.WriteLine("Press ESCAPE or ctrl + c to exit");
            //limited to one string, pull out first index
            while (true)
            {
                var data = Regex.Unescape(Console.ReadLine());              
                Sanitize.Setup(new string[]{ "," }, null, 1000, true);
                var currString = Sanitize.SetupProperties(data);
                //Setup logger
                Logger.Reset();
                Logger.Modifier = Sanitize.mathType;
                //get values
                var sanitize = Sanitize.Split(currString);
                var sum = 0.0M;                
                foreach (var val in sanitize)
                {
                    var clean = Sanitize.SanitizeString(val);
                    sum = Calc.DoMath(sum, clean, Sanitize.mathType);
                }
                Console.Write($"Calculation:{Logger.Log}{Environment.NewLine}");
                Console.WriteLine($"Result:{sum}");
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
