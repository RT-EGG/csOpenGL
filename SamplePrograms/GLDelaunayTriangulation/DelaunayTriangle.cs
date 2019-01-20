// System
using System;
using System.Collections.Generic;
// TKUtility
using rtUtility;
using rtUtility.rtMath;

namespace GLDelaunayTriangulation
{
    public class TDelaunayTriangle : TDisposableObject, IEquatable<TDelaunayTriangle>
    {
        public TDelaunayTriangle(TDelaunayVertex aVertex0, TDelaunayVertex aVertex1, TDelaunayVertex aVertex2)
        {
            p_Vertices.Add(aVertex0);
            p_Vertices.Add(aVertex1);
            p_Vertices.Add(aVertex2);
            p_Edges.Add(p_Vertices[0].FindOrGenerateEdgeWith(p_Vertices[1]));
            p_Edges.Add(p_Vertices[1].FindOrGenerateEdgeWith(p_Vertices[2]));
            p_Edges.Add(p_Vertices[2].FindOrGenerateEdgeWith(p_Vertices[0]));
            for (int i = 0; i < 3; ++i) {
                p_Edges[i].ReferenceTriangles.Add(this);
                p_Vertices[i].ReferenceTriangles.Add(this);
            }
            return;
        }

        protected override void Dispose(bool aDisposing)
        {
            base.Dispose(aDisposing);
            if (aDisposing) {
                for (int i = 0; i < 3; ++i) {
                    p_Edges[i].ReferenceTriangles.Remove(this);
                    p_Vertices[i].ReferenceTriangles.Remove(this);
                }
            }
            return;
        }

        public TCircle CircumscribedCircle 
        { get { return TCircle.CalcCircumscribed(p_Vertices[0].Position, p_Vertices[1].Position, p_Vertices[2].Position); } }

        public bool Contains(IROVector2 aPosition)
        {
            TVector2[] v = new TVector2[3] { new TVector2(Vertices[0].Position), new TVector2(Vertices[1].Position), new TVector2(Vertices[2].Position) };
            double sign = Math.Sign(TVector2.CrossProduct(v[1] - v[0], (new TVector2(aPosition)) - v[0]));
            if (sign != Math.Sign(TVector2.CrossProduct(v[2] - v[1], (new TVector2(aPosition)) - v[1])))
                return false;
            if (sign != Math.Sign(TVector2.CrossProduct(v[0] - v[2], (new TVector2(aPosition)) - v[2])))
                return false;

            return true;
        }

        public bool HasSameVertices(TDelaunayTriangle aOther)
        {
            for (int i = 0; i < 3; ++i) {
                for (int j = 0; j < 3; ++j) {
                    if (Vertices[i] == aOther.Vertices[j]) {
                        return (((Vertices[(i + 1) % 3] == aOther.Vertices[(j + 1) % 3]) && (Vertices[(i + 2) % 3] == aOther.Vertices[(j + 2) % 3])) ||
                                ((Vertices[(i + 1) % 3] == aOther.Vertices[(j + 2) % 3]) && (Vertices[(i + 2) % 3] == aOther.Vertices[(j + 1) % 3])));
                    }
                }
            }
            return false;
        }

        public IReadOnlyList<TDelaunayVertex> Vertices
        { get { return p_Vertices; } }
        public IReadOnlyList<TDelaunayEdge> Edges
        { get { return p_Edges; } }

        public bool IsClockwise
        {
            get
            {
                TVector2 v1 = new TVector2(p_Vertices[1].Position.X - p_Vertices[0].Position.X, p_Vertices[1].Position.Y - p_Vertices[0].Position.Y);
                TVector2 v2 = new TVector2(p_Vertices[2].Position.X - p_Vertices[0].Position.X, p_Vertices[2].Position.Y - p_Vertices[0].Position.Y);
                v1.Normalize();
                v2.Normalize();

                return TVector2.CrossProduct(v1, v2) < 0.0;
            }
        }

        public void MakeBeClockwise()
        {
            if (!IsClockwise) {
                TDelaunayVertex tmp = p_Vertices[2];
                p_Vertices[2] = p_Vertices[1];
                p_Vertices[1] = tmp;
            }
            return;
        }

        public void MakeBeUntiClockwise()
        {
            if (IsClockwise) {
                TDelaunayVertex tmp = p_Vertices[2];
                p_Vertices[2] = p_Vertices[1];
                p_Vertices[1] = tmp;
            }
            return;
        }

        public TDelaunayVertex GetVertexUncontainedByEdge(TDelaunayEdge aEdge)
        {
            for (int i = 0; i < 3; ++i) {
                if (p_Vertices[i] == aEdge.Vertex0) {
                    if (p_Vertices[(i + 1) % 3] == aEdge.Vertex1)
                        return p_Vertices[(i + 2) % 3];
                    else if (p_Vertices[(i + 2) % 3] == aEdge.Vertex1)
                        return p_Vertices[(i + 1) % 3];
                    else
                        throw new Exception("TTriangle::GetVertexUncontainedByEdge >> Searched vertex by invalid edge.");
                } else if (p_Vertices[i] == aEdge.Vertex1) {
                    if (p_Vertices[(i + 1) % 3] == aEdge.Vertex0)
                        return p_Vertices[(i + 2) % 3];
                    else if (p_Vertices[(i + 2) % 3] == aEdge.Vertex0)
                        return p_Vertices[(i + 1) % 3];
                    else
                        throw new Exception("TTriangle::GetVertexUncontainedByEdge >> Searched vertex by invalid edge.");
                }
            }

            throw new Exception();
        }

        public bool Equals(TDelaunayVertex aVertex0, TDelaunayVertex aVertex1, TDelaunayVertex aVertex2)
        {
            TDelaunayVertex[] aVertex = new TDelaunayVertex[3] { aVertex0, aVertex1, aVertex2 };
            for (int i = 0; i < 3; ++i) {
                for (int j = 0; j < 3; ++j) {
                    if (p_Vertices[i] == aVertex[j]) {
                        return (((p_Vertices[(i + 1) % 3] == aVertex[(j + 1) % 3]) && (p_Vertices[(i + 2) % 3] == aVertex[(j + 2) % 3])) ||
                                ((p_Vertices[(i + 1) % 3] == aVertex[(j + 2) % 3]) && (p_Vertices[(i + 2) % 3] == aVertex[(j + 1) % 3])));
                    }
                }
            }
            return false;
        }

        public bool Equals(TDelaunayTriangle aOther)
        {
            return HasSameVertices(aOther);
        }

        private List<TDelaunayVertex> p_Vertices = new List<TDelaunayVertex>();
        private List<TDelaunayEdge> p_Edges = new List<TDelaunayEdge>();
    }
}
