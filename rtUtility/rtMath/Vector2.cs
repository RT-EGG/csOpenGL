// System
using System;

namespace rtUtility.rtMath
{
    public interface IROVector2 : IROVector, IEquatable<IROVector2>
    {
        double X { get; }
        double Y { get; }
        IVector2 Normalized { get; }
    }

    public interface IVector2 : IROVector2, IEquatable<IVector2>
    {
        void Assign(IROVector2 aSrc);
        void Set(double aX, double aY);
        new double X { get; set; }
        new double Y { get; set; }
    }

    public class TVector2 : TVector, IVector2, IEquatable<TVector2>
    {
        public TVector2()
            : this(0.0, 0.0)
        {
            return;
        }

        public TVector2(IROVector2 aSrc)
            : this(aSrc.X, aSrc.Y)
        {
            return;
        }

        public TVector2(double aX, double aY)
            : base(2)
        {
            X = aX;
            Y = aY;
            return;
        }

        public void Assign(IROVector2 aSrc)
        {
            X = aSrc.X;
            Y = aSrc.Y;
        }

        public void Set(double aX, double aY)
        {
            X = aX;
            Y = aY;
            return;
        }

        public double X
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        public double Y
        {
            get { return this[1]; }
            set { this[1] = value; }
        }

        public TVector2 Normalized
        {
            get
            {
                TVector2 result = new TVector2(this);
                result.Normalize();
                return result;
            }
        }

        IVector2 IROVector2.Normalized => Normalized;

        #region operator
        public static TVector2 operator -(TVector2 aValue)
        {
            return new TVector2(-aValue.X, -aValue.Y);
        }

        public static TVector2 operator +(TVector2 aLeft, TVector2 aRight)
        {
            return new TVector2(aLeft.X + aRight.X, aLeft.Y + aRight.Y);
        }

        public static TVector2 operator -(TVector2 aLeft, TVector2 aRight)
        {
            return aLeft + (-aRight);
        }

        public static TVector2 operator *(TVector2 aLeft, double aRight)
        {
            return new TVector2(aLeft.X * aRight, aLeft.Y * aRight);
        }

        public static TVector2 operator *(double aLeft, TVector2 aRight)
        {
            return aRight * aLeft;
        }

        public static TVector2 operator /(TVector2 aLeft, double aRight)
        {
            return aLeft * (1.0 / aRight);
        }
        #endregion

        public static double CrossProduct(IROVector2 aLeft, IROVector2 aRight)
        {
            return (aLeft.X * aRight.Y) - (aLeft.Y * aRight.X);
        }

        public bool Equals(IROVector2 aOther)
        {
            return (X.AlmostEqual(aOther.X) && Y.AlmostEqual(aOther.Y));
        }

        public bool Equals(IVector2 aOther)
        {
            return Equals((IROVector2)aOther);
        }

        public bool Equals(TVector2 aOther)
        {
            return ((object)this).Equals(aOther) || Equals((IVector2)aOther);
        }
    }
}
