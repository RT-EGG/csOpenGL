using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    // Homogenerous Vector
    public interface IROVector2H : IROVector2, IEquatable<IROVector2H>
    {
        double H { get; }
    }

    public interface IVector2H : IROVector2H, IVector2, IEquatable<IVector2H>
    {
        new double H { get; set; }
    }

    public class TVector2H : TVector2, IVector2H, IEquatable<TVector2H>
    {
        public TVector2H()
            : this(0.0, 0.0)
        {
            return;
        }

        public TVector2H(IROVector2H aSrc)
            : this(aSrc.X, aSrc.Y, aSrc.H)
        {
            return;
        }

        public TVector2H(double aX, double aY, double aH = 1.0)
            : base(aX, aY)
        {
            H = aH;
            return;
        }

        public TVector2H(IVector2 aSrc, double aH = 1.0)
            : this(aSrc.X, aSrc.Y, aH)
        {
            return;
        }

        public void Set(double aX, double aY, double aH = 1.0)
        {
            base.Set(aX, aY);
            H = aH;
            return;
        }

        public double H
        { get; set; } = 1.0;

        public bool Equals(IROVector2H aOther)
        {
            return (X.AlmostEqual(aOther.X) && Y.AlmostEqual(aOther.Y) && H.AlmostEqual(aOther.H));
        }

        public bool Equals(IVector2H aOther)
        {
            return Equals((IROVector2H)aOther);
        }

        public bool Equals(TVector2H aOther)
        {
            return ((object)this).Equals(aOther) || Equals((IVector2H)aOther);
        }
    }
}
