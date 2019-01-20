using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    public interface IROQuaternion : IEquatable<IROQuaternion>
    {
        double Real { get; }
        IROVector3 Imaginary { get; }

        double Norm { get; }
        IROQuaternion Conjugated { get; }
        IROQuaternion Normalized { get; }
    }

    public interface IQuaternion : IROQuaternion
    {
        new double Real { get; set; }
        new IROVector3 Imaginary { get; set; }
    }

    public class TQuaternion : IROQuaternion, IEquatable<TQuaternion>
    {
        public TQuaternion()
        {
            return;
        }

        public TQuaternion(double aReal, double aImaginX, double aImaginY, double aImaginZ)
        {
            Real = aReal;
            Imaginary.Set(aImaginX, aImaginY, aImaginZ);
            return;
        }

        public TQuaternion(double aReal, IROVector3 aImaginary)
            : this(aReal, aImaginary.X, aImaginary.Y, aImaginary.Z)
        {
            return;
        }

        public TQuaternion(IROQuaternion aSrc)
            : this(aSrc.Real, aSrc.Imaginary)
        {
            return;
        }

        public TQuaternion(IROEulerAngle aAngle)
        {
            SetEulerAngle(aAngle);
            return;
        }

        public double Real
        { get; set; } = 0.0;
        public IVector3 Imaginary
        { get; set; } = new TVector3();

        public double Norm
        { get { return (Real * Real) + Imaginary.Length2; } }
        public double Abs
        { get { return Math.Sqrt(Norm); } }
        public TQuaternion Conjugated
        {
            get
            {
                TQuaternion result = new TQuaternion(this);
                result.Conjugate();

                return result;
            }
        }
        public TQuaternion Normalized
        {
            get
            {
                TQuaternion result = new TQuaternion(this);
                result.Normalize();

                return result;
            }
        }

        public void Conjugate()
        {
            Imaginary = -(new TVector3(Imaginary));
            return;
        }

        public bool Normalize()
        {
            double norm = Norm;
            if (norm.IsZero())
                return false;

            double abs = Math.Abs(norm);
            Imaginary = (new TVector3(Imaginary)) / abs;
            Real /= abs;

            return true;
        }

        public void SetRotation(double aAngleRad, double aAxisX, double aAxisY, double aAxisZ)
        {
            SetRotation(aAngleRad, new TVector3(aAxisX, aAxisY, aAxisZ));
            return;
        }

        public void SetRotation(double aAngleRad, IROVector3 aAxis)
        {
            Real = Math.Cos(aAngleRad * 0.5);
            Imaginary = (new TVector3(aAxis.Normalized)) * Math.Sin(aAngleRad * 0.5);
            return;
        }

        public void SetEulerAngle(double aYawRad, double aPitchRad, double aRollRad)
        {
            TQuaternion quat = TQuaternion.MakeRotation(aYawRad, TVector3.AxisY)
                             * TQuaternion.MakeRotation(aPitchRad, TVector3.AxisX)
                             * TQuaternion.MakeRotation(aRollRad, TVector3.AxisZ);

            this.Real = quat.Real;
            this.Imaginary = new TVector3(quat.Imaginary);
            return;
        }

        public void SetEulerAngle(IROEulerAngle aAngle)
        {
            SetEulerAngle(aAngle.YawRad, aAngle.PitchRad, aAngle.RollRad);
            return;
        }

        public IEulerAngle GetEulerAngle()
        {
            TVector3 front = new TVector3(0.0, 0.0, 1.0);
            TVector3 up = new TVector3(0.0, 1.0, 0.0);

            TQuaternion quat = this.Normalized;
            front = quat * front;
            up = quat * up;

            return TEulerAngle.EstimateFrom(front, up);
        }

        public static TQuaternion MakeRotation(double aAngleRad, double aAxisX, double aAxisY, double aAxisZ)
        {
            return MakeRotation(aAngleRad, new TVector3(aAxisX, aAxisY, aAxisZ));
        }

        public static TQuaternion MakeRotation(double aAngleRad, IROVector3 aAxis)
        {
            TQuaternion result = new TQuaternion();
            result.SetRotation(aAngleRad, aAxis);

            return result;
        }

        public static TQuaternion MakeEulerAngle(double aYawRad, double aPitchRad, double aRollRad)
        {
            return MakeEulerAngle(new TEulerAngle(aYawRad, aPitchRad, aRollRad));
        }

        public static TQuaternion MakeEulerAngle(IROEulerAngle aAngle)
        {
            return new TQuaternion(aAngle);
        }

        public static TQuaternion Add(IROQuaternion aLeft, IROQuaternion aRight)
        {
            return new TQuaternion(aLeft.Real + aRight.Real, TVector3.Add(aLeft.Imaginary, aRight.Imaginary));
        }

        public static TQuaternion Subtract(IROQuaternion aLeft, IROQuaternion aRight)
        {
            return new TQuaternion(aLeft.Real - aRight.Real, TVector3.Subtract(aLeft.Imaginary, aRight.Imaginary));
        }

        public static TQuaternion Multiply(IROQuaternion aLeft, IROQuaternion aRight)
        {
            TQuaternion result = new TQuaternion();
            result.Real = (aLeft.Real * aRight.Real) - (TVector.DotProduct(aLeft.Imaginary, aRight.Imaginary));
            result.Imaginary = (TVector3.Multiply(aLeft.Real, aRight.Imaginary) +
                                TVector3.Multiply(aLeft.Imaginary, aRight.Real) +
                                TVector3.CrossProduct(aLeft.Imaginary, aRight.Imaginary));
            return result;
        }

        public static TQuaternion Multiply(IROQuaternion aLeft, double aRight)
        {
            return new TQuaternion(aLeft.Real * aRight, TVector3.Multiply(aLeft.Imaginary, aRight));
        }

        public static TQuaternion Multiply(double aLeft, IROQuaternion aRight)
        {
            return Multiply(aRight, aLeft);
        }

        public static TVector3 Multiply(IROQuaternion aLeft, IROVector3 aRight)
        {
            TQuaternion quat = new TQuaternion(aLeft);
            TQuaternion conj = ~quat;
            TQuaternion result = quat * (new TQuaternion(0.0, aRight)) * conj;

            return new TVector3(result.Imaginary);
        }

        public static TQuaternion Divide(IROQuaternion aLeft, double aRight)
        {
            return Multiply(aLeft, 1.0 / aRight);
        }

        public bool Equals(IROQuaternion aOther)
        {
            return Real.AlmostEqual(aOther.Real) && Imaginary.Equals(aOther.Imaginary);
        }

        public bool Equals(IQuaternion aOther)
        {
            return Equals((IROQuaternion)aOther);
        }

        public bool Equals(TQuaternion aOther)
        {
            return ((object)this).Equals(aOther) || this.Equals((IQuaternion)aOther);
        }

        public static TQuaternion operator ~(TQuaternion aValue)
        {
            return aValue.Conjugated;
        }

        public static TQuaternion operator -(TQuaternion aValue)
        {
            return new TQuaternion(-aValue.Real, -(new TVector3(aValue.Imaginary)));
        }

        public static TQuaternion operator +(TQuaternion aLeft, TQuaternion aRight)
        {
            return Add(aLeft, aRight);
        }

        public static TQuaternion operator +(TQuaternion aLeft, IROQuaternion aRight)
        {
            return Add(aLeft, aRight);
        }

        public static TQuaternion operator +(IROQuaternion aLeft, TQuaternion aRight)
        {
            return Add(aLeft, aRight);
        }

        public static TQuaternion operator -(TQuaternion aLeft, TQuaternion aRight)
        {
            return Subtract(aLeft, aRight);
        }

        public static TQuaternion operator -(TQuaternion aLeft, IROQuaternion aRight)
        {
            return Subtract(aLeft, aRight);
        }

        public static TQuaternion operator -(IROQuaternion aLeft, TQuaternion aRight)
        {
            return Subtract(aLeft, aRight);
        }

        public static TQuaternion operator *(TQuaternion aLeft, TQuaternion aRight)
        {
            return Multiply(aLeft, aRight);
        }

        public static TQuaternion operator *(IROQuaternion aLeft, TQuaternion aRight)
        {
            return Multiply(aLeft, aRight);
        }

        public static TQuaternion operator *(TQuaternion aLeft, IROQuaternion aRight)
        {
            return Multiply(aLeft, aRight);
        }

        public static TQuaternion operator *(TQuaternion aLeft, double aRight)
        {
            return Multiply(aLeft, aRight);
        }

        public static TQuaternion operator *(double aLeft, TQuaternion aRight)
        {
            return Multiply(aLeft, aRight);
        }

        public static TVector3 operator *(TQuaternion aLeft, IVector3 aRight)
        {
            return Multiply(aLeft, aRight);
        }

        public static TQuaternion operator /(TQuaternion aLeft, double aRight)
        {
            return Divide(aLeft, aRight);
        }

        public static double operator ^(TQuaternion aLeft, TQuaternion aRight)
        {
            return ((aLeft.Real * aRight.Real) + TVector3.DotProduct(aLeft.Imaginary, aRight.Imaginary));
        }

        public static double operator ^(IROQuaternion aLeft, TQuaternion aRight)
        {
            return ((aLeft.Real * aRight.Real) + TVector3.DotProduct(aLeft.Imaginary, aRight.Imaginary));
        }

        public static double operator ^(TQuaternion aLeft, IROQuaternion aRight)
        {
            return ((aLeft.Real * aRight.Real) + TVector3.DotProduct(aLeft.Imaginary, aRight.Imaginary));
        }

        public static TQuaternion LinearInterpolate(IROQuaternion aValue1, IROQuaternion aValue2, double aRatio)
        {
            TQuaternion result = TQuaternion.Multiply(1.0 - aRatio, aValue1) + TQuaternion.Multiply(aRatio, aValue2);

            return result.Normalized;
        }

        public static TQuaternion SphereInterpolate(IROQuaternion aValue1, IROQuaternion aValue2, double aRatio)
        {
            TQuaternion q1 = new TQuaternion(aValue1);
            TQuaternion q2 = new TQuaternion(aValue2);
            if (q1.Equals(q2) || q1.Equals(-q2))
                return q1;

            if ((q1 ^ q2) < 0.0)
                q2 = -q2;

            q1.Normalize();
            q2.Normalize();

            double prod = (q1 ^ q2).Clamp(-1.0, 1.0);

            double thete = Math.Acos(prod);
            double sThete = Math.Sin(prod);

            if (sThete.IsZero())
                return LinearInterpolate(q1, q2, aRatio);
            else
                return ((Math.Sin(thete * (1.0 - aRatio)) / sThete) * q1) + ((Math.Sin(thete * aRatio) / sThete) * q2);
        }

        double IROQuaternion.Real => Real;
        IROVector3 IROQuaternion.Imaginary => Imaginary;
        IROQuaternion IROQuaternion.Conjugated => Conjugated;
        IROQuaternion IROQuaternion.Normalized => Normalized;
    }
}
