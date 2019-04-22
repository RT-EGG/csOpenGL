// System
using System;

namespace rtUtility.rtMath
{
    public class TColorRGB
    {
        public TColorRGB()
            : this(0.0, 0.0, 0.0)
        {
            return;
        }

        public TColorRGB(double aR, double aG, double aB)
        {
            R = aR;
            G = aG;
            B = aB;
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
            get { return (byte)Math.Truncate((R * (double)byte.MaxValue) + 0.5); }
            set { R = (double)value / (double)byte.MaxValue; }
        }

        public byte Gb
        {
            get { return (byte)Math.Truncate((G * (double)byte.MaxValue) + 0.5); }
            set { G = (double)value / (double)byte.MaxValue; }
        }

        public byte Bb
        {
            get { return (byte)Math.Truncate((B * (double)byte.MaxValue) + 0.5); }
            set { B = (double)value / (double)byte.MaxValue; }
        }
    }
}
