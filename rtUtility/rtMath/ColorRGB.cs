// System
using System;

namespace rtUtility.rtMath
{
    public interface IROColorRGB : IEquatable<IROColorRGB>
    {
        double R { get; }
        double G { get; }
        double B { get; }
        byte Rb { get; }
        byte Gb { get; }
        byte Bb { get; }
        TColorHSV ToHSV();
        System.Drawing.Color ToSystemColor();
    }

    public interface IColorRGB : IROColorRGB
    {
        void Set(double aR, double aG, double aB);
        void Set(System.Drawing.Color aColor);
        new double R { get; set; }
        new double G { get; set; }
        new double B { get; set; }
        new byte Rb { get; set; }
        new byte Gb { get; set; }
        new byte Bb { get; set; }
    }

    public class TColorRGB : IColorRGB
    {
        public TColorRGB()
            : this(0.0, 0.0, 0.0)
        {
            return;
        }

        public TColorRGB(IROColorRGB aSrc)
            : this(aSrc.R, aSrc.G, aSrc.B)
        {
            return;
        }

        public TColorRGB(System.Drawing.Color aColor)
        {

        }

        public TColorRGB(double aR, double aG, double aB)
        {
            R = aR;
            G = aG;
            B = aB;
            return;
        }

        public void Set(double aR, double aG, double aB)
        {
            R = aR;
            G = aG;
            B = aB;
            return;
        }

        public void Set(System.Drawing.Color aColor)
        {
            Rb = aColor.R;
            Gb = aColor.G;
            Bb = aColor.B;
            return;
        }

        public double R
        { get; set; } = 0.0;
        public double G
        { get; set; } = 0.0;
        public double B
        { get; set; } = 0.0;

        public byte Rb
        {
            get { return (byte)System.Math.Truncate((R * (double)byte.MaxValue) + 0.5); }
            set { R = (double)value / (double)byte.MaxValue; }
        }

        public byte Gb
        {
            get { return (byte)System.Math.Truncate((G * (double)byte.MaxValue) + 0.5); }
            set { G = (double)value / (double)byte.MaxValue; }
        }

        public byte Bb
        {
            get { return (byte)System.Math.Truncate((B * (double)byte.MaxValue) + 0.5); }
            set { B = (double)value / (double)byte.MaxValue; }
        }

        public TColorHSV ToHSV()
        {
            TColorHSV result = new TColorHSV();
            double min = System.Math.Min(R, System.Math.Min(G, B));
            double max = System.Math.Max(R, System.Math.Max(G, B));

            result.V = max;
            if (max == 0) {
                result.H = 0.0;
                result.S = 0.0;
            } else {
                result.S = (max - min) / max;

                if (max.AlmostEqual(min)) {
                    result.H = 0.0;
                } else {
                    double h;
                    if (max.AlmostEqual(B))
                        h = 60.0 * ((R - G) / (max - min)) + 240.0;
                    else if (max.AlmostEqual(G))
                        h = 60.0 * ((B - R) / (max - min)) + 120.0;
                    else
                        h = 60.0 * ((G - B) / (max - min));

                    result.H = h.Clamp(0.0, 360.0) / 360.0;
                }
            }

            return result;
        }

        public System.Drawing.Color ToSystemColor()
        {
            return System.Drawing.Color.FromArgb(255, Rb, Gb, Bb);
        }

        public bool Equals(IROColorRGB other)
        {
            return R.AlmostEqual(other.R)
                && G.AlmostEqual(other.G)
                && B.AlmostEqual(other.B);
        }
    }
}
