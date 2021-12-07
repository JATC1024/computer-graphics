namespace Graphics_drawing_GUI
{
    partial class HyperbolInput
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
            this.drawButton = new System.Windows.Forms.Button();
            this.bText = new System.Windows.Forms.RichTextBox();
            this.centerXText = new System.Windows.Forms.RichTextBox();
            this.centerYText = new System.Windows.Forms.RichTextBox();
            this.aText = new System.Windows.Forms.RichTextBox();
            this.richTextBox10 = new System.Windows.Forms.RichTextBox();
            this.richTextBox13 = new System.Windows.Forms.RichTextBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.textFirstPoint = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // drawButton
            // 
            this.drawButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.drawButton.Location = new System.Drawing.Point(63, 102);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(120, 36);
            this.drawButton.TabIndex = 40;
            this.drawButton.TabStop = false;
            this.drawButton.Text = "Draw!";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // bText
            // 
            this.bText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.bText.Location = new System.Drawing.Point(90, 72);
            this.bText.MaxLength = 4;
            this.bText.Multiline = false;
            this.bText.Name = "bText";
            this.bText.Size = new System.Drawing.Size(48, 24);
            this.bText.TabIndex = 4;
            this.bText.Text = "";
            // 
            // centerXText
            // 
            this.centerXText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.centerXText.Location = new System.Drawing.Point(90, 10);
            this.centerXText.MaxLength = 4;
            this.centerXText.Multiline = false;
            this.centerXText.Name = "centerXText";
            this.centerXText.Size = new System.Drawing.Size(48, 24);
            this.centerXText.TabIndex = 1;
            this.centerXText.Text = "";
            // 
            // centerYText
            // 
            this.centerYText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.centerYText.Location = new System.Drawing.Point(160, 10);
            this.centerYText.MaxLength = 4;
            this.centerYText.Multiline = false;
            this.centerYText.Name = "centerYText";
            this.centerYText.Size = new System.Drawing.Size(48, 24);
            this.centerYText.TabIndex = 2;
            this.centerYText.Text = "";
            // 
            // aText
            // 
            this.aText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.aText.Location = new System.Drawing.Point(90, 42);
            this.aText.MaxLength = 4;
            this.aText.Multiline = false;
            this.aText.Name = "aText";
            this.aText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.aText.Size = new System.Drawing.Size(48, 24);
            this.aText.TabIndex = 3;
            this.aText.Text = "";
            // 
            // richTextBox10
            // 
            this.richTextBox10.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox10.Location = new System.Drawing.Point(214, 10);
            this.richTextBox10.Name = "richTextBox10";
            this.richTextBox10.ReadOnly = true;
            this.richTextBox10.Size = new System.Drawing.Size(10, 24);
            this.richTextBox10.TabIndex = 31;
            this.richTextBox10.TabStop = false;
            this.richTextBox10.Text = ")";
            // 
            // richTextBox13
            // 
            this.richTextBox13.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox13.Location = new System.Drawing.Point(74, 11);
            this.richTextBox13.Name = "richTextBox13";
            this.richTextBox13.ReadOnly = true;
            this.richTextBox13.Size = new System.Drawing.Size(10, 24);
            this.richTextBox13.TabIndex = 32;
            this.richTextBox13.TabStop = false;
            this.richTextBox13.Text = "(";
            // 
            // richTextBox6
            // 
            this.richTextBox6.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox6.Location = new System.Drawing.Point(144, 11);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.ReadOnly = true;
            this.richTextBox6.Size = new System.Drawing.Size(10, 24);
            this.richTextBox6.TabIndex = 33;
            this.richTextBox6.TabStop = false;
            this.richTextBox6.Text = ",";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox2.Location = new System.Drawing.Point(12, 72);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(56, 24);
            this.richTextBox2.TabIndex = 34;
            this.richTextBox2.TabStop = false;
            this.richTextBox2.Text = "b:";
            // 
            // textFirstPoint
            // 
            this.textFirstPoint.BackColor = System.Drawing.SystemColors.Control;
            this.textFirstPoint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textFirstPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.textFirstPoint.Location = new System.Drawing.Point(12, 42);
            this.textFirstPoint.Name = "textFirstPoint";
            this.textFirstPoint.ReadOnly = true;
            this.textFirstPoint.Size = new System.Drawing.Size(56, 24);
            this.textFirstPoint.TabIndex = 35;
            this.textFirstPoint.TabStop = false;
            this.textFirstPoint.Text = "a:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(56, 24);
            this.richTextBox1.TabIndex = 41;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "Center:";
            // 
            // HyperbolInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 148);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.bText);
            this.Controls.Add(this.centerXText);
            this.Controls.Add(this.centerYText);
            this.Controls.Add(this.aText);
            this.Controls.Add(this.richTextBox10);
            this.Controls.Add(this.richTextBox13);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.textFirstPoint);
            this.MaximizeBox = false;
            this.Name = "HyperbolInput";
            this.Text = "HyperbolInput";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.RichTextBox bText;
        private System.Windows.Forms.RichTextBox centerXText;
        private System.Windows.Forms.RichTextBox centerYText;
        private System.Windows.Forms.RichTextBox aText;
        private System.Windows.Forms.RichTextBox richTextBox10;
        private System.Windows.Forms.RichTextBox richTextBox13;
        private System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox textFirstPoint;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private bool inputEntered = false;
        private int centerX;
        private int centerY;
        private int firstPrt;
        private int secondPrt;

        public int CenterX
        {
            get { return centerX; }
        }

        public int CenterY
        {
            get { return centerY; }
        }

        public int FirstPrt
        {
            get { return firstPrt; }
        }

        public int SecondPrt
        {
            get { return secondPrt; }
        }

        public bool InputEntered
        {
            get { return inputEntered; }
        }
    }
}