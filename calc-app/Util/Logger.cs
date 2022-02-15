using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc_app.Util
{
    static class Logger
    {
        public static int Modifier = 0;
        
        public static string Log = "";
        public static void Reset()
        {
            Modifier = 0;
            Log = "";
        }
        public static void Add(string val)
        {
            if (string.IsNullOrEmpty(Log))
            {
                Log = val;
            } else
            {
                Log = $"{Log}{GetMod()}{val}";
            }
        }
        internal static string GetMod()
        {
            if(Modifier == 0)
            {
                return "+";
            } else if(Modifier == 1)
            {
                return "-";
            }
            else if (Modifier == 2)
            {
                return "*";
            }
            else if (Modifier == 3)
            {
                return "/";
            }
            return "";
        }
    }
}
