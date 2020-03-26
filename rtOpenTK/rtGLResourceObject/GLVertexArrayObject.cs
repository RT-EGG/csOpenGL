using OpenTK.Graphics.OpenGL4;

namespace rtOpenTK.rtGLResourceObject
{
    public class TGLVertexArrayObject : TGLResourceObject
    {
        protected override void DoCreateGLResource(TrtGLControl aGL)
        {
            base.DoCreateGLResource(aGL);

            ID = GL.GenVertexArray();
            return;
        }

        protected override void DoDisposeGLResource(TrtGLControl aGL)
        {
            base.DoDisposeGLResource(aGL);

            GL.DeleteVertexArray(ID);
            return;
        }

        public override bool IsResourceReady => ID != 0;

        public int ID
        { get; private set; } = 0;
    }
}
