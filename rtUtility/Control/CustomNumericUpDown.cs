using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rtUtility.Control
{
    public partial class CustomNumericUpDown : NumericUpDown
    {
        public CustomNumericUpDown()
        {
            InitializeComponent();
        }

        public CustomNumericUpDown(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value + Tail; }
        }

        public string Tail
        {
            get { return p_Tail; }
            set
            {
                if (p_Tail != value) {
                    p_Tail = value;
                    UpdateEditText();
                    return;
                }
            }
        }

        private string p_Tail = "";
    }
}
