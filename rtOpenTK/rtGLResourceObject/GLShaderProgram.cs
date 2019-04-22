// System
using System.Collections.Generic;
// OpenTK
using OpenTK.Graphics.OpenGL4;

namespace rtOpenTK.rtGLResourceObject
{
    public class TGLShaderProgram : TGLResourceObject
    {
        public void AttachShader(TrtGLControl aGL, TGLShader aShader)
        {
            GL.AttachShader(ID, aShader.ID);
            return;
        }

        public void DetachShader(TrtGLControl aGL, TGLShader aShader)
        {
            GL.DetachShader(ID, aShader.ID);
            return;
        }

        public bool Link(TrtGLControl aGL)
        {
            GL.LinkProgram(ID);
            GL.GetProgram(ID, GetProgramParameterName.LinkStatus, out p_LinkState);

            p_LinkError.Clear();
            if (p_LinkState == 0) {
                string error = GL.GetProgramInfoLog(ID);
                p_LinkError.AddRange(error.Split('\n'));
            }
            return Linked;
        }

        public override bool IsResourceReady
        { get { return ID != 0; } }

        public int ID
        { get; private set; } = 0;

        public bool Linked
        { get { return p_LinkState != 0; } }

        protected override void DoCreateGLResource(TrtGLControl aGL)
        {
            base.DoCreateGLResource(aGL);
            ID = GL.CreateProgram();
            return;
        }

        protected override void DoDisposeGLResource(TrtGLControl aGL)
        {
            base.DoDisposeGLResource(aGL);
            GL.DeleteProgram(ID);
            return;
        }

        private int p_LinkState = 0;
        private List<string> p_LinkError = new List<string>();
    }
}
