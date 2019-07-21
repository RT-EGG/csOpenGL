// System
using System;

namespace rtUtility.rtMath
{
    public interface IROColorRGBA : IROColorRGB, IEquatable<IROColorRGBA>
    {
        double A { get; }
        byte Ab { get; }
        new System.Drawing.Color ToSystemColor();
    }

    public interface IColorRGBA : IColorRGB, IROColorRGBA
    {
        void Set(double aR, double aG, double aB, double aA);
        new void Set(System.Drawing.Color aColor);
        new double A { get; set; }
        new byte Ab { get; set; }
    }

    public class TColorRGBA : TColorRGB, IColorRGBA
    {
        public TColorRGBA()
        {
            return;
        }

        public TColorRGBA(IROColorRGBA aSrc)
            : this(aSrc.R, aSrc.G, aSrc.B, aSrc.A)
        {
            return;
        }

        public TColorRGBA(IROColorRGB aRGB, double aA = 1.0)
            : base(aRGB)
        {
            A = aA;
            return;
        }

        public TColorRGBA(double aR, double aG, double aB, double aA = 1.0)
            : base(aR, aG, aB)
        {
            A = aA;
            return;
        }

        public void Set(double aR, double aG, double aB, double aA)
        {
            base.Set(aR, aG, aB);
            A = aA;
            return;
        }

        public new void Set(System.Drawing.Color aColor)
        {
            base.Set(aColor);
            A = aColor.A;
            return;
        }

        public double A
        { get; set; } = 1.0;

        public byte Ab
        {
            get { return (byte)System.Math.Truncate((A * (double)byte.MaxValue) + 0.5); }
            set { A = (double)value / (double)byte.MaxValue; }
        }

        public new System.Drawing.Color ToSystemColor()
        {
            return System.Drawing.Color.FromArgb(Ab, Rb, Gb, Bb);
        }

        public bool Equals(IROColorRGBA other)
        {
            return (this as TColorRGB).Equals(other as IROColorRGB)
                && A.AlmostEqual(other.A);
        }
    }
}
