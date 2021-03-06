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
        static string[] delimeter = null;
        static int? exact = 0;
        static bool denyNeg = false;
        static int? maxNum = null;
        //0 = add, 1 = sub, 2 = mult, 3 = div
        public static int mathType = 0;
        /// <summary>
        /// Setup for sanitizer class
        /// </summary>
        /// <param name="_delimeter"></param>
        /// <param name="_exact"></param>
        public static void Setup(string[] _delimeter, int? _exact, int? _maxNum, bool _denyNeg = false)
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
        public static string SetupProperties(string text)
        {
            //get custom props values\*/....neg,upper###[sub/mult/div]
            var propindex = text.IndexOf("/*/");
            //make a rule, props must be at the end of the string. makes this easier...otherwise split this off from the rest and process seperately
            if(propindex > 0)
            {
                var prop = text.Substring(propindex);
                var propstring = prop.Replace("/*/", "");
                var props = propstring.Split(",");
                foreach(var rslt in props)
                {
                    if(rslt == "neg")
                    {
                        denyNeg = true;
                    }
                    if(rslt.IndexOf("upper") >= 0)
                    {
                        var index = rslt.IndexOf("upper");
                        var max = rslt.Substring(index + 5);
                        maxNum = Converter(max);
                    }
                    if(rslt == "add")
                    {
                        mathType = 0;
                    }
                    if(rslt == "sub")
                    {
                        mathType = 1;
                    }
                    if(rslt == "mult")
                    {
                        mathType = 2;
                    }
                    if(rslt == "div")
                    {
                        mathType = 3;
                    }
                }
                //clean up the string for processing values
                text = text.Substring(0, propindex);
            }
            //do the deliminator
            var delim = text.IndexOf("//");
            if(delim == 0)
            {
                //get index of first \n char.
                var newIndex = text.IndexOf("\n");
                if(newIndex > 0)
                {
                    //pull out the deliminator string
                    var addDelim = text.Substring(delim, newIndex + 1);
                    var delims = addDelim.Substring(2, addDelim.Length -4).Split("][");
                    var strings = delimeter.ToList();
                    foreach(var val in delims)
                    {
                        strings.Add(val.Replace("[", "").Replace("]", ""));
                    }
                    delimeter = strings.ToArray();
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
