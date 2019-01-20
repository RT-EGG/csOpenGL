// System
using System.Diagnostics;
// OpenTK
using OpenTK.Graphics.OpenGL;
// rtOpenTK
using rtOpenTK;
using rtOpenTK.rtGLResourceObject;

namespace GLDelaunayTriangulation
{
    public class TDelaunayTriangleRenderer
    {
        public void Setup(TDelaunayTriangulation.IResult aDelaunay)
        {
            TrtGLControl.EnqueueGLTask(InternalSetup, aDelaunay);
            return;
        }

        public void RenderTriangles(TrtGLControl aGL, bool aRenderOuter = false)
        {
            if ((p_VertexCount == 0) || (p_IndexCount == 0))
                return;

            GL.PushClientAttrib(ClientAttribMask.ClientAllAttribBits);
            try {
                GL.EnableClientState(ArrayCap.VertexArray);
                p_Vertices.Bind(aGL, BufferTarget.ArrayBuffer);
                GL.VertexPointer(2, VertexPointerType.Float, sizeof(float) * 2, 0);

                p_Indices.Bind(aGL, BufferTarget.ElementArrayBuffer);
                GL.DrawElements(PrimitiveType.Triangles, p_IndexCount, DrawElementsType.UnsignedInt, 0);

                if (aRenderOuter) {
                    p_Indices.Bind(aGL, BufferTarget.ElementArrayBuffer, 1);
                    GL.DrawElements(PrimitiveType.Triangles, p_OuterIndexCount, DrawElementsType.UnsignedInt, 0);
                }

                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            } finally {
                GL.PopClientAttrib();
            }

            return;
        }

        private void InternalSetup(TrtGLControl aGL, object aSrc)
        {
            if (aSrc == null) {
                p_VertexCount = 0;
                p_IndexCount  = 0;
                p_OuterIndexCount = 0;
                return;
            }

            Debug.Assert(aSrc is TDelaunayTriangulation.IResult);
            TDelaunayTriangulation.IResult src = aSrc as TDelaunayTriangulation.IResult;

            float[] vertices = new float[src.Vertices.Count * 2];
            for (int i = 0; i < src.Vertices.Count; ++i) {
                vertices[(i * 2) + 0] = (float)(src.Vertices[i].Position.X);
                vertices[(i * 2) + 1] = (float)(src.Vertices[i].Position.Y);
            }
            p_Vertices.Bind(aGL, BufferTarget.ArrayBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            uint[] indices = new uint[src.Triangles.Count * 3];
            for (int i = 0; i < src.Triangles.Count; ++i) {
                src.Triangles[i].MakeBeUntiClockwise();
                for (int j = 0; j < 3; ++j)
                    indices[(i * 3) + j] = (uint)(src.Triangles[i].Vertices[j].Index);
            }
            p_Indices.Bind(aGL, BufferTarget.ElementArrayBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            indices = new uint[src.OuterTriangles.Count * 3];
            for (int i = 0; i < src.OuterTriangles.Count - 1; ++i) {
                src.OuterTriangles[i].MakeBeUntiClockwise();

                for (int j = 0; j < 3; ++j)
                    indices[(i * 3) + j] = (uint)(src.OuterTriangles[i].Vertices[j].Index);
            }
            p_Indices.Bind(aGL, BufferTarget.ElementArrayBuffer, 1);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            p_VertexCount = src.Vertices.Count * 2;
            p_IndexCount  = src.Triangles.Count * 3;
            p_OuterIndexCount = src.OuterTriangles.Count * 3;

            return;
        }

        private TGLBufferObject p_Vertices = new TGLBufferObject(1);
        private TGLBufferObject p_Indices = new TGLBufferObject(2);
        private int p_VertexCount = 0;
        private int p_IndexCount = 0;
        private int p_OuterIndexCount = 0;
    }
}
