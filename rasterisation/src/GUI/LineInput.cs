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
    public partial class LineInput : Form
    {
        public LineInput()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the coordinates of the end points of the line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawButton_Click(object sender, EventArgs e)
        {
            try
            {
                firstPointX = System.Convert.ToInt32(firstPointXText.Text);
                firstPointY = System.Convert.ToInt32(firstPointYText.Text);
                secondPointX = System.Convert.ToInt32(secondPointXText.Text);
                secondPointY = System.Convert.ToInt32(secondPointYText.Text);                
            }
            catch (FormatException)
            {
                MessageBox.Show("You must enter integers!", "Invalid format.", MessageBoxButtons.OK, MessageBoxIcon.Warning);                                
            }
            inputEntered = true;
            this.Close();
        }        
    }
}
