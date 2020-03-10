using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
// OpenTK
using OpenTK;
using OpenTK.Graphics.OpenGL4;
// rtOpenTK
using rtOpenTK.rtGLResourceObject;

namespace rtOpenTK
{
    public delegate void TrtGLControlNotify(TrtGLControl aGL);
    public delegate void TrtGLTask(TrtGLControl aGL, object aObject);

    public partial class TrtGLControl : GLControl
    {
        public TrtGLControl()
        {
            InitializeComponent();
        }

        public new void MakeCurrent()
        {
            p_CurrentControl = this;
            base.MakeCurrent();
            ResourceManager.Process(this);
            return;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode) {
                e.Graphics.Clear(Color.Black);
                return;
            }

            base.OnPaint(e);

            MakeCurrent();
            GLPaint?.Invoke(this);

            SwapBuffers();
            return;
        }

        public static TrtGLControl CurrentControl
        {
            get
            {
                if (p_CurrentControl != null) {
                    if (!p_CurrentControl.Context.IsCurrent)
                        p_CurrentControl = null;
                }
                return p_CurrentControl;
            }
        }

        public static void EnqueueGLTask(TrtGLTask aTask, object aObject)
        {
            ResourceManager.EnqueueTask(aTask, aObject);
            return;
        }

        internal static TGLResourceManager ResourceManager
        { get; } = new TGLResourceManager();

        [Category("表示")]
        public event TrtGLControlNotify GLPaint;

        public new bool DesignMode => GetDesignMode(this);
        private bool GetDesignMode(Control inControl)
        {
            if (inControl == null) {
                return false;
            }

            bool mode = inControl.Site == null ? false : inControl.Site.DesignMode;
            return mode | GetDesignMode(inControl.Parent);
        }
        
        private static TrtGLControl p_CurrentControl = null;
    }
}
