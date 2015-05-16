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
            this.spectrogram1 = new Controls.Visualization.Spectrogram();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnTestDropshadow = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // spectrogram1
            // 
            this.spectrogram1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spectrogram1.Location = new System.Drawing.Point(0, 0);
            this.spectrogram1.Name = "spectrogram1";
            this.spectrogram1.Size = new System.Drawing.Size(731, 524);
            this.spectrogram1.TabIndex = 0;
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
            this.splitMain.Panel1.Controls.Add(this.pnTestDropshadow);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.spectrogram1);
            this.splitMain.Size = new System.Drawing.Size(731, 625);
            this.splitMain.SplitterDistance = 97;
            this.splitMain.TabIndex = 1;
            // 
            // pnTestDropshadow
            // 
            this.pnTestDropshadow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnTestDropshadow.Location = new System.Drawing.Point(103, 25);
            this.pnTestDropshadow.Name = "pnTestDropshadow";
            this.pnTestDropshadow.Size = new System.Drawing.Size(120, 33);
            this.pnTestDropshadow.TabIndex = 0;
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
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Visualization.Spectrogram spectrogram1;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnTestDropshadow;
    }
}

