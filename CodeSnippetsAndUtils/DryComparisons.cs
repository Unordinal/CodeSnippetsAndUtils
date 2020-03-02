using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CodeSnippetsAndUtils
{
    public static class DryComparisons
    {
        #region AnyOf<T>
        public static AnyOf<T> AnyOf<T>(T item1, T item2) where T : IComparable<T>
        {
            return new AnyOf<T>(item1, item2);
        }

        public static AnyOf<T> AnyOf<T>(T item1, T item2, T item3) where T : IComparable<T>
        {
            return new AnyOf<T>(item1, item2, item3);
        }
        
        public static AnyOf<T> AnyOf<T>(T item1, T item2, T item3, T item4) where T : IComparable<T>
        {
            return new AnyOf<T>(item1, item2, item3, item4);
        }
        
        public static AnyOf<T> AnyOf<T>(T item1, T item2, T item3, T item4, T item5) where T : IComparable<T>
        {
            return new AnyOf<T>(item1, item2, item3, item4, item5);
        }
        
        public static AnyOf<T> AnyOf<T>(T item1, T item2, T item3, T item4, T item5, T item6) where T : IComparable<T>
        {
            return new AnyOf<T>(item1, item2, item3, item4, item5, item6);
        }
        
        public static AnyOf<T> AnyOf<T>(T item1, T item2, T item3, T item4, T item5, T item6, T item7) where T : IComparable<T>
        {
            return new AnyOf<T>(item1, item2, item3, item4, item5, item6, item7);
        }
        #endregion

        #region AllOf<T>
        public static AllOf<T> AllOf<T>(T item1, T item2) where T : IComparable<T>
        {
            return new AllOf<T>(item1, item2);
        }

        public static AllOf<T> AllOf<T>(T item1, T item2, T item3) where T : IComparable<T>
        {
            return new AllOf<T>(item1, item2, item3);
        }
        
        public static AllOf<T> AllOf<T>(T item1, T item2, T item3, T item4) where T : IComparable<T>
        {
            return new AllOf<T>(item1, item2, item3, item4);
        }
        
        public static AllOf<T> AllOf<T>(T item1, T item2, T item3, T item4, T item5) where T : IComparable<T>
        {
            return new AllOf<T>(item1, item2, item3, item4, item5);
        }
        
        public static AllOf<T> AllOf<T>(T item1, T item2, T item3, T item4, T item5, T item6) where T : IComparable<T>
        {
            return new AllOf<T>(item1, item2, item3, item4, item5, item6);
        }
        
        public static AllOf<T> AllOf<T>(T item1, T item2, T item3, T item4, T item5, T item6, T item7) where T : IComparable<T>
        {
            return new AllOf<T>(item1, item2, item3, item4, item5, item6, item7);
        }
        #endregion

        public static FAnyOf<T> FAnyOf<T>(DryObject<T> item1, DryObject<T> item2) where T : IComparable<T>, IEquatable<T>
        {
            return new FAnyOf<T>(item1, item2);
        }
        
        public static FAnyOf<T> FAnyOf<T>(DryObject<T> item1, DryObject<T> item2, DryObject<T> item3) where T : IComparable<T>, IEquatable<T>
        {
            return new FAnyOf<T>(item1, item2, item3);
        }

        public static DryObject<T> AsDry<T>(Func<T> func) where T : IComparable<T>, IEquatable<T>
        {
            return func;
        }
    }

    public readonly struct AnyOf<T> where T : IComparable<T>
    {
        #region Fields
        private readonly T item1;
        private readonly T item2;
        private readonly T item3;
        private readonly T item4;
        private readonly T item5;
        private readonly T item6;
        private readonly T item7;
        #endregion

        #region Constructors
        public AnyOf(T item1, T item2)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item2;
            this.item4 = item2;
            this.item5 = item2;
            this.item6 = item2;
            this.item7 = item2;
        }
        
        public AnyOf(T item1, T item2, T item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item3;
            this.item5 = item3;
            this.item6 = item3;
            this.item7 = item3;
        }
        
        public AnyOf(T item1, T item2, T item3, T item4)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item4;
            this.item6 = item4;
            this.item7 = item4;
        }
        
        public AnyOf(T item1, T item2, T item3, T item4, T item5)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item5;
            this.item7 = item5;
        }
        
        public AnyOf(T item1, T item2, T item3, T item4, T item5, T item6)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.item7 = item6;
        }
        
        public AnyOf(T item1, T item2, T item3, T item4, T item5, T item6, T item7)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.item7 = item7;
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            return 
                (obj is AnyOf<T> that) &&
                this.item1.Equals(that.item1) &&
                this.item2.Equals(that.item2) &&
                this.item3.Equals(that.item3) &&
                this.item4.Equals(that.item4) &&
                this.item5.Equals(that.item5) &&
                this.item6.Equals(that.item6) &&
                this.item7.Equals(that.item7);
        }

        public override int GetHashCode()
        {
            int result = 37;
            result *= 397;
            result += item1.GetHashCode();
            result *= 397;
            result += item2.GetHashCode();
            result *= 397;
            result += item3.GetHashCode();
            result *= 397;
            result += item4.GetHashCode();
            result *= 397;
            result += item5.GetHashCode();
            result *= 397;
            result += item6.GetHashCode();
            result *= 397;
            result += item7.GetHashCode();
            return result;
        }
        #endregion

        #region Operators - (AnyOf<T>, T)
        public static bool operator ==(AnyOf<T> left, T right)
        {
            return 
                left.item1.Equals(right) ||
                left.item2.Equals(right) ||
                left.item3.Equals(right) ||
                left.item4.Equals(right) ||
                left.item5.Equals(right) ||
                left.item6.Equals(right) ||
                left.item7.Equals(right);
        }
        
        public static bool operator !=(AnyOf<T> left, T right)
        {
            return 
                !left.item1.Equals(right) ||
                !left.item2.Equals(right) ||
                !left.item3.Equals(right) ||
                !left.item4.Equals(right) ||
                !left.item5.Equals(right) ||
                !left.item6.Equals(right) ||
                !left.item7.Equals(right);
        }

        public static bool operator <(AnyOf<T> left, T right)
        {
            return
                left.item1.CompareTo(right) < 0 ||
                left.item2.CompareTo(right) < 0 ||
                left.item3.CompareTo(right) < 0 ||
                left.item4.CompareTo(right) < 0 ||
                left.item5.CompareTo(right) < 0 ||
                left.item6.CompareTo(right) < 0 ||
                left.item7.CompareTo(right) < 0;
        }
        
        public static bool operator >(AnyOf<T> left, T right)
        {
            return
                left.item1.CompareTo(right) > 0 ||
                left.item2.CompareTo(right) > 0 ||
                left.item3.CompareTo(right) > 0 ||
                left.item4.CompareTo(right) > 0 ||
                left.item5.CompareTo(right) > 0 ||
                left.item6.CompareTo(right) > 0 ||
                left.item7.CompareTo(right) > 0;
        }
        
        public static bool operator <=(AnyOf<T> left, T right)
        {
            return
                left.item1.CompareTo(right) <= 0 ||
                left.item2.CompareTo(right) <= 0 ||
                left.item3.CompareTo(right) <= 0 ||
                left.item4.CompareTo(right) <= 0 ||
                left.item5.CompareTo(right) <= 0 ||
                left.item6.CompareTo(right) <= 0 ||
                left.item7.CompareTo(right) <= 0;
        }
        
        public static bool operator >=(AnyOf<T> left, T right)
        {
            return
                left.item1.CompareTo(right) >= 0 ||
                left.item2.CompareTo(right) >= 0 ||
                left.item3.CompareTo(right) >= 0 ||
                left.item4.CompareTo(right) >= 0 ||
                left.item5.CompareTo(right) >= 0 ||
                left.item6.CompareTo(right) >= 0 ||
                left.item7.CompareTo(right) >= 0;
        }
        #endregion
        
        #region Operators - (AnyOf<T>, T) [Flipped]
        public static bool operator ==(T left, AnyOf<T> right)
        {
            return right == left;
        }
        
        public static bool operator !=(T left, AnyOf<T> right)
        {
            return right != left;
        }

        public static bool operator <(T left, AnyOf<T> right)
        {
            return right > left;
        }
        
        public static bool operator >(T left, AnyOf<T> right)
        {
            return right < left;
        }
        
        public static bool operator <=(T left, AnyOf<T> right)
        {
            return right >= left;
        }
        
        public static bool operator >=(T left, AnyOf<T> right)
        {
            return right <= left;
        }
        #endregion
    }
    
    public readonly struct AllOf<T> where T : IComparable<T>
    {
        #region Fields
        private readonly T item1;
        private readonly T item2;
        private readonly T item3;
        private readonly T item4;
        private readonly T item5;
        private readonly T item6;
        private readonly T item7;
        #endregion

        #region Constructors
        public AllOf(T item1, T item2)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item2;
            this.item4 = item2;
            this.item5 = item2;
            this.item6 = item2;
            this.item7 = item2;
        }
        
        public AllOf(T item1, T item2, T item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item3;
            this.item5 = item3;
            this.item6 = item3;
            this.item7 = item3;
        }
        
        public AllOf(T item1, T item2, T item3, T item4)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item4;
            this.item6 = item4;
            this.item7 = item4;
        }
        
        public AllOf(T item1, T item2, T item3, T item4, T item5)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item5;
            this.item7 = item5;
        }
        
        public AllOf(T item1, T item2, T item3, T item4, T item5, T item6)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.item7 = item6;
        }
        
        public AllOf(T item1, T item2, T item3, T item4, T item5, T item6, T item7)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.item7 = item7;
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            return 
                (obj is AllOf<T> that) &&
                this.item1.Equals(that.item1) &&
                this.item2.Equals(that.item2) &&
                this.item3.Equals(that.item3) &&
                this.item4.Equals(that.item4) &&
                this.item5.Equals(that.item5) &&
                this.item6.Equals(that.item6) &&
                this.item7.Equals(that.item7);
        }

        public override int GetHashCode()
        {
            int result = 37;
            result *= 397;
            result += item1.GetHashCode();
            result *= 397;
            result += item2.GetHashCode();
            result *= 397;
            result += item3.GetHashCode();
            result *= 397;
            result += item4.GetHashCode();
            result *= 397;
            result += item5.GetHashCode();
            result *= 397;
            result += item6.GetHashCode();
            result *= 397;
            result += item7.GetHashCode();
            return result;
        }
        #endregion

        #region Operators - (AllOf<T>, T)
        public static bool operator ==(AllOf<T> left, T right)
        {
            return 
                left.item1.Equals(right) &&
                left.item2.Equals(right) &&
                left.item3.Equals(right) &&
                left.item4.Equals(right) &&
                left.item5.Equals(right) &&
                left.item6.Equals(right) &&
                left.item7.Equals(right);
        }
        
        public static bool operator !=(AllOf<T> left, T right)
        {
            return 
                !left.item1.Equals(right) &&
                !left.item2.Equals(right) &&
                !left.item3.Equals(right) &&
                !left.item4.Equals(right) &&
                !left.item5.Equals(right) &&
                !left.item6.Equals(right) &&
                !left.item7.Equals(right);
        }

        public static bool operator <(AllOf<T> left, T right)
        {
            return
                left.item1.CompareTo(right) < 0 &&
                left.item2.CompareTo(right) < 0 &&
                left.item3.CompareTo(right) < 0 &&
                left.item4.CompareTo(right) < 0 &&
                left.item5.CompareTo(right) < 0 &&
                left.item6.CompareTo(right) < 0 &&
                left.item7.CompareTo(right) < 0;
        }
        
        public static bool operator >(AllOf<T> left, T right)
        {
            return
                left.item1.CompareTo(right) > 0 &&
                left.item2.CompareTo(right) > 0 &&
                left.item3.CompareTo(right) > 0 &&
                left.item4.CompareTo(right) > 0 &&
                left.item5.CompareTo(right) > 0 &&
                left.item6.CompareTo(right) > 0 &&
                left.item7.CompareTo(right) > 0;
        }
        
        public static bool operator <=(AllOf<T> left, T right)
        {
            return
                left.item1.CompareTo(right) <= 0 &&
                left.item2.CompareTo(right) <= 0 &&
                left.item3.CompareTo(right) <= 0 &&
                left.item4.CompareTo(right) <= 0 &&
                left.item5.CompareTo(right) <= 0 &&
                left.item6.CompareTo(right) <= 0 &&
                left.item7.CompareTo(right) <= 0;
        }
        
        public static bool operator >=(AllOf<T> left, T right)
        {
            return
                left.item1.CompareTo(right) >= 0 &&
                left.item2.CompareTo(right) >= 0 &&
                left.item3.CompareTo(right) >= 0 &&
                left.item4.CompareTo(right) >= 0 &&
                left.item5.CompareTo(right) >= 0 &&
                left.item6.CompareTo(right) >= 0 &&
                left.item7.CompareTo(right) >= 0;
        }
        #endregion

        #region Operators - (AllOf<T>, T) [Flipped]
        public static bool operator ==(T left, AllOf<T> right)
        {
            return right == left;
        }
        
        public static bool operator !=(T left, AllOf<T> right)
        {
            return right != left;
        }

        public static bool operator <(T left, AllOf<T> right)
        {
            return right > left;
        }
        
        public static bool operator >(T left, AllOf<T> right)
        {
            return right < left;
        }
        
        public static bool operator <=(T left, AllOf<T> right)
        {
            return right >= left;
        }
        
        public static bool operator >=(T left, AllOf<T> right)
        {
            return right <= left;
        }
        #endregion

        #region Operators - (AllOf<T>, AllOf<T>)
        public static bool operator ==(AllOf<T> left, AllOf<T> right)
        {
            return
                left == right.item1 &&
                right == left.item1;
        }
        
        public static bool operator !=(AllOf<T> left, AllOf<T> right)
        {
            return
                left != right.item1 &&
                right != left.item1;
        }
        
        public static bool operator <(AllOf<T> left, AllOf<T> right)
        {
            return
                left < right.item1 &&
                right > left.item1;
        }
        
        public static bool operator >(AllOf<T> left, AllOf<T> right)
        {
            return
                left > right.item1 &&
                right < left.item1;
        }
        
        public static bool operator <=(AllOf<T> left, AllOf<T> right)
        {
            return
                left <= right.item1 &&
                right >= left.item1;
        }
        
        public static bool operator >=(AllOf<T> left, AllOf<T> right)
        {
            return
                left >= right.item1 &&
                right <= left.item1;
        }
        #endregion
    }

    public readonly struct FAnyOf<T> : IEquatable<FAnyOf<T>> where T : IComparable<T>, IEquatable<T>
    {
        #region Fields
        private readonly DryObject<T> item1;
        private readonly DryObject<T> item2;
        private readonly DryObject<T> item3;
        private readonly DryObject<T> item4;
        private readonly DryObject<T> item5;
        private readonly DryObject<T> item6;
        private readonly DryObject<T> item7;
        #endregion

        #region Constructors
        public FAnyOf(DryObject<T> item1, DryObject<T> item2)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item2;
            this.item4 = item2;
            this.item5 = item2;
            this.item6 = item2;
            this.item7 = item2;
        }
        
        public FAnyOf(DryObject<T> item1, DryObject<T> item2, DryObject<T> item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item3;
            this.item5 = item3;
            this.item6 = item3;
            this.item7 = item3;
        }
        #endregion

        #region Overrides and Implementations
        public override int GetHashCode()
        {
            unchecked
            {
                int result = 37;
                result *= 397;
                result *= item1.GetHashCode();
                result *= item2.GetHashCode();
                result *= item3.GetHashCode();
                result *= item4.GetHashCode();
                result *= item5.GetHashCode();
                result *= item6.GetHashCode();
                result *= item7.GetHashCode();
                return result;
            }
        }

        public bool Equals([AllowNull] FAnyOf<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                this.item1.Equals(other.item1) &&
                this.item2.Equals(other.item2) &&
                this.item3.Equals(other.item3) &&
                this.item4.Equals(other.item4) &&
                this.item5.Equals(other.item5) &&
                this.item6.Equals(other.item6) &&
                this.item7.Equals(other.item7);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return (obj is FAnyOf<T> cObj) && this.Equals(cObj);
        }
        #endregion

        #region Operators
        #region Comparers - (FAnyOf<T>, T)
        public static bool operator ==(FAnyOf<T> left, T right)
        {
            return
                left.item1.Value.Equals(right) ||
                left.item2.Value.Equals(right) ||
                left.item3.Value.Equals(right) ||
                left.item4.Value.Equals(right) ||
                left.item5.Value.Equals(right) ||
                left.item6.Value.Equals(right) ||
                left.item7.Value.Equals(right);
        }
        
        public static bool operator !=(FAnyOf<T> left, T right)
        {
            return
                !left.item1.Value.Equals(right) ||
                !left.item2.Value.Equals(right) ||
                !left.item3.Value.Equals(right) ||
                !left.item4.Value.Equals(right) ||
                !left.item5.Value.Equals(right) ||
                !left.item6.Value.Equals(right) ||
                !left.item7.Value.Equals(right);
        }
        #endregion
        #endregion
    }

    public struct DryObject<T> : IComparable<DryObject<T>>, IEquatable<DryObject<T>> where T : IComparable<T>, IEquatable<T>
    {
        #region Fields
        private readonly Func<T> func;
        private T value;
        private bool hasValue;
        #endregion

        #region Properties
        public T Value
        {
            get
            {
                if (!hasValue)
                {
                    value = func();
                    hasValue = true;
                }

                return value;
            }
        }
        #endregion

        #region Constructors
        public DryObject(Func<T> value)
        {
            this.func = value;
            this.value = default;
            this.hasValue = false;
        }
        
        public DryObject(T value)
        {
            this.func = null;
            this.value = value;
            this.hasValue = true;
        }
        #endregion

        #region Overrides and Implementations
        public bool Equals([AllowNull] DryObject<T> other)
        {
            return
                this.func.Equals(other.func) &&
                this.value.Equals(other.value) &&
                this.hasValue.Equals(other.hasValue);
        }

        public override bool Equals(object obj)
        {
            return (obj is DryObject<T> cObj) && this.Equals(cObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 37;
                result *= 397;
                result *= this.func.GetHashCode();
                result *= 397;
                result *= this.value.GetHashCode();
                result *= 397;
                result *= this.hasValue.GetHashCode();
                return result;
            }
        }

        public int CompareTo([AllowNull] DryObject<T> other)
        {
            if (this.hasValue && !other.hasValue) return 1;
            if (!this.hasValue && other.hasValue) return -1;
            if (!this.hasValue && !other.hasValue) return 0;

            return
                this.value.CompareTo(other.value);
        }
        #endregion

        #region Conversions
        public static implicit operator DryObject<T>(Func<T> value)
        {
            return new DryObject<T>(value);
        }
        
        public static implicit operator DryObject<T>(T value)
        {
            return new DryObject<T>(value);
        }
        #endregion
    }
}
