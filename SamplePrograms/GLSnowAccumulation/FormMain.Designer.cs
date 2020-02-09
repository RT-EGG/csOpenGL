namespace GLSnowAccumulation
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.LabelTimestepScale = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TrackbarTimestepScale = new System.Windows.Forms.TrackBar();
            this.OpenGL = new rtOpenTK.TrtGLControl();
            this.UpDownTimestepScale = new rtUtility.rtControl.CustomNumericUpDown(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackbarTimestepScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTimestepScale)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Interval = 16;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.LabelTimestepScale);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(453, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 425);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // LabelTimestepScale
            // 
            this.LabelTimestepScale.AutoSize = true;
            this.LabelTimestepScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelTimestepScale.Location = new System.Drawing.Point(3, 0);
            this.LabelTimestepScale.Name = "LabelTimestepScale";
            this.LabelTimestepScale.Padding = new System.Windows.Forms.Padding(3);
            this.LabelTimestepScale.Size = new System.Drawing.Size(89, 18);
            this.LabelTimestepScale.TabIndex = 0;
            this.LabelTimestepScale.Text = "Timestep scale";
            this.LabelTimestepScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Controls.Add(this.TrackbarTimestepScale, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.UpDownTimestepScale, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 25);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // TrackbarTimestepScale
            // 
            this.TrackbarTimestepScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrackbarTimestepScale.Location = new System.Drawing.Point(3, 3);
            this.TrackbarTimestepScale.Maximum = 100;
            this.TrackbarTimestepScale.Name = "TrackbarTimestepScale";
            this.TrackbarTimestepScale.Size = new System.Drawing.Size(114, 19);
            this.TrackbarTimestepScale.TabIndex = 0;
            this.TrackbarTimestepScale.TickFrequency = 0;
            this.TrackbarTimestepScale.Value = 10;
            this.TrackbarTimestepScale.ValueChanged += new System.EventHandler(this.TrackbarTimestepScale_ValueChanged);
            // 
            // OpenGL
            // 
            this.OpenGL.BackColor = System.Drawing.Color.Black;
            this.OpenGL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenGL.Location = new System.Drawing.Point(0, 0);
            this.OpenGL.Name = "OpenGL";
            this.OpenGL.Size = new System.Drawing.Size(453, 425);
            this.OpenGL.TabIndex = 0;
            this.OpenGL.VSync = false;
            this.OpenGL.Load += new System.EventHandler(this.OpenGL_Load);
            this.OpenGL.Paint += new System.Windows.Forms.PaintEventHandler(this.OpenGL_Paint);
            // 
            // UpDownTimestepScale
            // 
            this.UpDownTimestepScale.DecimalPlaces = 1;
            this.UpDownTimestepScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.UpDownTimestepScale.Location = new System.Drawing.Point(123, 3);
            this.UpDownTimestepScale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.UpDownTimestepScale.Name = "UpDownTimestepScale";
            this.UpDownTimestepScale.Size = new System.Drawing.Size(74, 19);
            this.UpDownTimestepScale.TabIndex = 1;
            this.UpDownTimestepScale.Tail = " x";
            this.UpDownTimestepScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownTimestepScale.ValueChanged += new System.EventHandler(this.UpDownTimestepScale_ValueChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 425);
            this.Controls.Add(this.OpenGL);
            this.Controls.Add(this.flowLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackbarTimestepScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTimestepScale)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private rtOpenTK.TrtGLControl OpenGL;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label LabelTimestepScale;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar TrackbarTimestepScale;
        private rtUtility.rtControl.CustomNumericUpDown UpDownTimestepScale;
    }
}

