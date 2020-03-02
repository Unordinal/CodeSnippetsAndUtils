using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CodeSnippetsAndUtils.ASCIITypes
{
    // Instance Members
    /// <summary>
    /// Represents a character as an ASCII code unit.
    /// </summary>
    public readonly partial struct ASCIIChar
    {
        private readonly byte asciiCode;

        public ASCIIChar(char value)
        {
            if (!IsValid(value)) throw new ArgumentOutOfRangeException(nameof(value));
            
            this.asciiCode = (byte)value;
        }
        
        public ASCIIChar(byte value)
        {
            if (!IsValid(value)) throw new ArgumentOutOfRangeException(nameof(value));

            this.asciiCode = value;
        }
    }

    // Static Members
    public readonly partial struct ASCIIChar
    {
        public static readonly ASCIIChar MinValue = new ASCIIChar(0);
        public static readonly ASCIIChar MaxValue = new ASCIIChar(127);

        #region Validation
        /// <summary>
        /// Indicates whether the specified character is a valid ASCII character.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsValid(char value)
        {
            return value >= 0 && value <= 127;
        }

        /// <summary>
        /// Indicates whether the specified byte is a valid representation of an ASCII character.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsValid(byte value)
        {
            return value >= 0 && value <= 127;
        }

        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as an ASCII letter.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsLetter(ASCIIChar value)
        {
            return
                (value >= 65 && value <= 90) ||
                (value >= 97 && value <= 122);
        }

        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as a decimal digit.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsDigit(ASCIIChar value)
        {
            return value >= 48 && value <= 57;
        }

        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as a letter or decimal digit.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsLetterOrDigit(ASCIIChar value)
        {
            return
                (value >= 65 && value <= 90) ||
                (value >= 97 && value <= 122) ||
                (value >= 48 && value <= 57);
        }

        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as an uppercase letter.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsUpper(ASCIIChar value)
        {
            return value >= 65 && value <= 90;
        }

        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as a lowercase letter.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsLower(ASCIIChar value)
        {
            return value >= 97 && value <= 122;
        }

        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as a control character.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsControl(ASCIIChar value)
        {
            return (value >= 0 && value <= 31) || value == 127;
        }
        
        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as a punctuation mark.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsPunctuation(ASCIIChar value)
        {
            return
                (value >= 33 && value <= 35) ||
                (value >= 37 && value <= 42) ||
                (value >= 44 && value <= 47) ||
                (value >= 58 && value <= 59) ||
                (value >= 63 && value <= 64) ||
                (value >= 91 && value <= 93) ||
                (value == 95) || (value == 123) || (value == 125);
        }
        
        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as a symbol character.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsSymbol(ASCIIChar value)
        {
            return
                (value == 36)  ||
                (value == 43)  ||
                (value >= 60 && value <= 62) ||
                (value == 94)  ||
                (value == 96)  ||
                (value == 124) ||
                (value == 126);
        }
        
        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as a separator character.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsSeparator(ASCIIChar value)
        {
            return value >= 28 && value <= 31;
        }

        /// <summary>
        /// Indicates whether the specified ASCII character is categorized as white space.
        /// </summary>
        /// <param name="value">The ASCII character to evaluate.</param>
        /// <returns></returns>
        public static bool IsWhiteSpace(ASCIIChar value)
        {
            return value == 0 || (value >= 9 && value <= 13) || value == 32;
        }
        #endregion

        #region Conversion
        /// <summary>
        /// Converts the value of an ASCII character to its uppercase equivalent.
        /// </summary>
        /// <param name="value">The ASCII character to convert.</param>
        /// <returns></returns>
        public static ASCIIChar ToUpper(ASCIIChar value)
        {
            return new ASCIIChar(IsLower(value) ? (byte)(value + 32) : value);
        }

        /// <summary>
        /// Converts the value of an ASCII character to its lowercase equivalent.
        /// </summary>
        /// <param name="value">The ASCII character to convert.</param>
        /// <returns></returns>
        public static ASCIIChar ToLower(ASCIIChar value)
        {
            return new ASCIIChar(IsUpper(value) ? (byte)(value - 32) : value);
        }

        /// <summary>
        /// Converts the value of the specified character to its equivalent ASCII character.
        /// </summary>
        /// <param name="value">A valid ASCII character.</param>
        /// <returns></returns>
        public static ASCIIChar Parse(char value)
        {
            if (!IsValid(value)) throw new ArgumentOutOfRangeException(nameof(value));

            return (ASCIIChar)value;
        }

        /// <summary>
        /// Converts the value of the specified string to its equivalent ASCII character.
        /// </summary>
        /// <param name="value">A string that contains a single character, or null.</param>
        /// <returns></returns>
        public static ASCIIChar Parse(string value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (value.Length != 1) throw new FormatException("Input string must be a single character.");

            if (!IsValid(value[0])) throw new ArgumentOutOfRangeException(nameof(value));

            return (ASCIIChar)value[0];
        }

        /// <summary>
        /// Converts the value of the specified character to its equivalent ASCII character. A return code indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">A single character.</param>
        /// <param name="result">
        /// When this method returns, contains an ASCII character equivalent to the character represented by <paramref name="value"/>, if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed.
        /// The conversion fails if <paramref name="value"/> is greater than 127.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns></returns>
        public static bool TryParse(char value, out ASCIIChar result)
        {
            result = MinValue;
            
            if (!IsValid(value)) return false;

            result = (ASCIIChar)value;
            return true;
        }

        /// <summary>
        /// Converts the value of the specified string to its equivalent ASCII character. A return code indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">A string that contains a single character, or null.</param>
        /// <param name="result">
        /// When this method returns, contains an ASCII character equivalent to the character represented by <paramref name="value"/>, if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed.
        /// The conversion fails if <paramref name="value"/> is <see langword="null"/> or the length of <paramref name="value"/> is not 1.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns></returns>
        public static bool TryParse(string value, out ASCIIChar result)
        {
            result = MinValue;
            
            if (value is null) return false;
            if (value.Length != 1) return false;
            if (!IsValid(value[0])) return false;

            result = (ASCIIChar)value[0];
            return true;
        }
        #endregion

        #region Operators
        public static explicit operator char(ASCIIChar value)
        {
            return (char)value.asciiCode;
        }
        public static implicit operator ASCIIChar(char value)
        {
            return new ASCIIChar(value);
        }

        public static implicit operator byte(ASCIIChar value)
        {
            return value.asciiCode;
        }
        public static explicit operator ASCIIChar(byte value)
        {
            return new ASCIIChar(value);
        }
        #endregion
    }

    // Implementations and Overrides
    public readonly partial struct ASCIIChar : IComparable, IComparable<ASCIIChar>, IConvertible, IEquatable<ASCIIChar>
    {
        public override bool Equals(object? obj)
        {
            return (obj is ASCIIChar asciiChar) ? Equals(asciiChar) : false;
        }

        public bool Equals(ASCIIChar other)
        {
            return this.asciiCode == other.asciiCode;
        }

        public override int GetHashCode()
        {
            return this.asciiCode.GetHashCode();
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (obj is ASCIIChar asciiChar) return this.CompareTo(asciiChar);

            throw new ArgumentException("Object is not a valid ASCIIChar.", nameof(obj));
        }

        public int CompareTo(ASCIIChar other)
        {
            return this.asciiCode.CompareTo(other.asciiCode);
        }

        public override string ToString()
        {
            return ((char)this.asciiCode).ToString();
        }

        public string ToString(IFormatProvider? provider)
        {
            return ((char)this.asciiCode).ToString(provider);
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Byte;
        }

        #region IConvertible
        bool IConvertible.ToBoolean(IFormatProvider? provider)
        {
            return this.asciiCode != 0;
        }

        byte IConvertible.ToByte(IFormatProvider? provider)
        {
            return this.asciiCode;
        }

        char IConvertible.ToChar(IFormatProvider? provider)
        {
            return ((char)this.asciiCode);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            throw new InvalidCastException();
        }

        decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            return this.asciiCode;
        }

        double IConvertible.ToDouble(IFormatProvider? provider)
        {
            return this.asciiCode;
        }

        short IConvertible.ToInt16(IFormatProvider? provider)
        {
            return this.asciiCode;
        }

        int IConvertible.ToInt32(IFormatProvider? provider)
        {
            return this.asciiCode;
        }

        long IConvertible.ToInt64(IFormatProvider? provider)
        {
            return this.asciiCode;
        }

        sbyte IConvertible.ToSByte(IFormatProvider? provider)
        {
            return unchecked((sbyte)this.asciiCode);
        }

        float IConvertible.ToSingle(IFormatProvider? provider)
        {
            return this.asciiCode;
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            return Convert.ChangeType(this.asciiCode, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider? provider)
        {
            return this.asciiCode;
        }

        uint IConvertible.ToUInt32(IFormatProvider? provider)
        {
            return this.asciiCode;
        }

        ulong IConvertible.ToUInt64(IFormatProvider? provider)
        {
            return this.asciiCode;
        }
        #endregion
    }
}
