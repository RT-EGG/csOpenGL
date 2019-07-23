// System
using System;

namespace rtUtility.rtMath
{
    public interface IROMatrix44 : IROSquareMatrix
    {
        IROVector3H GetRow(int aRow);
        IROVector3H GetColumn(int aCol);
        IROMatrix33 Extract33(int aRowIndex, int aColIndex);
    }

    public partial class TMatrix44 : TSquareMatrix, IROMatrix44
    {
        public TMatrix44()
            : base(4)
        {
            return;
        }

        public TMatrix44(IROMatrix44 aSrc)
            : base(aSrc)
        {
            return;
        }

        public void SetRow(int aRow, IROVector3 aValue012, double aValue3)
        {
            this[aRow, 0] = aValue012.X;
            this[aRow, 1] = aValue012.Y;
            this[aRow, 2] = aValue012.Z;
            this[aRow, 3] = aValue3;
            return;
        }

        public void SetRow(int aRow, IROVector3H aValue)
        {
            this[aRow, 0] = aValue.X;
            this[aRow, 1] = aValue.Y;
            this[aRow, 2] = aValue.Z;
            this[aRow, 3] = aValue.W;
            return;
        }

        public IROMatrix33 Extract33(int aRowIndex, int aColIndex)
        {
            if (!(aRowIndex.InRange(0, 1) && aColIndex.InRange(0, 1)))
                throw new ArgumentOutOfRangeException("aRowIndex and aColIndex must be range in 0..1.");

            TMatrix33 result = new TMatrix33();
            for (int r = 0; r < 3; ++r) {
                for (int c = 0; c < 3; ++c) {
                    result[r, c] = this[r + aRowIndex, c + aColIndex];
                }
            }

            return result;
        }

        public IROVector3H GetRow(int aRow)
        {
            return new TVector3H(this[aRow, 0], this[aRow, 1], this[aRow, 2], this[aRow, 3]);
        }

        public void SetColumn(int aCol, IROVector3 aValue012, double aValue3)
        {
            this[0, aCol] = aValue012.X;
            this[1, aCol] = aValue012.Y;
            this[2, aCol] = aValue012.Z;
            this[3, aCol] = aValue3;
            return;
        }

        public void SetColumn(int aCol, IROVector3H aValue)
        {
            this[0, aCol] = aValue.X;
            this[1, aCol] = aValue.Y;
            this[2, aCol] = aValue.Z;
            this[3, aCol] = aValue.W;
            return;
        }

        public IROVector3H GetColumn(int aCol)
        {
            return new TVector3H(this[0, aCol], this[1, aCol], this[2, aCol], this[3, aCol]);
        }

        public static TMatrix44 IdentityMatrix
        {
            get
            {
                TMatrix44 result = new TMatrix44();
                result.MakeIdentity();
                return result;
            }
        }
    }

    // ModelviewMatrix part
    public partial class TMatrix44
    {
        public static TMatrix44 MakeTranslateMatrix(double aX, double aY, double aZ)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeTranslate(aX, aY, aZ);
            return result;
        }

        public static TMatrix44 MakeTranslateMatrix(IROVector3 aValue)
        {
            return MakeTranslateMatrix(aValue.X, aValue.Y, aValue.Z);
        }

        public static TMatrix44 MakeRotateMatrixYaw(double aRadian)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeRotateYaw(aRadian);
            return result;
        }

        public static TMatrix44 MakeRotateMatrixPitch(double aRadian)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeRotatePitch(aRadian);
            return result;
        }

        public static TMatrix44 MakeRotateMatrixRoll(double aRadian)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeRotateRoll(aRadian);
            return result;
        }

        public static TMatrix44 MakeRotateMatrix(double aThete, double aAxisX, double aAxisY, double aAxisZ)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeRotate(aThete, aAxisX, aAxisY, aAxisZ);
            return result;
        }

        public static TMatrix44 MakeRotateMatrix(double aThete, IROVector3 aAxis)
        {
            return MakeRotateMatrix(aThete, aAxis.X, aAxis.Y, aAxis.Z);
        }

        public static TMatrix44 MakeRotateMatrix(IROQuaternion aQuaternion)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeRotate(aQuaternion);
            return result;
        }

        public static TMatrix44 MakeScaleMatrix(double aX, double aY, double aZ)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeScale(aX, aY, aZ);
            return result;
        }

        public static TMatrix44 MakeScaleMatrix(IROVector3 aValue)
        {
            return MakeScaleMatrix(aValue.X, aValue.Y, aValue.Z);
        }

        public static TMatrix44 MakeLookAtMatrix(double aCenterX, double aCenterY, double aCenterZ, double aTargetX, double aTargetY, double aTargetZ, double aUpX, double aUpY, double aUpZ)
        {
            return MakeLookAtMatrix(new TVector3(aCenterX, aCenterY, aCenterZ), new TVector3(aTargetX, aTargetY, aTargetZ), new TVector3(aUpX, aUpY, aUpZ));
        }

        public static TMatrix44 MakeLookAtMatrix(IROVector3 aCenter, IROVector3 aTarget, IROVector3 aUp)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeLookAt(aCenter, aTarget, aUp);
            return result;
        }

        public void MakeTranslate(double aX, double aY, double aZ)
        {
            MakeIdentity();
            this[0, 3] = aX;
            this[1, 3] = aY;
            this[2, 3] = aZ;
            return;
        }

        public void MakeTranslate(IROVector3 aValue)
        {
            MakeTranslate(aValue.X, aValue.Y, aValue.Z);
            return;
        }

        public void MakeRotateYaw(double aRadian)
        {
            MakeIdentity();
            double s = Math.Sin(aRadian);
            double c = Math.Cos(aRadian);
            this[0, 0] = c;
            this[2, 2] = c;
            this[0, 2] = s;
            this[2, 0] = -s;
            return;
        }

        public void MakeRotatePitch(double aRadian)
        {
            MakeIdentity();
            double s = Math.Sin(aRadian);
            double c = Math.Cos(aRadian);
            this[1, 1] = c;
            this[2, 2] = c;
            this[1, 2] = -s;
            this[2, 1] = s;
            return;
        }

        public void MakeRotateRoll(double aRadian)
        {
            MakeIdentity();
            double s = Math.Sin(aRadian);
            double c = Math.Cos(aRadian);
            this[0, 0] = c;
            this[1, 1] = c;
            this[0, 1] = -s;
            this[1, 0] = s;
            return;
        }

        public void MakeRotate(double aThete, double aAxisX, double aAxisY, double aAxisZ)
        {
            MakeRotate(aThete, new TVector3(aAxisX, aAxisY, aAxisZ));
            return;
        }

        public void MakeRotate(double aThete, IROVector3 aAxis)
        {
            TVector3 axis = new TVector3(aAxis);

            MakeIdentity();
            if (!axis.Normalize())
                return;

            double sThete = Math.Sin(aThete);
            double cThete = Math.Cos(aThete);

            this[0, 0] = (axis.X * axis.X * (1.0 - cThete)) + cThete;
            this[0, 1] = (axis.X * axis.Y * (1.0 - cThete)) - (axis.Z * sThete);
            this[0, 2] = (axis.X * axis.Z * (1.0 - cThete)) + (axis.Y * sThete);
            this[1, 0] = (axis.Y * axis.X * (1.0 - cThete)) + (axis.Z * sThete);
            this[1, 1] = (axis.Y * axis.Y * (1.0 - cThete)) + cThete;
            this[1, 2] = (axis.Y * axis.Z * (1.0 - cThete)) - (axis.X * sThete);
            this[2, 0] = (axis.Z * axis.X * (1.0 - cThete)) - (axis.Y * sThete);
            this[2, 1] = (axis.Z * axis.Y * (1.0 - cThete)) + (axis.X * sThete);
            this[2, 2] = (axis.Z * axis.Z * (1.0 - cThete)) + cThete;

            return;
        }

        public void MakeRotate(IROQuaternion aQuaternion)
        {
            this[0, 0] = 1.0 - (2.0 * Math.Pow(aQuaternion.Imaginary.Y, 2.0)) - (2.0 * Math.Pow(aQuaternion.Imaginary.Z, 2.0));
            this[0, 1] = (2.0 * aQuaternion.Imaginary.X * aQuaternion.Imaginary.Y) + (2.0 * aQuaternion.Imaginary.Z * aQuaternion.Real);
            this[0, 2] = (2.0 * aQuaternion.Imaginary.X * aQuaternion.Imaginary.Z) - (2.0 * aQuaternion.Imaginary.Y * aQuaternion.Real);
            this[0, 3] = 0.0;
            this[1, 0] = (2.0 * aQuaternion.Imaginary.X * aQuaternion.Imaginary.Y) + (2.0 * aQuaternion.Imaginary.Z * aQuaternion.Real);
            this[1, 1] = 1.0 - (2.0 * Math.Pow(aQuaternion.Imaginary.X, 2.0)) - (2.0 * Math.Pow(aQuaternion.Imaginary.Z, 2.0));
            this[1, 2] = (2.0 * aQuaternion.Imaginary.Y * aQuaternion.Imaginary.Z) + (2.0 * aQuaternion.Imaginary.X * aQuaternion.Real);
            this[1, 3] = 0.0;
            this[2, 0] = (2.0 * aQuaternion.Imaginary.X * aQuaternion.Imaginary.Z) + (2.0 * aQuaternion.Imaginary.Y * aQuaternion.Real);
            this[2, 1] = (2.0 * aQuaternion.Imaginary.Y * aQuaternion.Imaginary.Z) + (2.0 * aQuaternion.Imaginary.X * aQuaternion.Real);
            this[2, 2] = 1.0 - (2.0 * Math.Pow(aQuaternion.Imaginary.X, 2.0)) - (2.0 * Math.Pow(aQuaternion.Imaginary.Y, 2.0));
            this[2, 3] = 0.0;

            return;
        }

        public void MakeScale(double aX, double aY, double aZ)
        {
            MakeIdentity();
            this[0, 0] = aX;
            this[1, 1] = aY;
            this[2, 2] = aZ;
            return;
        }

        public void MakeScale(IROVector3 aValue)
        {
            MakeScale(aValue.X, aValue.Y, aValue.Z);
            return;
        }

        private void MakeLookAt(double aCenterX, double aCenterY, double aCenterZ, double aTargetX, double aTargetY, double aTargetZ, double aUpX, double aUpY, double aUpZ)
        {
            MakeLookAt(new TVector3H(aCenterX, aCenterY, aCenterZ), new TVector3H(aTargetX, aTargetY, aTargetZ), new TVector3H(aUpX, aUpY, aUpZ));
            return;
        }

        private void MakeLookAt(IROVector3 aCenter, IROVector3 aTarget, IROVector3 aUp)
        {
            MakeLookAt(new TVector3(aCenter), new TVector3(aTarget), new TVector3(aUp));
            return;
        }

        private void MakeLookAt(TVector3 aCenter, TVector3 aTarget, TVector3 aUp)
        {
            MakeIdentity();
            if ((aCenter == aTarget) || aUp.IsZero)
                return;

            TVector3H z = new TVector3H((aCenter - aTarget).Normalized, 0.0);
            TVector3H x = new TVector3H(TVector3.CrossProduct(aUp, z).Normalized, 0.0);
            TVector3H y = new TVector3H(TVector3.CrossProduct(z, x), 0.0);

            double d = (aTarget - aCenter).Length;

            TVector3H t = new TVector3H();
            t.X = -TVector3.DotProduct(x, aCenter);
            t.Y = -TVector3.DotProduct(y, aCenter);
            t.Z = -TVector3.DotProduct(z, aCenter);
            t.W = 1.0;

            this.SetRow(0, x);
            this.SetRow(1, y);
            this.SetRow(2, z);
            this.SetColumn(3, t);
            //result[3, 2] = -d;

            return;
        }

        public TEulerAngle EstimateAngle()
        {
            TVector3H front = this * (new TVector3H(0.0, 0.0, 1.0, 0.0));
            TVector3H up = this * (new TVector3H(0.0, 1.0, 0.0, 0.0));

            return new TEulerAngle(TEulerAngle.EstimateFrom(front, up));
        }
    }

    // ProjectionMatrix part
    public partial class TMatrix44
    {
        public static TMatrix44 MakeOrthoMatrix(double aLeft, double aRight, double aBottom, double aTop, double aNear, double aFar)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeOrtho(aLeft, aRight, aBottom, aTop, aNear, aFar);
            return result;
        }

        public static TMatrix44 MakeOrthoMatrix(double aWidth, double aHeight, double aNear, double aFar)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeOrtho(aWidth, aHeight, aNear, aFar);
            return result;
        }

        public static TMatrix44 MakeOrthoMatrix(double aFovY, double aWidth, double aHeight, double aNear, double aFar)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeOrtho(aFovY, aWidth, aHeight, aNear, aFar);
            return result;
        }

        public static TMatrix44 MakeFrustumMatrix(double aLeft, double aRight, double aBottom, double aTop, double aNear, double aFar)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeFrustum(aLeft, aRight, aBottom, aTop, aNear, aFar);
            return result;
        }

        public static TMatrix44 MakeFrustumMatrix(double aWidth, double aHeight, double aNear, double aFar)
        {
            TMatrix44 result = new TMatrix44();
            result.MakeFrustum(aWidth, aHeight, aNear, aFar);
            return result;
        }

        public static TMatrix44 MakePerspectiveMatrix(double aFovY, double aAspect, double aNear, double aFar)
        {
            TMatrix44 result = new TMatrix44();
            result.MakePerspective(aFovY, aAspect, aNear, aFovY);
            return result;
        }

        public static TMatrix44 MakePerspectiveMatrix(double aFovY, double aWidth, double aHeight, double aNear, double aFar)
        {
            TMatrix44 result = new TMatrix44();
            result.MakePerspective(aFovY, aWidth, aHeight, aNear, aFar);
            return result;
        }

        public void MakeOrtho(double aLeft, double aRight, double aBottom, double aTop, double aNear, double aFar)
        {
            if ((aLeft - aRight).IsZero() || (aBottom - aTop).IsZero() || (aNear - aFar).IsZero())
                throw new DivideByZeroException("The \"aLeft\" and the \"aRight\", the \"aBottom\" and the \"aTop\", the \"aNear\" and the \"aFar\" must be difference value.");

            MakeIdentity();

            this[0, 0] = 2.0 / (aRight - aLeft);
            this[0, 3] = -(aRight + aLeft) / (aRight - aLeft);
            this[1, 1] = 2.0 / (aTop - aBottom);
            this[1, 3] = -(aTop + aBottom) / (aTop - aBottom);
            this[2, 2] = -2.0 / (aFar - aNear);
            this[2, 3] = -(aFar + aNear) / (aFar - aNear);

            return;
        }

        public void MakeOrtho(double aWidth, double aHeight, double aNear, double aFar)
        {
            MakeOrtho(-aWidth * 0.5, aWidth * 0.5, -aHeight * 0.5, aHeight * 0.5, aNear, aFar);
            return;
        }

        public void MakeOrtho(double aFovY, double aWidth, double aHeight, double aNear, double aFar)
        {
            MakeOrtho(aFovY * (aWidth / aHeight), aFovY, aNear, aFar);
            return;
        }

        public void MakeFrustum(double aLeft, double aRight, double aBottom, double aTop, double aNear, double aFar)
        {
            if ((aLeft - aRight).IsZero() || (aBottom - aTop).IsZero() || (aNear - aFar).IsZero())
                throw new DivideByZeroException("The \"aLeft\" and the \"aRight\", the \"aBottom\" and the \"aTop\", the \"aNear\" and the \"aFar\" must be difference value.");

            MakeZero();

            this[0, 0] = (2.0 * aNear) / (aRight - aLeft);
            this[0, 2] = (aRight + aLeft) / (aRight - aLeft);
            this[1, 1] = (2.0 * aNear) / (aTop - aBottom);
            this[1, 2] = (aTop + aBottom) / (aTop - aBottom);
            this[2, 2] = -(aFar + aNear) / (aFar - aNear);
            this[2, 3] = -(2.0 * aFar * aNear) / (aFar - aNear);
            this[3, 2] = -1.0;

            return;
        }

        public void MakeFrustum(double aWidth, double aHeight, double aNear, double aFar)
        {
            MakeFrustum(-aWidth * 0.5, aWidth * 0.5, -aHeight * 0.5, aHeight * 0.5, aNear, aFar);
            return;
        }

        public void MakePerspective(double aFovY, double aAspect, double aNear, double aFar)
        {
            double ty = Math.Tan((aFovY * 0.5) * (Math.PI / 180.0));
            double tx = ty * aAspect;

            MakeFrustum(-tx, tx, -ty, ty, aNear, aFar);
            return;
        }

        public void MakePerspective(double aFovY, double aWidth, double aHeight, double aNear, double aFar)
        {
            MakePerspective(aFovY, aWidth / aHeight, aNear, aFar);
            return;
        }
    }

    public partial class TMatrix44
    {
        // about inverse
        // refered to following URL
        // http://miffysora.wikidot.com/invertmatrix-4x4
        // http://www.iwata-system-support.com/CAE_HomePage/vector/vector18/vector18.html
        // http://physmath.main.jp/src/inverse-cofactor-ex4.html

        public bool Inverse()
        {
            double det = Deternimant;
            if (det.IsZero())
                return false;

            TMatrix44 tmp = new TMatrix44(this);
            for (int row = 0; row < 4; ++row) {
                for (int col = 0; col < 4; ++col) {
                    if (((row + col) % 2) == 0)
                        this[row, col] = tmp.GetSubDeterminant(col, row) / det;
                    else
                        this[row, col] = -tmp.GetSubDeterminant(col, row) / det;
                }
            }

            return true;
        }

        public double Deternimant
        {
            get
            {
                return (this[0, 0] * GetSubDeterminant(0, 0))
                     - (this[1, 0] * GetSubDeterminant(1, 0))
                     + (this[2, 0] * GetSubDeterminant(2, 0))
                     - (this[3, 0] * GetSubDeterminant(3, 0));
            }
        }

        private double GetSubDeterminant(int aRow, int aCol)
        {
            double[] subMatrix = new double[3 * 3];
            Func<int, int, double> GetSub = (r, c) => { return subMatrix[RowColToIndex(r, c, RowCount - 1, ColCount - 1)]; };
            Action<int, int, double> SetSub = (r, c, value) => { subMatrix[RowColToIndex(r, c, RowCount - 1, ColCount - 1)] = value; };

            int srcRow, srcCol;

            srcRow = 0;
            for (int dstRow = 0; dstRow < 3; ++dstRow) {
                if (dstRow == aRow)
                    ++srcRow;

                srcCol = 0;
                for (int dstCol = 0; dstCol < 3; ++dstCol) {
                    if (dstCol == aCol)
                        ++srcCol;

                    SetSub(dstRow, dstCol, this[srcRow, srcCol]);
                    ++srcCol;
                }
                ++srcRow;
            }

            double result =
                (GetSub(0, 0) * GetSub(1, 1) * GetSub(2, 2))
              + (GetSub(0, 1) * GetSub(1, 2) * GetSub(2, 0))
              + (GetSub(0, 2) * GetSub(1, 0) * GetSub(2, 1))
              - (GetSub(0, 2) * GetSub(1, 1) * GetSub(2, 0))
              - (GetSub(0, 0) * GetSub(1, 2) * GetSub(2, 1))
              - (GetSub(0, 1) * GetSub(1, 0) * GetSub(2, 2));

            return result;
        }
    }

    public partial class TMatrix44
    {
        public static TMatrix44 operator -(TMatrix44 aValue)
        {
            return aValue * -1.0;
        }

        public static TMatrix44 operator +(TMatrix44 aLeft, TMatrix44 aRight)
        {
            TMatrix44 result = new TMatrix44();
            for (int i = 0; i < 16; ++i)
                result[i] = aLeft[i] + aRight[i];
            return result;
        }

        public static TMatrix44 operator -(TMatrix44 aLeft, TMatrix44 aRight)
        {
            return aLeft + (-aRight);
        }

        public static TMatrix44 operator *(TMatrix44 aLeft, double aRight)
        {
            TMatrix44 result = new TMatrix44();
            for (int i = 0; i < 16; ++i)
                result[i] = aLeft[i] * aRight;
            return result;
        }

        public static TMatrix44 operator *(double aLeft, TMatrix44 aRight)
        {
            return aRight * aLeft;
        }

        public static TMatrix44 operator /(TMatrix44 aLeft, double aRight)
        {
            return aLeft * (1.0 / aRight);
        }

        public static TMatrix44 operator *(TMatrix44 aLeft, TMatrix44 aRight)
        {
            return Multiply(aLeft, aRight);
        }

        public static TVector3H operator *(TMatrix44 aLeft, TVector3H aRight)
        {
            return Multiply(aLeft, aRight);
        }

        public static TMatrix44 Multiply(IROMatrix44 aLeft, IROMatrix44 aRight)
        {
            TMatrix44 result = new TMatrix44();
            for (int r = 0; r < 4; ++r) {
                for (int c = 0; c < 4; ++c) {
                    double v = 0.0;
                    for (int i = 0; i < 4; ++i)
                        v += aRight[r, i] * aLeft[i, c];
                    result[r, c] = v;
                }
            }
            return result;
        }

        public static TVector3H Multiply(IROMatrix44 aLeft, IROVector3H aRight)
        {
            TVector3H result = new TVector3H();

            Func<int, double> GetVecElem = (i) => {
                switch (i) {
                    case 0: return aRight.X;
                    case 1: return aRight.Y;
                    case 2: return aRight.Z;
                    case 3: return aRight.W;
                }
                return 0.0;
            };
            Action<int, double> SetVecElem = (i, value) => {
                switch (i) {
                    case 0: result.X = value; break;
                    case 1: result.Y = value; break;
                    case 2: result.Z = value; break;
                    case 3: result.W = value; break;
                }
                return;
            };

            for (int i = 0; i < 4; ++i) {
                double v = 0.0;
                for (int k = 0; k < 4; ++k)
                    v += aLeft[i, k] * GetVecElem(k);
                SetVecElem(i, v);
            }
            return result;
        }
    }
}
