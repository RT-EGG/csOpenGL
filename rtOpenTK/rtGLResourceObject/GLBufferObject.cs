
namespace rtOpenTK.rtGLResourceObject
{
    using OpenGL = OpenTK.Graphics.OpenGL;
    using OpenGL4 = OpenTK.Graphics.OpenGL4;

    public class TGLBufferObject : TGLResourceObject
    {
        public TGLBufferObject()
        {
            return;
        }

        public int ID
        { get; private set; } = 0;

        protected override void DoCreateGLResource(TrtGLControl aGL)
        {
            base.DoCreateGLResource(aGL);

            if (ID == 0)
                ID = OpenGL4.GL.GenBuffer();
            return;
        }

        protected override void DoDisposeGLResource(TrtGLControl aGL)
        {
            base.DoDisposeGLResource(aGL);

            if (ID != 0)
                OpenGL4.GL.DeleteBuffer(ID);
            return;
        }

        public override bool IsResourceReady
        { get { return ID != 0; } }
    }
}
