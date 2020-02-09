// System
using System;
using System.Collections.Generic;
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
    public class TSnowfallParticles : TDisposableObject
    {
        public TSnowfallParticles()
        {
            TrtGLControl.EnqueueGLTask(InitializeShaders, null);
            return;
        }

        public float GeneratePerSec
        { get; set; } = 10.0f;

        public THeightMap HeightMap
        { get; set; } = null;

        public void AddParticle(TVector3 aPosition, float aRadius)
        {
            TParticle p = new TParticle();
            p.Position.Assign(aPosition);
            p.Radius = aRadius;

            p_AddedParticles.Enqueue(p);

            return;
        }

        public void Render(TrtGLControl aGL, TGLRenderingStatus aStatus)
        {
            if (p_PointCount == 0)
                return;

            if (!p_RenderShaderProgram.Linked)
                return;

            aStatus.AttribStack.EnableGroup.Push();
            try {
                GL.UseProgram(p_RenderShaderProgram.ID);
                try {
                    aStatus.AttribStack.EnableGroup.Enable(EnableCap.PointSprite);
                    aStatus.AttribStack.EnableGroup.Enable(EnableCap.VertexProgramPointSize);
                    aStatus.AttribStack.EnableGroup.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

                    GL.ProgramUniformMatrix4(p_RenderShaderProgram.ID, 0, 1, false, aStatus.ProjectionMatrix.CurrentMatrix.FloatArray);
                    GL.ProgramUniformMatrix4(p_RenderShaderProgram.ID, 1, 1, false, aStatus.ModelViewMatrix.CurrentMatrix.FloatArray);
                    GL.ProgramUniform2(p_RenderShaderProgram.ID, 2, aStatus.Viewport.Width, aStatus.Viewport.Height);

                    GL.EnableVertexAttribArray(0);
                    GL.EnableVertexAttribArray(1);
                    GL.EnableVertexAttribArray(2);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, p_Buffer.ID);
                    GL.VertexAttribIPointer(0, 1, VertexAttribIntegerType.Int, con_ParticleBufferSize, (IntPtr)0);
                    GL.VertexAttribPointer(1, 1, VertexAttribPointerType.Float, false, con_ParticleBufferSize, sizeof(int));
                    GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, con_ParticleBufferSize, sizeof(int) + sizeof(float));
            
                    GL.DrawArrays(PrimitiveType.Points, 0, p_PointCount);

                } finally {
                    GL.DisableVertexAttribArray(0);
                    GL.DisableVertexAttribArray(1);
                    GL.DisableVertexAttribArray(2);
                    GL.UseProgram(0);
                }
            } finally {
                aStatus.AttribStack.EnableGroup.Pop();
            }

            return;
        }

        public void Timestep(TrtGLControl aGL, float aTime)
        {
            if (!GeneratePerSec.IsZero()) {
                p_PrevGenTime += aTime;
                float genTimePer1 = 1.0f / GeneratePerSec;
                while (p_PrevGenTime > genTimePer1) {
                    double x = p_GenRandomizer.NextDouble() - 0.5;
                    double z = p_GenRandomizer.NextDouble() - 0.5;
                    AddParticle(new TVector3(x, 1.0, z), 0.01f);

                    p_PrevGenTime -= genTimePer1;
                }
            }

            UpdateBuffer(aGL);

            if (!(p_TimestepComputeShader.Compiled && p_TimestepComputeShaderProgram.Linked))
                return;

            ErrorCode e = GL.GetError();
            GL.UseProgram(p_TimestepComputeShaderProgram.ID);
            try {
                GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, p_Buffer.ID);
                GL.BindImageTexture(0, (HeightMap == null) ? 0 : HeightMap.TextureID, 0, false, 0, TextureAccess.ReadWrite, SizedInternalFormat.Rgba32f);
                try {
                    GL.ProgramUniform1(p_TimestepComputeShaderProgram.ID, 0, p_PointCount);
                    GL.ProgramUniform1(p_TimestepComputeShaderProgram.ID, 1, aTime);
                    GL.ProgramUniform1(p_TimestepComputeShaderProgram.ID, 2, (HeightMap == null) ? 0 : HeightMap.MinHeight);
                    GL.ProgramUniform1(p_TimestepComputeShaderProgram.ID, 3, (HeightMap == null) ? 0 : HeightMap.MaxHeight);
                    GL.ProgramUniform2(p_TimestepComputeShaderProgram.ID, 4, (HeightMap == null) ? 0 : HeightMap.Width, (HeightMap == null) ? 0 : HeightMap.Height);

                    GL.DispatchCompute(256, 256, 1);
                    GL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);

                } finally {
                    GL.BindImageTexture(0, 0, 0, false, 0, TextureAccess.ReadWrite, SizedInternalFormat.Rgba32f);
                    GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, 0);
                }
            } finally {
                GL.UseProgram(0);
            }
            e = GL.GetError();

            UpdateDeadIndices(aGL);

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

        private void UpdateBuffer(TrtGLControl aGL)
        {
            if (p_AddedParticles.Count == 0)
                return;

            int increment = p_AddedParticles.Count - p_DeadIndices.Count;
            unsafe {
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, p_Buffer.ID);
                try {
                    if (p_PointCapacity < (p_PointCount + increment)) {
                        p_PointCapacity = Math.Max(10000, p_PointCapacity * 2);

                        byte[] buffer = new byte[p_PointCapacity * con_ParticleBufferSize];
                        GL.GetBufferSubData(BufferTarget.ShaderStorageBuffer, (IntPtr)0, p_PointCount * con_ParticleBufferSize, buffer);
                        // realloc
                        GL.BufferData(BufferTarget.ShaderStorageBuffer, p_PointCapacity * con_ParticleBufferSize, buffer, BufferUsageHint.DynamicDraw);
                    }

                    byte* head = (byte*)GL.MapBuffer(BufferTarget.ShaderStorageBuffer, BufferAccess.WriteOnly);
                    try {
                        int index = 0;
                        while (p_AddedParticles.Count > 0) {
                            if (p_DeadIndices.Count > 0) {
                                index = p_DeadIndices.Dequeue();
                            } else {
                                index = p_PointCount++;
                            }

                            Assign(head + (con_ParticleBufferSize * index), p_AddedParticles.Dequeue());
                        }

                    } finally {
                        GL.UnmapBuffer(BufferTarget.ShaderStorageBuffer);
                    }

                } finally {
                    GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);
                }
            }

            return;
        }

        private void UpdateDeadIndices(TrtGLControl aGL)
        {
            unsafe {
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, p_Buffer.ID);
                try {
                    byte* head = (byte*)GL.MapBuffer(BufferTarget.ShaderStorageBuffer, BufferAccess.ReadWrite);
                    try {
                        for (int i = 0; i < p_PointCount; ++i) {
                            switch (*(int*)(head + (con_ParticleBufferSize * i))) {
                                case 1:
                                    p_DeadIndices.Enqueue(i);
                                    *(int*)(head + (con_ParticleBufferSize * i)) = 2;
                                    break;
                            }
                        }

                    } finally {
                        GL.UnmapBuffer(BufferTarget.ShaderStorageBuffer);
                    }

                } finally {
                    GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);
                }
            }

            return;
        }

        private unsafe void Assign(byte* aDst, TParticle aSrc)
        { 
            AssignAndInc<int>(ref aDst, 0); 
            AssignAndInc<float>(ref aDst, aSrc.Radius);
            AssignAndInc<float>(ref aDst, (float)aSrc.Position.X);
            AssignAndInc<float>(ref aDst, (float)aSrc.Position.Y);
            AssignAndInc<float>(ref aDst, (float)aSrc.Position.Z);
            AssignAndInc<float>(ref aDst, (float)aSrc.Velocity.X);
            AssignAndInc<float>(ref aDst, (float)aSrc.Velocity.Y);
            AssignAndInc<float>(ref aDst, (float)aSrc.Velocity.Z);
            
            return;
        }

        private unsafe void AssignAndInc<T>(ref byte* aDst, T aSrc) where T : unmanaged
        {
            *((T*)aDst) = aSrc;
            aDst += sizeof(T);
            return;
        }

        private void InitializeShaders(TrtGLControl aGL, object aObject)
        {
            p_TimestepComputeShader.CreateGLResource(aGL);
            p_TimestepComputeShaderProgram.CreateGLResource(aGL);

            p_TimestepComputeShader.Compile(aGL, TGLShaderTextSource.CreateFileSource("..\\resource\\shader\\Snowfall\\Timestep.glsl"));
            p_TimestepComputeShaderProgram.AttachShader(aGL, p_TimestepComputeShader);
            p_TimestepComputeShaderProgram.Link(aGL);

            bool compiled = true;
            p_VertexShader.CreateGLResource(aGL);
            p_FragmentShader.CreateGLResource(aGL);
            compiled &= p_VertexShader.Compile(aGL, TGLShaderTextSource.CreateFileSource("..\\resource\\shader\\Snowfall\\Vertex.glsl"));
            compiled &= p_FragmentShader.Compile(aGL, TGLShaderTextSource.CreateFileSource("..\\resource\\shader\\Snowfall\\Fragment.glsl"));

            if (compiled) {
                p_RenderShaderProgram.CreateGLResource(aGL);

                p_RenderShaderProgram.AttachShader(aGL, p_VertexShader);
                p_RenderShaderProgram.AttachShader(aGL, p_FragmentShader);

                p_RenderShaderProgram.Link(aGL);
            }

            return;
        }

        /*
            int Status;
            float Radius;
            vec3(vec4) Position;
            vec3(vec4) Velocity;
        */
        private TGLBufferObject p_Buffer = new TGLBufferObject();
        private int p_PointCount = 0;
        private int p_PointCapacity = 0;
        private Queue<TParticle> p_AddedParticles = new Queue<TParticle>();
        private Queue<int> p_DeadIndices = new Queue<int>();
        private float p_PrevGenTime = 0.0f;
        private Random p_GenRandomizer = new Random();

        // timestep shader
        private TGLShader p_TimestepComputeShader = new TGLShader.TGLComputeShader();
        private TGLShaderProgram p_TimestepComputeShaderProgram = new TGLShaderProgram();
        // render shader
        private TGLShader p_VertexShader = new TGLShader.TGLVertexShader();
        private TGLShader p_FragmentShader = new TGLShader.TGLFragmentShader();
        private TGLShaderProgram p_RenderShaderProgram = new TGLShaderProgram();

        private const int con_ParticleBufferSize = sizeof(int) + sizeof(float) + (sizeof(float) * 3 * 2);
        private class TParticle
        {
            public float Radius = 0.01f;
            public TVector3 Position = new TVector3();
            public TVector3 Velocity = new TVector3();
        }
    }
}
