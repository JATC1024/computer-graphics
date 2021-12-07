using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace sketch_2d
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private Sequence seq;
        private DragCommand cmd;
        private Canvas cv;
        private bool mouseHold, control;
        public MainForm()
        {
            InitializeComponent();
            seq = new Sequence();
            cv = new Canvas(pictureBox1);
            mouseHold = control = false;
            cmd = null;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Line);
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Rect);
        }

        private void parallelogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Parall);
        }

        private void polygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Polygon);
        }

        private void polylinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Polyline);
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Circle);
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Ellipse);
        }

        private void circleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Circle2);
        }

        private void ellipseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Ellipse2);
        }

        private void circleToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Circle4);
        }

        private void ellipseToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Ellipse4);
        }

        private void parabolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Parabol);
        }

        private void hyperbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Hyper);
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Text);
        }

        private void bezierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Bezier);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseHold = true;
            cmd = new DragCommand(cv);
            if (!cmd.AddStartAndCommit(e.Location, control))
                cmd = null;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseHold = false;
            if (cmd != null)
            {
                cmd.StopEdit();
                if (cmd.Valid())
                    seq.Add(cmd);
                cmd = null;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseHold && cmd != null)
                cmd.EditEndAndCommit(e.Location);
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupCommand cmd0 = new GroupCommand(cv);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void ungroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UngroupCommand cmd0 = new UngroupCommand(cv);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutCommand cmd0 = new CutCommand(cv);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }


        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyCommand cmd0 = new CopyCommand(cv);
            cmd0.Commit();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteCommand cmd0 = new PasteCommand(cv);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCommand cmd0 = new DeleteCommand(cv);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void borderColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BorderColorCommand cmd0 = new BorderColorCommand(cv, colorDialog1.Color);
                bool ok = cmd0.Commit();
                if (ok) seq.Add(cmd0);
            }
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorderLineCommand cmd0 = new BorderLineCommand(cv, DashStyle.Solid);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorderLineCommand cmd0 = new BorderLineCommand(cv, DashStyle.Dash);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void dashDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorderLineCommand cmd0 = new BorderLineCommand(cv, DashStyle.Dot);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void dashDotToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BorderLineCommand cmd0 = new BorderLineCommand(cv, DashStyle.DashDot);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void dashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorderLineCommand cmd0 = new BorderLineCommand(cv, DashStyle.DashDotDot);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            BorderThickCommand cmd0 = new BorderThickCommand(cv, 1);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            BorderThickCommand cmd0 = new BorderThickCommand(cv, 2);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            BorderThickCommand cmd0 = new BorderThickCommand(cv, 3);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            BorderThickCommand cmd0 = new BorderThickCommand(cv, 4);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            BorderThickCommand cmd0 = new BorderThickCommand(cv, 5);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void fillColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                FillColorCommand cmd0 = new FillColorCommand(cv, colorDialog1.Color);
                bool ok = cmd0.Commit();
                if (ok) seq.Add(cmd0);
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                FontCommand cmd0 = new FontCommand(cv, fontDialog1.Font);
                bool ok = cmd0.Commit();
                if (ok) seq.Add(cmd0);
            }
        }

        private void sendBackwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendBackwardCommand cmd0 = new SendBackwardCommand(cv);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void bringForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BringForwardCommand cmd0 = new BringForwardCommand(cv);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendToBackCommand cmd0 = new SendToBackCommand(cv);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BringToFrontCommand cmd0 = new BringToFrontCommand(cv);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                control = false;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmd == null)
                seq.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmd == null)
                seq.Redo();
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCommand cmd0 = new DeleteCommand(cv, true);
            bool ok = cmd0.Commit();
            if (ok) seq.Add(cmd0);
        }

        private void toolStripLine_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Line);
        }

        private void toolStripRect_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Rect);
        }

        private void toolStripParallel_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Parall);
        }

        private void toolStripPolygon_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Polygon);
        }

        private void toolStripPolyline_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Polyline);
        }

        private void toolStripCircle_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Circle);
        }

        private void toolStripHalfCircle_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Circle2);
        }

        private void toolStripQuarCircle_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Circle4);
        }

        private void toolStripEllipse_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Ellipse);
        }

        private void toolStripHalfEllipse_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Ellipse2);
        }

        private void toolStripQuarEllipse_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Ellipse4);
        }

        private void toolStripParabol_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Parabol);
        }

        private void toolStripHyper_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Hyper);
        }

        private void toolStripText_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Text);
        }

        private void toolStripBezi_Click(object sender, EventArgs e)
        {
            cv.ChangeShape(Canvas.ShapeType.Bezier);
        }

        private void fillTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextureDialog dlg = new TextureDialog();
            dlg.ShowDialog();
            if (dlg.bmp != null)
            {
                var cmd0 = new FillTextureCommand(cv, dlg.bmp);
                bool ok = cmd0.Commit();
                if (ok) seq.Add(cmd0);
            }
        }

        private void Escape()
        {
            if (cmd == null)
            {
                int sign = Math.Abs(cv.CancelShape());
                seq.Remove(sign);
            }
            else
            {
                int sign = Math.Abs(cmd.CancelShape());
                if (cmd.Valid())
                    seq.Add(cmd);
                else --sign;
                seq.Remove(sign);
                cmd = null;
            }
            cv.SetUnselect(true);
        }

        private void Clear()
        {
            mouseHold = control = false;
            cmd = null;
            seq.Clear();
            cv.Clear();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Escape();
            if (saveFileDialog1.ShowDialog()==DialogResult.OK && saveFileDialog1.FileName != "")
            {
                System.IO.FileStream file = (System.IO.FileStream)saveFileDialog1.OpenFile();
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(file);
                cv.Save(bw);
                bw.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != "")
            {
                Clear();
                System.IO.FileStream file = (System.IO.FileStream)openFileDialog1.OpenFile();
                System.IO.BinaryReader br = new System.IO.BinaryReader(file);
                cv.Open(br);
                br.Close();
            }
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
            MessageBox.Show("Image has been copied to clipboard.");
        }

        private void saveAsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog2.ShowDialog() == DialogResult.OK && saveFileDialog2.FileName != "")
            {
                System.IO.FileStream file = (System.IO.FileStream)saveFileDialog2.OpenFile();
                string ext = System.IO.Path.GetExtension(saveFileDialog2.FileName);
                Image img = pictureBox1.Image;
                if (ext == ".bmp")
                    img.Save(file, System.Drawing.Imaging.ImageFormat.Bmp);
                else if (ext == ".png")
                    img.Save(file, System.Drawing.Imaging.ImageFormat.Png);
                else if (ext == ".gif")
                    img.Save(file, System.Drawing.Imaging.ImageFormat.Gif);
                else if (ext == ".tiff")
                    img.Save(file, System.Drawing.Imaging.ImageFormat.Tiff);
                else if (ext == ".jpeg")
                    img.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg);
                file.Close();
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Escape();
            else if (e.KeyCode == Keys.ControlKey)
                control = true;
        }
    }
}
