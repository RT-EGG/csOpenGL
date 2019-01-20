// System
using System;

namespace rtUtility.rtMath
{
    public interface IROCircle : IEquatable<IROCircle>
    {
        bool Contains(IROVector2 aPoint);
        bool Contains(double aX, double aY);

        IROVector2 Center { get; }
        double Radius { get; }
    }

    public interface ICircle : IROCircle, IEquatable<ICircle>
    {
        new TVector2 Center { get; set; }
        new double Radius { get; set; }
    }

    public class TCircle : ICircle, IEquatable<TCircle>
    {
        public TCircle()
            : this(new TVector2(0.0, 0.0), 1.0)
        {
            return;
        }

        public TCircle(IROVector2 aCenter, double aRadius)
        {
            Center = new TVector2(Center.X, Center.Y);
            Radius = aRadius;
            return;
        }

        public TCircle(IROCircle aSrc)
        {
            Center = new TVector2(aSrc.Center);
            Radius = aSrc.Radius;
            return;
        }

        public static TCircle CalcCircumscribed(IROVector2 aPoint0, IROVector2 aPoint1, IROVector2 aPoint2)
        {
            TCircle result = new TCircle();

            double c = 2.0 * (((aPoint1.X - aPoint0.X) * (aPoint2.Y - aPoint0.Y)) - ((aPoint1.Y - aPoint0.Y) * (aPoint2.X - aPoint0.X)));
            if (!c.IsZero()) {
                TVector2 center = new TVector2();
                center.X += (aPoint2.Y - aPoint0.Y) * (Math.Pow(aPoint1.X, 2.0) - Math.Pow(aPoint0.X, 2.0) + Math.Pow(aPoint1.Y, 2.0) - Math.Pow(aPoint0.Y, 2.0));
                center.X += (aPoint0.Y - aPoint1.Y) * (Math.Pow(aPoint2.X, 2.0) - Math.Pow(aPoint0.X, 2.0) + Math.Pow(aPoint2.Y, 2.0) - Math.Pow(aPoint0.Y, 2.0));
                center.X /= c;
                center.Y += (aPoint0.X - aPoint2.X) * (Math.Pow(aPoint1.X, 2.0) - Math.Pow(aPoint0.X, 2.0) + Math.Pow(aPoint1.Y, 2.0) - Math.Pow(aPoint0.Y, 2.0));
                center.Y += (aPoint1.X - aPoint0.X) * (Math.Pow(aPoint2.X, 2.0) - Math.Pow(aPoint0.X, 2.0) + Math.Pow(aPoint2.Y, 2.0) - Math.Pow(aPoint0.Y, 2.0));
                center.Y /= c;

                result.Center = center;
                result.Radius = (new TVector2(aPoint0.X - center.X, aPoint0.Y - center.Y)).Length;
            }

            return result;
        }

        public bool Contains(IROVector2 aPoint)
        {
            return Contains(aPoint.X, aPoint.Y);
        }

        public bool Contains(double aX, double aY)
        {
            return (((new TVector2(aX, aY)) - Center).Length2 - Math.Pow(Radius, 2.0)) <= 1.0e-10;
        }

        public TVector2 Center
        { get; set; } = new TVector2(0.0f, 0.0f);
        public double Radius
        { get; set; } = 1.0;

        IROVector2 IROCircle.Center => Center;
        double IROCircle.Radius => Radius;

        public bool Equals(IROCircle aOther)
        {
            return (Center == aOther.Center) && (Radius == aOther.Radius);
        }

        public bool Equals(ICircle aOther)
        {
            return Equals((IROCircle)aOther);
        }

        public bool Equals(TCircle aOther)
        {
            return ((object)this).Equals(aOther) || Equals((ICircle)aOther);
        }
    }
}
