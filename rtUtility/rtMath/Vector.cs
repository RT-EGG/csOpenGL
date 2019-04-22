// System
using System;
using System.Diagnostics;

namespace rtUtility.rtMath
{
    public interface IROVector : IEquatable<IROVector>
    {
        double this[int aIndex] { get; }
        int Dimension { get; }
        double Length { get; }
        double Length2 { get; }
        bool IsZero { get; }
    }

    public interface IVector : IROVector, IEquatable<IVector>
    {
        void Assign(IROVector aSrc);
        new double this[int aIndex] { get; set; }
    }

    public class TVector : IVector, IEquatable<TVector>
    {
        public TVector(int aDimension)
        {
            p_Elements = new double[aDimension];
            return;
        }

        public void Assign(IROVector aSrc)
        {
            Debug.Assert(Dimension == aSrc.Dimension);
            for (int i = 0; i < Dimension; ++i)
                this[i] = aSrc[i];
            return;
        }

        public double this[int aIndex]
        {
            get { return p_Elements[aIndex]; }
            set { p_Elements[aIndex] = value; }
        }
        public int Dimension
        { get { return p_Elements.Length; } }

        public double Length
        {
            get { return Math.Sqrt(Length2); }
        }

        public double Length2
        {
            get
            {
                double result = 0.0;
                foreach (double v in p_Elements)
                    result += Math.Pow(v, 2.0);
                return result;
            }
        }

        public bool IsZero
        {
            get { return Length2.IsZero(); }
        }

        public bool Normalize()
        {
            double len = Length2;
            if (len.IsZero())
                return false;

            len = Math.Sqrt(len);
            for (int i = 0; i < Dimension; ++i)
                this[i] = this[i] / len;
            return true;
        }

        public static double DotProduct(IROVector aLeft, IROVector aRight)
        {
            Debug.Assert(aLeft.Dimension == aRight.Dimension);
            double result = 0.0;
            for (int i = 0; i < aLeft.Dimension; ++i) {
                result += aLeft[i] * aRight[i];
            }

            return result;
        }

        public bool Equals(IROVector aOther)
        {
            Debug.Assert(this.Dimension == aOther.Dimension);
            for (int i = 0; i < this.Dimension; ++i) {
                if (!this[i].AlmostEqual(aOther[i]))
                    return false;
            }
            return true;
        }

        public bool Equals(IVector aOther)
        {
            return Equals((IROVector)aOther);
        }

        public bool Equals(TVector aOther)
        {
            return ((object)this).Equals(aOther) || Equals((IVector)aOther);
        }

        public override int GetHashCode()
        {
            var hashCode = 583173876;
            hashCode = hashCode * -1521134295 + p_Elements.GetHashCode();
            hashCode = hashCode * -1521134295 + Dimension.GetHashCode();
            return hashCode;
        }

        private double[] p_Elements = null;
    }
}
