// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    public interface IROVector3 : IROVector, IEquatable<IROVector3>
    {
        double X { get; }
        double Y { get; }
        double Z { get; }
        IVector2 XY { get; }
        IVector2 YZ { get; }
        IVector2 XZ { get; }
        IVector3 Normalized { get; }
    }

    public interface IVector3 : IVector, IROVector3, IEquatable<IVector3>
    {
        void Assign(IROVector3 aSrc);
        void Set(double aX, double aY, double aZ);

        new double X { get; set; }
        new double Y { get; set; }
        new double Z { get; set; }
        new IVector2 XY { get; set; }
        new IVector2 YZ { get; set; }
        new IVector2 XZ { get; set; }
    }

    public class TVector3 : TVector, IVector3, IEquatable<TVector3>
    {
        public static readonly IROVector3 AxisX = new TVector3(1.0, 0.0, 0.0);
        public static readonly IROVector3 AxisY = new TVector3(0.0, 1.0, 0.0);
        public static readonly IROVector3 AxisZ = new TVector3(0.0, 0.0, 1.0);

        public TVector3()
            : this(0.0, 0.0, 0.0)
        {
            return;
        }

        public TVector3(IROVector3 aSrc)
            : this(aSrc.X, aSrc.Y, aSrc.Z)
        {
            return;
        }

        public TVector3(double aX, double aY, double aZ)
            : base(3)
        {
            X = aX;
            Y = aY;
            Z = aZ;
            return;
        }

        public void Assign(IROVector3 aSrc)
        {
            Set(aSrc.X, aSrc.Y, aSrc.Z);
            return;
        }

        public void Set(double aX, double aY, double aZ)
        {
            X = aX;
            Y = aY;
            Z = aZ;
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

        public double Z
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        public IVector2 XY
        {
            get { return new TVector2(X, Y); }
            set { X = value.X; Y = value.Y; }
        }

        public IVector2 YZ
        {
            get { return new TVector2(Y, Z); }
            set { Y = value.X; Z = value.Y; }
        }

        public IVector2 XZ
        {
            get { return new TVector2(X, Z); }
            set { X = value.X; Z = value.Y; }
        }

        public TVector3 Normalized
        {
            get
            {
                TVector3 result = new TVector3(this);
                result.Normalize();
                return result;
            }
        }

        IVector3 IROVector3.Normalized => Normalized;

        #region operator
        public static TVector3 operator -(TVector3 aValue)
        {
            return new TVector3(-aValue.X, -aValue.Y, -aValue.Z);
        }

        public static TVector3 operator +(TVector3 aLeft, TVector3 aRight)
        {
            return new TVector3(aLeft.X + aRight.X, aLeft.Y + aRight.Y, aLeft.Z + aRight.Z);
        }

        public static TVector3 operator -(TVector3 aLeft, TVector3 aRight)
        {
            return aLeft + (-aRight);
        }

        public static TVector3 operator *(TVector3 aLeft, double aRight)
        {
            return new TVector3(aLeft.X * aRight, aLeft.Y * aRight, aLeft.Z * aRight);
        }

        public static TVector3 operator *(double aLeft, TVector3 aRight)
        {
            return aRight * aLeft;
        }

        public static TVector3 operator /(TVector3 aLeft, double aRight)
        {
            return aLeft * (1.0 / aRight);
        }
        #endregion

        public static TVector3 Add(IROVector3 aLeft, IROVector3 aRight)
        {
            return new TVector3(aLeft.X + aRight.X,
                                aLeft.Y + aRight.Y,
                                aLeft.Z + aRight.Z);
        }

        public static TVector3 Subtract(IROVector3 aLeft, IROVector3 aRight)
        {
            return new TVector3(aLeft.X - aRight.X,
                                aLeft.Y - aRight.Y,
                                aLeft.Z - aRight.Z);
        }

        public static TVector3 Multiply(IROVector3 aLeft, double aRight)
        {
            return new TVector3(aLeft.X * aRight,
                                aLeft.Y * aRight,
                                aLeft.Z * aRight);
        }

        public static TVector3 Multiply(double aLeft, IROVector3 aRight)
        {
            return new TVector3(aLeft * aRight.X,
                                aLeft * aRight.Y,
                                aLeft * aRight.Z);
        }

        public static TVector3 Divide(IROVector3 aLeft, double aRight)
        {
            return new TVector3(aLeft.X / aRight,
                                aLeft.Y / aRight,
                                aLeft.Z / aRight);
        }

        public static TVector3 CrossProduct(IROVector3 aLeft, IROVector3 aRight)
        {
            return new TVector3((aLeft.Y * aRight.Z) - (aLeft.Z * aRight.Y),
                                (aLeft.Z * aRight.X) - (aLeft.X * aRight.Z),
                                (aLeft.X * aRight.Y) - (aLeft.Y * aRight.X));
        }

        public bool Equals(IROVector3 aOther)
        {
            return (X.AlmostEqual(aOther.X) && Y.AlmostEqual(aOther.Y) && Z.AlmostEqual(aOther.Z));
        }

        public bool Equals(IVector3 aOther)
        {
            return Equals((IROVector3)aOther);
        }

        public bool Equals(TVector3 aOther)
        {
            return ((object)this).Equals(aOther) || Equals((IVector3)aOther);
        }
    }
}
