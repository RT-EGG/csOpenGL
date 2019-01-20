using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    public interface IROSquareMatrix : IROMatrix
    {
        int Dimension { get; }
    }

    public partial class TSquareMatrix : TMatrix, IROSquareMatrix
    {
        public TSquareMatrix(int aDimension)
            : base(aDimension, aDimension)
        {
            return;
        }

        public TSquareMatrix(IROSquareMatrix aSrc)
            : base(aSrc)
        {
            return;
        }

        public void MakeIdentity()
        {
            for (int r = 0; r < RowCount; ++r) {
                for (int c = 0; c < ColCount; ++c) {
                    this[r, c] = (r == c) ? 1.0 : 0.0;
                }
            }
        }

        public int Dimension
        { get { return RowCount; } }

        public void Tranpose()
        {
            double[] tmpArray = new double[Dimension * Dimension];
            Array.Copy(DoubleArray, tmpArray, tmpArray.Length);

            for (int r = 0; r < RowCount; ++r) {
                for (int c = 0; c < ColCount; ++c) {
                    this[r, c] = tmpArray[RowColToIndex(c, r, ColCount, RowCount)];
                }
            }
            return;
        }
    }
}
