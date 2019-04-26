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
            this.OpenGL = new rtOpenTK.TrtGLControl();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // OpenGL
            // 
            this.OpenGL.BackColor = System.Drawing.Color.Black;
            this.OpenGL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenGL.Location = new System.Drawing.Point(0, 0);
            this.OpenGL.Name = "OpenGL";
            this.OpenGL.Size = new System.Drawing.Size(863, 450);
            this.OpenGL.TabIndex = 0;
            this.OpenGL.VSync = false;
            this.OpenGL.Load += new System.EventHandler(this.OpenGL_Load);
            this.OpenGL.Paint += new System.Windows.Forms.PaintEventHandler(this.OpenGL_Paint);
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Interval = 16;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 450);
            this.Controls.Add(this.OpenGL);
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private rtOpenTK.TrtGLControl OpenGL;
        private System.Windows.Forms.Timer MainTimer;
    }
}

