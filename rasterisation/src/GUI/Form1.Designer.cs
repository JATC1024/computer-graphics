namespace Graphics_drawing_GUI
{
    partial class Form1
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
            this.drawingBox = new System.Windows.Forms.PictureBox();
            this.algoChoices = new System.Windows.Forms.CheckedListBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.shapeChoices = new System.Windows.Forms.CheckedListBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).BeginInit();
            this.SuspendLayout();
            // 
            // drawingBox
            // 
            this.drawingBox.BackColor = System.Drawing.Color.White;
            this.drawingBox.Location = new System.Drawing.Point(0, 0);
            this.drawingBox.Name = "drawingBox";
            this.drawingBox.Size = new System.Drawing.Size(1019, 693);
            this.drawingBox.TabIndex = 0;
            this.drawingBox.TabStop = false;
            // 
            // algoChoices
            // 
            this.algoChoices.BackColor = System.Drawing.SystemColors.Control;
            this.algoChoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.algoChoices.CheckOnClick = true;
            this.algoChoices.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.algoChoices.FormattingEnabled = true;
            this.algoChoices.Items.AddRange(new object[] {
            "DDA",
            "Bresenham",
            "Midpoint"});
            this.algoChoices.Location = new System.Drawing.Point(1025, 32);
            this.algoChoices.Name = "algoChoices";
            this.algoChoices.Size = new System.Drawing.Size(120, 57);
            this.algoChoices.TabIndex = 2;
            this.algoChoices.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.algoChoices_ItemCheck);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.richTextBox1.Location = new System.Drawing.Point(1025, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(282, 26);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "Choose the algorithm to be performed:";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.richTextBox2.Location = new System.Drawing.Point(1025, 95);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(230, 29);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "Choose the shape to be drawn:";
            // 
            // shapeChoices
            // 
            this.shapeChoices.BackColor = System.Drawing.SystemColors.Control;
            this.shapeChoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.shapeChoices.CheckOnClick = true;
            this.shapeChoices.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.shapeChoices.FormattingEnabled = true;
            this.shapeChoices.Items.AddRange(new object[] {
            "Line",
            "Circle",
            "Ellipse",
            "Parabol",
            "Hyperbol"});
            this.shapeChoices.Location = new System.Drawing.Point(1025, 130);
            this.shapeChoices.Name = "shapeChoices";
            this.shapeChoices.Size = new System.Drawing.Size(120, 95);
            this.shapeChoices.TabIndex = 9;
            this.shapeChoices.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.shapeChoices_ItemCheck);
            // 
            // doneButton
            // 
            this.doneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.doneButton.Location = new System.Drawing.Point(1025, 231);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(137, 39);
            this.doneButton.TabIndex = 10;
            this.doneButton.Text = "Done!";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.clearButton.Location = new System.Drawing.Point(1168, 231);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(137, 39);
            this.clearButton.TabIndex = 10;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1366, 705);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.shapeChoices);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.algoChoices);
            this.Controls.Add(this.drawingBox);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.Text = "Graphics drawing";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox drawingBox;
        private System.Windows.Forms.CheckedListBox algoChoices;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.CheckedListBox shapeChoices;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Button clearButton;
    }
}

