using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    public interface IROTriangle2D : IEquatable<IROTriangle2D>
    {
        IROVector2 Vertex0 { get; }
        IROVector2 Vertex1 { get; }
        IROVector2 Vertex2 { get; }

        bool Contains(IROVector2 aPoint);
        bool Contains(double aX, double aY);
        bool IsClockwise { get; }
    }

    public interface ITriangle2D : IROTriangle2D, IEquatable<ITriangle2D>
    {
        new IVector2 Vertex0 { get; }
        new IVector2 Vertex1 { get; }
        new IVector2 Vertex2 { get; }
    }

    public class TTriangle2D : ITriangle2D, IEquatable<TTriangle2D>
    {
        public TTriangle2D()
        {
            return;
        }

        public TTriangle2D(IROVector2 aV0, IROVector2 aV1, IROVector2 aV2)
        {
            Vertex0.Assign(aV0);
            Vertex1.Assign(aV1);
            Vertex2.Assign(aV2);
            return;
        }

        public TTriangle2D(IROTriangle2D aSrc)
            : this(aSrc.Vertex0, aSrc.Vertex1, aSrc.Vertex2)
        {
            return;
        }

        public bool Contains(IROVector2 aPoint)
        {
            return Contains(aPoint.X, aPoint.Y);
        }

        public bool Contains(double aX, double aY)
        {
            TVector2 p = new TVector2(aX, aY);
            TVector2[] v = new TVector2[3] { new TVector2(Vertex0), new TVector2(Vertex1), new TVector2(Vertex2) };
            double sign = Math.Sign(TVector2.CrossProduct(v[1] - v[0], p - v[0]));
            if (sign != Math.Sign(TVector2.CrossProduct(v[2] - v[1], p - v[1])))
                return false;
            if (sign != Math.Sign(TVector2.CrossProduct(v[0] - v[2], p - v[2])))
                return false;

            return true;
        }

        public bool IsClockwise
        {
            get
            {
                TVector2 v1 = new TVector2(Vertex1.X - Vertex0.X, Vertex1.Y - Vertex0.Y);
                TVector2 v2 = new TVector2(Vertex2.X - Vertex0.X, Vertex2.Y - Vertex0.Y);
                v1.Normalize();
                v2.Normalize();

                return TVector2.CrossProduct(v1, v2) < 0.0;
            }
        }

        public bool Equals(IROTriangle2D aOther)
        {
            return (Vertex0 == aOther.Vertex0)
                && (Vertex1 == aOther.Vertex1)
                && (Vertex2 == aOther.Vertex2);
        }

        public bool Equals(ITriangle2D aOther)
        {
            return Equals((IROLineSegment2D)aOther);
        }

        public bool Equals(TTriangle2D aOther)
        {
            return ((object)this).Equals(aOther) || Equals((ITriangle2D)aOther);
        }

        public IVector2 Vertex0
        { get; } = new TVector2();
        public IVector2 Vertex1
        { get; } = new TVector2();
        public IVector2 Vertex2
        { get; } = new TVector2();

        IROVector2 IROTriangle2D.Vertex0 => Vertex0;
        IROVector2 IROTriangle2D.Vertex1 => Vertex1;
        IROVector2 IROTriangle2D.Vertex2 => Vertex2;
    }
}
