// rtUtility
using rtUtility.rtMath;

namespace rtOpenTK
{
    public class TGLViewMatrixStack : TGLModelMatrixStack
    {
        public void LookAt(double aCenterX, double aCenterY, double aCenterZ, double aTargetX, double aTargetY, double aTargetZ, double aUpX, double aUpY, double aUpZ)
        {
            LookAt(new TVector3(aCenterX, aCenterY, aCenterZ), new TVector3(aTargetX, aTargetY, aTargetZ), new TVector3(aUpX, aUpY, aUpZ));
            return;
        }

        public void LookAt(IROVector3 aCenter, IROVector3 aTarget, IROVector3 aUp)
        {
            MultiMatrix(TMatrix44.MakeLookAtMatrix(aCenter, aTarget, aUp));
            return;
        }
    }
}
