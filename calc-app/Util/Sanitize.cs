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
        /// <summary>
        /// Setup for sanitizer class
        /// </summary>
        /// <param name="_delimeter"></param>
        /// <param name="_exact"></param>
        public static void Setup(char[] _delimeter, int? _exact)
        {
            delimeter = _delimeter;
            exact = _exact;
        }
        /// <summary>
        /// Configurable splitter
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] Split(string text)
        {

            var rslt = text.Split(delimeter, StringSplitOptions.RemoveEmptyEntries);
            if (exact.HasValue)
            {
                if (rslt.Length != exact)
                {
                    throw new Exception("Invalid number of values in your arguments");
                }
            }
            return rslt;
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
            return Converter(text);
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
