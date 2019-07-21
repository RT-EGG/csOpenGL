using System;

namespace rtUtility.rtMath
{
    public interface IROColorHSV : IEquatable<IROColorHSV>
    {
        double H { get; }
        double S { get; }
        double V { get; }
        TColorRGB ToRGB();
    }

    public interface IColorHSV : IROColorHSV
    {
        void Set(double aH, double aS, double aV);
        new double H { get; set; }
        new double S { get; set; }
        new double V { get; set; }
    }

    public class TColorHSV : IColorHSV
    {
        public TColorHSV()
            : this(1.0, 1.0, 1.0)
        {
            return;
        }

        public TColorHSV(IROColorHSV aSrc)
            : this(aSrc.H, aSrc.S, aSrc.V)
        {
            return;
        }

        public TColorHSV(double aH, double aS, double aV)
        {
            H = aH;
            S = aS;
            V = aV;
            return;
        }

        public void Set(double aH, double aS, double aV)
        {
            H = aH;
            S = aS;
            V = aV;
            return;
        }

        public TColorRGB ToRGB()
        {
            if (S.IsZero()) {
                return new TColorRGB(V, V, V);
            } else {
                float h = (float)H;
                float s = (float)S;
                float v = (float)V;
                float hueF = h * 6.0f;
                int   hueI = (int)System.Math.Truncate(hueF);
                float fr = hueF - hueI;
                float m = v * (1.0f - s);
                float n = v * (1.0f - (s * fr));
                float p = v * (1.0f - (s * (1.0f - fr)));

                switch (hueI % 6) {
                    case 0: return new TColorRGB(v, p, m);
                    case 1: return new TColorRGB(n, v, m);
                    case 2: return new TColorRGB(m, v, p);
                    case 3: return new TColorRGB(m, n, v);
                    case 4: return new TColorRGB(p, m, v); 
                    case 5: return new TColorRGB(v, m, n);
                }
            }
            return new TColorRGB(0.0, 0.0, 0.0);
        }

        public double H
        {
            set { p_H = value; }
            get { return p_H.Clamp(0.0, 1.0); }
        }

        public double S
        {
            set { p_S = value; }
            get { return p_S.Clamp(0.0, 1.0); }
        }

        public double V
        {
            set { p_V = value; }
            get { return p_V.Clamp(0.0, 1.0); }
        }

        public bool Equals(IROColorHSV aOther)
        {
            return H.AlmostEqual(aOther.H)
                && S.AlmostEqual(aOther.S)
                && V.AlmostEqual(aOther.V);
        }

        private double p_H;
        private double p_S;
        private double p_V;
    }
}
