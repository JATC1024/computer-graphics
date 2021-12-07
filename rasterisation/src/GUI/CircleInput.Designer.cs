using Point = Graphics_drawing.Point;
namespace Graphics_drawing_GUI
{
    partial class CircleInput
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
            this.centerXText = new System.Windows.Forms.RichTextBox();
            this.centerYText = new System.Windows.Forms.RichTextBox();
            this.richTextBox10 = new System.Windows.Forms.RichTextBox();
            this.richTextBox13 = new System.Windows.Forms.RichTextBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.textFirstPoint = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.radiusText = new System.Windows.Forms.RichTextBox();
            this.drawButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // centerXText
            // 
            this.centerXText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.centerXText.Location = new System.Drawing.Point(91, 11);
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
            this.centerYText.Location = new System.Drawing.Point(161, 10);
            this.centerYText.MaxLength = 4;
            this.centerYText.Multiline = false;
            this.centerYText.Name = "centerYText";
            this.centerYText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.centerYText.Size = new System.Drawing.Size(48, 24);
            this.centerYText.TabIndex = 2;
            this.centerYText.Text = "";
            // 
            // richTextBox10
            // 
            this.richTextBox10.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox10.Location = new System.Drawing.Point(215, 10);
            this.richTextBox10.Name = "richTextBox10";
            this.richTextBox10.ReadOnly = true;
            this.richTextBox10.Size = new System.Drawing.Size(10, 24);
            this.richTextBox10.TabIndex = 18;
            this.richTextBox10.Text = ")";
            this.richTextBox10.Visible = false;
            // 
            // richTextBox13
            // 
            this.richTextBox13.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox13.Location = new System.Drawing.Point(75, 12);
            this.richTextBox13.Name = "richTextBox13";
            this.richTextBox13.ReadOnly = true;
            this.richTextBox13.Size = new System.Drawing.Size(10, 24);
            this.richTextBox13.TabIndex = 19;
            this.richTextBox13.Text = "(";
            this.richTextBox13.Visible = false;
            // 
            // richTextBox6
            // 
            this.richTextBox6.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox6.Location = new System.Drawing.Point(145, 12);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.ReadOnly = true;
            this.richTextBox6.Size = new System.Drawing.Size(10, 24);
            this.richTextBox6.TabIndex = 20;
            this.richTextBox6.Text = ",";
            this.richTextBox6.Visible = false;
            // 
            // textFirstPoint
            // 
            this.textFirstPoint.BackColor = System.Drawing.SystemColors.Control;
            this.textFirstPoint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textFirstPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.textFirstPoint.Location = new System.Drawing.Point(12, 11);
            this.textFirstPoint.Name = "textFirstPoint";
            this.textFirstPoint.ReadOnly = true;
            this.textFirstPoint.Size = new System.Drawing.Size(57, 24);
            this.textFirstPoint.TabIndex = 21;
            this.textFirstPoint.TabStop = false;
            this.textFirstPoint.Text = "Center:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox1.Location = new System.Drawing.Point(12, 41);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(57, 24);
            this.richTextBox1.TabIndex = 21;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "Radius:";
            // 
            // radiusText
            // 
            this.radiusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radiusText.Location = new System.Drawing.Point(91, 40);
            this.radiusText.MaxLength = 4;
            this.radiusText.Multiline = false;
            this.radiusText.Name = "radiusText";
            this.radiusText.Size = new System.Drawing.Size(48, 24);
            this.radiusText.TabIndex = 3;
            this.radiusText.Text = "";
            // 
            // drawButton
            // 
            this.drawButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.drawButton.Location = new System.Drawing.Point(89, 75);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(120, 36);
            this.drawButton.TabIndex = 24;
            this.drawButton.TabStop = false;
            this.drawButton.Text = "Draw!";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // CircleInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 123);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.radiusText);
            this.Controls.Add(this.centerXText);
            this.Controls.Add(this.centerYText);
            this.Controls.Add(this.richTextBox10);
            this.Controls.Add(this.richTextBox13);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textFirstPoint);
            this.MaximizeBox = false;
            this.Name = "CircleInput";
            this.Text = "CircleInput";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox centerXText;
        private System.Windows.Forms.RichTextBox centerYText;
        private System.Windows.Forms.RichTextBox richTextBox10;
        private System.Windows.Forms.RichTextBox richTextBox13;
        private System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.RichTextBox textFirstPoint;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox radiusText;
        private System.Windows.Forms.Button drawButton;
        private int centerX;
        private int centerY;
        private int radius;
        private bool inputEntered = false;

        public int CenterX
        {
            get { return centerX; }
        }

        public int CenterY
        {
            get { return centerY; }
        }

        public int Radius
        {
            get { return radius; }
        }

        public bool InputEntered
        {
            get { return inputEntered; }
        }
    }
}