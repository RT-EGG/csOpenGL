// System
using System.Windows.Forms;
// rtUtility
using rtUtility.rtMath;

namespace rtOpenTK.rtGLUtility.rtGLCameraController
{
    public abstract class TGLCameraController
    {
        public virtual void RegisterToControl(Control aControl)
        { 
            
        }

        public virtual void UnregisterFromControl(Control aControl)
        {

        }

        public virtual IROVector3 UpVector
        { get { return new TVector3(0.0, 1.0, 0.0); } }

        public IROMatrix44 ViewMatrix
        {
            get
            {
                return TMatrix44.MakeLookAtMatrix(EyePosition, TVector3.Add(EyePosition, FrontVector), UpVector);
            }
        }

        public abstract IROVector3 EyePosition { get; }
        public abstract IROVector3 FrontVector { get; }
    }
}
