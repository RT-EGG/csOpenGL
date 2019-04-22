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
    public class TLightMaterial
    {
        public TColorRGBA Ambient = new TColorRGBA(1.0f, 1.0f, 1.0f, 1.0f);
        public TColorRGBA Diffuse = new TColorRGBA(1.0f, 1.0f, 1.0f, 1.0f);
        public TColorRGBA Specular = new TColorRGBA(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public class TLightInstance : TDisposableObject
    {
        public TLightInstance()
        {
            TrtGLControl.EnqueueGLTask(InitializeBuffers, null);
            return;
        }

        public int BufferID
        { get { return p_Buffer.ID; } }


        public void UpdateBuffer(TrtGLControl aGL)
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, p_Buffer.ID);
            try {
                unsafe {
                    float* ptr = (float*)(GL.MapBuffer(BufferTarget.UniformBuffer, BufferAccess.WriteOnly));
                    try {
                        void AssignVector(ref float* aDst, TVector3 aSrc)
                        {
                            for (int i = 0; i < 3; ++i) {
                                *(aDst++) = (float)aSrc[i];
                            }
                            aDst++;
                            return;
                        }

                        void AssignColor(ref float* aDst, TColorRGBA aSrc)
                        {
                            *(aDst++) = (float)aSrc.R;
                            *(aDst++) = (float)aSrc.G;
                            *(aDst++) = (float)aSrc.B;
                            *(aDst++) = (float)aSrc.A;
                            return;
                        }

                        AssignVector(ref ptr, Position);
                        AssignVector(ref ptr, Direction);
                        AssignColor(ref ptr, Material.Ambient);
                        AssignColor(ref ptr, Material.Diffuse);
                        AssignColor(ref ptr, Material.Specular);

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
            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, p_Buffer.ID);
            try {
                GL.BufferData(BufferTarget.ShaderStorageBuffer, con_DataSize, (IntPtr)null, BufferUsageHint.StaticDraw);

            } finally {
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);
            }

            UpdateBuffer(aGL);
            return;
        }

        private TGLBufferObject p_Buffer = new TGLBufferObject();
        public TVector3 Position = new TVector3(0.0f, 0.0f, 0.0f);
        public TVector3 Direction = new TVector3(0.0f, 0.0f, 0.0f);
        public TLightMaterial Material = new TLightMaterial();

        private readonly int con_DataSize = sizeof(float) * (4 * (2 + 3));
    }
}
