using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    public interface IROMatrix33 : IROSquareMatrix
    {

    }

    public partial class TMatrix33 : TSquareMatrix, IROMatrix33
    {
        public TMatrix33()
            : base(3)
        {
            return;
        }

        public TMatrix33(IROMatrix33 aSrc)
            : base(aSrc)
        {
            return;
        }

        #region operator
        public static TMatrix33 operator -(TMatrix33 aValue)
        {
            return aValue * -1.0;
        }

        public static TMatrix33 operator +(TMatrix33 aLeft, TMatrix33 aRight)
        {
            TMatrix33 result = aLeft;
            for (int i = 0; i < aLeft.RowCount * aLeft.ColCount; ++i)
                result[i] = aLeft[i] + aRight[i];
            return result;
        }

        public static TMatrix33 operator -(TMatrix33 aLeft, TMatrix33 aRight)
        {
            return aLeft + (-aRight);
        }

        public static TMatrix33 operator *(TMatrix33 aLeft, double aRight)
        {
            TMatrix33 result = aLeft;
            for (int i = 0; i < aLeft.RowCount * aLeft.ColCount; ++i)
                result[i] = aLeft[i] * aRight;
            return result;
        }

        public static TMatrix33 operator *(double aLeft, TMatrix33 aRight)
        {
            return aRight * aLeft;
        }

        public static TMatrix33 operator /(TMatrix33 aLeft, double aRight)
        {
            return aLeft * (1.0 / aRight);
        }

        public static TMatrix33 operator *(TMatrix33 aLeft, TMatrix33 aRight)
        {
            return Multiply(aLeft, aRight);
        }

        public static TVector2H operator *(TMatrix33 aLeft, TVector2H aRight)
        {
            return Multiply(aLeft, aRight);
        }
        #endregion

        public static TMatrix33 Multiply(IROMatrix33 aLeft, IROMatrix33 aRight)
        {
            TMatrix33 result = new TMatrix33();
            for (int r = 0; r < 3; ++r) {
                for (int c = 0; c < 3; ++c) {
                    double v = 0.0;
                    for (int i = 0; i < 3; ++i)
                        v += aRight[r, i] * aLeft[i, c];
                    result[r, c] = v;
                }
            }
            return result;
        }

        public static TVector2H Multiply(IROMatrix33 aLeft, IROVector2H aRight)
        {
            TVector2H result = new TVector2H();

            Func<int, double> GetVecElem = (i) => {
                switch (i) {
                    case 0: return aRight.X;
                    case 1: return aRight.Y;
                    case 2: return aRight.H;
                }
                return 0.0;
            };
            Action<int, double> SetVecElem = (i, value) => {
                switch (i) {
                    case 0: result.X = value; break;
                    case 1: result.Y = value; break;
                    case 2: result.H = value; break;
                }
                return;
            };

            for (int i = 0; i < 3; ++i) {
                double v = 0.0;
                for (int k = 0; k < 3; ++k)
                    v += aLeft[i, k] * GetVecElem(k);
                SetVecElem(i, v);
            }
            return result;
        }
    }

    // ModelviewMatrix part
    public partial class TMatrix33
    {
        public static TMatrix33 MakeTranslateMatrix(double aX, double aY)
        {
            TMatrix33 result = new TMatrix33();
            result.MakeTranslate(aX, aY);
            return result;
        }

        public static TMatrix33 MakeTranslateMatrix(IVector2 aValue)
        {
            return MakeTranslateMatrix(aValue.X, aValue.Y);
        }

        public static TMatrix33 MakeRotateMatrix(double aRadian)
        {
            TMatrix33 result = new TMatrix33();
            result.MakeRotate(aRadian);
            return result;
        }

        public static TMatrix33 MakeScaleMatrix(double aX, double aY)
        {
            TMatrix33 result = new TMatrix33();
            result.MakeScale(aX, aY);
            return result;
        }

        public static TMatrix33 MakeScaleMatrix(IVector2 aValue)
        {
            return MakeScaleMatrix(aValue.X, aValue.Y);
        }

        public void MakeTranslate(double aX, double aY)
        {
            MakeIdentity();
            this[0, 2] = aX;
            this[1, 2] = aY;
            return;
        }

        public void MakeTranslate(IVector2 aValue)
        {
            MakeTranslate(aValue.X, aValue.Y);
            return;
        }

        public void MakeRotate(double aRadian)
        {
            MakeIdentity();
            double s = Math.Sin(aRadian);
            double c = Math.Cos(aRadian);
            this[0, 0] = c;
            this[0, 1] = -s;
            this[1, 0] = s;
            this[1, 1] = c;
            return;
        }

        public void MakeScale(double aX, double aY)
        {
            MakeIdentity();
            this[0, 0] = aX;
            this[1, 1] = aY;
            return;
        }

        public void MakeScale(IVector2 aValue)
        {
            MakeScale(aValue.X, aValue.Y);
            return;
        }
    }

    public partial class TMatrix33
    {
        public double Deternimant
        {
            get
            {
                return (this[0, 0] * this[1, 1] * this[2, 2])
                       + (this[0, 1] * this[1, 2] * this[2, 0])
                       + (this[0, 2] * this[1, 0] * this[2, 1])
                       - (this[0, 0] * this[1, 2] * this[2, 1])
                       - (this[0, 2] * this[1, 1] * this[2, 0])
                       - (this[0, 1] * this[1, 0] * this[2, 2]);
            }
        }

        public bool Inverse()
        {
            double det = Deternimant;
            if (det.IsZero())
                return false;

            TMatrix33 result = new TMatrix33(this);
            this[0, 0] = ((result[1, 1] * result[2, 2]) - (result[1, 2] * result[2, 1])) / det;
            this[0, 1] = ((result[0, 2] * result[2, 1]) - (result[0, 1] * result[2, 2])) / det;
            this[0, 2] = ((result[0, 1] * result[1, 2]) - (result[0, 2] * result[1, 1])) / det;
            this[1, 0] = ((result[1, 2] * result[2, 0]) - (result[1, 0] * result[2, 2])) / det;
            this[1, 1] = ((result[0, 0] * result[2, 2]) - (result[0, 2] * result[2, 0])) / det;
            this[1, 2] = ((result[0, 2] * result[1, 0]) - (result[0, 0] * result[1, 2])) / det;
            this[2, 0] = ((result[1, 0] * result[2, 1]) - (result[1, 1] * result[2, 0])) / det;
            this[2, 1] = ((result[0, 1] * result[2, 0]) - (result[0, 0] * result[2, 1])) / det;
            this[2, 2] = ((result[0, 0] * result[1, 1]) - (result[0, 1] * result[1, 0])) / det;
            return true;
        }
    }
}
