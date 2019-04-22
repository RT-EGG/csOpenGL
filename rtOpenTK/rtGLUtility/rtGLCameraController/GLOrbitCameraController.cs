// System
using System;
using System.Drawing;
using System.Windows.Forms;
// rtUtility
using rtUtility.rtMath;
using rtUtility.Collections;

namespace rtOpenTK.rtGLUtility.rtGLCameraController
{
    public class TGLOrbitCameraController : TGLCameraController
    {
        public delegate void TCoordinateAdjustment(ref double aAzimuthAngleRad, ref double aElevationAngleRad, ref double aRadius);

        public void InitializeDeg(double aAzimuth, double aElevation, double aRadius)
        {
            AzimuthAngleDeg = aAzimuth;
            ElevationAngleDeg = aElevation;
            Radius = aRadius;
            return;
        }

        public void InitializeRad(double aAzimuth, double aElevation, double aRadius)
        {
            AzimuthAngleRad = aAzimuth;
            ElevationAngleRad = aElevation;
            Radius = aRadius;
            return;
        }

        public override void RegisterToControl(Control aControl)
        {
            base.RegisterToControl(aControl);
            aControl.MouseWheel += OnMouseWheel;
            aControl.MouseDown  += OnMouseDown;
            aControl.MouseUp    += OnMouseUp;
            aControl.MouseMove  += OnMouseMove;

            return;
        }

        public override void UnregisterFromControl(Control aControl)
        {
            base.UnregisterFromControl(aControl);
            aControl.MouseWheel -= OnMouseWheel;
            aControl.MouseDown  -= OnMouseDown;
            aControl.MouseUp    -= OnMouseUp;
            aControl.MouseMove  -= OnMouseMove;

            return;
        }

        public override IROVector3 EyePosition
        { get { return p_ViewPosition + (new TVector3(p_Coordinate.Rectangular)); } }
        public override IROVector3 FrontVector
        { get { return ((new TVector3(ViewPosition)) - (new TVector3(EyePosition))).Normalized; } }
        public IROVector3 ViewPosition
        {
            get { return p_ViewPosition; }
            set { p_ViewPosition.Assign(value); }
        }

        public event TCoordinateAdjustment CoordinateAdjustment;

        public double AzimuthAngleRad
        {
            get { return p_Coordinate.AzimuthAngleRad; }
            set { p_Coordinate.AzimuthAngleRad = value; }
        }
        public double AzimuthAngleDeg
        {
            get { return p_Coordinate.AzimuthAngleDeg; }
            set { p_Coordinate.AzimuthAngleDeg = value; }
        }

        public double ElevationAngleRad
        {
            get { return p_Coordinate.ElevationAngleRad; }
            set { p_Coordinate.ElevationAngleRad = value; }
        }
        public double ElevationAngleDeg
        {
            get { return p_Coordinate.ElevationAngleDeg; }
            set { p_Coordinate.ElevationAngleDeg = value; }
        }

        public double Radius
        {
            get { return p_Coordinate.Radius; }
            set { p_Coordinate.Radius = value; }
        }

        public double AzimuthRotationPerPixelInRad
        { get; set; } = 1.0 / 180.0 * Math.PI;
        public double AzimuthRotationPerPixelInDeg
        {
            get { return AzimuthRotationPerPixelInRad.RadToDeg(); }
            set { AzimuthRotationPerPixelInRad = value.DegToRad(); }
        }

        public double ElevationRotationPerPixelInRad
        { get; set; } = 1.0 / 180.0 * Math.PI;
        public double ElevationRotationPerPixelInDeg
        {
            get { return ElevationRotationPerPixelInRad.RadToDeg(); }
            set { ElevationRotationPerPixelInRad = value.DegToRad(); }
        }

        public double TranslatePerPixel
        { get; set; } = 0.01;

        public double TranslatePerWheel
        { get; set; } = 0.1;

        private void MoveXZPlane(Point aTransfer)
        {
            TMatrix44 rot = TMatrix44.MakeRotateMatrixYaw(AzimuthAngleRad);

            TVector3 z = rot * (new TVector3H(0.0, 0.0, -1.0));
            z.Y = 0.0;
            z.Normalize();

            TVector3 x = TVector3.CrossProduct(z, new TVector3(0.0, 1.0, 0.0));

            p_ViewPosition += (x * aTransfer.X * TranslatePerPixel) + (z * -aTransfer.Y * TranslatePerPixel);
            return;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Point transfer = new Point(e.X - p_MousePoint.X, e.Y - p_MousePoint.Y);

            if ((p_MouseButtonDown[MouseButtons.Left] && p_MouseButtonDown[MouseButtons.Right]) || p_MouseButtonDown[MouseButtons.Middle]) {
                MoveXZPlane(transfer);
            } else {
                if (p_MouseButtonDown[MouseButtons.Left]) {
                    AzimuthAngleRad   -= transfer.X * AzimuthRotationPerPixelInRad;
                    ElevationAngleRad += transfer.Y * ElevationRotationPerPixelInRad;
                } else if (p_MouseButtonDown[MouseButtons.Right]) {
                    Radius += transfer.X * TranslatePerPixel;
                    p_ViewPosition.Y += transfer.Y * TranslatePerPixel;
                }
            }

            p_MousePoint.X = e.X;
            p_MousePoint.Y = e.Y;

            DoCoordinateAdjustment();

            return;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            p_MouseButtonDown[e.Button] = false;
            p_MousePoint = e.Location;
            return;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            p_MouseButtonDown[e.Button] = true;
            p_MousePoint = e.Location;
            return;
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            p_Coordinate.Radius -= (e.Delta / SystemInformation.MouseWheelScrollDelta) * TranslatePerWheel;
            DoCoordinateAdjustment();
            return;
        }

        private void DoCoordinateAdjustment()
        {
            double a = p_Coordinate.AzimuthAngleRad;
            double e = p_Coordinate.ElevationAngleRad;
            double r = p_Coordinate.Radius;

            CoordinateAdjustment?.Invoke(ref a, ref e, ref r);

            p_Coordinate.AzimuthAngleRad = a;
            p_Coordinate.ElevationAngleRad = e;
            p_Coordinate.Radius = r;

            return;
        }

        private void SetMouseDown(MouseButtons aButton, bool aDown)
        {
            p_MouseButtonDown[aButton] = aDown;
        }

        private bool GetMouseDown(MouseButtons aButton)
        {
            return p_MouseButtonDown[aButton];
        }

        private TSphericalCoordinate p_Coordinate = new TSphericalCoordinate();
        private TVector3 p_ViewPosition = new TVector3();
        private TEnumArray<MouseButtons, bool> p_MouseButtonDown = new TEnumArray<MouseButtons, bool>();
        private Point p_MousePoint = new Point();
    }
}
