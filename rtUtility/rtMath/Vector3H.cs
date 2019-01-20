using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    // Homogenerous Vector
    public interface IROVector3H : IROVector3, IEquatable<IROVector3H>
    {
        double W { get; }
    }

    public interface IVector3H : IVector3, IROVector3H, IEquatable<IVector3H>
    {
        new double W { get; set; }
    }

    public class TVector3H : TVector3, IVector3H, IEquatable<TVector3H>
    {
        public TVector3H()
            : this(0.0, 0.0, 0.0)
        {
            return;
        }

        public TVector3H(IVector3H aSrc)
            : this(aSrc.X, aSrc.Y, aSrc.Z, aSrc.W)
        {
            return;
        }

        public TVector3H(double aX, double aY, double aZ, double aW = 1.0)
            : base(aX, aY, aZ)
        {
            W = aW;
            return;
        }

        public TVector3H(IVector3 aSrc, double aW = 1.0)
            : this(aSrc.X, aSrc.Y, aSrc.Z, aW)
        {
            return;
        }

        public void Set(double aX, double aY, double aZ, double aW = 1.0)
        {
            base.Set(aX, aY, aZ);
            W = aW;
            return;
        }

        public double W
        { get; set; } = 1.0;

        public bool Equals(IROVector3H aOther)
        {
            return (X.AlmostEqual(aOther.X) && Y.AlmostEqual(aOther.Y) && Z.AlmostEqual(aOther.Z) && W.AlmostEqual(aOther.W));
        }

        public bool Equals(IVector3H aOther)
        {
            return Equals((IROVector3H)aOther);
        }

        public bool Equals(TVector3H aOther)
        {
            return ((object)this).Equals(aOther) || Equals((IVector3H)aOther);
        }
    }
}
