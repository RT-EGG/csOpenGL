namespace GLDelaunayTriangulation
{
    partial class TFormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PanelEditor = new System.Windows.Forms.Panel();
            this.CheckRenderOuterTriangle = new System.Windows.Forms.CheckBox();
            this.ButtonCalculate = new System.Windows.Forms.Button();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.LabelTriangleCount = new System.Windows.Forms.Label();
            this.LabelPointCount = new System.Windows.Forms.Label();
            this.LabelTimeCost = new System.Windows.Forms.Label();
            this.OpenGL = new rtOpenTK.TrtGLControl();
            this.LabelControlDescription = new System.Windows.Forms.Label();
            this.PanelEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelEditor
            // 
            this.PanelEditor.Controls.Add(this.CheckRenderOuterTriangle);
            this.PanelEditor.Controls.Add(this.ButtonCalculate);
            this.PanelEditor.Controls.Add(this.ButtonClear);
            this.PanelEditor.Controls.Add(this.LabelTriangleCount);
            this.PanelEditor.Controls.Add(this.LabelPointCount);
            this.PanelEditor.Controls.Add(this.LabelTimeCost);
            this.PanelEditor.Dock = System.Windows.Forms.DockStyle.Right;
            this.PanelEditor.Location = new System.Drawing.Point(355, 0);
            this.PanelEditor.Name = "PanelEditor";
            this.PanelEditor.Padding = new System.Windows.Forms.Padding(3);
            this.PanelEditor.Size = new System.Drawing.Size(166, 355);
            this.PanelEditor.TabIndex = 2;
            // 
            // CheckRenderOuterTriangle
            // 
            this.CheckRenderOuterTriangle.AutoSize = true;
            this.CheckRenderOuterTriangle.Dock = System.Windows.Forms.DockStyle.Top;
            this.CheckRenderOuterTriangle.Location = new System.Drawing.Point(3, 103);
            this.CheckRenderOuterTriangle.Name = "CheckRenderOuterTriangle";
            this.CheckRenderOuterTriangle.Size = new System.Drawing.Size(160, 16);
            this.CheckRenderOuterTriangle.TabIndex = 5;
            this.CheckRenderOuterTriangle.Text = "RenderOuter";
            this.CheckRenderOuterTriangle.UseVisualStyleBackColor = true;
            this.CheckRenderOuterTriangle.CheckedChanged += new System.EventHandler(this.CheckRenderOuterTriangle_CheckedChanged);
            // 
            // ButtonCalculate
            // 
            this.ButtonCalculate.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonCalculate.Location = new System.Drawing.Point(3, 80);
            this.ButtonCalculate.Name = "ButtonCalculate";
            this.ButtonCalculate.Size = new System.Drawing.Size(160, 23);
            this.ButtonCalculate.TabIndex = 2;
            this.ButtonCalculate.Text = "Calculate";
            this.ButtonCalculate.UseVisualStyleBackColor = true;
            this.ButtonCalculate.Click += new System.EventHandler(this.ButtonCalculate_Click);
            // 
            // ButtonClear
            // 
            this.ButtonClear.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonClear.Location = new System.Drawing.Point(3, 57);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(160, 23);
            this.ButtonClear.TabIndex = 1;
            this.ButtonClear.Text = "Clear";
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // LabelTriangleCount
            // 
            this.LabelTriangleCount.AutoSize = true;
            this.LabelTriangleCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelTriangleCount.Location = new System.Drawing.Point(3, 39);
            this.LabelTriangleCount.Margin = new System.Windows.Forms.Padding(3);
            this.LabelTriangleCount.Name = "LabelTriangleCount";
            this.LabelTriangleCount.Padding = new System.Windows.Forms.Padding(3);
            this.LabelTriangleCount.Size = new System.Drawing.Size(100, 18);
            this.LabelTriangleCount.TabIndex = 3;
            this.LabelTriangleCount.Text = "Triangle count : 0";
            // 
            // LabelPointCount
            // 
            this.LabelPointCount.AutoSize = true;
            this.LabelPointCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelPointCount.Location = new System.Drawing.Point(3, 21);
            this.LabelPointCount.Margin = new System.Windows.Forms.Padding(3);
            this.LabelPointCount.Name = "LabelPointCount";
            this.LabelPointCount.Padding = new System.Windows.Forms.Padding(3);
            this.LabelPointCount.Size = new System.Drawing.Size(113, 18);
            this.LabelPointCount.TabIndex = 0;
            this.LabelPointCount.Text = "Input point count : 0";
            // 
            // LabelTimeCost
            // 
            this.LabelTimeCost.AutoSize = true;
            this.LabelTimeCost.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelTimeCost.Location = new System.Drawing.Point(3, 3);
            this.LabelTimeCost.Margin = new System.Windows.Forms.Padding(3);
            this.LabelTimeCost.Name = "LabelTimeCost";
            this.LabelTimeCost.Padding = new System.Windows.Forms.Padding(3);
            this.LabelTimeCost.Size = new System.Drawing.Size(95, 18);
            this.LabelTimeCost.TabIndex = 7;
            this.LabelTimeCost.Text = "Calc time : 0 ms";
            // 
            // OpenGL
            // 
            this.OpenGL.BackColor = System.Drawing.Color.Black;
            this.OpenGL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenGL.Location = new System.Drawing.Point(0, 0);
            this.OpenGL.Name = "OpenGL";
            this.OpenGL.Size = new System.Drawing.Size(355, 355);
            this.OpenGL.TabIndex = 3;
            this.OpenGL.VSync = false;
            this.OpenGL.Load += new System.EventHandler(this.OpenGL_Load);
            this.OpenGL.Paint += new System.Windows.Forms.PaintEventHandler(this.OpenGL_Paint);
            this.OpenGL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OpenGL_MouseClick);
            // 
            // LabelControlDescription
            // 
            this.LabelControlDescription.BackColor = System.Drawing.SystemColors.Control;
            this.LabelControlDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelControlDescription.Location = new System.Drawing.Point(0, 0);
            this.LabelControlDescription.Name = "LabelControlDescription";
            this.LabelControlDescription.Size = new System.Drawing.Size(355, 12);
            this.LabelControlDescription.TabIndex = 4;
            this.LabelControlDescription.Text = "Clcik to add point, and \"Calculate\" button to generate triangles.";
            // 
            // TFormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 355);
            this.Controls.Add(this.LabelControlDescription);
            this.Controls.Add(this.OpenGL);
            this.Controls.Add(this.PanelEditor);
            this.Name = "TFormMain";
            this.Text = "MainForm";
            this.PanelEditor.ResumeLayout(false);
            this.PanelEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelEditor;
        private System.Windows.Forms.CheckBox CheckRenderOuterTriangle;
        private System.Windows.Forms.Button ButtonCalculate;
        private System.Windows.Forms.Button ButtonClear;
        private System.Windows.Forms.Label LabelTriangleCount;
        private System.Windows.Forms.Label LabelPointCount;
        private System.Windows.Forms.Label LabelTimeCost;
        private rtOpenTK.TrtGLControl OpenGL;
        private System.Windows.Forms.Label LabelControlDescription;
    }
}