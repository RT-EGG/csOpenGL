// System
using System;
// OpenTK
using OpenTK.Graphics.OpenGL;
// rtUtility
using rtUtility.rtMath;

namespace rtOpenTK
{
    public class TGLModelviewMatrixStack
    {
        public TGLModelviewMatrixStack()
        {
            Model.OnMatrixChanged += MatrixChanged;
            View.OnMatrixChanged += MatrixChanged;

            p_ModelViewMatrix.MakeIdentity();
            p_NormalMatrix.MakeIdentity();

            return;
        }

        public TMatrix44 CurrentMatrix
        {
            get
            {
                if (p_MvMatrixChanged) {
                    p_ModelViewMatrix = (new TMatrix44(Model.CurrentMatrix)) * (new TMatrix44(View.CurrentMatrix));
                    p_MvMatrixChanged = false;
                }
                return p_ModelViewMatrix;
            }
        }

        public IROMatrix33 NormalMatrix
        {
            get
            {
                if (p_NormMatrixChanged) {
                    for (int r = 0; r < 3; ++r) {
                        for (int c = 0; c < 3; ++c) {
                            p_NormalMatrix[r, c] = CurrentMatrix[r, c];
                        }
                    }

                    if (p_NormalMatrix.Inverse()) {
                        p_NormalMatrix.Tranpose();
                    } else {
                        p_NormalMatrix.MakeIdentity();
                    }

                    p_NormMatrixChanged = false;
                }
                return p_NormalMatrix;
            }
        }

        private void MatrixChanged(object sender, EventArgs e)
        {
            p_MvMatrixChanged = true;
            p_NormMatrixChanged = true;

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(CurrentMatrix.FloatArray);

            return;
        }

        public TGLModelMatrixStack Model { get; } = new TGLModelMatrixStack();
        public TGLViewMatrixStack View { get; } = new TGLViewMatrixStack();
        private TMatrix33 p_NormalMatrix = new TMatrix33();
        private TMatrix44 p_ModelViewMatrix = new TMatrix44();
        private bool p_MvMatrixChanged = true;
        private bool p_NormMatrixChanged = true;
    }
}
