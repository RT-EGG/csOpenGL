using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtUtility.rtMath
{
    public interface IROSphericalCoordinate : IEquatable<IROSphericalCoordinate>
    {
        double AzimuthAngleRad { get; }
        double AzimuthAngleDeg { get; }
        double ElevationAngleRad { get; }
        double ElevationAngleDeg { get; }
        double Radius { get; }
        IROVector3 Rectangular { get; }
    }

    public interface ISphericalCoordinate : IROSphericalCoordinate, IEquatable<ISphericalCoordinate>
    {
        new double AzimuthAngleRad { get; set; }
        new double AzimuthAngleDeg { get; set; }
        new double ElevationAngleRad { get; set; }
        new double ElevationAngleDeg { get; set; }
        new double Radius { get; set; }
    }

    public class TSphericalCoordinate : ISphericalCoordinate, IEquatable<TSphericalCoordinate>
    {
        public double AzimuthAngleRad
        { get; set; } = 0.0;
        public double AzimuthAngleDeg
        {
            get { return AzimuthAngleRad.RadToDeg(); }
            set { AzimuthAngleRad = value.DegToRad(); }
        }

        public double ElevationAngleRad
        { get; set; } = 0.0;
        public double ElevationAngleDeg
        {
            get { return ElevationAngleRad.RadToDeg(); }
            set { ElevationAngleRad = value.DegToRad(); }
        }
        
        public double Radius
        { get; set; } = 1.0;

        public IROVector3 Rectangular
        {
            get
            {
                return TMatrix44.MakeRotateMatrixPitch(-ElevationAngleRad)
                     * TMatrix44.MakeRotateMatrixYaw(AzimuthAngleRad)
                     * (new TVector3H(0.0, 0.0, Radius, 1.0));
            }
        }

        public bool Equals(IROSphericalCoordinate aOther)
        {            
            return AzimuthAngleRad.AlmostEqual(aOther.AzimuthAngleRad)
                && ElevationAngleRad.AlmostEqual(aOther.ElevationAngleRad)
                && Radius.AlmostEqual(aOther.Radius);
        }

        public bool Equals(ISphericalCoordinate aOther)
        {
            return Equals((IROSphericalCoordinate)aOther);
        }

        public bool Equals(TSphericalCoordinate aOther)
        {
            return ((object)this).Equals(aOther) || Equals((ISphericalCoordinate)aOther);
        }
    }
}
