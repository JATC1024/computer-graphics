using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphics_drawing;
using Point = Graphics_drawing.Point;

namespace Graphics_drawing_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.MaximizeBox = false;            
            this.drawingBox.Image = new Bitmap(this.drawingBox.Size.Width, this.drawingBox.Size.Height);
            this.algoChoices.SelectionMode = SelectionMode.One;
        }              
     
        private void algoChoices_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < this.algoChoices.Items.Count; i++)
                    if (e.Index != i) algoChoices.SetItemChecked(i, false);
            }
        }

        private void shapeChoices_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < this.shapeChoices.Items.Count; i++)
                    if (e.Index != i) shapeChoices.SetItemChecked(i, false);                
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            int algo = GetCheckItem(algoChoices); // The index of the algorithm that the user chooses.
            if(algo == -1) // No algorithm is chosen.
            {
                MessageBox.Show("You have to select an algorithm!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int shape = GetCheckItem(shapeChoices); // The index of the shape that the user chooses.            
            if(shape == -1) // No shape is chosen.
            {
                MessageBox.Show("You have to select a shape!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Drawer drawer = null; // Declares a new Drawer.
            switch (algo) // Decides the drawer.
            {
                case 0:
                    drawer = new DDA(this.drawingBox.Size.Width, this.drawingBox.Size.Height);
                    break;
                case 1: // Bresenham algorithm.
                    drawer = new Bresenham(this.drawingBox.Size.Width, this.drawingBox.Size.Height);
                    break;
                case 2: // MidPoint algorithm.
                    drawer = new MidPoint(this.drawingBox.Size.Width, this.drawingBox.Size.Height);
                    break;                                    
            }
            List<Point> pixels = null; // List of the pixels that need to be drawn.
            switch (shape) // Decides the shape.
            {
                case 0: // Line.
                    {
                        if (GetLineEndPoints(out Point A, out Point B) == false) // No input is received.
                            break;
                        pixels = drawer.DrawLine(A, B);
                        break;
                    }                    
                case 1: // Circle.
                    {
                        if (GetCircleInput(out Point center, out int radius) == false) // No input is received.
                            break;
                        pixels = drawer.DrawCircle(center, radius);
                        break;
                    }
                case 2: // Ellipse
                    {
                        if (GetEllipseInput(out Point center, out int firstLength, out int secondLength) == false) // No input is received.
                            break;
                        pixels = drawer.DrawEllipse(center, firstLength, secondLength);
                        break;
                    }
                case 3: // Parabol
                    {
                        if (GetParabolInput(out Point center, out Point border) == false) // No input is received.
                            break;
                        pixels = drawer.DrawParabol(center, border);
                        break;
                    }
                case 4:
                    {
                        if (GetHyperbolInput(out Point center, out int a, out int b) == false) // No input is received.
                            break;
                        pixels = drawer.DrawHyperbol(center, a, b);
                        break;
                    }
            }
            if(pixels != null)
                SetPixels(pixels);
        }

        /// <summary>
        /// Sets all the pixels in the given list.
        /// </summary>
        /// <param name="pixels"> The give list of pixels. </param>
        private void SetPixels(List<Point> pixels)
        {
            for(int i = 0; i < pixels.Count; i++)
            {
                var px = new Point(pixels[i].x, this.drawingBox.Height - pixels[i].y - 1); // The grid is up-side-down.
                if (px.x < 0 || px.x >= drawingBox.Image.Size.Width || px.y < 0 || px.y >= drawingBox.Image.Size.Height) // The pixel is outside the drawing place.
                    continue;
                ((Bitmap)this.drawingBox.Image).SetPixel(px.x, px.y, Color.Red);
            }
            this.drawingBox.Refresh();
        }

        /// <summary>
        /// Returns the index of the checked item in the given CheckedListBox.
        /// </summary>
        /// <param name="checkList"> The given CheckedListBox. </param>
        /// <returns></returns>
        private int GetCheckItem(CheckedListBox checkList)
        {
            for (int i = 0; i < checkList.Items.Count; i++)
                if (checkList.GetItemChecked(i)) return i;
            return -1;
        }

        /// <summary>
        /// Gets two end points of the line.
        /// </summary>
        /// <param name="A"> A the first end point. </param>
        /// <param name="B"> B the second end point. </param>
        /// <returns> True if the end points is successfully obtained, false otherwise. </returns>
        /// /// <returns> True if input is entered, false otherwise. </returns>
        private bool GetLineEndPoints(out Point A, out Point B)
        {            
            var newForm = new LineInput();            
            newForm.ShowDialog();
            if (newForm.InputEntered == false)
            {
                B = A = new Point(0, 0);
                return false;
            }
            A = new Point(newForm.FirstPointX, newForm.FirstPointY);
            B = new Point(newForm.SecondPointX, newForm.SecondPointY);
            return true;
        }

        /// <summary>
        /// Gets the center and the radius of the circle.
        /// </summary>
        /// <param name="center"> The center of the circle. </param>
        /// <param name="radius"> The radius of the circle. </param>
        /// /// <returns> True if input is entered, false otherwise. </returns>
        private bool GetCircleInput(out Point center, out int radius)
        {
            var newForm = new CircleInput();
            newForm.ShowDialog();
            if (newForm.InputEntered == false) // User presses cancel.
            {
                center = new Point(0, 0);
                radius = 0;
                return false;
            }
            center = new Point(newForm.CenterX, newForm.CenterY);
            radius = newForm.Radius;
            return true;
        }

        /// <summary>
        /// Gets the center and the lengths of the Ellipse.
        /// </summary>
        /// <param name="center"> The center of the Ellipse. </param>
        /// <param name="firstLength"> The first length of the Ellipse. </param>
        /// <param name="secondLength"> The second length of the Ellipse. </param>
        /// <returns> True if input is entered, false otherwise. </returns>
        private bool GetEllipseInput(out Point center, out int firstLength, out int secondLength)
        {
            var newForm = new EllipseInput();
            newForm.ShowDialog();
            if (newForm.InputEntered == false)
            {
                center = new Point(0, 0);
                firstLength = secondLength = 0;
                return false;
            }
            center = new Point(newForm.CenterX, newForm.CenterY);
            firstLength = newForm.FirstLength;
            secondLength = newForm.SecondLength;
            return true;
        }

        /// <summary>
        /// Gets the center and the formula of the Parabol.
        /// </summary>
        /// <param name="center"> The center of the Parabol. </param>
        /// <param name="a, b, c"> The formula of the Parabol. </param>
        /// <returns> True if input is entered, false otherwise. </returns>
        private bool GetParabolInput(out Point center, out Point border)
        {
            var newForm = new ParabolInput();
            newForm.ShowDialog();
            if(newForm.InputEntered == false)
            {
                center = new Point(0, 0);
                border = new Point(0, 0);
                return false;
            }
            center = new Point(newForm.CenterX, newForm.CenterY);
            border = new Point(newForm.BorderX, newForm.BorderY);
            return true;
        }

        private bool GetHyperbolInput(out Point center, out int a, out int b)
        {
            var newForm = new HyperbolInput();
            newForm.ShowDialog();
            if(newForm.InputEntered == false)
            {
                center = new Point(0, 0);
                a = b = 0;
                return false;
            }
            center = new Point(newForm.CenterX, newForm.CenterY);
            a = newForm.FirstPrt;
            b = newForm.SecondPrt;
            return true;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            var G = Graphics.FromImage(this.drawingBox.Image);
            G.Clear(Color.White);
            this.drawingBox.Refresh();
        }
    }
}
