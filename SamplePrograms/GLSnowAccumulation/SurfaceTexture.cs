// 
using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using rtOpenTK;
using rtOpenTK.rtGLResourceObject;
using rtUtility.rtMath;

namespace GLSnowAccumulation
{
    public class TSurfaceTexture : TGLResourceObject
    {
        public int TextureID
        { get; private set; } = 0;

        public int Width
        { get; private set; } = 0;

        public int Height
        { get; private set; } = 0;

        public int PotWidth
        { get; private set; } = 0;

        public int PotHeight
        { get; private set; } = 0;

        public override bool IsResourceReady
        { get { return TextureID != 0; } }

        protected override void DoCreateGLResource(TrtGLControl aGL)
        {
            base.DoCreateGLResource(aGL);

            try {
                Bitmap bmp = new Bitmap("..\\resource\\image\\Surface.jpg");

                PotWidth  = (int)((uint)bmp.Width).UpToPowerOfTwo();
                PotHeight = (int)((uint)bmp.Height).UpToPowerOfTwo();
                byte[] buffer = new byte[PotWidth * PotHeight * 4];

                Width  = bmp.Width;
                Height = bmp.Height;

                try {
                    BitmapData data = bmp.LockBits(new Rectangle(new Point(0, 0), bmp.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    try {
                        unsafe {
                            int x, y;
                            byte* bits = (byte*)data.Scan0;
                            int index = 0;
                            for (y = 0; y < bmp.Height; ++y) {
                                for (x = 0; x < bmp.Width; ++x) {
                                    buffer[index++] = *(bits + 0);
                                    buffer[index++] = *(bits + 1);
                                    buffer[index++] = *(bits + 2);
                                    buffer[index++] = *(bits + 3);

                                    bits += 4;
                                }

                                for (x = bmp.Width; x < PotWidth; ++x) {
                                    buffer[index++] = 0;
                                    buffer[index++] = 0;
                                    buffer[index++] = 0;
                                    buffer[index++] = 0;
                                }
                            }

                            for (y = bmp.Height; y < PotHeight; ++y) {
                                for (x = 0; x < PotWidth; ++x) {
                                    buffer[index++] = 0;
                                    buffer[index++] = 0;
                                    buffer[index++] = 0;
                                    buffer[index++] = 0;
                                }
                            }
                        }

                    } finally {
                        bmp.UnlockBits(data);
                    }

                    TextureID = GL.GenTexture();
                    GL.BindTexture(TextureTarget.Texture2D, TextureID);
                    try {
                        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, PotWidth, PotHeight, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.UnsignedByte, buffer);

                        GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)All.ClampToBorder });
                        GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)All.ClampToBorder });
                        GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)All.Linear });
                        GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)All.Linear });

                    } finally {
                        GL.BindTexture(TextureTarget.Texture2D, 0);
                    }
                } finally {
                    bmp.Dispose();
                }
            } catch (Exception e) {
            }

            return;
        }

        protected override void DoDisposeGLResource(TrtGLControl aGL)
        {
            base.DoDisposeGLResource(aGL);

            GL.DeleteTexture(TextureID);
            return;
        }
    }
}
