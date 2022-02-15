using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc_app.Util
{
    /// <summary>
    /// String sanitizer. Houses the rules for the strings. Converts them as necessary. Return num for calculations
    /// </summary>
    static class Sanitize
    {
        static char[] delimeter = null;
        static int? exact = 0;
        static bool denyNeg = false;
        static int? maxNum = null;
        /// <summary>
        /// Setup for sanitizer class
        /// </summary>
        /// <param name="_delimeter"></param>
        /// <param name="_exact"></param>
        public static void Setup(char[] _delimeter, int? _exact, int? _maxNum, bool _denyNeg = false)
        {
            delimeter = _delimeter;
            exact = _exact;
            denyNeg = _denyNeg;
            maxNum = _maxNum;
        }

        /// <summary>
        /// Configurable splitter
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] Split(string text)
        {
            //split on custom delimeter
            var rslt = text.Split(delimeter, StringSplitOptions.RemoveEmptyEntries);
            //check for exact value
            if (exact.HasValue)
            {
                if (rslt.Length != exact)
                {
                    throw new Exception("Invalid number of values in your arguments");
                }
            }
            //check for negative
            if (denyNeg)
            {
                var negs = rslt.Where(w => w.Length > 0 && w[0] == '-');
                if (negs.Any())
                {
                    throw new Exception($"You have added negative numbers. The following are not permitted:{string.Join(",",negs)}");
                }
            }
            return rslt;
        }
        /// <summary>
        /// Check if there is a deliminator. If so add to the array of delimeters and return the string to parse
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SetupDeliminator(string text)
        {
            var delim = text.IndexOf("//");
            if(delim == 0)
            {
                //get index of first \n char.
                var newIndex = text.IndexOf("\n");
                if(newIndex > 0)
                {
                    //pull out the deliminator string
                    var addDelim = text.Substring(delim, newIndex + 1);
                    //get value of deliminator
                    var value = text.Substring(delim + 2, 1);
                    var chars = delimeter.ToList();
                    chars.Add(char.Parse(value));
                    delimeter = chars.ToArray();
                    return text.Replace(addDelim, "");
                }
            }
            return text;
        }
        /// <summary>
        /// Convert based on current rules
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int SanitizeString(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }
            var rslt = Converter(text);
            if (maxNum.HasValue)
            {
                if(rslt > maxNum)
                {
                    rslt = 0;
                }
            }
            return rslt;
        }
        /// <summary>
        /// Perform conversion
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static int Converter(string text)
        {
            //convert values based on match. TryParse converts values to a number or 0 if it can't find a match (or too large)
            int num = 0;
            int.TryParse(text, out num);
            return num;
        }
    }
}
