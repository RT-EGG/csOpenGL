// System
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace rtUtility.rtMath
{
    public interface IROMatrix : IEquatable<IROMatrix>
    {
        double this[int aIndex] { get; }
        double this[int aRow, int aCol] { get; }
        double[] DoubleArray { get; }
        float[] FloatArray { get; }
        int RowCount { get; }
        int ColCount { get; }
    }

    public interface IMatrix : IROMatrix, IEquatable<IMatrix>
    {
        new double this[int aIndex] { get; set; }
        new double this[int aRow, int aCol] { get; set; }
    }

    public class TMatrix : IMatrix, IEquatable<TMatrix>
    {
        public TMatrix(int aRowCount, int aColCount)
        {
            p_Elements = new double[aRowCount * aColCount];
            RowCount = aRowCount;
            ColCount = aColCount;
            return;
        }

        public TMatrix(IROMatrix aSrc)
        {
            p_Elements = new double[aSrc.RowCount * aSrc.ColCount];
            Array.Copy(aSrc.DoubleArray, p_Elements, p_Elements.Length);

            RowCount = aSrc.RowCount;
            ColCount = aSrc.ColCount;
            return;
        }

        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public double this[int aIndex]
        {
            set { p_Elements[aIndex] = value; }
            get { return p_Elements[aIndex]; }
        }
        public double this[int aRow, int aCol]
        {
            set { this[RowColToIndex(aRow, aCol, RowCount, ColCount)] = value; }
            get { return this[RowColToIndex(aRow, aCol, RowCount, ColCount)]; }
        }

        public double[] DoubleArray
        {
            get
            {
                double[] result = new double[p_Elements.Length];
                Array.Copy(p_Elements, result, result.Length);
                return result;
            }
        }
        public float[] FloatArray
        {
            get
            {
                float[] result = new float[p_Elements.Length];
                for (int i = 0; i < result.Length; ++i)
                    result[i] = (float)this[i];
                return result;
            }
        }

        public void MakeZero()
        {
            Array.Clear(p_Elements, 0, p_Elements.Length);
            return;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static int RowColToIndex(int aRow, int aCol, int aRowCount, int aColCount)
        {
            return (aCol * aRowCount) + aRow;
        }

        public bool Equals(IROMatrix aOther)
        {
            Debug.Assert((this.RowCount == aOther.RowCount) && (this.ColCount == aOther.ColCount));
            for (int i = 0; i < this.p_Elements.Length; ++i) {
                if (!this[i].AlmostEqual(aOther[i]))
                    return false;
            }
            return true;
        }

        public bool Equals(IMatrix aOther)
        {
            return Equals((IROMatrix)aOther);
        }

        public bool Equals(TMatrix aOther)
        {
            return ((object)this).Equals(aOther) || Equals((IMatrix)aOther);
        }

        public override int GetHashCode()
        {
            var hashCode = -137422731;
            hashCode = hashCode * -1521134295 + EqualityComparer<double[]>.Default.GetHashCode(p_Elements);
            hashCode = hashCode * -1521134295 + RowCount.GetHashCode();
            hashCode = hashCode * -1521134295 + ColCount.GetHashCode();
            return hashCode;
        }

        private double[] p_Elements;
    }
}
