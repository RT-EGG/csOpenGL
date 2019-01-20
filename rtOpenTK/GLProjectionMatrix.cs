// OpenTK
using OpenTK.Graphics.OpenGL;
// rtUtility
using rtUtility.rtMath;

namespace rtOpenTK
{
    public class TGLProjectionMatrixStack : TGLMatrixStack
    {
        public void Ortho(double aLeft, double aRight, double aBottom, double aTop, double aNear, double aFar)
        {
            MultiMatrix(TMatrix44.MakeOrthoMatrix(aLeft, aRight, aBottom, aTop, aNear, aFar));
            return;
        }

        public void Ortho(double aWidth, double aHeight, double aNear, double aFar)
        {
            MultiMatrix(TMatrix44.MakeOrthoMatrix(aWidth, aHeight, aNear, aFar));
            return;
        }

        public void Ortho(double aFovY, double aWidth, double aHeight, double aNear, double aFar)
        {
            MultiMatrix(TMatrix44.MakeOrthoMatrix(aFovY, aWidth, aHeight, aNear, aFar));
            return;
        }

        public void Frustum(double aLeft, double aRight, double aBottom, double aTop, double aNear, double aFar)
        {
            MultiMatrix(TMatrix44.MakeFrustumMatrix(aLeft, aRight, aBottom, aTop, aNear, aFar));
            return;
        }

        public void Frustum(double aWidth, double aHeight, double aNear, double aFar)
        {
            MultiMatrix(TMatrix44.MakeFrustumMatrix(aWidth, aHeight, aNear, aFar));
            return;
        }

        public void Perspective(double aFovY, double aAspect, double aNear, double aFar)
        {
            MultiMatrix(TMatrix44.MakePerspectiveMatrix(aFovY, aAspect, aNear, aFar));
            return;
        }

        public void Perspective(double aFovY, double aWidth, double aHeight, double aNear, double aFar)
        {
            MultiMatrix(TMatrix44.MakePerspectiveMatrix(aFovY, aWidth, aHeight, aNear, aFar));
            return;
        }

        protected override MatrixMode TargetMatrixMode
        { get { return MatrixMode.Projection; } }
    }
}
