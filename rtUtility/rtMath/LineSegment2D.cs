using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    public interface IROLineSegment2D : IEquatable<IROLineSegment2D>
    {
        IROVector2 Point0 { get; }
        IROVector2 Point1 { get; }

        bool IsIntersectWith(IROLineSegment2D aOther);
        bool IsIntersectWith(IROLineSegment2D aOther, ref double aParam, ref double aOtherParam);
    }

    public interface ILineSegment2D : IROLineSegment2D, IEquatable<ILineSegment2D>
    {
        new IVector2 Point0 { get; }
        new IVector2 Point1 { get; }
    }

    public class TLineSegment2D : ILineSegment2D, IEquatable<TLineSegment2D>
    {
        public TLineSegment2D()
        {
            return;
        }

        public TLineSegment2D(IROVector2 aPoint0, IROVector2 aPoint1)
        {
            Point0.Assign(aPoint0);
            Point1.Assign(aPoint1);
            return;
        }

        public TLineSegment2D(IROLineSegment2D aSrc)
            : this(aSrc.Point0, aSrc.Point1)
        {
            return;
        }

        public IVector2 Point0
        { get; } = new TVector2();
        public IVector2 Point1
        { get; } = new TVector2();

        public bool IsIntersectWith(IROLineSegment2D aOther)
        {
            double p0 = 0.0;
            double p1 = 0.0;
            return IsIntersectWith(aOther, ref p0, ref p1);
        }

        public bool IsIntersectWith(IROLineSegment2D aOther, ref double aParam, ref double aOtherParam)
        {
            TVector2 v = (new TVector2(aOther.Point0)) - (new TVector2(Point0));
            TVector2 v1 = (new TVector2(Point1)) - (new TVector2(Point0));
            TVector2 v2 = (new TVector2(aOther.Point1)) - (new TVector2(aOther.Point0));

            if (TVector2.CrossProduct(v1, v2).IsZero()) {
                aParam = double.NaN;
                aOtherParam = double.NaN;
                return false;
            }
            aParam      = TVector2.CrossProduct(v, v2) / TVector2.CrossProduct(v1, v2);
            aOtherParam = TVector2.CrossProduct(v, v1) / TVector2.CrossProduct(v1, v2);

            return aParam.InRange(0.0, 1.0) && aOtherParam.InRange(0.0, 1.0);
        }

        public bool Equals(IROLineSegment2D aOther)
        {
            return (Point0 == aOther.Point0) && (Point1 == aOther.Point1);
        }

        public bool Equals(ILineSegment2D aOther)
        {
            return Equals((IROLineSegment2D)aOther);
        }

        public bool Equals(TLineSegment2D aOther)
        {
            return ((object)this).Equals(aOther) || Equals((ILineSegment2D)aOther);
        }

        IROVector2 IROLineSegment2D.Point0 => Point0;
        IROVector2 IROLineSegment2D.Point1 => Point1;
    }
}
