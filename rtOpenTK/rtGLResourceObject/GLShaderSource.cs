// rtUtility
using rtUtility;

namespace rtOpenTK.rtGLResourceObject
{
    public abstract class TGLShaderSource : TDisposableObject
    {
        public void Load(TrtGLControl aGL, int aShaderID)
        {
            DoLoad(aGL, aShaderID);
            return;
        }

        protected abstract void DoLoad(TrtGLControl aGL, int aShaderID);
    }
}
