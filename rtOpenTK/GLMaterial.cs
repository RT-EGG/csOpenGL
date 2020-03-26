using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using rtOpenTK.rtGLResourceObject;

namespace rtOpenTK
{
    public class TGLMaterial
    {
        public void Bind()
        {
            if ((Shader == null) || (!Shader.Linked)) {
                return;
            }
            
            GL.UseProgram(Shader.ID);
            SetupAfterBind();
            return;
        }

        public void Unbind()
        {
            GL.UseProgram(0);
            return;
        }

        public TGLShaderProgram Shader
        {
            get => m_Shader;
            set {
                if (m_Shader == value) {
                    return;
                }

                m_Shader = value;
                UniformVariables = null;
                return;
            }
        }

        protected virtual void SetupAfterBind()
        {
            return;
        }

        protected int GetAttribLocation(string inName)
        {
            if (Shader == null) {
                return -1;
            }

            if (Attributes == null) {
                CollectAttributes();
                if (Attributes == null) {
                    return -1;
                }
            }

            if (Attributes.TryGetValue(inName, out IShaderAttributes attribute)) {
                return attribute.Index;
            }
            return -1;
        }

        protected int GetUniformLocation(string inName)
        {
            if (Shader == null) {
                return -1;
            }

            if (UniformVariables == null) {
                CollectUniformVariables();
                if (UniformVariables == null) {
                    return -1;
                }
            }
            
            if (UniformVariables.TryGetValue(inName, out IShaderUniformVariable variable)) {
                return variable.Index;
            }
            return -1;
        }

        private void CollectAttributes()
        {
            Attributes = null;
            if (Shader == null) {
                return;
            }

            Dictionary<string, IShaderAttributes> attributes = new Dictionary<string, IShaderAttributes>();
            GL.GetProgram(Shader.ID, GetProgramParameterName.ActiveAttributes, out int count);
            for (int i = 0; i < count; ++i) {
                var attrib = new TShaderAttributes(Shader, i);
                attributes.Add(attrib.Name, attrib);
            }

            Attributes = attributes;
            return;
        }

        private void CollectUniformVariables()
        {
            UniformVariables = null;
            if (Shader == null) {
                return;
            }

            Dictionary<string, IShaderUniformVariable> variables = new Dictionary<string, IShaderUniformVariable>();
            GL.GetProgram(Shader.ID, GetProgramParameterName.ActiveUniforms, out int count);
            for (int i = 0; i < count; ++i) {
                var variable = new TShaderUniformVariable(Shader, i);
                variables.Add(variable.Name, variable);
            }

            UniformVariables = variables;
            return;　
        }
        
        private TGLShaderProgram m_Shader = null;
        private IReadOnlyDictionary<string, IShaderAttributes> Attributes
        { get; set; } = null;
        private IReadOnlyDictionary<string, IShaderUniformVariable> UniformVariables
        { get; set; } = null;

        public interface IShaderAttributes
        {
            string Name { get; }
            int Index { get; }
            int Size { get; }
            ActiveAttribType Type { get; }
        }

        public interface IShaderUniformVariable
        {
            string Name { get; }
            int Index { get; }
            int Size { get; }
            ActiveUniformType Type { get; }
        }

        private class TShaderAttributes : IShaderAttributes
        {
            public TShaderAttributes(TGLShaderProgram inShader, int inIndex)
            {
                GL.GetActiveAttrib(inShader.ID, inIndex, 128, out int length, out int size, out ActiveAttribType type, out string name);

                Name = name;
                Index = GL.GetAttribLocation(inShader.ID, name);
                Size = size;
                Type = type;
                return;
            }

            public string Name
            { get; private set; } = "";
            public int Index
            { get; private set; } = 0;
            public int Size
            { get; private set; } = 0;
            public ActiveAttribType Type
            { get; private set; } = (ActiveAttribType)(-1);
        }

        private class TShaderUniformVariable : IShaderUniformVariable
        {
            public TShaderUniformVariable(TGLShaderProgram inShader, int inIndex)
            {
                GL.GetActiveUniform(inShader.ID, inIndex, 128, out int length, out int size, out ActiveUniformType type, out string name);

                Name = name;
                Index = GL.GetUniformLocation(inShader.ID, name);
                Size = size;
                Type = type;
                return;
            }

            public string Name
            { get; private set; } = "";
            public int Index
            { get; private set; } = 0;
            public int Size
            { get; private set; } = 0;
            public ActiveUniformType Type
            { get; private set; } = (ActiveUniformType)(-1);
        }
    }
}
