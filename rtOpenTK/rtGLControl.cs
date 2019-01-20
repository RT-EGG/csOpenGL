// OpenTK
using OpenTK;
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
        
        private static TrtGLControl p_CurrentControl = null;
    }
}
