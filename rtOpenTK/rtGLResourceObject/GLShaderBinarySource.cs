// System
using System;
using System.IO;
using System.Runtime.InteropServices;
// OpenTK
using OpenTK.Graphics.OpenGL4;

namespace rtOpenTK.rtGLResourceObject
{
    public class TGLShaderBinarySource : TGLShaderSource
    {
        public bool LoadFromFile(string aFilePath, string aEntryPoint = "main")
        {
            if (BinaryData != (IntPtr)null) {
                Marshal.FreeCoTaskMem(BinaryData);
                BinaryData = (IntPtr)null;
            }
            BinarySize = 0;

            if (!File.Exists(aFilePath))
                return false;

            FileStream input = new FileStream(aFilePath, FileMode.Open);
            try {
                byte[] bin = new byte[input.Length];
                input.Read(bin, 0, bin.Length);

                BinarySize = bin.Length;
                BinaryData = Marshal.AllocCoTaskMem(bin.Length);
                Marshal.Copy(bin, 0, BinaryData, bin.Length);

            } finally {
                input.Dispose();
            }
            return true;
        }

        protected override void DoLoad(TrtGLControl aGL, int aShaderID)
        {
            GL.ShaderBinary(1, ref aShaderID, (BinaryFormat)(All.ShaderBinaryFormatSpirVArb), BinaryData, BinarySize);
            GL.SpecializeShader(aShaderID, EntryPoint, 0, new int[0], new int[0]);
            return;
        }

        protected override void Dispose(bool aDisposing)
        {
            base.Dispose(aDisposing);
            if (BinaryData != (IntPtr)null) {
                Marshal.FreeCoTaskMem(BinaryData);
                BinaryData = (IntPtr)null;
            }
            BinarySize = 0;
            return;
        }

        private IntPtr BinaryData
        { get; set; } = (IntPtr)null;
        private int BinarySize
        { get; set; } = 0;
        private string EntryPoint
        { get; set; } = "main";
    }
}
