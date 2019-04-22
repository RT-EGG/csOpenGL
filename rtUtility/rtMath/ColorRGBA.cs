// System
using System;

namespace rtUtility.rtMath
{
    public class TColorRGBA : TColorRGB
    {
        public TColorRGBA()
        {
            return;
        }

        public TColorRGBA(double aR, double aG, double aB, double aA = 1.0)
            : base (aR, aG, aB)
        {
            A = aA;
            return;
        }

        public double A
        { get; set; } = 1.0;

        public byte Ab
        {
            get { return (byte)Math.Truncate((A * (double)byte.MaxValue) + 0.5); }
            set { A = (double)value / (double)byte.MaxValue; }
        }
    }
}
