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
    public partial class ParabolInput : Form
    {
        public ParabolInput()
        {
            InitializeComponent();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            try
            {
                centerX = System.Convert.ToInt32(centerXText.Text);
                centerY = System.Convert.ToInt32(centerYText.Text);
                borderX = System.Convert.ToInt32(borderXText.Text);
                borderY = System.Convert.ToInt32(borderYText.Text);                
            }
            catch(FormatException)
            {
                MessageBox.Show("You must enter integers!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            inputEntered = true;
            this.Close();
        }
    }
}
