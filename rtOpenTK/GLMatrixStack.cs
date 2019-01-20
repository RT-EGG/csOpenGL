// System
using System;
using System.Collections.Generic;
// OpenTK
using OpenTK.Graphics.OpenGL;
// rtUtility
using rtUtility.rtMath;

namespace rtOpenTK
{
    public abstract class TGLMatrixStack
    {
        public TGLMatrixStack()
        {
            LoadIdentity();
            return;
        }

        public void LoadIdentity()
        {
            LoadMatrix(TMatrix44.IdentityMatrix);
            return;
        }

        public void LoadMatrix(IROMatrix44 aValue)
        {
            p_CurrentMatrix = new TMatrix44(aValue);
            GL.MatrixMode(TargetMatrixMode);
            GL.LoadMatrix(p_CurrentMatrix.FloatArray);

            OnMatrixChanged?.Invoke(this, EventArgs.Empty);

            return;
        }

        public void MultiMatrix(IROMatrix44 aValue)
        {
            MultiMatrix(new TMatrix44(aValue));
            return;
        }

        public void MultiMatrix(TMatrix44 aValue)
        {
            LoadMatrix(aValue * p_CurrentMatrix);
            return;
        }

        public void PushMatrix()
        {
            GL.MatrixMode(TargetMatrixMode);
            p_Stack.Push(p_CurrentMatrix);
            return;
        }

        public void PopMatrix()
        {
            GL.MatrixMode(TargetMatrixMode);
            LoadMatrix(p_Stack.Pop());
            return;
        }

        public IROMatrix44 CurrentMatrix
        { get { return p_CurrentMatrix; } }

        public EventHandler OnMatrixChanged
        { get; set; }

        protected abstract MatrixMode TargetMatrixMode
        { get; }

        private TMatrix44 p_CurrentMatrix;
        private Stack<TMatrix44> p_Stack = new Stack<TMatrix44>();
    }
}
