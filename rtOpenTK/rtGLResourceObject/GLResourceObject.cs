
using rtUtility;

namespace rtOpenTK.rtGLResourceObject
{
    public interface IGLResourceObject
    {
        bool IsResourceReady { get; }
    }

    public abstract class TGLResourceObject : TDisposableObject, IGLResourceObject
    {
        public TGLResourceObject()
        {
            if (TrtGLControl.CurrentControl != null)
                CreateGLResource(TrtGLControl.CurrentControl);
            else
                TrtGLControl.ResourceManager.EnqueueCreateResourceObject(this);
            return;
        }

        internal void CreateGLResource(TrtGLControl aGL)
        {
            DoCreateGLResource(aGL);
            return;
        }

        internal void DisposeGLResource(TrtGLControl aGL)
        {
            DoDisposeGLResource(aGL);
            return;
        }

        public abstract bool IsResourceReady
        { get; }

        protected override void Dispose(bool aDisposing)
        {
            //if (TrtGLControl.CurrentControl != null)
            //    DisposeGLResource(TrtGLControl.CurrentControl);
            //else
            //    TrtGLControl.ResourceManager.EnqueueDisposeResourceObject(this);
            base.Dispose(aDisposing);
            return;
        }

        protected virtual void DoCreateGLResource(TrtGLControl aGL)
        {
            return;
        }

        protected virtual void DoDisposeGLResource(TrtGLControl aGL)
        {
            return;
        }
    }
}
