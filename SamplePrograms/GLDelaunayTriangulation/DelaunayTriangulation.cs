// System
using System;
using System.Collections.Generic;
// rtUtility
using rtUtility;
using rtUtility.rtMath;

namespace GLDelaunayTriangulation
{
    public class TDelaunayTriangulation
    {
        public interface IResult
        {
            IReadOnlyList<IROVector2> Points { get; }
            IReadOnlyList<TDelaunayVertex> Vertices { get; }
            IReadOnlyList<TDelaunayTriangle> Triangles { get; }
            IReadOnlyList<TDelaunayTriangle> OuterTriangles { get; }
        }

        public static IResult Calculate(IEnumerable<IROVector2> aPoints)
        {
            TResult result = new TResult();

            List<IROVector2> points = result.Points;
            List<TDelaunayVertex> vertices = result.Vertices;
            List<TDelaunayTriangle> triangles = result.Triangles;

            points.AddRange(aPoints);
            for (int i = 0; i < points.Count; ++i)
                vertices.Add(new TDelaunayVertex(points, i));

            List<TDelaunayOuterVertex> outerVertices = new List<TDelaunayOuterVertex>();
            // calc outer triangle (first triangle)
            {
                TAABB2D aabb = new TAABB2D();

                foreach (TAxis2D axis in Enum.GetValues(typeof(TAxis2D))) {
                    aabb.SetBoundary(axis, TMinMax.Min, double.MaxValue);
                    aabb.SetBoundary(axis, TMinMax.Max, double.MinValue);
                }
                foreach (IVector2 point in points) {
                    aabb.SetBoundary(TAxis2D.X, TMinMax.Min, Math.Min(aabb.GetBoundary(TAxis2D.X, TMinMax.Min), point.X));
                    aabb.SetBoundary(TAxis2D.X, TMinMax.Max, Math.Max(aabb.GetBoundary(TAxis2D.X, TMinMax.Max), point.X));
                    aabb.SetBoundary(TAxis2D.Y, TMinMax.Min, Math.Min(aabb.GetBoundary(TAxis2D.Y, TMinMax.Min), point.Y));
                    aabb.SetBoundary(TAxis2D.Y, TMinMax.Max, Math.Max(aabb.GetBoundary(TAxis2D.Y, TMinMax.Max), point.Y));
                }

                TVector2 center = new TVector2(aabb.Center);
                double radius = aabb.OuterRadius;

                const double con_Scale = 10.0;
                outerVertices.Add(new TDelaunayOuterVertex(new TVector2(center.X - (Math.Sqrt(3.0) * radius * con_Scale), center.Y - radius * con_Scale)));
                outerVertices.Add(new TDelaunayOuterVertex(new TVector2(center.X + (Math.Sqrt(3.0) * radius * con_Scale), center.Y - radius * con_Scale)));
                outerVertices.Add(new TDelaunayOuterVertex(new TVector2(center.X, center.Y + (2.0 * radius * con_Scale))));
                triangles.Add(new TDelaunayTriangle(outerVertices[2], outerVertices[1], outerVertices[0]));
            }

            // main
            {
                List<TDelaunayTriangle> hitTriangles = new List<TDelaunayTriangle>();
                List<TDelaunayTriangle> newTriangleCandidate = new List<TDelaunayTriangle>();
                List<TDelaunayTriangle> duplicateTriangles = new List<TDelaunayTriangle>();
                List<TDelaunayTriangle> newTriangles = new List<TDelaunayTriangle>();

                 foreach (var vertex in vertices) {
                    // find triangle that's circumscribed circle contains the point
                    foreach (var triangle in triangles) {
                        if (triangle.CircumscribedCircle.Contains(vertex.Position)) {
                            hitTriangles.Add(triangle);
                        }
                    }

                    foreach (var triangle in hitTriangles) { 
                        newTriangleCandidate.Add(new TDelaunayTriangle(vertex, triangle.Vertices[0], triangle.Vertices[1]));
                        newTriangleCandidate.Add(new TDelaunayTriangle(vertex, triangle.Vertices[1], triangle.Vertices[2]));
                        newTriangleCandidate.Add(new TDelaunayTriangle(vertex, triangle.Vertices[2], triangle.Vertices[0]));
                        triangles.Remove(triangle);
                        triangle.Dispose();
                    }
                    hitTriangles.Clear();

                    for (int t0 = 0; t0 < newTriangleCandidate.Count; ++t0) {
                        bool found = false;
                        for (int t1 = 0; t1 < newTriangleCandidate.Count; ++t1) {
                            if (t0 == t1)
                                continue;
                            if (newTriangleCandidate[t0].HasSameVertices(newTriangleCandidate[t1])) {
                                found = true;
                                break;
                            }
                        }

                        if (found)
                            duplicateTriangles.Add(newTriangleCandidate[t0]);
                        else
                            newTriangles.Add(newTriangleCandidate[t0]);
                    }
                    triangles.AddRange(newTriangles);
                    newTriangleCandidate.Clear();
                    newTriangles.Clear();

                    foreach (var t in duplicateTriangles)
                        t.Dispose();
                    duplicateTriangles.Clear();
                }
            }

            // remove outer triangle
            List<TDelaunayTriangle> outerTriangle = result.OuterTriangles;
            foreach (TDelaunayVertex vertex in outerVertices) {
                foreach (TDelaunayTriangle triangle in vertex.ReferenceTriangles) {
                    if (!outerTriangle.Contains(triangle))
                        outerTriangle.Add(triangle);
                    triangles.Remove(triangle);
                }

                vertex.Index = points.Count;
                points.Add(new TVector2(vertex.Position));
                vertices.Add(vertex);
            }

            return result;
        }

        public class TResult : IResult
        {
            public List<IROVector2> Points = new List<IROVector2>();
            public List<TDelaunayVertex> Vertices = new List<TDelaunayVertex>();
            public List<TDelaunayTriangle> Triangles = new List<TDelaunayTriangle>();
            public List<TDelaunayTriangle> OuterTriangles = new List<TDelaunayTriangle>();

            IReadOnlyList<IROVector2> IResult.Points => Points;
            IReadOnlyList<TDelaunayVertex> IResult.Vertices => Vertices;
            IReadOnlyList<TDelaunayTriangle> IResult.Triangles => Triangles;
            IReadOnlyList<TDelaunayTriangle> IResult.OuterTriangles => OuterTriangles;
        }
    }
}
