using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using rtOpenTK;
using rtOpenTK.rtGLUtility.rtGLCameraController;
using rtUtility.rtMath;

namespace GLSnowAccumulation
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            udMinHeight.Value = (decimal)p_SnowAccumulationModel.MinHeight;
            udMaxHeight.Value = (decimal)p_SnowAccumulationModel.MaxHeight;

            return;
        }

        ~FormMain()
        {
            p_CameraController.UnregisterFromControl(OpenGL);
            return;
        }

        private void OpenGL_Load(object sender, EventArgs e)
        {
            p_CameraController.RegisterToControl(OpenGL);
            p_CameraController.CoordinateAdjustment += OnCameraControllerCoordinateAdjustment;
            p_CameraController.ElevationAngleDeg = 45.0;
            p_CameraController.Radius = 100.0;
            return;
        }

        private void OpenGL_Paint(object sender, PaintEventArgs e)
        {
            OpenGL.MakeCurrent();

            GL.ClearColor(0.2f, 0.5f, 0.7f, 1.0f);
            GL.ClearDepth(1.0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            TGLRenderingStatus status = new TGLRenderingStatus();

            status.Viewport.Rect = OpenGL.ClientRectangle;
            status.Viewport.Adapt(OpenGL);

            status.ProjectionMatrix.LoadIdentity();
            status.ProjectionMatrix.Perspective((45.0).DegToRad(), OpenGL.Width, OpenGL.Height, 0.01, 1000.0);

            status.ModelViewMatrix.View.LoadIdentity();
            status.ModelViewMatrix.View.LoadMatrix(p_CameraController.ViewMatrix);

            status.ModelViewMatrix.Model.LoadIdentity();
            status.ModelViewMatrix.Model.RotatePitch(-Math.PI * 0.5);

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Lequal);
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            p_SnowAccumulationModel.Render(OpenGL, status);

            OpenGL.SwapBuffers();

            return;
        }

        private void OnCameraControllerCoordinateAdjustment(ref double aAzimuthAngleRad, ref double aElevationAngle, ref double aRadius)
        {
            aElevationAngle = aElevationAngle.Clamp((-75.0).DegToRad(), (75.0).DegToRad());

            OpenGL.Invalidate();
            return;
        }

        private TSnowAccumulationModel p_SnowAccumulationModel = new TSnowAccumulationModel();
        private TGLOrbitCameraController p_CameraController = new TGLOrbitCameraController();

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.F5:
                    p_SnowAccumulationModel.ReloadShader();
                    OpenGL.Invalidate();
                    break;
                case Keys.F4:
                    p_SnowAccumulationModel.RandomizeHeight();
                    OpenGL.Invalidate();
                    break;
            }
            return;
        }

        private void UdMinHeight_ValueChanged(object sender, EventArgs e)
        {
            p_SnowAccumulationModel.MinHeight = (float)udMinHeight.Value;
            OpenGL.Invalidate();
            return;
        }

        private void UdMaxHeight_ValueChanged(object sender, EventArgs e)
        {
            p_SnowAccumulationModel.MaxHeight = (float)udMaxHeight.Value;
            OpenGL.Invalidate();
            return;
        }
    }
}
