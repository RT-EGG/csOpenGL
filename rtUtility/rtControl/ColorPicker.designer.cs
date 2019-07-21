namespace rtUtility.rtControl
{
    partial class ColorPicker
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PanelDataView = new System.Windows.Forms.Panel();
            this.PanelNumericInput = new System.Windows.Forms.Panel();
            this.PanelAlpha = new System.Windows.Forms.Panel();
            this.LabelA = new System.Windows.Forms.Label();
            this.upDownA = new rtUtility.rtControl.CustomNumericUpDown(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.UpDownB = new rtUtility.rtControl.CustomNumericUpDown(this.components);
            this.LabelB = new System.Windows.Forms.Label();
            this.UpDownV = new rtUtility.rtControl.CustomNumericUpDown(this.components);
            this.LabelV = new System.Windows.Forms.Label();
            this.UpDownR = new rtUtility.rtControl.CustomNumericUpDown(this.components);
            this.LabelR = new System.Windows.Forms.Label();
            this.UpDownG = new rtUtility.rtControl.CustomNumericUpDown(this.components);
            this.LabelG = new System.Windows.Forms.Label();
            this.UpDownH = new rtUtility.rtControl.CustomNumericUpDown(this.components);
            this.LabelH = new System.Windows.Forms.Label();
            this.LabelS = new System.Windows.Forms.Label();
            this.UpDownS = new rtUtility.rtControl.CustomNumericUpDown(this.components);
            this.PanelSampleView = new System.Windows.Forms.Panel();
            this.PanelSampleViewPainter = new System.Windows.Forms.Panel();
            this.PanelRGB = new System.Windows.Forms.Panel();
            this.PanelHSV = new System.Windows.Forms.Panel();
            this.PanelPicker = new rtUtility.rtControl.DoubleBuffedPanel(this.components);
            this.PanelHSPicker = new rtUtility.rtControl.DoubleBuffedPanel(this.components);
            this.PanelVPicker = new rtUtility.rtControl.DoubleBuffedPanel(this.components);
            this.PanelDataView.SuspendLayout();
            this.PanelNumericInput.SuspendLayout();
            this.PanelAlpha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownA)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownS)).BeginInit();
            this.PanelSampleView.SuspendLayout();
            this.PanelPicker.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelDataView
            // 
            this.PanelDataView.Controls.Add(this.PanelNumericInput);
            this.PanelDataView.Controls.Add(this.PanelSampleView);
            this.PanelDataView.Controls.Add(this.PanelRGB);
            this.PanelDataView.Controls.Add(this.PanelHSV);
            this.PanelDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDataView.Location = new System.Drawing.Point(0, 181);
            this.PanelDataView.Name = "PanelDataView";
            this.PanelDataView.Size = new System.Drawing.Size(289, 105);
            this.PanelDataView.TabIndex = 6;
            // 
            // PanelNumericInput
            // 
            this.PanelNumericInput.Controls.Add(this.PanelAlpha);
            this.PanelNumericInput.Controls.Add(this.tableLayoutPanel1);
            this.PanelNumericInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelNumericInput.Location = new System.Drawing.Point(73, 0);
            this.PanelNumericInput.Name = "PanelNumericInput";
            this.PanelNumericInput.Size = new System.Drawing.Size(216, 105);
            this.PanelNumericInput.TabIndex = 5;
            // 
            // PanelAlpha
            // 
            this.PanelAlpha.AutoSize = true;
            this.PanelAlpha.Controls.Add(this.LabelA);
            this.PanelAlpha.Controls.Add(this.upDownA);
            this.PanelAlpha.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelAlpha.Location = new System.Drawing.Point(0, 77);
            this.PanelAlpha.Name = "PanelAlpha";
            this.PanelAlpha.Size = new System.Drawing.Size(216, 28);
            this.PanelAlpha.TabIndex = 3;
            // 
            // LabelA
            // 
            this.LabelA.AutoSize = true;
            this.LabelA.Location = new System.Drawing.Point(6, 8);
            this.LabelA.Name = "LabelA";
            this.LabelA.Size = new System.Drawing.Size(19, 12);
            this.LabelA.TabIndex = 9;
            this.LabelA.Text = "A :";
            // 
            // upDownA
            // 
            this.upDownA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upDownA.Location = new System.Drawing.Point(31, 6);
            this.upDownA.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.upDownA.Name = "upDownA";
            this.upDownA.Size = new System.Drawing.Size(182, 19);
            this.upDownA.TabIndex = 8;
            this.upDownA.Tail = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.UpDownB, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.LabelB, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.UpDownV, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.LabelV, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.UpDownR, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelR, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.UpDownG, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.LabelG, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.UpDownH, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelH, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelS, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.UpDownS, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(216, 77);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // UpDownB
            // 
            this.UpDownB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpDownB.Location = new System.Drawing.Point(136, 53);
            this.UpDownB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDownB.Name = "UpDownB";
            this.UpDownB.Size = new System.Drawing.Size(77, 19);
            this.UpDownB.TabIndex = 4;
            this.UpDownB.Tail = "";
            // 
            // LabelB
            // 
            this.LabelB.AutoSize = true;
            this.LabelB.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelB.Location = new System.Drawing.Point(111, 50);
            this.LabelB.Name = "LabelB";
            this.LabelB.Size = new System.Drawing.Size(19, 27);
            this.LabelB.TabIndex = 5;
            this.LabelB.Text = "B :";
            this.LabelB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpDownV
            // 
            this.UpDownV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpDownV.Location = new System.Drawing.Point(28, 53);
            this.UpDownV.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDownV.Name = "UpDownV";
            this.UpDownV.Size = new System.Drawing.Size(77, 19);
            this.UpDownV.TabIndex = 10;
            this.UpDownV.Tail = "";
            // 
            // LabelV
            // 
            this.LabelV.AutoSize = true;
            this.LabelV.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelV.Location = new System.Drawing.Point(3, 50);
            this.LabelV.Name = "LabelV";
            this.LabelV.Size = new System.Drawing.Size(19, 27);
            this.LabelV.TabIndex = 11;
            this.LabelV.Text = "V :";
            this.LabelV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpDownR
            // 
            this.UpDownR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpDownR.Location = new System.Drawing.Point(136, 3);
            this.UpDownR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDownR.Name = "UpDownR";
            this.UpDownR.Size = new System.Drawing.Size(77, 19);
            this.UpDownR.TabIndex = 11;
            this.UpDownR.Tail = "";
            // 
            // LabelR
            // 
            this.LabelR.AutoSize = true;
            this.LabelR.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelR.Location = new System.Drawing.Point(111, 0);
            this.LabelR.Name = "LabelR";
            this.LabelR.Size = new System.Drawing.Size(19, 25);
            this.LabelR.TabIndex = 10;
            this.LabelR.Text = "R :";
            this.LabelR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpDownG
            // 
            this.UpDownG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpDownG.Location = new System.Drawing.Point(136, 28);
            this.UpDownG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDownG.Name = "UpDownG";
            this.UpDownG.Size = new System.Drawing.Size(77, 19);
            this.UpDownG.TabIndex = 2;
            this.UpDownG.Tail = "";
            // 
            // LabelG
            // 
            this.LabelG.AutoSize = true;
            this.LabelG.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelG.Location = new System.Drawing.Point(111, 25);
            this.LabelG.Name = "LabelG";
            this.LabelG.Size = new System.Drawing.Size(19, 25);
            this.LabelG.TabIndex = 3;
            this.LabelG.Text = "G :";
            this.LabelG.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpDownH
            // 
            this.UpDownH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpDownH.Location = new System.Drawing.Point(28, 3);
            this.UpDownH.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.UpDownH.Name = "UpDownH";
            this.UpDownH.Size = new System.Drawing.Size(77, 19);
            this.UpDownH.TabIndex = 9;
            this.UpDownH.Tail = "";
            // 
            // LabelH
            // 
            this.LabelH.AutoSize = true;
            this.LabelH.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelH.Location = new System.Drawing.Point(3, 0);
            this.LabelH.Name = "LabelH";
            this.LabelH.Size = new System.Drawing.Size(19, 25);
            this.LabelH.TabIndex = 8;
            this.LabelH.Text = "H :";
            this.LabelH.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelS
            // 
            this.LabelS.AutoSize = true;
            this.LabelS.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelS.Location = new System.Drawing.Point(4, 25);
            this.LabelS.Name = "LabelS";
            this.LabelS.Size = new System.Drawing.Size(18, 25);
            this.LabelS.TabIndex = 12;
            this.LabelS.Text = "S :";
            this.LabelS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpDownS
            // 
            this.UpDownS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpDownS.Location = new System.Drawing.Point(28, 28);
            this.UpDownS.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDownS.Name = "UpDownS";
            this.UpDownS.Size = new System.Drawing.Size(77, 19);
            this.UpDownS.TabIndex = 13;
            this.UpDownS.Tail = "";
            // 
            // PanelSampleView
            // 
            this.PanelSampleView.Controls.Add(this.PanelSampleViewPainter);
            this.PanelSampleView.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelSampleView.Location = new System.Drawing.Point(0, 0);
            this.PanelSampleView.Name = "PanelSampleView";
            this.PanelSampleView.Padding = new System.Windows.Forms.Padding(10);
            this.PanelSampleView.Size = new System.Drawing.Size(73, 105);
            this.PanelSampleView.TabIndex = 0;
            // 
            // PanelSampleViewPainter
            // 
            this.PanelSampleViewPainter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelSampleViewPainter.Location = new System.Drawing.Point(10, 10);
            this.PanelSampleViewPainter.Name = "PanelSampleViewPainter";
            this.PanelSampleViewPainter.Size = new System.Drawing.Size(53, 85);
            this.PanelSampleViewPainter.TabIndex = 6;
            // 
            // PanelRGB
            // 
            this.PanelRGB.AutoSize = true;
            this.PanelRGB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelRGB.Location = new System.Drawing.Point(0, 105);
            this.PanelRGB.Name = "PanelRGB";
            this.PanelRGB.Size = new System.Drawing.Size(289, 0);
            this.PanelRGB.TabIndex = 1;
            // 
            // PanelHSV
            // 
            this.PanelHSV.AutoSize = true;
            this.PanelHSV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelHSV.Location = new System.Drawing.Point(0, 105);
            this.PanelHSV.Name = "PanelHSV";
            this.PanelHSV.Size = new System.Drawing.Size(289, 0);
            this.PanelHSV.TabIndex = 2;
            // 
            // PanelPicker
            // 
            this.PanelPicker.Controls.Add(this.PanelHSPicker);
            this.PanelPicker.Controls.Add(this.PanelVPicker);
            this.PanelPicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelPicker.Location = new System.Drawing.Point(0, 0);
            this.PanelPicker.Name = "PanelPicker";
            this.PanelPicker.Size = new System.Drawing.Size(289, 181);
            this.PanelPicker.TabIndex = 5;
            // 
            // PanelHSPicker
            // 
            this.PanelHSPicker.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.PanelHSPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHSPicker.Location = new System.Drawing.Point(0, 0);
            this.PanelHSPicker.Name = "PanelHSPicker";
            this.PanelHSPicker.Size = new System.Drawing.Size(263, 181);
            this.PanelHSPicker.TabIndex = 4;
            this.PanelHSPicker.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelHSPicker_Paint);
            this.PanelHSPicker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelHSPicker_MouseDown);
            this.PanelHSPicker.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelHSPicker_MouseMove);
            this.PanelHSPicker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelHSPicker_MouseUp);
            this.PanelHSPicker.Resize += new System.EventHandler(this.PanelHSPicker_Resize);
            // 
            // PanelVPicker
            // 
            this.PanelVPicker.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.PanelVPicker.Dock = System.Windows.Forms.DockStyle.Right;
            this.PanelVPicker.Location = new System.Drawing.Point(263, 0);
            this.PanelVPicker.Name = "PanelVPicker";
            this.PanelVPicker.Size = new System.Drawing.Size(26, 181);
            this.PanelVPicker.TabIndex = 5;
            this.PanelVPicker.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelVPicker_Paint);
            this.PanelVPicker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelVPicker_MouseDown);
            this.PanelVPicker.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelVPicker_MouseMove);
            this.PanelVPicker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelVPicker_MouseUp);
            this.PanelVPicker.Resize += new System.EventHandler(this.PanelVPicker_Resize);
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelDataView);
            this.Controls.Add(this.PanelPicker);
            this.DoubleBuffered = true;
            this.Name = "ColorPicker";
            this.Size = new System.Drawing.Size(289, 286);
            this.Load += new System.EventHandler(this.ColorPicker_Load);
            this.Resize += new System.EventHandler(this.ColorControl_Resize);
            this.PanelDataView.ResumeLayout(false);
            this.PanelDataView.PerformLayout();
            this.PanelNumericInput.ResumeLayout(false);
            this.PanelNumericInput.PerformLayout();
            this.PanelAlpha.ResumeLayout(false);
            this.PanelAlpha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownA)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownS)).EndInit();
            this.PanelSampleView.ResumeLayout(false);
            this.PanelPicker.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffedPanel PanelPicker;
        private DoubleBuffedPanel PanelVPicker;
        private DoubleBuffedPanel PanelHSPicker;
        private System.Windows.Forms.Panel PanelDataView;
        private System.Windows.Forms.Panel PanelSampleView;
        private System.Windows.Forms.Panel PanelSampleViewPainter;
        private System.Windows.Forms.Panel PanelRGB;
        private System.Windows.Forms.Label LabelB;
        private CustomNumericUpDown UpDownB;
        private System.Windows.Forms.Label LabelG;
        private CustomNumericUpDown UpDownG;
        private System.Windows.Forms.Panel PanelHSV;
        private System.Windows.Forms.Label LabelV;
        private CustomNumericUpDown UpDownV;
        private System.Windows.Forms.Panel PanelAlpha;
        private System.Windows.Forms.Label LabelA;
        private CustomNumericUpDown upDownA;
        private System.Windows.Forms.Panel PanelNumericInput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomNumericUpDown UpDownR;
        private System.Windows.Forms.Label LabelR;
        private CustomNumericUpDown UpDownH;
        private System.Windows.Forms.Label LabelH;
        private System.Windows.Forms.Label LabelS;
        private CustomNumericUpDown UpDownS;
    }
}
