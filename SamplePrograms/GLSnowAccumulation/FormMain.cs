// System
using System;
using System.Windows.Forms;
using System.Diagnostics;
// OpenTK
using OpenTK.Graphics.OpenGL;
// rtOpenTK
using rtOpenTK;
using rtOpenTK.rtGLUtility.rtGLCameraController;
// rtUtility
using rtUtility.rtMath;

namespace GLSnowAccumulation
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            p_SnowAccumulationModel.HeightMap = p_HeightMap;
            p_SnowfallParticles.HeightMap = p_HeightMap;

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

            float timestep = p_SimulationTimer.ElapsedMilliseconds * 0.001f;
            p_SimulationTimer.Restart();

            p_SnowfallParticles.Timestep(OpenGL, timestep * (float)UpDownTimestepScale.Value);

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

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Lequal);
            if (p_LineMesh)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            else
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            p_SnowAccumulationModel.Render(OpenGL, status);
            p_SnowfallParticles.Render(OpenGL, status);

            OpenGL.SwapBuffers();

            return;
        }

        private void OnCameraControllerCoordinateAdjustment(ref double aAzimuthAngleRad, ref double aElevationAngle, ref double aRadius)
        {
            aElevationAngle = aElevationAngle.Clamp((-75.0).DegToRad(), (75.0).DegToRad());

            return;
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.F5:
                    p_SnowAccumulationModel.ReloadShader();
                    break;
                case Keys.F4:
                    TrtGLControl.EnqueueGLTask(RandomizeHeight, null);
                    break;
                case Keys.F3:
                    p_LineMesh = !p_LineMesh;
                    break;
            }
            return;
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            OpenGL.Invalidate();
            return;
        }

        private void RandomizeHeight(TrtGLControl aGL, object aObject)
        {
            p_HeightMap.Randomize(aGL);
            return;
        }

        private THeightMap p_HeightMap = new THeightMap(512, 512);
        private TSnowAccumulationModel p_SnowAccumulationModel = new TSnowAccumulationModel();
        private TSnowfallParticles p_SnowfallParticles = new TSnowfallParticles();
        private TGLOrbitCameraController p_CameraController = new TGLOrbitCameraController();
        private bool p_LineMesh = false;

        private Stopwatch p_SimulationTimer = new Stopwatch();

        private void UpDownTimestepScale_ValueChanged(object sender, EventArgs e)
        {
            TrackbarTimestepScale.ValueChanged -= TrackbarTimestepScale_ValueChanged;
            try {
                TrackbarTimestepScale.Value = (int)Math.Truncate(((double)UpDownTimestepScale.Value * 10.0) + 0.5);

            } finally {
                TrackbarTimestepScale.ValueChanged += TrackbarTimestepScale_ValueChanged;
            }
            return;
        }

        private void TrackbarTimestepScale_ValueChanged(object sender, EventArgs e)
        {
            UpDownTimestepScale.ValueChanged -= UpDownTimestepScale_ValueChanged;
            try {
                UpDownTimestepScale.Value = (decimal)TrackbarTimestepScale.Value * 0.1m;

            } finally {
                UpDownTimestepScale.ValueChanged += UpDownTimestepScale_ValueChanged;
            }
            return;
        }
    }
}
