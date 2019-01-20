// System
using System;
using System.Collections.Generic;
// rtUtility
using rtUtility.rtMath;

namespace GLDelaunayTriangulation
{
    public class TDelaunayVertex : IEquatable<TDelaunayVertex>
    {
        public TDelaunayVertex(IReadOnlyList<IROVector2> aVertexPositions, int aIndex)
        {
            p_Parent = aVertexPositions;
            Index = aIndex;
            return;
        }

        public TDelaunayEdge FindOrGenerateEdgeWith(TDelaunayVertex aVertex)
        {
            TDelaunayEdge result = null;
            foreach (TDelaunayEdge edge in ReferenceEdges) {
                if (edge.Vertex0 == this) {
                    if (edge.Vertex1 == aVertex)
                        return edge;
                } else if (edge.Vertex1 == this) {
                    if (edge.Vertex0 == aVertex)
                        return edge;
                } else {
                    throw new Exception("TDelaunayVertex::FindOrGenerateEdgeWith >> The edge does not have valid vertex.");
                }
            }

            if (result != null)
                return result;

            return new TDelaunayEdge(this, aVertex);
        }

        public bool Equals(TDelaunayVertex aOther)
        {
            return DoEquals(aOther);
        }

        public virtual IROVector2 Position
        { get { return p_Parent[Index]; } }

        public int Index
        { get; set; } = -1;

        public IList<TDelaunayEdge> ReferenceEdges
        { get; } = new List<TDelaunayEdge>();

        public IList<TDelaunayTriangle> ReferenceTriangles
        { get; } = new List<TDelaunayTriangle>();

        protected virtual bool DoEquals(TDelaunayVertex aOther)
        {
            return (Index == aOther.Index);
        }

        private IReadOnlyList<IROVector2> p_Parent = null;
    }

    public class TDelaunayOuterVertex : TDelaunayVertex
    {
        public TDelaunayOuterVertex(IROVector2 aPosition)
            : base(null, -1)
        {
            p_Position.Assign(aPosition);
            return;
        }

        protected override bool DoEquals(TDelaunayVertex aOther)
        {
            if (aOther is TDelaunayOuterVertex)
                return p_Position == (new TVector2(aOther.Position));
            else
                return false;
        }

        public override IROVector2 Position { get { return p_Position; } }
        private TVector2 p_Position = new TVector2();
    }
}
