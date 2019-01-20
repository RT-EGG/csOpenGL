/***************************************************

referd http://tercel-sakuragaoka.blogspot.com/2011/06/processingdelaunay_3958.html

***************************************************/


// System
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
// OpenTK
using OpenTK.Graphics.OpenGL;
// rtOpenTK
using rtOpenTK;
// rtUtility
using rtUtility.rtMath;

namespace GLDelaunayTriangulation
{
    public partial class TFormMain : Form
    {
        public TFormMain()
        {
            InitializeComponent();
        }

        private void OpenGL_Load(object sender, EventArgs e)
        {
            return;
        }

        private void OpenGL_Paint(object sender, PaintEventArgs e)
        {
            OpenGL.MakeCurrent();

            TGLRenderingStatus status = new TGLRenderingStatus();
            status.Viewport.Rect = OpenGL.ClientRectangle;
            status.Viewport.Adapt(OpenGL);

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.ClearDepth(1.0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushAttrib(AttribMask.AllAttribBits);
            try {
                status.ProjectionMatrix.PushMatrix();
                try {
                    status.ProjectionMatrix.LoadIdentity();
                    status.ProjectionMatrix.Ortho(status.Viewport.Width, status.Viewport.Height, 0.01, 10.0);

                    status.ModelViewMatrix.Model.PushMatrix();
                    try {
                        status.ModelViewMatrix.Model.LoadIdentity();
                        status.ModelViewMatrix.Model.Translate(0.0, 0.0, -1.0);

                        GL.Disable(EnableCap.Lighting);
                        GL.Disable(EnableCap.AlphaTest);
                        GL.Disable(EnableCap.DepthTest);
                        GL.Color3(1.0, 1.0, 1.0);

                        GL.PointSize(3.0f);
                        GL.LineWidth(1.0f);

                        RenderInputPoints();

                        OpenTK.Graphics.OpenGL.GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                        p_Renderer.RenderTriangles(OpenGL, CheckRenderOuterTriangle.Checked);

                    } finally {
                        status.ModelViewMatrix.Model.PopMatrix();
                    }

                } finally {
                    status.ProjectionMatrix.PopMatrix();
                }
            } finally {
                GL.PopAttrib();
            }

            OpenGL.SwapBuffers();

            return;
        }

        private void OpenGL_MouseClick(object sender, MouseEventArgs e)
        {
            TVector2 position = new TVector2();
            position.X = e.X - (OpenGL.Width * 0.5);
            position.Y = ((OpenGL.Height - e.Y) - (OpenGL.Height * 0.5));

            p_InputPoints.Add(position);
            OpenGL.Invalidate();
            return;
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            Clear();
            return;
        }

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            if (p_InputPoints.Count < 3)
                return;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            TDelaunayTriangulation.IResult delaunay = TDelaunayTriangulation.Calculate(p_InputPoints);
            sw.Stop();

            p_Renderer.Setup(delaunay);

            LabelTimeCost.Text = $"Calc time : {sw.ElapsedMilliseconds}ms";
            LabelPointCount.Text = $"Input point count : {p_InputPoints.Count}";
            LabelTriangleCount.Text = $"Triangle count : {delaunay.Triangles.Count}";

            OpenGL.Invalidate();

            return;
        }

        private void CheckRenderOuterTriangle_CheckedChanged(object sender, EventArgs e)
        {
            OpenGL.Invalidate();
            return;
        }

        private void RenderInputPoints()
        {
            float[] vertices = new float[p_InputPoints.Count * 2];
            for (int i  = 0; i < p_InputPoints.Count; ++i) {
                vertices[(i * 2) + 0] = (float)(p_InputPoints[i].X);
                vertices[(i * 2) + 1] = (float)(p_InputPoints[i].Y);
            }

            GL.PushClientAttrib(ClientAttribMask.ClientAllAttribBits);
            try {
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.VertexPointer(2, VertexPointerType.Float, sizeof(float) * 2, vertices);

                GL.DrawArrays(PrimitiveType.Points, 0, vertices.Length / 2);

            } finally {
                GL.PopClientAttrib();
            }

            return;
        }

        private void Clear()
        {
            p_InputPoints.Clear();
            p_Renderer.Setup(null);

            LabelTimeCost.Text = $"Calc time : -ms";
            LabelPointCount.Text = "Input point count : 0";
            LabelTriangleCount.Text = "Triangle count : 0";

            OpenGL.Invalidate();
            return;
        }

        List<IROVector2> p_InputPoints = new List<IROVector2>();
        TDelaunayTriangleRenderer p_Renderer = new TDelaunayTriangleRenderer();
    }
}
