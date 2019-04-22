// System
using System;
// OpenTK
using OpenTK.Graphics.OpenGL4;
// rtOpenTK
using rtOpenTK;
using rtOpenTK.rtGLResourceObject;
// rtUtility
using rtUtility;
using rtUtility.rtMath;

namespace GLSnowAccumulation
{   
    public class TMaterial : TDisposableObject
    {
        public TMaterial()
        {
            TrtGLControl.EnqueueGLTask(InitializeBuffers, null);
            return;
        }

        public int BufferID
        { get { return p_Buffer.ID; } }

        public TColorRGBA Ambient
        { get; set; } = new TColorRGBA(0.2f, 0.2f, 0.2f, 1.0f);
        public TColorRGBA Diffuse
        { get; set; } = new TColorRGBA(0.8f, 0.8f, 0.8f, 1.0f);
        public TColorRGBA Specular
        { get; set; } = new TColorRGBA(0.0f, 0.0f, 0.0f, 1.0f);
        public TColorRGBA Emission
        { get; set; } = new TColorRGBA(0.0f, 0.0f, 0.0f, 1.0f);
        public float Shininess
        { get; set; } = 0.0f;


        public void UpdateBuffer(TrtGLControl aGL)
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, p_Buffer.ID);
            try {
                unsafe {
                    float* ptr = (float*)(GL.MapBuffer(BufferTarget.UniformBuffer, BufferAccess.WriteOnly));
                    try {
                        void Assign(ref float* aDst, TColorRGBA aSrc)
                        {
                            *(aDst++) = (float)aSrc.R;
                            *(aDst++) = (float)aSrc.G;
                            *(aDst++) = (float)aSrc.B;
                            *(aDst++) = (float)aSrc.A;
                            return;
                        }

                        Assign(ref ptr, Ambient);
                        Assign(ref ptr, Diffuse);
                        Assign(ref ptr, Specular);
                        Assign(ref ptr, Emission);
                        *(++ptr) = Shininess;

                    } finally {
                        GL.UnmapBuffer(BufferTarget.UniformBuffer);
                    }
                }

            } finally {
                GL.BindBuffer(BufferTarget.UniformBuffer, 0);
            }

            return;
        }

        protected override void Dispose(bool aDisposing)
        {
            base.Dispose(aDisposing);
            if (aDisposing) {
                p_Buffer.Dispose();
            }
            return;
        }

        private void InitializeBuffers(TrtGLControl aGL, object aObject)
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, p_Buffer.ID);
            try {
                GL.BufferData(BufferTarget.UniformBuffer, sizeof(float) * ((4 * 4) + 1), (IntPtr)null, BufferUsageHint.StaticDraw);

            } finally {
                GL.BindBuffer(BufferTarget.UniformBuffer, 0);
            }

            UpdateBuffer(aGL);
            return;
        }

        private TGLBufferObject p_Buffer = new TGLBufferObject();
    }
}
