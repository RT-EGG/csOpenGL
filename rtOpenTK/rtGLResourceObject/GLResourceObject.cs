﻿
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
            TrtGLControl.ResourceManager.EnqueueCreateResourceObject(this);
            return;
        }

        public void CreateGLResource(TrtGLControl aGL)
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
            base.Dispose(aDisposing);
            if (aDisposing) {
                TrtGLControl.ResourceManager.EnqueueDisposeResourceObject(this);
            }

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
