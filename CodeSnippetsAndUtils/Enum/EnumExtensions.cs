using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSnippetsAndUtils.Enums
{
    public static class EnumExtensions
    {
        public static bool HasAllFlags<T>(this T self, T other) where T : struct, Enum
        {
            long selfCast = (long)(object)self;
            long otherCast = (long)(object)other;
            return (selfCast & otherCast) == otherCast;
        }
        
        public static bool HasAnyFlags<T>(this T self, T other) where T : struct, Enum
        {
            long selfCast = (long)(object)self;
            long otherCast = (long)(object)other;
            return (selfCast & otherCast) != 0;
        }
        
        public static T CommonFlags<T>(this T self, T other) where T : struct, Enum
        {
            long selfCast = (long)(object)self;
            long otherCast = (long)(object)other;
            return (T)(object)(selfCast & otherCast);
        }
    }
}
