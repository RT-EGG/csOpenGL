using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using rtUtility.rtMath;
using static rtUtility.rtMath.TMathExtension;

namespace rtUtility.rtControl
{
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
            RGB = new TColorRGB(255, 0, 0);

            return;
        }

        ~ColorPicker()
        {
            if (p_HSImage != null)
                p_HSImage.Dispose();
            if (p_VImage != null)
                p_VImage.Dispose();

            return;
        }

        public TColorRGB RGB
        {
            set { HSV = value.ToHSV(); }
            get { return HSV.ToRGB(); }

        }

        public TColorHSV HSV
        {
            set
            {
                H = value.H;
                S = value.S;
                V = value.V;

                PanelHSPicker.Invalidate();
                PanelVPicker.Invalidate();

                TColorRGB rgb = RGB;
                PanelSampleViewPainter.BackColor = Color.FromArgb(255, rgb.Rb, rgb.Gb, rgb.Bb);

                UpdateRGBUpDown();
                UpdateHSVUpDown();
            }
            get
            {
                return new TColorHSV(H, S, V);
            }
        }

        public int SVPickerPanelWidth
        { get { return PanelHSPicker.Width; } }
        public int HPickerPanelHeight
        {
            set { PanelVPicker.Height = value; }
            get { return PanelVPicker.Height; }
        }

        private void ColorPicker_Load(object sender, EventArgs e)
        {
            CreateHSImage();
            return;
        }

        private void ColorControl_Resize(object sender, EventArgs e)
        {
            PanelHSPicker.Width = PanelHSPicker.Height;
        }

        private void PanelHSPicker_Resize(object sender, EventArgs e)
        {
            PanelHSPicker.Invalidate();
            return;
        }

        private void PanelVPicker_Resize(object sender, EventArgs e)
        {
            PanelVPicker.Invalidate();
            return;
        }

        private void PanelHSPicker_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.DrawImage(p_HSImage, CalcHSPickerRect());

            RectangleF pickerRect = CalcHSPickerRect();
            PointF pos = new PointF();
            pos.X = (float)(pickerRect.Width * H) + pickerRect.X;
            pos.Y = pickerRect.Height - (float)(pickerRect.Height * S) + pickerRect.Y;

            Pen bPen = new Pen(Color.Black, 1.0f);
            Pen wPen = new Pen(Color.White, 1.0f);
            try {
                float[] size = new float[3] { 7.0f, 5.0f, 3.0f };
                e.Graphics.DrawRectangle(wPen, pos.X - (size[0] * 0.5f), pos.Y - (size[0] * 0.5f), size[0], size[0]);
                e.Graphics.DrawRectangle(bPen, pos.X - (size[1] * 0.5f), pos.Y - (size[1] * 0.5f), size[1], size[1]);
                e.Graphics.DrawRectangle(wPen, pos.X - (size[2] * 0.5f), pos.Y - (size[2] * 0.5f), size[2], size[2]);
            } finally {
                bPen.Dispose();
                wPen.Dispose();
            }

            return;
        }

        private void PanelVPicker_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            if (p_VImageChange) {
                CreateVImage();
                p_VImageChange = false;
            }

            RectangleF rect = CalcVPickerRect();
            e.Graphics.DrawImage(p_VImage, rect);

            const float RECT_SIZE = 5.0f;
            float y = (float)(rect.Height * (1.0 - V)) + rect.Top;
            Pen pen = new Pen(Color.Black, 2.0f);
            try {
                e.Graphics.DrawRectangle(pen, rect.Left - (RECT_SIZE * 0.5f), y - (RECT_SIZE * 0.5f), RECT_SIZE, RECT_SIZE);
                e.Graphics.DrawRectangle(pen, rect.Right - (RECT_SIZE * 0.5f), y - (RECT_SIZE * 0.5f), RECT_SIZE, RECT_SIZE);
                e.Graphics.DrawLine(pen, rect.Left + (RECT_SIZE * 0.5f), y, rect.Right - (RECT_SIZE * 0.5f), y);
            } finally {
                pen.Dispose();
            }

            return;
        }

        private RectangleF CalcVPickerRect()
        {
            const float MARGIN = 5.0f;

            RectangleF result = new RectangleF();
            result.X = MARGIN;
            result.Y = MARGIN;
            result.Width  = PanelVPicker.Width -  (MARGIN * 2.0f);
            result.Height = PanelVPicker.Height - (MARGIN * 2.0f);
            
            return result;
        }

        private RectangleF CalcHSPickerRect()
        {
            const float MARGIN = 5.0f;

            RectangleF result = new RectangleF();
            result.X = MARGIN;
            result.Y = MARGIN;
            result.Width  = PanelHSPicker.Width  - (MARGIN * 2.0f);
            result.Height = PanelHSPicker.Height - (MARGIN * 2.0f);

            return result;
        }

        private void CreateHSImage()
        {
            if (this.GetDesignMode())
                return;

            int width = p_HSImage.Width;
            int height = p_HSImage.Height;
            byte[] buffer = new byte[width * height * 3];
            Parallel.For(0, height, y =>
            {
                TColorHSV hsv = new TColorHSV();
                TColorRGB rgb;
                int headIndex = y * width * 3;
                for (int x = 0; x < width; ++x) {
                    hsv.Set((double)x / (double)width, 1.0 - ((double)y / (double)height), 1.0);
                    rgb = hsv.ToRGB();

                    int index = headIndex + (x * 3);
                    buffer[index + 0] = rgb.Bb;
                    buffer[index + 1] = rgb.Gb;
                    buffer[index + 2] = rgb.Rb;
                }
            });

            BitmapData bmpData = p_HSImage.LockBits(new Rectangle(0, 0, p_HSImage.Width, p_HSImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            try {
                Marshal.Copy(buffer, 0, bmpData.Scan0, buffer.Length);

            } finally {
                p_HSImage.UnlockBits(bmpData);
            }
            PanelHSPicker.Invalidate();
            return;
        }

        private void CreateVImage()
        {
            if (this.GetDesignMode())
                return;
            Graphics graph = Graphics.FromImage(p_VImage);
            try {
                TColorRGB rgb1 = (new TColorHSV(H, S, 0.0)).ToRGB();
                TColorRGB rgb0 = (new TColorHSV(H, S, 1.0)).ToRGB();

                LinearGradientBrush brush = new LinearGradientBrush(graph.VisibleClipBounds, rgb0.ToSystemColor(), rgb1.ToSystemColor(), LinearGradientMode.Vertical);
                try {
                    brush.GammaCorrection = true;
                    graph.FillRectangle(brush, graph.VisibleClipBounds);

                } finally {
                    brush.Dispose();
                }

            } finally {
                graph.Dispose();
            }

            PanelVPicker.Invalidate();
            return;
        }

        private void PanelHSPicker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                RectangleF rect = CalcHSPickerRect();
                if (e.X.InRange(rect.Left.Round(), rect.Right.Round()) && e.Y.InRange(rect.Top.Round(), rect.Bottom.Round())) {
                    p_IsClickingHS = true;

                    PanelHSPicker_MouseMove(sender, e);
                }
            }

            return;
        }

        private void PanelHSPicker_MouseUp(object sender, MouseEventArgs e)
        {
            p_IsClickingHS = false;
            return;
        }

        private void PanelHSPicker_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                p_IsClickingHS = false;
            if (!p_IsClickingHS)
                return;

            RectangleF rect = CalcHSPickerRect();
            HSV = new TColorHSV(((e.X - rect.Left) / rect.Width).Clamp(0.0f, 1.0f), 1.0 - ((e.Y - rect.Top) / rect.Height).Clamp(0.0f, 1.0f), V);

            UpdateRGBUpDown();
            UpdateHSVUpDown();

            PanelHSPicker.Invalidate();

            return;
        }

        private void PanelVPicker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                RectangleF rect = CalcVPickerRect();
                if (e.X.InRange(rect.Left.Round(), rect.Right.Round()) && e.Y.InRange(rect.Top.Round(), rect.Bottom.Round())) {
                    p_IsClickingV = true;

                    PanelVPicker_MouseMove(sender, e);
                }
            }
            return;
        }

        private void PanelVPicker_MouseUp(object sender, MouseEventArgs e)
        {
            p_IsClickingV = false;
            return;
        }

        private void PanelVPicker_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                p_IsClickingV = false;
            if (!p_IsClickingV)
                return;

            RectangleF rect = CalcVPickerRect();
            HSV = new TColorHSV(H, S, 1.0 -((e.Y - rect.Top) / rect.Height).Clamp(0.0f, 1.0f));

            UpdateRGBUpDown();
            UpdateHSVUpDown();

            PanelVPicker.Invalidate();

            return;
        }

        private void OnUpDownRGBChanged(object sender, EventArgs e)
        {
            RGB = new TColorRGB((double)UpDownR.Value / 255.0, (double)UpDownG.Value / 255.0, (double)UpDownB.Value / 255.0);

            UpdateHSVUpDown();
        }

        private void OnUpDownHSVChanged(object sender, EventArgs e)
        {
            HSV = new TColorHSV((double)UpDownH.Value / 360.0, (double)UpDownS.Value / 255.0, (double)UpDownV.Value / 255.0);

            UpdateRGBUpDown();
        }

        private void SetUpDownEventEnable(bool aEnable)
        {
            if (aEnable) {
                UpDownR.ValueChanged += OnUpDownRGBChanged;
                UpDownG.ValueChanged += OnUpDownRGBChanged;
                UpDownB.ValueChanged += OnUpDownRGBChanged;
                UpDownH.ValueChanged += OnUpDownHSVChanged;
                UpDownS.ValueChanged += OnUpDownHSVChanged;
                UpDownV.ValueChanged += OnUpDownHSVChanged;
            } else {
                UpDownR.ValueChanged -= OnUpDownRGBChanged;
                UpDownG.ValueChanged -= OnUpDownRGBChanged;
                UpDownB.ValueChanged -= OnUpDownRGBChanged;
                UpDownH.ValueChanged -= OnUpDownHSVChanged;
                UpDownS.ValueChanged -= OnUpDownHSVChanged;
                UpDownV.ValueChanged -= OnUpDownHSVChanged;
            }
        }

        private void UpdateRGBUpDown()
        {
            SetUpDownEventEnable(false);
            try {
                TColorRGB rgb = RGB;
                UpDownR.Value = (decimal)(rgb.R * 255.0);
                UpDownG.Value = (decimal)(rgb.G * 255.0);
                UpDownB.Value = (decimal)(rgb.B * 255.0);

                UpDownR.Invalidate();
                UpDownG.Invalidate();
                UpDownB.Invalidate();

            } finally {
                SetUpDownEventEnable(true);
            }
        }

        private void UpdateHSVUpDown()
        {
            SetUpDownEventEnable(false);
            try {
                TColorHSV hsv = HSV;
                UpDownH.Value = (decimal)(hsv.H * 360.0);
                UpDownS.Value = (decimal)(hsv.S * 255.0);
                UpDownV.Value = (decimal)(hsv.V * 255.0);

                UpDownH.Invalidate();
                UpDownS.Invalidate();
                UpDownV.Invalidate();

            } finally {
                SetUpDownEventEnable(true);
            }
        }

        private double H
        {
            set
            {
                if (!p_H.AlmostEqual(value)) {
                    p_H = value;
                    p_VImageChange = true;
                    PanelVPicker.Invalidate();
                }
                return;
            }
            get { return p_H; }
        }

        private double S
        {
            set
            {
                if (!p_S.AlmostEqual(value)) {
                    p_S = value;
                    p_VImageChange = true;
                    PanelVPicker.Invalidate();
                }
                return;
            }
            get
            { return p_S; }
        }

        private double V
        { get; set; } = 1.0;

        private double p_H = 1.0;
        private double p_S = 1.0;

        private Bitmap p_HSImage = new Bitmap(256, 256);
        private Bitmap p_VImage = new Bitmap(64, 256);
        private bool p_VImageChange = true;
        private bool p_IsClickingHS = false;
        private bool p_IsClickingV  = false;
    }
}
