namespace GK_Proj1
{
    partial class Form
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
            this.bitMap = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.bitMap)).BeginInit();
            this.SuspendLayout();
            // 
            // bitMap
            // 
            this.bitMap.BackColor = System.Drawing.Color.White;
            this.bitMap.Location = new System.Drawing.Point(0, 0);
            this.bitMap.Name = "bitMap";
            this.bitMap.Size = new System.Drawing.Size(647, 451);
            this.bitMap.TabIndex = 0;
            this.bitMap.TabStop = false;
            this.bitMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bitMap_MouseDown);
            this.bitMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bitMap_MouseMove);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Location = new System.Drawing.Point(644, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 451);
            this.panel1.TabIndex = 1;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bitMap);
            this.Name = "Form";
            this.Text = "Polygon Editor";
            ((System.ComponentModel.ISupportInitialize)(this.bitMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox bitMap;
        private Panel panel1;
    }
}