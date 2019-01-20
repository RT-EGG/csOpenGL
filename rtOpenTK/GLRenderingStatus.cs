

namespace rtOpenTK
{
    public class TGLRenderingStatus
    {
        public TGLViewport Viewport
        { get; } = new TGLViewport();
        public TGLModelviewMatrixStack ModelViewMatrix
        { get; } = new TGLModelviewMatrixStack();
        public TGLProjectionMatrixStack ProjectionMatrix
        { get; } = new TGLProjectionMatrixStack();
    }
}
