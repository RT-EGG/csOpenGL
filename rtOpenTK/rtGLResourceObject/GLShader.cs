using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace rtOpenTK.rtGLResourceObject
{
    public abstract class TGLShader : TGLResourceObject
    {
        public bool Compile(TrtGLControl aGL, TGLShaderSource aSource)
        {
            p_CompileError.Clear();
            if (ID== 0) {
                p_CompileError.Add("Shader has not created.");
                return false;
            }
            if (aSource == null) {
                p_CompileError.Add("Source has not ready.");
                return false;
            }

            aSource.Load(aGL, ID);
            GL.CompileShader(ID);

            GL.GetShader(ID, ShaderParameter.CompileStatus, out p_CompileState);
            if (p_CompileState == 0) {
                string error = GL.GetShaderInfoLog(ID);
                p_CompileError.AddRange(error.Split('\n'));
            }

            return p_CompileState != 0;
        }

        public override bool IsResourceReady
        { get { return ID != 0; } }

        protected override void DoCreateGLResource(TrtGLControl aGL)
        {
            if (ID == 0)
                ID = GL.CreateShader(Type);
            return;
        }

        protected override void DoDisposeGLResource(TrtGLControl aGL)
        {
            if (ID != 0)
                GL.DeleteShader(ID);
            ID = 0;
            return;
        }

        public abstract ShaderType Type
        { get; }

        public int ID
        { get; private set; }

        public bool Compiled
        { get { return p_CompileState != 0; } }
        public IReadOnlyList<string> CompileError
        { get { return p_CompileError; } }

        private int p_CompileState = -1;
        private List<string> p_CompileError = new List<string>();

        public class TGLVertexShader : TGLShader
        {
            public override ShaderType Type => ShaderType.VertexShader;
        }

        public class TGLFragmentShader : TGLShader
        {
            public override ShaderType Type => ShaderType.FragmentShader;
        }

        public class TGLTessControlShader : TGLShader
        {
            public override ShaderType Type => ShaderType.TessControlShader;
        }

        public class TGLTessEvaluationShader : TGLShader
        {
            public override ShaderType Type => ShaderType.TessEvaluationShader;
        }

        public class TGLComputeShader : TGLShader
        {
            public override ShaderType Type
            { get { return ShaderType.ComputeShader; } }

            public static int GetMaxComputeWorkGroupCount(TrtGLControl aGL)
            { return GL.GetInteger((GetPName)All.MaxComputeWorkGroupCount); }
        }
    }
}
