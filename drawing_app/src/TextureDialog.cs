using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sketch_2d
{
    public partial class TextureDialog : Form
    {
        public TextureDialog()
        {
            InitializeComponent();
            bmp = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox1.Image;
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox2.Image;
            Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox3.Image;
            Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox4.Image;
            Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox5.Image;
            Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox6.Image;
            Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox7.Image;
            Close();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox8.Image;
            Close();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox9.Image;
            Close();
        }
    }
}
