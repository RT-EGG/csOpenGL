// System
using System;
using System.Runtime.CompilerServices;

namespace rtUtility.rtMath
{
    public interface IROEulerAngle : IEquatable<IROEulerAngle>
    {
        double YawDeg { get; }
        double YawRad { get; }
        double PitchDeg { get; }
        double PitchRad { get; }
        double RollDeg { get; }
        double RollRad { get; }
    }

    public interface IEulerAngle : IROEulerAngle, IEquatable<IEulerAngle>
    {
        new double YawDeg { get; set; }
        new double YawRad { get; set; }
        new double PitchDeg { get; set; }
        new double PitchRad { get; set; }
        new double RollDeg { get; set; }
        new double RollRad { get; set; }
    }

    public class TEulerAngle : IEulerAngle, IEquatable<TEulerAngle>
    {
        public TEulerAngle()
            : this(0.0, 0.0, 0.0)
        {
            return;
        }

        public TEulerAngle(IEulerAngle aAngle)
            : this(aAngle.YawRad, aAngle.PitchRad, aAngle.RollRad)
        {
            return;
        }

        public TEulerAngle(double aYawRad, double aPitchRad, double aRollRad)
        {
            YawRad = aYawRad;
            PitchRad = aPitchRad;
            RollRad = aRollRad;
            return;
        }

        public static IEulerAngle EstimateFrom(IVector3 aZ, IVector3 aY)
        {
            TEulerAngle result = new TEulerAngle();

            TVector3 z = new TVector3(aZ);
            TVector3 y = new TVector3(aY);

            if (!(z.Normalize() && y.Normalize()))
                return result; // zero vector

            // z to pitch
            result.PitchRad = Math.Asin(-z.Y);
            // z to yaw
            if (z.Z.IsZero()) {
                if (z.X.IsZero()) {
                    result.YawRad = Math.Atan2(y.X, y.Z);
                    if (z.Y > 0.0)
                        result.YawRad += Math.PI;
                } else {
                    result.YawRad = Math.PI * 0.5;
                    if (z.X < 0.0)
                        result.YawRad *= -1.0;
                }
            } else {
                result.YawRad = Math.Atan2(z.X, z.Z);
            }
            result.YawRad = result.YawRad.Modulate(-Math.PI, Math.PI);

            // y to roll
            if (!y.Y.IsZero()) { // not polar direction
                TVector3 y2 = TMatrix44.MakeRotateMatrixPitch(result.PitchRad) * TMatrix44.MakeRotateMatrixYaw(result.YawRad) * (new TVector3H(0.0, 1.0, 0.0, 0.0));
                result.RollRad = Math.Acos(TVector.DotProduct(y, y2).Clamp(-1.0, 1.0));

                if (z.Z.IsZero()) {
                    if ((z.X > 0.0) == (TVector3.CrossProduct(y, y2).X > 0.0))
                        result.RollRad *= -1.0;
                } else {
                    if (result.YawRad.InRange(-Math.PI * 0.5, Math.PI * 0.5) == (TVector3.CrossProduct(y, y2).Z > 0.0))
                        result.RollRad *= -1.0;
                }
            }
            result.RollRad = result.RollRad.Modulate(-Math.PI, Math.PI);

            return result;
        }

        public double YawDeg
        {
            get { return RadToDeg(YawRad); }
            set { YawRad = DegToRad(value); }
        }

        public double YawRad
        { get; set; } = 0.0;

        public double PitchDeg
        {
            get { return RadToDeg(PitchRad); }
            set { PitchRad = DegToRad(value); }
        }

        public double PitchRad
        { get; set; } = 0.0;

        public double RollDeg
        {
            get { return RadToDeg(RollRad); }
            set { RollRad = DegToRad(value); }
        }

        public double RollRad
        { get; set; } = 0.0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double RadToDeg(double aRad)
        {
            return (aRad / Math.PI) * 180.0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double DegToRad(double aDeg)
        {
            return (aDeg / 180.0) * Math.PI;
        }

        public bool Equals(IROEulerAngle aOther)
        {
            return YawRad.AlmostEqual(aOther.YawRad) && PitchRad.AlmostEqual(aOther.PitchRad) && RollRad.AlmostEqual(aOther.RollRad);
        }

        public bool Equals(IEulerAngle aOther)
        {
            return Equals((IROEulerAngle)aOther);
        }

        public bool Equals(TEulerAngle aOther)
        {
            return ((object)this).Equals(aOther) || Equals((IEulerAngle)aOther);
        }

        public override int GetHashCode()
        {
            var hashCode = 839115868;
            hashCode = hashCode * -1521134295 + YawRad.GetHashCode();
            hashCode = hashCode * -1521134295 + PitchRad.GetHashCode();
            hashCode = hashCode * -1521134295 + RollRad.GetHashCode();
            return hashCode;
        }
    }
}
