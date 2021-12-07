using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace sketch_2d
{
    class Canvas
    {
        private const int LEN = 10;
        public enum ShapeType { None = 0, Line = 1, Parall = 2, Rect = 4, Polygon = 5, Polyline = 3,
            Circle = 10, Circle2 = 12, Circle4 = 14, Ellipse = 20, Ellipse2 = 22, Ellipse4 = 24,
            Parabol = 9, Hyper = 8, Text = 65, Bezier = 30 };

        private PictureBox picture;
        private Bitmap map0, map1;
        private LinkedList<Frame> obj, cache;
        private ShapeType shape;
        private Frame active;
        private TextFrame box;
        private bool creating, doub;

        public void SetUnselect(bool draw = false)
        {
            foreach (Frame x in obj)
                x.IsSelected = false;
            if (box != null)
            {
                picture.Controls.Remove(box.TextBox);
                picture.FindForm().KeyPreview = true;
                box = null;
            }
            active = null;
            if (draw)
                DrawAll();
        }
        private bool OnePointShape()
        {
            return OnePointShape(shape);
        }
        public static bool OnePointShape(ShapeType shape)
        {
            return shape == ShapeType.Polygon || shape == ShapeType.Polyline || shape == ShapeType.Bezier;
        }
        private bool AddShape(Point loc)
        {
            SetUnselect();

            Frame x = null;
            if (!OnePointShape())
            {
                if (shape == ShapeType.Circle)
                    x = new CircleFrame(loc.X, loc.Y, LEN);
                else if (shape == ShapeType.Circle2)
                    x = new HalfCircleFrame(loc.X, loc.Y, LEN);
                else if (shape == ShapeType.Circle4)
                    x = new QuarCircleFrame(loc.X, loc.Y, LEN);
                else if (shape == ShapeType.Ellipse)
                    x = new EllipseFrame(loc.X, loc.Y, LEN, LEN);
                else if (shape == ShapeType.Ellipse2)
                    x = new HalfEllipseFrame(loc.X, loc.Y, LEN, LEN);
                else if (shape == ShapeType.Ellipse4)
                    x = new QuarEllipseFrame(loc.X, loc.Y, LEN, LEN);
                else if (shape == ShapeType.Hyper)
                    x = new HyperbolFrame(loc.X, loc.Y, LEN, LEN);
                else if (shape == ShapeType.Line)
                    x = new LineFrame(loc, new Point(loc.X + LEN, loc.Y + LEN));
                else if (shape == ShapeType.Parabol)
                    x = new ParabolFrame(loc, new Point(loc.X + LEN, loc.Y + LEN));
                else if (shape == ShapeType.Parall)
                    x = new ParallelFrame(loc.X, loc.Y, LEN, LEN);
                else if (shape == ShapeType.Rect)
                    x = new RectFrame(loc.X, loc.Y, LEN, LEN);
                else if (shape == ShapeType.Text)
                    x = new TextFrame(loc.X, loc.Y, LEN, LEN);
            }
            else
            {
                if (creating)
                {
                    try
                    {
                        bool completed = ((PolylineFrame)obj.Last.Value).Add(loc);
                        if (completed)
                        {
                            creating = false;
                            shape = ShapeType.None;
                            obj.Last.Value.IsSelected = true;
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                }
                if (shape == ShapeType.Polygon)
                    x = new PolygonFrame(loc);
                else if (shape == ShapeType.Polyline)
                    x = new PolylineFrame(loc);
                else if (shape == ShapeType.Bezier)
                    x = new BezierFrame(loc);
            }
            if (x != null)
            {
                obj.AddLast(x);
                creating = true;
                return true;
            }
            return false;
        }
        private void EditShape(Point start, Point end)
        {
            var x = obj.Last.Value;
            if (OnePointShape() || shape == ShapeType.Line || shape == ShapeType.Parabol)
                ((PolylineFrame)x).LastVertice = end;
            else
            {
                int temp;
                if (start.X > end.X)
                {
                    temp = start.X;
                    start.X = end.X;
                    end.X = temp;
                }
                if (start.Y > end.Y)
                {
                    temp = start.Y;
                    start.Y = end.Y;
                    end.Y = temp;
                }
                x.Resize(start, end.X - start.X, end.Y - start.Y);
            }
        }
        private void DrawAll()
        {
            Graphics graph = Graphics.FromImage(map1);
            graph.Clear(Color.White);
            for(var x = obj.First; x!=null; x = x.Next)
                if (x.Value != box)
                    x.Value.Draw(map1);

            Bitmap temp = map0;
            map0 = map1;
            map1 = temp;

            picture.Image = map0;
            picture.Refresh();
        }
        private void Insert(LinkedList<int> pos, LinkedList<Frame> rope)
        {
            var ptr = pos.First;
            int c = 0;
            for (var x = obj.First; ptr != null; x = x.Next, ++c)
            {
                if (c == ptr.Value)
                {
                    var node = rope.First;
                    rope.RemoveFirst();
                    if (x == null)
                        obj.AddLast(node);
                    else
                        obj.AddBefore(x, node);
                    x = node;
                    ptr = ptr.Next;
                }
            }
        }
        private LinkedList<Frame> Extract(LinkedList<int> pos)
        {
            LinkedList<Frame> res = new LinkedList<Frame>();
            var ptr = pos.Last;
            int c = obj.Count - 1;
            for (var x = obj.Last; x != null && ptr!=null; --c)
            {
                var y = x.Previous;
                if (c == ptr.Value)
                {
                    obj.Remove(x);
                    res.AddFirst(x);
                    ptr = ptr.Previous;
                }
                x = y;
            }
            return res;
        }

        public Canvas(PictureBox pic)
        {
            obj = new LinkedList<Frame>();
            cache = new LinkedList<Frame>();
            shape = ShapeType.None;
            picture = pic;
            map0 = new Bitmap(pic.Width, pic.Height);
            map1 = new Bitmap(pic.Width, pic.Height);
            creating = doub = false;
            active = box = null;
            DrawAll();
        }
        public void Clear()
        {
            if (box != null)
            {
                picture.Controls.Remove(box.TextBox);
                picture.FindForm().KeyPreview = true;
                box = null;
            }
            active = null;
            shape = ShapeType.None;
            creating = doub = false;
            obj.Clear();
            cache.Clear();
            DrawAll();
        }
        public void Save(System.IO.BinaryWriter bw)
        {
            bw.Write((ushort)obj.Count);
            for(var x = obj.First; x!=null; x=x.Next)
            {
                Frame a = x.Value;
                if (a is TextFrame)
                    bw.Write((byte)ShapeType.Text);
                else if (a is BezierFrame)
                    bw.Write((byte)ShapeType.Bezier);
                else if (a is ParabolFrame)
                    bw.Write((byte)ShapeType.Parabol);
                else if (a is LineFrame)
                    bw.Write((byte)ShapeType.Line);
                else if (a is HyperbolFrame)
                    bw.Write((byte)ShapeType.Hyper);
                else if (a is RectFrame)
                    bw.Write((byte)ShapeType.Rect);
                else if (a is ParallelFrame)
                    bw.Write((byte)ShapeType.Parall);
                else if (a is PolygonFrame)
                    bw.Write((byte)ShapeType.Polygon);
                else if (a is PolylineFrame)
                    bw.Write((byte)ShapeType.Polyline);
                else if (a is CircleFrame)
                    bw.Write((byte)ShapeType.Circle);
                else if (a is HalfCircleFrame)
                    bw.Write((byte)ShapeType.Circle2);
                else if (a is QuarCircleFrame)
                    bw.Write((byte)ShapeType.Circle4);
                else if (a is HalfEllipseFrame)
                    bw.Write((byte)ShapeType.Ellipse2);
                else if (a is QuarEllipseFrame)
                    bw.Write((byte)ShapeType.Ellipse4);
                else if (a is EllipseFrame)
                    bw.Write((byte)ShapeType.Ellipse);
            }
            for (var x = obj.First; x != null; x = x.Next)
                x.Value.Save(bw.BaseStream);
        }
        public void Open(System.IO.BinaryReader br)
        {
            ushort size = br.ReadUInt16();
            var list = new List<byte>();
            for (ushort i = 0; i < size; ++i)
                list.Add(br.ReadByte());
            
            Frame a;
            for (ushort i=0; i<size; ++i)
            {
                byte type = list[i];
                if (type == (byte)ShapeType.Text)
                    a = new TextFrame();
                else if (type == (byte)ShapeType.Bezier)
                    a = new BezierFrame();
                else if (type == (byte)ShapeType.Parabol)
                    a = new ParabolFrame();
                else if (type == (byte)ShapeType.Line)
                    a = new LineFrame();
                else if (type == (byte)ShapeType.Hyper)
                    a = new HyperbolFrame();
                else if (type == (byte)ShapeType.Rect)
                    a = new RectFrame();
                else if (type == (byte)ShapeType.Parall)
                    a = new ParallelFrame();
                else if (type == (byte)ShapeType.Polygon)
                    a = new PolygonFrame();
                else if (type == (byte)ShapeType.Polyline)
                    a = new PolylineFrame();
                else if (type == (byte)ShapeType.Circle)
                    a = new CircleFrame();
                else if (type == (byte)ShapeType.Circle2)
                    a = new HalfCircleFrame();
                else if (type == (byte)ShapeType.Circle4)
                    a = new QuarCircleFrame();
                else if (type == (byte)ShapeType.Ellipse2)
                    a = new HalfEllipseFrame();
                else if (type == (byte)ShapeType.Ellipse4)
                    a = new QuarEllipseFrame();
                else if (type == (byte)ShapeType.Ellipse)
                    a = new EllipseFrame();
                else
                    a = null;

                if (a != null)
                    obj.AddLast(a.Load(br.BaseStream));
            }
            DrawAll();
        }
        public bool Available()
        {
            return !creating && active == null;
        }
        public void ChangeShape(ShapeType shp)
        {
            if (!creating)
                shape = shp;
        }
        public int CancelShape()
        {
            int sign = 0;
            if (creating)
            {
                if (shape == ShapeType.Bezier || shape == ShapeType.Polyline)
                {
                    sign = ((PolylineFrame)obj.Last.Value).SetComplete();
                    if (sign < 0)
                    {
                        obj.RemoveLast();
                        DrawAll();
                    }
                    else
                    {
                        obj.Last.Value.IsSelected = true;
                        DrawAll();
                    }
                }
                else
                {
                    obj.RemoveLast();
                    sign = -1;
                    DrawAll();
                }
                creating = false;
            }
            shape = ShapeType.None;
            return sign;
        }
        public ShapeType GetShape()
        {
            return shape;
        }
        
        public Frame Select(Point loc, bool control = false)
        {
            if (shape != ShapeType.None)
            {
                bool edit = AddShape(loc);
                if (edit)
                    DrawAll();
                return edit ? obj.Last.Value : null;
            }

            if (box != null)
            {
                picture.Controls.Remove(box.TextBox);
                picture.FindForm().KeyPreview = true;
                box = null;
            }
            active = null;
            for (var x = obj.Last; x != null && active == null; x = x.Previous)
                if (x.Value.IsClicked(loc))
                    active = x.Value;
            if (!control)
                foreach (Frame x in obj)
                    if (x != active)
                        x.IsSelected = false;

            if (active != null)
            {
                doub = active.IsSelected;
                active.IsSelected = true;
            }

            DrawAll();
            return active;
        }
        public void Unselect(Point start, Point end)
        {
            if (shape != ShapeType.None && !OnePointShape())
            {
                creating = false;
                shape = ShapeType.None;
                if (obj.Last.Value is LineFrame || obj.Last.Value is ParabolFrame)
                    ((PolylineFrame)obj.Last.Value).SetComplete();
                obj.Last.Value.IsSelected = true;
                DrawAll();
            }
            else if (shape == ShapeType.None && active != null)
            {
                active.Drag(start, end);
                if (doub && active is TextFrame && start == end)
                {
                    foreach (var x in obj)
                        x.IsSelected = false;
                    box = (TextFrame)active;
                    picture.Controls.Add(box.TextBox);
                    doub = false;
                }
                active = null;
                DrawAll();
            }
        }
        public Point Drag(Point start, Point end)
        {
            if (shape != ShapeType.None)
            {
                EditShape(start, end);
                DrawAll();
                return end;
            }
            else if (active != null && active != box)
            {
                Point real_end = active.FakeDrag(start, end);
                DrawAll();
                return real_end;
            }
            return Frame.NullPoint;            
        }
        public void Rewind(ShapeType shp)
        {
            if (shp != ShapeType.None)
            {
                if (OnePointShape(shp))
                {
                    bool ok = ((PolylineFrame)obj.Last.Value).Rewind();
                    if (!ok)
                    {
                        obj.RemoveLast();
                        shape = ShapeType.None;
                        creating = false;
                    }
                    else
                    {
                        shape = shp;
                        creating = true;
                        SetUnselect();
                    }
                }
                else
                {
                    obj.RemoveLast();
                    shape = ShapeType.None;
                    creating = false;
                }
                DrawAll();
            }
        }

        public void Group(LinkedList<int> list)
        {
            if (list.Count < 2) return;
            var group = Extract(list);
            obj.AddLast(new GroupFrame(group));
            obj.Last.Value.IsSelected = true;
            DrawAll();
        }
        public void GroupRewind(LinkedList<int> pos)
        {
            var node = obj.Last;
            obj.RemoveLast();
            LinkedList<Frame> group = ((GroupFrame)node.Value).Frames;
            Insert(pos, group);
            DrawAll();
        }
        public void Ungroup(LinkedList<Tuple<int,int>> list)
        {
            var ptr = list.Last;
            int c = obj.Count - 1;
            for(var x=obj.Last; x!=null && ptr!=null; --c)
            {
                var y = x.Previous;
                if (c == ptr.Value.Item1)
                {
                    LinkedList<Frame> group = ((GroupFrame)x.Value).Frames;
                    while (group.Count > 0)
                    {
                        var node = group.Last;
                        group.RemoveLast();
                        obj.AddAfter(x, node);
                    }
                    obj.Remove(x);
                    ptr = ptr.Previous;
                }
                x = y;
            }
            DrawAll();
        }
        public void UngroupRewind(LinkedList<Tuple<int, int>> list)
        {
            var ptr = list.First;
            int c = 0;
            for(var x = obj.First; x!=null && ptr!=null; x=x.Next, ++c)
            {
                if (c == ptr.Value.Item1)
                {
                    var pre = x.Previous;
                    LinkedList<Frame> temp = new LinkedList<Frame>();
                    for(int i=0; i<ptr.Value.Item2; ++i)
                    {
                        var node = pre.Next;
                        obj.Remove(node);
                        temp.AddLast(node);
                    }
                    obj.AddAfter(pre, new GroupFrame(temp));
                    x = pre.Next;
                    ptr = ptr.Next;
                }
            }
            DrawAll();
        }

        public void BorderColor(LinkedList<Tuple<int, Color>> list, Color color)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.BorderColor = color;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void BorderColor(LinkedList<Tuple<int, Color>> list)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr!=null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.BorderColor = ptr.Value.Item2;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void Border(LinkedList<Tuple<int,DashStyle>> list, DashStyle border)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.BorderType = border;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void Border(LinkedList<Tuple<int, DashStyle>> list)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.BorderType = ptr.Value.Item2;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void BorderThick(LinkedList<Tuple<int, int>> list, int thick)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.BorderWidth = thick;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void BorderThick(LinkedList<Tuple<int, int>> list)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.BorderWidth = ptr.Value.Item2;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void FillColor(LinkedList<Tuple<int, Color>> list, Color color)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.ShapeColor = color;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void FillColor(LinkedList<Tuple<int, Color>> list)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.ShapeColor = ptr.Value.Item2;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void FillTexture(LinkedList<Tuple<int, Bitmap>> list, Bitmap texture)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.Texture = texture;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void FillTexture(LinkedList<Tuple<int, Bitmap>> list)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    x.Value.Texture = ptr.Value.Item2;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void SetFont(LinkedList<Tuple<int, Font>> list, Font font)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    ((TextFrame)x.Value).TextFont = font;
                    ptr = ptr.Next;
                }
            DrawAll();
        }
        public void SetFont(LinkedList<Tuple<int, Font>> list)
        {
            var ptr = list.First;
            int c = 0;
            for (var x = obj.First; x != null && ptr != null; x = x.Next, ++c)
                if (c == ptr.Value.Item1)
                {
                    ((TextFrame)x.Value).TextFont = ptr.Value.Item2;
                    ptr = ptr.Next;
                }
            DrawAll();
        }

        public void SendBackward(LinkedList<int> list)
        {
            LinkedListNode<Frame> pre = null;
            var ptr = list.First;
            int c = 0;
            for(var x=obj.First; x!=null && ptr!=null; ++c)
            {
                var y = x.Next;
                if (c == ptr.Value)
                {
                    if (pre != null)
                    {
                        obj.Remove(x);
                        obj.AddBefore(pre, x);
                        --ptr.Value;
                    }
                    ptr = ptr.Next;
                }
                else pre = x;
                x = y;
            }
            DrawAll();
        }
        public void BringForward(LinkedList<int> list)
        {
            LinkedListNode<Frame> nxt = null;
            var ptr = list.Last;
            int c = obj.Count - 1;
            for(var x=obj.Last; x!=null && ptr!=null; --c)
            {
                var y = x.Previous;
                if (c == ptr.Value)
                {
                    if (nxt != null)
                    {
                        obj.Remove(x);
                        obj.AddAfter(nxt, x);
                        ++ptr.Value;
                    }
                    ptr = ptr.Previous;
                }
                else nxt = x;
                x = y;
            }
            DrawAll();
        }
        public void SendToBack(LinkedList<int> list)
        {
            var head = Extract(list);
            for(var x = head.Last; x!=null;)
            {
                var y = x.Previous;
                head.Remove(x);
                obj.AddFirst(x);
                x = y;
            }
            DrawAll();
        }
        public void SendToBackRewind(LinkedList<int> list)
        {
            LinkedList<Frame> head = new LinkedList<Frame>();
            for (int i = 0; i < list.Count; ++i)
            {
                var node = obj.First;
                obj.RemoveFirst();
                head.AddLast(node);
            }
            Insert(list, head);
            DrawAll();
        }
        public void BringToFront(LinkedList<int> list)
        {
            var tail = Extract(list);
            for (var x = tail.First; x!=null; )
            {
                var y = x.Next;
                tail.Remove(x);
                obj.AddLast(x);
                x = y;
            }
            DrawAll();
        }
        public void BringToFrontRewind(LinkedList<int> list)
        {
            LinkedList<Frame> tail = new LinkedList<Frame>();
            for (int i = 0; i < list.Count; ++i)
            {
                var node = obj.Last;
                obj.RemoveLast();
                tail.AddFirst(node);
            }
            Insert(list, tail);
            DrawAll();
        }

        public void Copy(LinkedList<int> pos)
        {
            var ptr = pos.First;
            int c = 0;
            cache.Clear();
            for (var x = obj.First; x != null && ptr!=null; x = x.Next, ++c)
                if (c == ptr.Value)
                {
                    cache.AddLast(x.Value.Clone());
                    ptr = ptr.Next;
                }
        }
        public LinkedList<Frame> Delete(LinkedList<int> list)
        {
            var rope = Extract(list);
            DrawAll();
            return rope;
        }
        public void DeleteRewind(LinkedList<int> pos, LinkedList<Frame> rope)
        {
            Insert(pos, rope);
            DrawAll();
        }
        public LinkedList<Frame> Cut(LinkedList<int> list)
        {
            Copy(list);
            return Delete(list);
        }
        public void Paste()
        {
            SetUnselect();
            for (var x = cache.First; x != null; x = x.Next)
                obj.AddLast(x.Value.Clone());
            DrawAll();
        }
        public void Unpaste(int size)
        {
            int i = 0;
            for(var x=obj.Last; x!=null; x=x.Previous, ++i)
                x.Value.IsSelected = i < size;
            CutCommand cmd = new CutCommand(this);
            cmd.Commit();
            DrawAll();
        }

        public LinkedList<int> ExportAll()
        {
            var list = new LinkedList<int>();
            for (int i = 0; i < obj.Count; ++i)
                list.AddLast(i);
            return list;
        }
        public LinkedList<int> ExportSelected()
        {
            var list = new LinkedList<int>();
            int c = 0;
            for (var x = obj.First; x != null; x = x.Next, ++c)
                if (x.Value.IsSelected)
                    list.AddLast(c);
            return list;
        }
        public LinkedList<Tuple<int, int>> ExportSelectedCount()
        {
            var list = new LinkedList<Tuple<int, int>>();
            int c = 0;
            for (var x = obj.First; x != null; x = x.Next, ++c)
                if (x.Value is GroupFrame && x.Value.IsSelected)
                    list.AddLast(new Tuple<int, int>(c, ((GroupFrame)x.Value).Frames.Count));
            return list;
        }
        public LinkedList<Tuple<int, Color>> ExportSelectedColor()
        {
            var list = new LinkedList<Tuple<int, Color>>();
            int c = 0;
            for (var x = obj.First; x != null; x = x.Next, ++c)
                if (x.Value.IsSelected)
                    list.AddLast(new Tuple<int, Color>(c, x.Value.BorderColor));
            return list;
        }
        public LinkedList<Tuple<int, DashStyle>> ExportSelectedLine()
        {
            var list = new LinkedList<Tuple<int, DashStyle>>();
            int c = 0;
            for (var x = obj.First; x != null; x = x.Next, ++c)
                if (x.Value.IsSelected)
                    list.AddLast(new Tuple<int, DashStyle>(c, x.Value.BorderType));
            return list;
        }
        public LinkedList<Tuple<int, int>> ExportSelectedThick()
        {
            var list = new LinkedList<Tuple<int, int>>();
            int c = 0;
            for (var x = obj.First; x != null; x = x.Next, ++c)
                if (x.Value.IsSelected)
                    list.AddLast(new Tuple<int, int>(c, x.Value.BorderWidth));
            return list;
        }
        public LinkedList<Tuple<int, Color>> ExportSelectedFillColor()
        {
            var list = new LinkedList<Tuple<int, Color>>();
            int c = 0;
            for (var x = obj.First; x != null; x = x.Next, ++c)
                if (x.Value.IsSelected)
                    list.AddLast(new Tuple<int, Color>(c, x.Value.ShapeColor));
            return list;
        }
        public LinkedList<Tuple<int, Bitmap>> ExportSelectedTexture()
        {
            var list = new LinkedList<Tuple<int, Bitmap>>();
            int c = 0;
            for (var x = obj.First; x != null; x = x.Next, ++c)
                if (x.Value.IsSelected)
                    list.AddLast(new Tuple<int, Bitmap>(c, x.Value.Texture));
            return list;
        }
        public LinkedList<Tuple<int, Font>> ExportSelectedFont()
        {
            var list = new LinkedList<Tuple<int, Font>>();
            int c = 0;
            for (var x = obj.First; x != null; x = x.Next, ++c)
                if (x.Value is TextFrame && x.Value.IsSelected)
                    list.AddLast(new Tuple<int, Font>(c, ((TextFrame)x.Value).TextFont));
            return list;
        }
        public int CacheSize()
        {
            return cache.Count;
        }
    }
}
