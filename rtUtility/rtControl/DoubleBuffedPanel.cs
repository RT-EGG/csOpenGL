using System.ComponentModel;
using System.Windows.Forms;

namespace rtUtility.rtControl
{
    public partial class DoubleBuffedPanel : Panel
    {
        public DoubleBuffedPanel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            return;
        }

        public DoubleBuffedPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            return;
        }
    }
}
