// OpenTK
using OpenTK.Graphics.OpenGL4;

namespace rtOpenTK.rtGLResourceObject
{
    public class TGLSampler : TGLResourceObject
    {
        public TGLSampler()
        {
            return;
        }

        public int ID
        { get; private set; }

        public override bool IsResourceReady
        { get { return ID != 0; } }

        protected override void DoCreateGLResource(TrtGLControl aGL)
        {
            base.DoCreateGLResource(aGL);

            ID = GL.GenSampler();
            return;
        }

        protected override void DoDisposeGLResource(TrtGLControl aGL)
        {
            base.DoDisposeGLResource(aGL);

            GL.DeleteSampler(ID);
            return;
        }
    }
}
