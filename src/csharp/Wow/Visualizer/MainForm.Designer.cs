namespace Visualizer
{
    partial class MainForm
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
            if (disposing && (components != null))
            {
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
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.gradientPanel1 = new Controls.Basic.GradientPanel();
            this.pnTestDropshadow = new Controls.Basic.GradientPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.spectrogram1 = new Controls.Visualization.Spectrogram();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnTestDropshadow.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.splitMain.Panel1.Controls.Add(this.gradientPanel1);
            this.splitMain.Panel1.Controls.Add(this.pnTestDropshadow);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.spectrogram1);
            this.splitMain.Size = new System.Drawing.Size(731, 625);
            this.splitMain.SplitterDistance = 97;
            this.splitMain.TabIndex = 1;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.GradientColor1 = System.Drawing.Color.AliceBlue;
            this.gradientPanel1.GradientColor2 = System.Drawing.Color.LightSteelBlue;
            this.gradientPanel1.Location = new System.Drawing.Point(336, 50);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(100, 33);
            this.gradientPanel1.TabIndex = 1;
            // 
            // pnTestDropshadow
            // 
            this.pnTestDropshadow.Controls.Add(this.label1);
            this.pnTestDropshadow.GradientColor1 = System.Drawing.Color.AliceBlue;
            this.pnTestDropshadow.GradientColor2 = System.Drawing.Color.LightSteelBlue;
            this.pnTestDropshadow.Location = new System.Drawing.Point(12, 12);
            this.pnTestDropshadow.Name = "pnTestDropshadow";
            this.pnTestDropshadow.Size = new System.Drawing.Size(142, 71);
            this.pnTestDropshadow.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(142, 71);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wow, this \"UI\" looks legitimately terrible.  But I\'m just playing around with dif" +
    "ferent possibilities at this point.";
            // 
            // spectrogram1
            // 
            this.spectrogram1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spectrogram1.Location = new System.Drawing.Point(0, 0);
            this.spectrogram1.Name = "spectrogram1";
            this.spectrogram1.Size = new System.Drawing.Size(731, 524);
            this.spectrogram1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 625);
            this.Controls.Add(this.splitMain);
            this.Name = "MainForm";
            this.Text = "wow such signal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnTestDropshadow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Visualization.Spectrogram spectrogram1;
        private System.Windows.Forms.SplitContainer splitMain;
        private Controls.Basic.GradientPanel pnTestDropshadow;
        private System.Windows.Forms.Label label1;
        private Controls.Basic.GradientPanel gradientPanel1;
    }
}

