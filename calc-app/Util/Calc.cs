using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc_app.Util
{
    /// <summary>
    /// Calculates the value of the strings. Returns as a string for output.
    /// </summary>
    static class Calc
    {
        public static decimal DoMath(decimal a, decimal b, int type)
        {
            if (type == 0)
            {
                Logger.Add(b.ToString());
                return add(a, b);
            }
            else if (type == 1)
            {
                Logger.Add(b.ToString());
                return subtract(a, b);
            }
            else if (type == 2)
            {
                if (a == 0)
                {
                    a = 1;
                }
                if (b == 0)
                {
                    return a;
                }
                Logger.Add(b.ToString());
                return multiply(a, b);
            }
            else if (type == 3)
            {
                if (b == 0)
                {
                    return a;
                }
                Logger.Add(b.ToString());
                if (a == 0)
                {
                    return b;
                }
                return divide(a, b);
            }
            return 0;
        }
        public static decimal add(decimal a, decimal b)
        {
            return a + b;
        }
        public static decimal subtract(decimal a, decimal b)
        {
            return a - b;
        }
        public static decimal divide(decimal a, decimal b)
        {
            return a / b;
        }
        public static decimal multiply(decimal a, decimal b)
        {
            return a * b;
        }
    }
}
