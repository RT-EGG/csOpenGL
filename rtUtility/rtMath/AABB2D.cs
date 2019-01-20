using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    public interface IROAABB2D : IEquatable<IROAABB2D>
    {
        double GetBoundary(TAxis2D aAxis, TMinMax aMinMax);
        double GetWidth(TAxis2D aAxis);
        IROVector2 GetVertex(TMinMax aX, TMinMax aY);
        IROVector2 Center { get; }
        double Area { get; }
        double InnerRadius { get; }
        double OuterRadius { get; }
        bool IsValid { get; }
    }

    public interface IAABB2D : IROAABB2D, IEquatable<IAABB2D>
    {
        void SetBoundary(TAxis2D aAxis, TMinMax aMinMax, double aValue);
    }

    public class TAABB2D : IROAABB2D, IEquatable<TAABB2D>
    {
        public TAABB2D()
        {
            Initialize();
            return;
        }

        public TAABB2D(IROAABB2D aSrc)
        {
            AssignFrom(aSrc);
            return;
        }

        public static TAABB2D operator &(TAABB2D aLeft, TAABB2D aRight)
        {
            TAABB2D result = new TAABB2D();
            for (int i = 0; i < 2; ++i) {
                result.SetBoundary((TAxis2D)i, TMinMax.Min, System.Math.Max(aLeft.GetBoundary((TAxis2D)i, TMinMax.Min), aRight.GetBoundary((TAxis2D)i, TMinMax.Min)));
                result.SetBoundary((TAxis2D)i, TMinMax.Max, System.Math.Min(aLeft.GetBoundary((TAxis2D)i, TMinMax.Max), aRight.GetBoundary((TAxis2D)i, TMinMax.Max)));
            }
            if (!result.IsValid)
                result.Initialize();

            return result;
        }

        public static TAABB2D operator |(TAABB2D aLeft, TAABB2D aRight)
        {
            TAABB2D result = new TAABB2D();
            for (int i = 0; i < 2; ++i) {
                result.SetBoundary((TAxis2D)i, TMinMax.Min, System.Math.Min(aLeft.GetBoundary((TAxis2D)i, TMinMax.Min), aRight.GetBoundary((TAxis2D)i, TMinMax.Min)));
                result.SetBoundary((TAxis2D)i, TMinMax.Max, System.Math.Max(aLeft.GetBoundary((TAxis2D)i, TMinMax.Max), aRight.GetBoundary((TAxis2D)i, TMinMax.Max)));
            }
            return result;
        }

        public static TAABB2D CalcInclusionBoundary(IROVector2[] aPoints)
        {
            TAABB2D result = new TAABB2D();
            foreach (TAxis2D axis in Enum.GetValues(typeof(TAxis2D))) {
                result.SetBoundary(axis, TMinMax.Max, double.MinValue);
                result.SetBoundary(axis, TMinMax.Min, double.MaxValue);
            }
            foreach (IROVector2 point in aPoints) {
                result.Include(point);
            }

            return result;
        }

        public void Initialize()
        {
            Array.Clear(p_Boundaries, 0, p_Boundaries.Length);
            return;
        }

        public void AssignFrom(IROAABB2D aSrc)
        {
            foreach (TAxis2D axis in Enum.GetValues(typeof(TAxis2D))) {
                foreach (TMinMax minmax in Enum.GetValues(typeof(TMinMax))) {
                    SetBoundary(axis, minmax, aSrc.GetBoundary(axis, minmax));
                }
            }
            return;
        }

        public void SetBoundary(TAxis2D aAxis, TMinMax aMinMax, double aValue)
        {
            p_Boundaries[ToIndex(aAxis, aMinMax)] = aValue;
            return;
        }

        public double GetBoundary(TAxis2D aAxis, TMinMax aMinMax)
        {
            return p_Boundaries[ToIndex(aAxis, aMinMax)];
        }

        public double GetWidth(TAxis2D aAxis)
        {
            return GetBoundary(aAxis, TMinMax.Max) - GetBoundary(aAxis, TMinMax.Min);
        }

        public IROVector2 GetVertex(TMinMax aX, TMinMax aY)
        {
            TVector2 result = new TVector2();
            result.X = p_Boundaries[ToIndex(TAxis2D.X, aX)];
            result.Y = p_Boundaries[ToIndex(TAxis2D.Y, aY)];
            return result;
        }

        public IROVector2 Center
        {
            get
            {
                TVector2 result = new TVector2();
                result.X = GetBoundary(TAxis2D.X, TMinMax.Min) + (GetWidth(TAxis2D.X) * 0.5);
                result.Y = GetBoundary(TAxis2D.Y, TMinMax.Min) + (GetWidth(TAxis2D.Y) * 0.5);
                return result;
            }
        }

        public double Area
        {
            get { return GetWidth(TAxis2D.X) * GetWidth(TAxis2D.Y); }
        }

        public double InnerRadius
        {
            get { return Math.Min(GetWidth(TAxis2D.X), GetWidth(TAxis2D.Y)); }
        }

        public double OuterRadius
        {
            get
            {
                return Math.Sqrt(Math.Pow(GetWidth(TAxis2D.X), 2.0) +
                                 Math.Pow(GetWidth(TAxis2D.Y), 2.0)) * 0.5;
            }
        }

        public bool IsValid
        {
            get { return (GetWidth(TAxis2D.X) > 0.0) && (GetWidth(TAxis2D.Y) > 0.0); }
        }

        public bool Contains(double aX, double aY)
        {
            return Contains(new TVector2(aX, aY));
        }

        public bool Contains(IROVector2 aPoint)
        {
            foreach (TAxis2D axis in Enum.GetValues(typeof(TAxis2D))) {
                if ((GetBoundary(axis, TMinMax.Min) > aPoint[(int)axis]) ||
                    (GetBoundary(axis, TMinMax.Max) < aPoint[(int)axis]))
                    return false;
            }
            return true;
        }

        public void Include(double aX, double aY)
        {
            Include(new TVector2(aX, aY));
            return;
        }

        public void Include(IROVector2 aPoint)
        {
            foreach (TAxis2D axis in Enum.GetValues(typeof(TAxis2D))) {
                if (GetBoundary(axis, TMinMax.Min) > aPoint[(int)axis])
                    SetBoundary(axis, TMinMax.Min, aPoint[(int)axis]);
                if (GetBoundary(axis, TMinMax.Max) < aPoint[(int)axis])
                    SetBoundary(axis, TMinMax.Max, aPoint[(int)axis]);
            }
            return;
        }

        public bool Equals(IROAABB2D aOther)
        {
            return GetBoundary(TAxis2D.X, TMinMax.Min).AlmostEqual(aOther.GetBoundary(TAxis2D.X, TMinMax.Min))
                 & GetBoundary(TAxis2D.X, TMinMax.Max).AlmostEqual(aOther.GetBoundary(TAxis2D.X, TMinMax.Max))
                 & GetBoundary(TAxis2D.Y, TMinMax.Min).AlmostEqual(aOther.GetBoundary(TAxis2D.Y, TMinMax.Min))
                 & GetBoundary(TAxis2D.Y, TMinMax.Max).AlmostEqual(aOther.GetBoundary(TAxis2D.Y, TMinMax.Max));
        }

        public bool Equals(TAABB2D aOther)
        {
            return ((object)this).Equals(aOther) || Equals((IROAABB2D)aOther);
        }

        private int ToIndex(TAxis2D aAxis, TMinMax aMinMax)
        {
            return ((int)aAxis * 2) + (int)aMinMax;
        }

        private double[] p_Boundaries = new double[2 * 2];
    }
}
