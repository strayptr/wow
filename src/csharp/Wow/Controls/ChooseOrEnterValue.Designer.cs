namespace Controls
{
    partial class ChooseOrEnterValue
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbValue
            // 
            this.tbValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbValue.Location = new System.Drawing.Point(0, 0);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(159, 20);
            this.tbValue.TabIndex = 0;
            // 
            // ChooseOrEnterValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbValue);
            this.Name = "ChooseOrEnterValue";
            this.Size = new System.Drawing.Size(159, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbValue;
    }
}
