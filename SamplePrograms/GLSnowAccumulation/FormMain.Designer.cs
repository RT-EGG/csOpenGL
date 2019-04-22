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
            this.OpenGL = new rtOpenTK.TrtGLControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LabelMinHeight = new System.Windows.Forms.Label();
            this.udMinHeight = new System.Windows.Forms.NumericUpDown();
            this.LabelMaxHeight = new System.Windows.Forms.Label();
            this.udMaxHeight = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMaxHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenGL
            // 
            this.OpenGL.BackColor = System.Drawing.Color.Black;
            this.OpenGL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenGL.Location = new System.Drawing.Point(0, 0);
            this.OpenGL.Name = "OpenGL";
            this.OpenGL.Size = new System.Drawing.Size(663, 450);
            this.OpenGL.TabIndex = 0;
            this.OpenGL.VSync = false;
            this.OpenGL.Load += new System.EventHandler(this.OpenGL_Load);
            this.OpenGL.Paint += new System.Windows.Forms.PaintEventHandler(this.OpenGL_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.udMaxHeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LabelMaxHeight, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.LabelMinHeight, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.udMinHeight, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(663, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 450);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // LabelMinHeight
            // 
            this.LabelMinHeight.AutoSize = true;
            this.LabelMinHeight.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelMinHeight.Location = new System.Drawing.Point(3, 0);
            this.LabelMinHeight.Name = "LabelMinHeight";
            this.LabelMinHeight.Size = new System.Drawing.Size(62, 25);
            this.LabelMinHeight.TabIndex = 1;
            this.LabelMinHeight.Text = "MinHeight :";
            this.LabelMinHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // udMinHeight
            // 
            this.udMinHeight.DecimalPlaces = 2;
            this.udMinHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udMinHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udMinHeight.Location = new System.Drawing.Point(74, 3);
            this.udMinHeight.Name = "udMinHeight";
            this.udMinHeight.Size = new System.Drawing.Size(123, 19);
            this.udMinHeight.TabIndex = 2;
            this.udMinHeight.ValueChanged += new System.EventHandler(this.UdMinHeight_ValueChanged);
            // 
            // LabelMaxHeight
            // 
            this.LabelMaxHeight.AutoSize = true;
            this.LabelMaxHeight.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelMaxHeight.Location = new System.Drawing.Point(3, 25);
            this.LabelMaxHeight.Name = "LabelMaxHeight";
            this.LabelMaxHeight.Size = new System.Drawing.Size(65, 25);
            this.LabelMaxHeight.TabIndex = 3;
            this.LabelMaxHeight.Text = "MaxHeight :";
            this.LabelMaxHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // udMaxHeight
            // 
            this.udMaxHeight.DecimalPlaces = 2;
            this.udMaxHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udMaxHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udMaxHeight.Location = new System.Drawing.Point(74, 28);
            this.udMaxHeight.Name = "udMaxHeight";
            this.udMaxHeight.Size = new System.Drawing.Size(123, 19);
            this.udMaxHeight.TabIndex = 4;
            this.udMaxHeight.ValueChanged += new System.EventHandler(this.UdMaxHeight_ValueChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 450);
            this.Controls.Add(this.OpenGL);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMaxHeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private rtOpenTK.TrtGLControl OpenGL;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown udMaxHeight;
        private System.Windows.Forms.Label LabelMaxHeight;
        private System.Windows.Forms.Label LabelMinHeight;
        private System.Windows.Forms.NumericUpDown udMinHeight;
    }
}

