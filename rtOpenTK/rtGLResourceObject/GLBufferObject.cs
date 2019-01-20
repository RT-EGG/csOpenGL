
namespace rtOpenTK.rtGLResourceObject
{
    using OpenGL = OpenTK.Graphics.OpenGL;
    using OpenGL4 = OpenTK.Graphics.OpenGL4;

    public class TGLBufferObject : TGLResourceObject
    {
        public TGLBufferObject(int aGenCount = 1)
        {
            p_BufferObjects = new uint[aGenCount];
            return;
        }

        public void Bind(TrtGLControl aGL, OpenGL.BufferTarget aTarget)
        {
            Bind(aGL, aTarget, 0);
            return;
        }

        public void Bind(TrtGLControl aGL, OpenGL.BufferTarget aTarget, int aBufferIndex)
        {
            OpenGL.GL.BindBuffer(aTarget, p_BufferObjects[aBufferIndex]);
            return;
        }

        public void Bind(TrtGLControl aGL, OpenGL4.BufferTarget aTarget)
        {
            Bind(aGL, aTarget, 0);
            return;
        }

        public void Bind(TrtGLControl aGL, OpenGL4.BufferTarget aTarget, int aBufferIndex)
        {
            OpenGL4.GL.BindBuffer(aTarget, p_BufferObjects[aBufferIndex]);
            return;
        }

        protected override void DoCreateGLResource(TrtGLControl aGL)
        {
            base.DoCreateGLResource(aGL);

            OpenGL4.GL.GenBuffers(p_BufferObjects.Length, p_BufferObjects);
            return;
        }

        protected override void DoDisposeGLResource(TrtGLControl aGL)
        {
            base.DoDisposeGLResource(aGL);

            OpenGL4.GL.DeleteBuffers(p_BufferObjects.Length, p_BufferObjects);
            return;
        }

        public override bool IsResourceReady
        { get { return p_BufferObjects[0] != 0; } }

        private uint[] p_BufferObjects = null;
    }
}
