using RichTextBox = System.Windows.Forms.RichTextBox;

namespace Graphics_drawing_GUI
{
    partial class LineInput
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
            this.richTextBox10 = new System.Windows.Forms.RichTextBox();
            this.richTextBox13 = new System.Windows.Forms.RichTextBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.textFirstPoint = new System.Windows.Forms.RichTextBox();
            this.firstPointXText = new System.Windows.Forms.RichTextBox();
            this.firstPointYText = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.secondPointYText = new System.Windows.Forms.RichTextBox();
            this.secondPointXText = new System.Windows.Forms.RichTextBox();
            this.richTextBox8 = new System.Windows.Forms.RichTextBox();
            this.drawButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox10
            // 
            this.richTextBox10.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox10.Location = new System.Drawing.Point(254, 11);
            this.richTextBox10.Name = "richTextBox10";
            this.richTextBox10.ReadOnly = true;
            this.richTextBox10.Size = new System.Drawing.Size(10, 24);
            this.richTextBox10.TabIndex = 12;
            this.richTextBox10.TabStop = false;
            this.richTextBox10.Text = ")";
            // 
            // richTextBox13
            // 
            this.richTextBox13.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox13.Location = new System.Drawing.Point(114, 12);
            this.richTextBox13.Name = "richTextBox13";
            this.richTextBox13.ReadOnly = true;
            this.richTextBox13.Size = new System.Drawing.Size(10, 24);
            this.richTextBox13.TabIndex = 13;
            this.richTextBox13.TabStop = false;
            this.richTextBox13.Text = "(";
            // 
            // richTextBox6
            // 
            this.richTextBox6.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox6.Location = new System.Drawing.Point(184, 13);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.ReadOnly = true;
            this.richTextBox6.Size = new System.Drawing.Size(10, 24);
            this.richTextBox6.TabIndex = 14;
            this.richTextBox6.TabStop = false;
            this.richTextBox6.Text = ",";
            // 
            // textFirstPoint
            // 
            this.textFirstPoint.BackColor = System.Drawing.SystemColors.Control;
            this.textFirstPoint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textFirstPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.textFirstPoint.Location = new System.Drawing.Point(12, 12);
            this.textFirstPoint.Name = "textFirstPoint";
            this.textFirstPoint.ReadOnly = true;
            this.textFirstPoint.Size = new System.Drawing.Size(96, 24);
            this.textFirstPoint.TabIndex = 15;
            this.textFirstPoint.TabStop = false;
            this.textFirstPoint.Text = "First point:";
            // 
            // firstPointXText
            // 
            this.firstPointXText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.firstPointXText.Location = new System.Drawing.Point(130, 12);
            this.firstPointXText.MaxLength = 4;
            this.firstPointXText.Multiline = false;
            this.firstPointXText.Name = "firstPointXText";
            this.firstPointXText.Size = new System.Drawing.Size(48, 24);
            this.firstPointXText.TabIndex = 1;
            this.firstPointXText.Text = "";
            // 
            // firstPointYText
            // 
            this.firstPointYText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.firstPointYText.Location = new System.Drawing.Point(200, 11);
            this.firstPointYText.MaxLength = 4;
            this.firstPointYText.Multiline = false;
            this.firstPointYText.Name = "firstPointYText";
            this.firstPointYText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.firstPointYText.Size = new System.Drawing.Size(48, 24);
            this.firstPointYText.TabIndex = 2;
            this.firstPointYText.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox1.Location = new System.Drawing.Point(12, 42);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(96, 24);
            this.richTextBox1.TabIndex = 15;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "Second point:";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox2.Location = new System.Drawing.Point(184, 43);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(10, 24);
            this.richTextBox2.TabIndex = 14;
            this.richTextBox2.TabStop = false;
            this.richTextBox2.Text = ",";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox3.Location = new System.Drawing.Point(114, 42);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.Size = new System.Drawing.Size(10, 24);
            this.richTextBox3.TabIndex = 13;
            this.richTextBox3.TabStop = false;
            this.richTextBox3.Text = "(";
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox4.Location = new System.Drawing.Point(270, 69);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ReadOnly = true;
            this.richTextBox4.Size = new System.Drawing.Size(10, 24);
            this.richTextBox4.TabIndex = 12;
            this.richTextBox4.Text = ")";
            // 
            // secondPointYText
            // 
            this.secondPointYText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.secondPointYText.Location = new System.Drawing.Point(200, 41);
            this.secondPointYText.MaxLength = 4;
            this.secondPointYText.Multiline = false;
            this.secondPointYText.Name = "secondPointYText";
            this.secondPointYText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.secondPointYText.Size = new System.Drawing.Size(48, 24);
            this.secondPointYText.TabIndex = 4;
            this.secondPointYText.Text = "";
            // 
            // secondPointXText
            // 
            this.secondPointXText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.secondPointXText.Location = new System.Drawing.Point(130, 41);
            this.secondPointXText.MaxLength = 4;
            this.secondPointXText.Multiline = false;
            this.secondPointXText.Name = "secondPointXText";
            this.secondPointXText.Size = new System.Drawing.Size(48, 24);
            this.secondPointXText.TabIndex = 3;
            this.secondPointXText.Text = "";
            // 
            // richTextBox8
            // 
            this.richTextBox8.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.richTextBox8.Location = new System.Drawing.Point(254, 41);
            this.richTextBox8.Name = "richTextBox8";
            this.richTextBox8.ReadOnly = true;
            this.richTextBox8.Size = new System.Drawing.Size(10, 24);
            this.richTextBox8.TabIndex = 12;
            this.richTextBox8.TabStop = false;
            this.richTextBox8.Text = ")";
            // 
            // drawButton
            // 
            this.drawButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.drawButton.Location = new System.Drawing.Point(74, 85);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(120, 36);
            this.drawButton.TabIndex = 18;
            this.drawButton.TabStop = false;
            this.drawButton.Text = "Draw!";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // LineInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 133);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.secondPointXText);
            this.Controls.Add(this.firstPointXText);
            this.Controls.Add(this.secondPointYText);
            this.Controls.Add(this.firstPointYText);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.richTextBox8);
            this.Controls.Add(this.richTextBox10);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox13);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textFirstPoint);
            this.MaximizeBox = false;
            this.Name = "LineInput";
            this.Text = "LineInput";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox10;
        private System.Windows.Forms.RichTextBox richTextBox13;
        private System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.RichTextBox textFirstPoint;
        private System.Windows.Forms.RichTextBox firstPointXText;
        private System.Windows.Forms.RichTextBox firstPointYText;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.RichTextBox secondPointYText;
        private System.Windows.Forms.RichTextBox secondPointXText;
        private System.Windows.Forms.RichTextBox richTextBox8;
        private System.Windows.Forms.Button drawButton;
        private int firstPointX;
        private int firstPointY;
        private int secondPointX;
        private int secondPointY;
        private bool inputEntered = false;

        public int FirstPointX
        {
            get { return firstPointX; }
        }

        public int SecondPointX
        {
            get { return secondPointX; }
        }

        public int FirstPointY
        {
            get { return firstPointY; }
        }

        public int SecondPointY
        {
            get { return secondPointY; }
        }

        public bool InputEntered
        {
            get { return inputEntered; }
        }
    }
}