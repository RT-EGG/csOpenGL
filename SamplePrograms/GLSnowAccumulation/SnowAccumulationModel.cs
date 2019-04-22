// OpenTK
using OpenTK.Graphics.OpenGL;
// rtOpenTK
using rtOpenTK;
using rtOpenTK.rtGLResourceObject;
// rtUtility
using rtUtility.rtMath;
using rtUtility;

namespace GLSnowAccumulation
{
    public class TSnowAccumulationModel : TDisposableObject
    {
        public TSnowAccumulationModel()
        {
            p_LightInstance.Position.Set(10.0, 10.0, 10.0);
            p_LightInstance.Direction = (-p_LightInstance.Position).Normalized;

            p_Material.Ambient.R = 0.0;
            p_Material.Ambient.G = 0.0;
            p_Material.Ambient.B = 0.2;
            p_Material.Diffuse.R = 1.0;
            p_Material.Diffuse.G = 1.0;
            p_Material.Diffuse.B = 1.0;

            TrtGLControl.EnqueueGLTask(InitializeBuffers, null);
            TrtGLControl.EnqueueGLTask(InitializeShaders, null);

            return;
        }

        public float MinHeight
        { get; set; } = 0.0f;

        public float MaxHeight
        { get; set; } = 0.2f;

        public void RandomizeHeight()
        {
            p_HeightRandomize = true;
            return;
        }

        public void ReloadShader()
        {
            TrtGLControl.EnqueueGLTask(InitializeShaders, null);
            return;
        }

        public void Render(TrtGLControl aGL, TGLRenderingStatus aStatus)
        {
            if (!p_RenderShaderProgram.Linked)
                return;

            if (p_HeightRandomize) {
                p_HeightMap.Randomize(aGL);
                p_HeightRandomize = false;
            }

            p_Material.UpdateBuffer(aGL);
            p_LightInstance.UpdateBuffer(aGL);

            GL.UseProgram(p_RenderShaderProgram.ID);
            try {
                TMatrix33 normal = new TMatrix33();
                for (int r = 0; r < 3; ++r) {
                    for (int c = 0; c < 3; ++c) {
                        normal[r, c] = aStatus.ModelViewMatrix.Model.CurrentMatrix[r, c];
                    }
                }
                normal.Inverse();
                normal.Tranpose();

                TVector2H n = normal * (new TVector2H(0.0, 0.0, 1.0));

                GL.ProgramUniform1(p_RenderShaderProgram.ID, 0, 64.0f);
                GL.ProgramUniform1(p_RenderShaderProgram.ID, 1, 64.0f);
                GL.ProgramUniformMatrix4(p_RenderShaderProgram.ID, 2, 1, false, aStatus.ProjectionMatrix.CurrentMatrix.FloatArray);
                GL.ProgramUniformMatrix4(p_RenderShaderProgram.ID, 3, 1, false, aStatus.ModelViewMatrix.CurrentMatrix.FloatArray);
                GL.ProgramUniformMatrix4(p_RenderShaderProgram.ID, 4, 1, false, aStatus.ModelViewMatrix.Model.CurrentMatrix.FloatArray);
                GL.ProgramUniformMatrix3(p_RenderShaderProgram.ID, 5, 1, false, normal.FloatArray);
                GL.ProgramUniform1(p_RenderShaderProgram.ID, 6, MinHeight);
                GL.ProgramUniform1(p_RenderShaderProgram.ID, 7, MaxHeight);

                GL.BindBufferBase(BufferRangeTarget.UniformBuffer, GL.GetUniformBlockIndex(p_RenderShaderProgram.ID, "Material"), p_Material.BufferID);
                GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 1, p_LightInstance.BufferID);

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, p_HeightMap.TextureID);
                GL.ActiveTexture(TextureUnit.Texture1);
                GL.BindTexture(TextureTarget.Texture2D, p_Surface.TextureID);
                GL.BindSampler(0, p_SamplerObject.ID);
                GL.BindSampler(1, p_SamplerObject.ID);
                GL.ProgramUniform2(p_RenderShaderProgram.ID, 8, p_Surface.Width, p_Surface.Height);
                GL.ProgramUniform2(p_RenderShaderProgram.ID, 9, p_HeightMap.Width, p_HeightMap.Height);

                byte[] buffer = new byte[1024 * 1024 * 4];
                GL.GetTexImage(TextureTarget.Texture2D, 0, PixelFormat.Rgba, PixelType.UnsignedByte, buffer);

                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(1);
                GL.EnableVertexAttribArray(2);
                GL.BindBuffer(BufferTarget.ArrayBuffer, p_Vertices.ID);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, sizeof(float) * 4 * 3);
                GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, sizeof(float) * ((4 * 3) + (4 * 3)));

                GL.PatchParameter(PatchParameterInt.PatchVertices, 4);
                GL.DrawArrays(PrimitiveType.Patches, 0, 4);

            } finally {
                GL.ActiveTexture(TextureUnit.Texture1);
                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.UseProgram(0);
            }

            return;
        }

        private void InitializeBuffers(TrtGLControl aGL, object aObject)
        {
            const float con_MeshSize = 2.0f;
            const float con_MeshHalfSize = con_MeshSize * 0.5f;
            float[] vertices = new float[(4 * 3) + (4 * 3) + (4 * 2)] {
                -con_MeshHalfSize, +con_MeshHalfSize, 0.0f,
                -con_MeshHalfSize, -con_MeshHalfSize, 0.0f,                
                +con_MeshHalfSize, -con_MeshHalfSize, 0.0f,
                +con_MeshHalfSize, +con_MeshHalfSize, 0.0f,
                0.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 1.0f,
                0.0f, 0.0f, 1.0f,
                0.0f, 1.0f,
                0.0f, 0.0f,
                1.0f, 0.0f,
                1.0f, 1.0f
            };

            p_Vertices.CreateGLResource(aGL);

            GL.BindBuffer(BufferTarget.ArrayBuffer, p_Vertices.ID);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            p_SamplerObject.CreateGLResource(aGL);

            GL.SamplerParameterI(p_SamplerObject.ID, SamplerParameterName.TextureWrapS, new int[] { (int)All.ClampToEdge });
            GL.SamplerParameterI(p_SamplerObject.ID, SamplerParameterName.TextureWrapT, new int[] { (int)All.ClampToEdge });
            GL.SamplerParameterI(p_SamplerObject.ID, SamplerParameterName.TextureMinFilter, new int[] { (int)All.Linear });
            GL.SamplerParameterI(p_SamplerObject.ID, SamplerParameterName.TextureMagFilter, new int[] { (int)All.Linear });

            return;
        }

        protected override void Dispose(bool aDisposing)
        {
            base.Dispose(aDisposing);
            if (aDisposing) {
                p_Vertices.Dispose();
                p_VertexShader.Dispose();
                p_FragmentShader.Dispose();
                p_TessControlShader.Dispose();
                p_TessEvacuateShader.Dispose();
                p_RenderShaderProgram.Dispose();

                p_Material.Dispose();
                p_LightInstance.Dispose();
            }
            return;
        }

        private void InitializeShaders(TrtGLControl aGL, object aObject)
        {
            p_VertexShader.CreateGLResource(aGL);
            p_FragmentShader.CreateGLResource(aGL);
            p_TessControlShader.CreateGLResource(aGL);
            p_TessEvacuateShader.CreateGLResource(aGL);

            bool compiled = true;
            compiled &= p_VertexShader.Compile(aGL, TGLShaderTextSource.CreateFileSource("..\\resource\\shader\\Vertex.glsl"));
            compiled &= p_FragmentShader.Compile(aGL, TGLShaderTextSource.CreateFileSource("..\\resource\\shader\\Fragment.glsl"));
            compiled &= p_TessControlShader.Compile(aGL, TGLShaderTextSource.CreateFileSource("..\\resource\\shader\\TessControl.glsl"));
            compiled &= p_TessEvacuateShader.Compile(aGL, TGLShaderTextSource.CreateFileSource("..\\resource\\shader\\TessEvacuate.glsl"));

            if (compiled) {
                p_RenderShaderProgram.CreateGLResource(aGL);

                p_RenderShaderProgram.AttachShader(aGL, p_VertexShader);
                p_RenderShaderProgram.AttachShader(aGL, p_FragmentShader);
                p_RenderShaderProgram.AttachShader(aGL, p_TessControlShader);
                p_RenderShaderProgram.AttachShader(aGL, p_TessEvacuateShader);

                p_RenderShaderProgram.Link(aGL);
            }

            return;
        }

        private TGLBufferObject p_Vertices = new TGLBufferObject();
        private TGLShader p_VertexShader = new TGLShader.TGLVertexShader();
        private TGLShader p_FragmentShader = new TGLShader.TGLFragmentShader();
        private TGLShader p_TessControlShader = new TGLShader.TGLTessControlShader();
        private TGLShader p_TessEvacuateShader = new TGLShader.TGLTessEvaluationShader();
        private TGLShaderProgram p_RenderShaderProgram = new TGLShaderProgram();

        private TSurfaceTexture p_Surface = new TSurfaceTexture();
        private THeightTexture p_HeightMap = new THeightTexture(2048, 2048);
        private bool p_HeightRandomize = false;
        private TGLSampler p_SamplerObject = new TGLSampler();

        private TMaterial p_Material = new TMaterial();
        private TLightInstance p_LightInstance = new TLightInstance();
    }
}
