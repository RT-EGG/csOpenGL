// System
using System;
using System.Collections.Generic;

namespace GLDelaunayTriangulation
{
    public class TDelaunayEdge : IEquatable<TDelaunayEdge>
    {
        public TDelaunayEdge(TDelaunayVertex aVertex0, TDelaunayVertex aVertex1)
        {
            Vertex0 = aVertex0;
            Vertex1 = aVertex1;

            Vertex0.ReferenceEdges.Add(this);
            Vertex1.ReferenceEdges.Add(this);

            return;
        }

        ~TDelaunayEdge()
        {
            Vertex0.ReferenceEdges.Remove(this);
            Vertex1.ReferenceEdges.Remove(this);
            return;
        }

        public TDelaunayVertex Vertex0
        { get; private set; }
        public TDelaunayVertex Vertex1
        { get; private set; }
        public IList<TDelaunayTriangle> ReferenceTriangles
        { get; } = new List<TDelaunayTriangle>();

        public bool Equals(TDelaunayEdge aOther)
        {
            return ((Vertex0 == aOther.Vertex0) && (Vertex1 == aOther.Vertex1))
                || ((Vertex0 == aOther.Vertex1) && (Vertex1 == aOther.Vertex0));
        }
    }
}
