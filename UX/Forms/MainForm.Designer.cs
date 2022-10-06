namespace RocketAI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CanvasImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CanvasImage)).BeginInit();
            this.SuspendLayout();
            // 
            // CanvasImage
            // 
            this.CanvasImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CanvasImage.Location = new System.Drawing.Point(0, 0);
            this.CanvasImage.Name = "CanvasImage";
            this.CanvasImage.Size = new System.Drawing.Size(800, 450);
            this.CanvasImage.TabIndex = 3;
            this.CanvasImage.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CanvasImage);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "RocketAI Thrust Vectoring Test Harness";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.CanvasImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBox CanvasImage;
    }
}