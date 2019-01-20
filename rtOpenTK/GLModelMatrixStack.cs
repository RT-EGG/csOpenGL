// OpenTK
using OpenTK.Graphics.OpenGL;
// rtUtility
using rtUtility.rtMath;

namespace rtOpenTK
{
    public class TGLModelMatrixStack : TGLMatrixStack
    {
        public void Translate(double aX, double aY, double aZ)
        {
            MultiMatrix(TMatrix44.MakeTranslateMatrix(aX, aY, aZ));
            return;
        }

        public void Translate(IROVector3 aValue)
        {
            Translate(aValue.X, aValue.Y, aValue.Z);
            return;
        }

        public void RotateYaw(double aRadian)
        {
            MultiMatrix(TMatrix44.MakeRotateMatrixYaw(aRadian));
            return;
        }

        public void RotatePitch(double aRadian)
        {
            MultiMatrix(TMatrix44.MakeRotateMatrixPitch(aRadian));
            return;
        }

        public void RotateRoll(double aRadian)
        {
            MultiMatrix(TMatrix44.MakeRotateMatrixRoll(aRadian));
            return;
        }

        public void Rotate(double aRadian, double aX, double aY, double aZ)
        {
            MultiMatrix(TMatrix44.MakeRotateMatrix(aRadian, aX, aY, aZ));
            return;
        }

        public void Rotate(double aThete, IROVector3 aVector)
        {
            Rotate(aThete, aVector.X, aVector.Y, aVector.Z);
            return;
        }

        public void Scale(double aX, double aY, double aZ)
        {
            MultiMatrix(TMatrix44.MakeScaleMatrix(aX, aY, aZ));
            return;
        }

        protected override MatrixMode TargetMatrixMode
        { get { return MatrixMode.Modelview; } }
    }
}
