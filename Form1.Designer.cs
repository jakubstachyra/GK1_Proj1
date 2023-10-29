namespace GK_Proj1
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.RelationGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteRelationCheckBox = new System.Windows.Forms.CheckBox();
            this.HorizontalCheckBox = new System.Windows.Forms.CheckBox();
            this.VerticalCheckBox = new System.Windows.Forms.CheckBox();
            this.ModeGroupBox = new System.Windows.Forms.GroupBox();
            this.MoveButton = new System.Windows.Forms.RadioButton();
            this.DeleteButton = new System.Windows.Forms.RadioButton();
            this.EditButton = new System.Windows.Forms.RadioButton();
            this.DrawButton = new System.Windows.Forms.RadioButton();
            this.OffsetGroupBox = new System.Windows.Forms.GroupBox();
            this.OffsetCheckBox = new System.Windows.Forms.CheckBox();
            this.OffsetBar = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.bitMap = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.RelationGroupBox.SuspendLayout();
            this.ModeGroupBox.SuspendLayout();
            this.OffsetGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitMap)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.RelationGroupBox);
            this.panel1.Controls.Add(this.ModeGroupBox);
            this.panel1.Controls.Add(this.OffsetGroupBox);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(615, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 450);
            this.panel1.TabIndex = 1;
            // 
            // RelationGroupBox
            // 
            this.RelationGroupBox.Controls.Add(this.DeleteRelationCheckBox);
            this.RelationGroupBox.Controls.Add(this.HorizontalCheckBox);
            this.RelationGroupBox.Controls.Add(this.VerticalCheckBox);
            this.RelationGroupBox.Location = new System.Drawing.Point(6, 139);
            this.RelationGroupBox.Name = "RelationGroupBox";
            this.RelationGroupBox.Size = new System.Drawing.Size(176, 114);
            this.RelationGroupBox.TabIndex = 6;
            this.RelationGroupBox.TabStop = false;
            this.RelationGroupBox.Text = "Relation";
            // 
            // DeleteRelationCheckBox
            // 
            this.DeleteRelationCheckBox.AutoSize = true;
            this.DeleteRelationCheckBox.Location = new System.Drawing.Point(16, 72);
            this.DeleteRelationCheckBox.Name = "DeleteRelationCheckBox";
            this.DeleteRelationCheckBox.Size = new System.Drawing.Size(102, 19);
            this.DeleteRelationCheckBox.TabIndex = 4;
            this.DeleteRelationCheckBox.Text = "Delete relation";
            this.DeleteRelationCheckBox.UseVisualStyleBackColor = true;
            this.DeleteRelationCheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DeleteRelationCheckBox_MouseClick);
            // 
            // HorizontalCheckBox
            // 
            this.HorizontalCheckBox.AutoSize = true;
            this.HorizontalCheckBox.Location = new System.Drawing.Point(16, 47);
            this.HorizontalCheckBox.Name = "HorizontalCheckBox";
            this.HorizontalCheckBox.Size = new System.Drawing.Size(104, 19);
            this.HorizontalCheckBox.TabIndex = 3;
            this.HorizontalCheckBox.Text = "Add horizontal";
            this.HorizontalCheckBox.UseVisualStyleBackColor = true;
            this.HorizontalCheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HorizontalCheckBox_MouseClick);
            // 
            // VerticalCheckBox
            // 
            this.VerticalCheckBox.AutoSize = true;
            this.VerticalCheckBox.Location = new System.Drawing.Point(16, 22);
            this.VerticalCheckBox.Name = "VerticalCheckBox";
            this.VerticalCheckBox.Size = new System.Drawing.Size(89, 19);
            this.VerticalCheckBox.TabIndex = 2;
            this.VerticalCheckBox.Text = "Add vertical";
            this.VerticalCheckBox.UseVisualStyleBackColor = true;
            this.VerticalCheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.VerticalCheckBox_MouseClick);
            // 
            // ModeGroupBox
            // 
            this.ModeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModeGroupBox.Controls.Add(this.MoveButton);
            this.ModeGroupBox.Controls.Add(this.DeleteButton);
            this.ModeGroupBox.Controls.Add(this.EditButton);
            this.ModeGroupBox.Controls.Add(this.DrawButton);
            this.ModeGroupBox.Location = new System.Drawing.Point(6, 3);
            this.ModeGroupBox.MinimumSize = new System.Drawing.Size(176, 135);
            this.ModeGroupBox.Name = "ModeGroupBox";
            this.ModeGroupBox.Size = new System.Drawing.Size(176, 135);
            this.ModeGroupBox.TabIndex = 1;
            this.ModeGroupBox.TabStop = false;
            this.ModeGroupBox.Text = "Mode";
            // 
            // MoveButton
            // 
            this.MoveButton.AutoSize = true;
            this.MoveButton.Location = new System.Drawing.Point(16, 97);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(55, 19);
            this.MoveButton.TabIndex = 4;
            this.MoveButton.TabStop = true;
            this.MoveButton.Text = "Move";
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MoveButton_MouseClick);
            // 
            // DeleteButton
            // 
            this.DeleteButton.AutoSize = true;
            this.DeleteButton.Location = new System.Drawing.Point(16, 72);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(58, 19);
            this.DeleteButton.TabIndex = 3;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DeleteButton_MouseClick);
            // 
            // EditButton
            // 
            this.EditButton.AutoSize = true;
            this.EditButton.Location = new System.Drawing.Point(16, 47);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(45, 19);
            this.EditButton.TabIndex = 2;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EditButton_MouseClick);
            // 
            // DrawButton
            // 
            this.DrawButton.AutoSize = true;
            this.DrawButton.Checked = true;
            this.DrawButton.Location = new System.Drawing.Point(16, 22);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(52, 19);
            this.DrawButton.TabIndex = 1;
            this.DrawButton.TabStop = true;
            this.DrawButton.Text = "Draw";
            this.DrawButton.UseVisualStyleBackColor = true;
            this.DrawButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawButton_MouseClick);
            // 
            // OffsetGroupBox
            // 
            this.OffsetGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OffsetGroupBox.Controls.Add(this.OffsetCheckBox);
            this.OffsetGroupBox.Controls.Add(this.OffsetBar);
            this.OffsetGroupBox.Location = new System.Drawing.Point(6, 252);
            this.OffsetGroupBox.MaximumSize = new System.Drawing.Size(176, 110);
            this.OffsetGroupBox.Name = "OffsetGroupBox";
            this.OffsetGroupBox.Size = new System.Drawing.Size(176, 92);
            this.OffsetGroupBox.TabIndex = 5;
            this.OffsetGroupBox.TabStop = false;
            this.OffsetGroupBox.Text = "Offset";
            // 
            // OffsetCheckBox
            // 
            this.OffsetCheckBox.AutoSize = true;
            this.OffsetCheckBox.Location = new System.Drawing.Point(16, 22);
            this.OffsetCheckBox.Name = "OffsetCheckBox";
            this.OffsetCheckBox.Size = new System.Drawing.Size(58, 19);
            this.OffsetCheckBox.TabIndex = 5;
            this.OffsetCheckBox.Text = "Offset";
            this.OffsetCheckBox.UseVisualStyleBackColor = true;
            this.OffsetCheckBox.CheckedChanged += new System.EventHandler(this.OffsetCheckBox_CheckedChanged);
            // 
            // OffsetBar
            // 
            this.OffsetBar.LargeChange = 8;
            this.OffsetBar.Location = new System.Drawing.Point(16, 47);
            this.OffsetBar.Maximum = 75;
            this.OffsetBar.Minimum = 10;
            this.OffsetBar.Name = "OffsetBar";
            this.OffsetBar.Size = new System.Drawing.Size(147, 45);
            this.OffsetBar.TabIndex = 0;
            this.OffsetBar.Value = 30;
            this.OffsetBar.Scroll += new System.EventHandler(this.OffsetBar_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(6, 350);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algorithm";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(16, 47);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(147, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Brezenham\'s algorithm";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(16, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(116, 19);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Library algorithm";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // bitMap
            // 
            this.bitMap.BackColor = System.Drawing.Color.White;
            this.bitMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bitMap.Location = new System.Drawing.Point(0, 0);
            this.bitMap.Name = "bitMap";
            this.bitMap.Size = new System.Drawing.Size(800, 450);
            this.bitMap.TabIndex = 2;
            this.bitMap.TabStop = false;
            this.bitMap.Paint += new System.Windows.Forms.PaintEventHandler(this.bitMap_Paint);
            this.bitMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.bitMap_MouseClick);
            this.bitMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.bitMap_MouseDoubleClick);
            this.bitMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bitMap_MouseDown);
            this.bitMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bitMap_MouseMove);
            this.bitMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bitMap_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bitMap);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.RelationGroupBox.ResumeLayout(false);
            this.RelationGroupBox.PerformLayout();
            this.ModeGroupBox.ResumeLayout(false);
            this.ModeGroupBox.PerformLayout();
            this.OffsetGroupBox.ResumeLayout(false);
            this.OffsetGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panel1;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private GroupBox ModeGroupBox;
        private PictureBox bitMap;
        private RadioButton EditButton;
        private RadioButton DrawButton;
        private RadioButton DeleteButton;
        private RadioButton MoveButton;
        private GroupBox OffsetGroupBox;
        private TrackBar OffsetBar;
        private CheckBox OffsetCheckBox;
        private GroupBox RelationGroupBox;
        private CheckBox HorizontalCheckBox;
        private CheckBox VerticalCheckBox;
        private CheckBox DeleteRelationCheckBox;
    }
}