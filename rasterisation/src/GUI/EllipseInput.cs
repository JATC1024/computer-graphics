using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_drawing_GUI
{
    public partial class EllipseInput : Form
    {
        public EllipseInput()
        {
            InitializeComponent();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.centerX = System.Convert.ToInt32(this.centerXText.Text);
                this.centerY = System.Convert.ToInt32(this.centerYText.Text);
                this.firstLength = System.Convert.ToInt32(this.firstLengthText.Text);
                this.secondLength = System.Convert.ToInt32(this.secondLengthText.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("You must enter integers!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(centerX < 0 || centerY < 0)
            {
                MessageBox.Show("Corrdinates must greater or equal 0!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(firstLength <= 0 || secondLength <= 0)
            {
                MessageBox.Show("Lengths must be positive integers!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
            inputEntered = true;
        }
    }
}
