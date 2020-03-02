using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSnippetsAndUtils
{
    public static class MathUtils
    {
        public static bool IsInteger(float value, double epsilon = double.Epsilon)
        {
            return Math.Abs(value % 1) <= (epsilon * 100);
        }
        
        public static bool IsInteger(double value, double epsilon = double.Epsilon)
        {
            return Math.Abs(value % 1) <= (epsilon * 100);
        }
    }
}
