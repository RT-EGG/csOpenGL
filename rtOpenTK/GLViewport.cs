// System
using System.Drawing;
// OpenTK
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace rtOpenTK
{
    public interface IROGLViewport
    {
        void Adapt();

        int X { get; }
        int Y { get; }
        int Width { get; }
        int Height { get; }
        double AspectRatio { get; }
        Rectangle Rect { get; }
    }

    public interface IGLViewport : IROGLViewport
    {
        new int X { get; set; }
        new int Y { get; set; }
        new int Width { get; set; }
        new int Height { get; set; }
        new Rectangle Rect { get; set; }
    }

    public class TGLViewport : IGLViewport
    {
        public void Adapt()
        {
            GL.Viewport(X, Y, Width, Height);
            return;
        }

        public int X
        { get; set; } = 0;
        public int Y
        { get; set; } = 0;
        public int Width
        { get; set; } = 1;
        public int Height
        { get; set; } = 1;
        public double AspectRatio
        { get { return (double)Width / (double)Height; } }
        public Rectangle Rect
        {
            get
            { return new Rectangle(X, Y, Width, Height); }
            set
            {
                X      = value.X;
                Y      = value.Y;
                Width  = value.Width;
                Height = value.Height;
            }
        }
    }
}
