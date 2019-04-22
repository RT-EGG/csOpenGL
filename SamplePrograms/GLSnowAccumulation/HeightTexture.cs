﻿using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using rtOpenTK;
using rtOpenTK.rtGLResourceObject;

namespace GLSnowAccumulation
{
    public class THeightTexture : TGLResourceObject
    {
        public THeightTexture(int aWidth, int aHeight)
            : base()
        {
            Width  = aWidth;
            Height = aHeight;

            return;
        }
        public int TextureID
        { get; private set; }

        public void Randomize(TrtGLControl aGL)
        {
            if (!(p_Shader.Compiled && p_ShaderProgram.Linked))
                return;

            GL.UseProgram(p_ShaderProgram.ID);
            try {
                GL.BindImageTexture(0, TextureID, 0, false, 0, TextureAccess.ReadWrite, SizedInternalFormat.Rgba32f);
                try {
                    int p = GL.GetUniformLocation(p_ShaderProgram.ID, "inDstTexture");
                    GL.ProgramUniform1(p_ShaderProgram.ID, 0, 0);
                    GL.ProgramUniform1(p_ShaderProgram.ID, 1, Width);
                    GL.ProgramUniform1(p_ShaderProgram.ID, 2, Height);
                    GL.ProgramUniform1(p_ShaderProgram.ID, 3, (float)(new Random()).NextDouble());

                    GL.DispatchCompute(256, 256, 1);
                    GL.MemoryBarrier(MemoryBarrierFlags.ShaderImageAccessBarrierBit);

                } finally {
                    GL.BindImageTexture(0, 0, 0, false, 0, TextureAccess.ReadWrite, SizedInternalFormat.Rgba32f);
                }
            } finally {
                GL.UseProgram(0);
            }
            
            return;
        }

        public List<string> GenerateShaderSource()
        {
            List<string> result = new List<string>();

            result.Add("#version 430");
            result.Add("");
            result.Add("layout (binding = 0, ) writeonly uniform image2D inDstTexture;");
            result.Add("layout (location = 0) uniform int inTextureWidth;");
            result.Add("layout (location = 0) uniform int inTextureWidth;");
            result.Add("");
            result.Add("layout (localsize_x = 32, local_size_y = 32) in;");
            result.Add("");
            result.Add("void main(void)");
            result.Add("{");
            result.Add("");
            result.Add("    return;");
            result.Add("}");

            return result;
        }

        public int Width
        { get; private set; } = 0;

        public int Height
        { get; private set; } = 0;

        public override bool IsResourceReady
        { get { return TextureID != 0; } }

        protected override void DoCreateGLResource(TrtGLControl aGL)
        {
            base.DoCreateGLResource(aGL);

            p_Shader = new TGLShader.TGLComputeShader();
            p_ShaderProgram = new TGLShaderProgram();

            p_Shader.Compile(aGL, TGLShaderTextSource.CreateFileSource(".\\shader\\HeightInitialization.glsl"));
            //p_Shader.Compile(aGL, TGLShaderTextSource.CreateTextSource(GenerateShaderSource()));
            p_ShaderProgram.AttachShader(aGL, p_Shader);
            p_ShaderProgram.Link(aGL);

            float[] buffer = new float[Width * Height * 4];
            for (int x = 0; x < Width; ++x) {
                for (int y = 0; y < Height; ++y) {
                    int index = (y * Width * 4) + (x * 4);
                    buffer[index + 0] = (float)x / (float)Width;//1.0f;
                    buffer[index + 1] = 0.0f;
                    buffer[index + 2] = 1.0f;
                    buffer[index + 3] = 1.0f;
                }
            }

            TextureID = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            try {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba32f, Width, Height, 0, PixelFormat.Rgba, PixelType.Float, (IntPtr)null);
                               
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)All.ClampToEdge });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)All.ClampToEdge });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)All.Linear });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)All.Linear });

            } finally {
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }

            Randomize(aGL);

            return;
        }

        protected override void DoDisposeGLResource(TrtGLControl aGL)
        {
            base.DoDisposeGLResource(aGL);

            GL.DeleteTexture(TextureID);
            return;
        }

        private TGLShader p_Shader = null;
        private TGLShaderProgram p_ShaderProgram = null;
    }
}