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
    public partial class HyperbolInput : Form
    {
        public HyperbolInput()
        {
            InitializeComponent();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            try
            {
                centerX = System.Convert.ToInt32(centerXText.Text);
                centerY = System.Convert.ToInt32(centerYText.Text);
                firstPrt = System.Convert.ToInt32(aText.Text);
                secondPrt = System.Convert.ToInt32(bText.Text);
            }
            catch(FormatException)
            {
                MessageBox.Show("You must enter integers!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(centerX < 0 || centerY < 0)
            {
                MessageBox.Show("Coordinates must greater or equal 0!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            
            if(firstPrt <= 0 || secondPrt <= 0)
            {
                MessageBox.Show("a and b must be positive integers!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            inputEntered = true;
            this.Close();
        }
    }
}
