using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace sketch_2d
{
    [Serializable]
    abstract class ControlPoint
    {
        private Point location;        
        private int penWidth;
        private Color backColor;
        private Color borderColor;
        private static readonly int size = 10;

        /// <summary>
        /// Initializes the ControlPoint object with default values.
        /// </summary>
        protected ControlPoint()
        {
            location = new Point(0, 0);
            penWidth = 0;
            backColor = SystemColors.Control;
            borderColor = Color.Gray;
        }
           
        /// <summary>
        /// Initializes the ControlPoint object with the given location and size.
        /// </summary>
        /// <param name="location"> The given location. </param>
        /// <param name="size"> The given size. </param>
        public ControlPoint(Point location, int penWidth, Color backColor, Color borderColor) : this()
        {
            this.location = location;
            this.penWidth = penWidth;
            this.backColor = backColor;
            this.borderColor = borderColor;
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }        

        public int PenWidth
        {
            get { return penWidth; }
            set { penWidth = value; }
        }

        /// <summary>
        /// Draws the control point to the specified Graphics object.
        /// </summary>
        /// <param name="graphics"> The specified Graphics object. </param>
        public void DrawToGraphics(Graphics graphics)
        {
            var rect = new Rectangle(location.X - size / 2, location.Y - size / 2, size, size);
            graphics.FillEllipse(new SolidBrush(backColor), rect);
            graphics.DrawEllipse(new Pen(borderColor, penWidth), rect);
        }

        public abstract void Transform(Point start, Point end, Frame frame);

        /// <summary>
        /// Checks whether a given Point is covered by the ControlPoint or not.
        /// </summary>
        /// <param name="location"> The given Point. </param>
        /// <returns> True if the Point is inside the ControlPoint, false otherwise. </returns>
        public bool IsCovered(Point location)
        {
            return new Rectangle(Location.X - size / 2, Location.Y - size / 2, size, size).Contains(location);
        }

        public abstract ControlPoint Clone();

        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
    }

    class CornerControlPoint : ControlPoint
    {
        public CornerControlPoint() : base() { }
        public CornerControlPoint(Point location, int penWidth, Color backColor, Color borderColor) :
            base(location, penWidth, backColor, borderColor)
        { }

        /// <summary>
        /// Relocates the Frame according to the Drag action of the user.        
        /// </summary>
        /// <param name="end"> The Point where the user releases the mouse button. </param>
        /// <param name="frameLocation"> The original location of the Frame covered by the ControlPoint objects. </param>
        /// <param name="frameSize"> The original Size of the Frame. </param>
        public override void Transform(Point start, Point end, Frame frame)
        {
            frame.Location = new Point(Math.Min(end.X, this.Location.X), Math.Min(end.Y, this.Location.Y));
            frame.FrameSize = new Size(Math.Abs(this.Location.X - end.X), Math.Abs(this.Location.Y - end.Y));
        }        

        public override ControlPoint Clone()
        {
            var res = new CornerControlPoint()
            {
                Location = this.Location,
                PenWidth = this.PenWidth,
                BackColor = this.BackColor,
                BorderColor = this.BorderColor
            };
            return res;
        }
    }    
    class SideControlPoint : ControlPoint
    {
        private AnchorStyles style;
        public SideControlPoint() : base()
        {
            style = 0;
        }

        public SideControlPoint(Point location, int penWidth, Color backColor, Color borderColor, AnchorStyles style) :
            base(location, penWidth, backColor, borderColor)
        {
            this.style = style;
        }

        public AnchorStyles Style
        {
            get { return style; }
            set { style = value; }
        }

        /// <summary>
        /// Relocates the Frame according to the Drag action of the user.     
        /// </summary>
        /// <param name="end"> The point where the user releases the mouse button. </param>
        /// <param name="frameLocation"> The original location of the Frame that is corvered by the ControlPoint objects. </param>
        /// <param name="frameSize"> The orighinal size of the Frame. </param>
        /// <param name="angle"> The angle of the frame. </param>
        public override void Transform(Point start, Point end, Frame frame)
        {
            if(style == AnchorStyles.Top || style == AnchorStyles.Bottom)
            {
                frame.Location = new Point(frame.Location.X, Math.Min(end.Y, this.Location.Y));
                frame.FrameSize = new Size(frame.FrameSize.Width, Math.Abs(end.Y - this.Location.Y));
            }
            else if(style == AnchorStyles.Left || style == AnchorStyles.Right)
            {
                frame.Location = new Point(Math.Min(end.X, this.Location.X), frame.Location.Y);
                frame.FrameSize = new Size(Math.Abs(end.X - this.Location.X), frame.FrameSize.Height);
            }
            else
            {
                throw new Exception("The \"style\" attribute of the SideControlPoint object must not be 0");                
            }
            if(frame.FrameSize.Width == 0)
            {
                int x = 1;
                x++;
            }
        }

        public override ControlPoint Clone()
        {
            var res = new SideControlPoint()
            {
                Location = this.Location,
                PenWidth = this.PenWidth,
                BackColor = this.BackColor,
                BorderColor = this.BorderColor,
                Style = this.Style
            };
            return res;
        }
    }

    class RotateControlPoint : ControlPoint
    {
        public RotateControlPoint() : base() { }
        public RotateControlPoint(Point location, int penWidth, Color backColor, Color borderColor) 
            : base(location, penWidth, backColor, borderColor)
        { }

        /// <summary>
        /// Relocates the frame according to the Drag action of the user.
        /// </summary>
        /// <param name="end"> The point where the user releases the mouse button. </param>
        /// <param name="frameLocation"> The original location of the Frame that is corvered by the ControlPoint objects. </param>
        /// <param name="frameSize"> The orighinal size of the Frame. </param>
        /// <param name="angle"> The angle of the frame. </param>
        public override void Transform(Point start, Point end, Frame frame)
        {            
            var origin = new Point(frame.Location.X + frame.FrameSize.Width / 2, frame.Location.Y + frame.FrameSize.Height / 2);
            var A = new Point(start.X - origin.X, start.Y - origin.Y);
            var B = new Point(end.X - origin.X, end.Y - origin.Y);
            //int cross = A.X * B.Y - A.Y * B.X;
            //float incAngle = (float)Math.Asin(((float)cross) / (Helper.VectorLeng(A) * Helper.VectorLeng(B))) / (float)Math.PI * 180; 
            float incAngle = (float)(Math.Atan2(B.Y, B.X) - Math.Atan2(A.Y, A.X)) / (float)Math.PI * 180;
            frame.Angle += incAngle;
        }

        public override ControlPoint Clone()
        {
            var res = new RotateControlPoint()
            {
                Location = this.Location,
                PenWidth = this.PenWidth,
                BackColor = this.BackColor,
                BorderColor = this.BorderColor
            };
            return res;
        }
    }

    [Serializable]
    class PivotControlPoint : ControlPoint
    {
        public PivotControlPoint() : base() { }
        public PivotControlPoint(Point location, int penWidth, Color backColor, Color borderColor) 
            : base(location, penWidth, backColor, borderColor)
        { }

        /// <summary>
        /// Relocates the frame according to the Drag action of the user.
        /// </summary>
        /// <param name="end"> The point where the user releases the mouse button. </param>
        /// <param name="frameLocation"> The original location of the Frame that is corvered by the ControlPoint objects. </param>
        /// <param name="frameSize"> The orighinal size of the Frame. </param>
        /// <param name="angle"> The angle of the frame. </param>
        public override void Transform(Point start,Point end, Frame frame)
        {
            this.Location = end; // The drag operation won't affect the other values, only the location of the pivot is changed.
        }

        public override ControlPoint Clone()
        {
            var res = new CornerControlPoint()
            {
                Location = this.Location,
                PenWidth = this.PenWidth,
                BackColor = this.BackColor,
                BorderColor = this.BorderColor
            };
            return res;
        }
    }

    [Serializable]
    abstract class Frame : ISerializable
    {        
        private Point location;
        private Size frameSize;
        private float angle;        
        private Color shapeColor;
        private Color borderColor;
        private DashStyle borderType;
        private int borderWidth;
        private bool isSelected;        
        private ControlPoint[] controlPoints;
        private Bitmap texture;
        private Brush fillBrush;
        private Pen outlinePen;
        private Frame fakeFrame;        
        private static readonly int frameThickness = 1;        
        private static readonly int frameCornerWidth = 1;
        private static readonly int rotateCirclePadding = 20;
        private static readonly Color controlPointBorderColor = Color.Gray;
        private static readonly Color controlPointBackColor = SystemColors.Control;
        private static readonly Color rotatePointBorderColor = Color.Orange;
        private static readonly Color rotatePointBackColor = Color.Orange;
        protected static readonly int mouseArea = 5;
        private static int minimumArea = 10;
        public static Point NullPoint = new Point(-1, -1);
        /// <summary>
        /// Initializes a Frame object with all the attributes being default values.
        /// </summary>
        public Frame()
        {            
            frameSize = new Size(0, 0);
            location = new Point(0, 0);
            angle = 0;            
            borderColor = Color.Black;
            shapeColor = Color.Transparent;
            borderType = DashStyle.Solid;
            borderWidth = 1;
            isSelected = false;
            texture = null;
            controlPoints = null;
            fakeFrame = null;
            fillBrush = new SolidBrush(shapeColor);
            outlinePen = new Pen(borderColor, borderWidth)
            {
                DashStyle = BorderType
            };
        }

        public Frame(SerializationInfo info, StreamingContext ctext)
        {
            location = (Point)info.GetValue("location", typeof(Point));
            frameSize = (Size)info.GetValue("frameSize", typeof(Size));
            angle = (float)info.GetValue("angle", typeof(float));
            shapeColor = (Color)info.GetValue("shapeColor", typeof(Color));
            borderColor = (Color)info.GetValue("borderColor", typeof(Color));
            borderType = (DashStyle)info.GetValue("borderType", typeof(DashStyle));
            borderWidth = (int)info.GetValue("borderWidth", typeof(int));                        
            texture = (Bitmap)info.GetValue("texture", typeof(Bitmap));
            if (texture != null)
                fillBrush = new TextureBrush(texture);
            else
                fillBrush = new SolidBrush(shapeColor);
            outlinePen = new Pen(borderColor, borderWidth)
            {
                DashStyle = borderType
            };
        }            

        public virtual Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (Frame)bnr.Deserialize(stream);
        }

        public virtual void Save(Stream stream)
        {
            var bnr = new BinaryFormatter();
            bnr.Serialize(stream, this);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctext)
        {
            info.AddValue("location", location);
            info.AddValue("frameSize", frameSize);
            info.AddValue("angle", angle);
            info.AddValue("shapeColor", shapeColor);
            info.AddValue("borderColor", borderColor);
            info.AddValue("borderType", borderType);
            info.AddValue("borderWidth", borderWidth);            
            info.AddValue("texture", texture);            
        }

        public Frame(Frame other) : this()
        {
            this.frameSize = other.frameSize;
            this.location = other.location;
            this.angle = other.angle;            
            this.borderColor = other.borderColor;
            this.shapeColor = other.shapeColor;
            this.borderType = other.borderType;
            this.borderWidth = other.borderWidth;
            this.isSelected = other.isSelected;
            this.texture = other.texture;
            this.fillBrush = (Brush)other.fillBrush.Clone();            
            this.outlinePen = (Pen)other.outlinePen.Clone();
            this.controlPoints = new ControlPoint[other.controlPoints.Length];
            for (int i = 0; i < this.controlPoints.Length; i++)
                this.controlPoints[i] = other.controlPoints[i].Clone();
        }

        /// <summary>
        /// Initializes a Frame with the specified location, width, height and all the other attributes being default value.
        /// </summary>
        /// <param name="x"> The specified location in x. </param>
        /// <param name="y"> The specified location in y. </param>
        /// <param name="width"> The specifed width. </param>
        /// <param name="height"> The specified height. </param>
        public Frame(int x, int y, int width, int height) : this()
        {
            this.location = new Point(x, y);
            this.frameSize = new Size(width, height);            
        }

        public virtual void GetCorners(ref int minX, ref int minY, ref int maxX, ref int maxY)
        {
            minY = minX = (int)1e9;
            maxY = maxX = 0;
            var arr = new Point[4]
            {
                new Point(this.Location.X, this.Location.Y),
                new Point(this.Location.X + this.FrameSize.Width, this.Location.Y),
                new Point(this.Location.X, this.Location.Y + this.FrameSize.Height),
                new Point(this.Location.X + this.FrameSize.Width, this.Location.Y + this.FrameSize.Height)
            };
            for (int i = 0; i < 4; i++)
                arr[i] = Helper.Rotate(arr[i], GetOrigin(), angle);
            for(int i = 0; i < 4; i++)
            {
                minX = Math.Min(minX, arr[i].X);
                minY = Math.Min(minY, arr[i].Y);
                maxX = Math.Max(maxX, arr[i].X);
                maxY = Math.Max(maxY, arr[i].Y);
            }
        }

        /// <summary>
        /// Initializes a Fram with the specified location, width, height and angle.
        /// </summary>
        /// <param name="x"> The specified location in x. </param>
        /// <param name="y"> The specified location in y. </param>
        /// <param name="width"> The specifed width. </param>
        /// <param name="height"> The specified height. </param>
        /// <param name="angle"> The specified angle. </param>
        public Frame(int x, int y, int width, int height, float angle) : this(x, y, width, height)
        {
            this.angle = angle;
        }

        public Brush FillBrush
        {
            get { return fillBrush; }
            set { fillBrush = value; }
        }

        public Pen OutlinePen
        {
            get { return outlinePen; }
            set { outlinePen = value; }
        }

        /// <summary>
        /// Converts to a Rectangle object.
        /// </summary>
        /// <returns> The converted Rectangle object. </returns>
        public Rectangle ToRect()
        {
            return new Rectangle(location, FrameSize);
        }

        public virtual Bitmap Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                fillBrush = new TextureBrush(value);
            }
        }

        /// <summary>
        /// Checks whether a point is inside the Frame object or not. 
        /// </summary>
        /// <param name="location"> The location of the given point. </param>
        /// <returns> True if the point is inside the Frame or false otherwise. </returns>
        protected virtual bool IsInsideFrame(Point p)
        {            
            var origin = GetOrigin();
            var tmp = Helper.Rotate(p, origin, -angle); // Rotates the given point according to the origin.
            CreateControlPoints();
            foreach (var cp in controlPoints)
                if (cp.IsCovered(tmp))
                    return true;
            return this.ToRect().Contains(tmp);
        }

        public abstract bool IsInsideShape(Point location);
     
        public virtual Color ShapeColor
        {
            get { return shapeColor; }
            set
            {
                shapeColor = value;
                fillBrush = new SolidBrush(value);
            }
        }
      
        public virtual Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                outlinePen.Color = value;
            }
        }      

        public virtual DashStyle BorderType
        {
            get { return borderType; }
            set
            {
                borderType = value;
                outlinePen.DashStyle = value;
            }
        }        

        public virtual int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;                
                outlinePen.Width = value;
            }
        }

        public virtual bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                if (value == false)
                    controlPoints = null;
                else
                    CreateControlPoints();
            }
        }

        /// <summary>
        /// Helper method for createing a ControlPoint object.
        /// </summary>
        /// <param name="i"> Indicates where to create the ControlPoint object. </param>
        /// <param name="j"> Indicates where to create the ControlPoint object. </param>
        /// <returns> The created ControlPoint object. </returns>
        private CornerControlPoint CreateCornerControlPoint(int i, int j)
        {
            var rect = ToRect();
            return new CornerControlPoint(new Point(rect.X + i * frameSize.Width, rect.Y + j * frameSize.Height),
                                        frameCornerWidth,
                                        controlPointBackColor,
                                        controlPointBorderColor);
        }

        /// <summary>
        /// Helper method for createing a ControlPoint object.
        /// </summary>
        /// <param name="i"> Indicates where to create the ControlPoint object. </param>
        /// <param name="j"> Indicates where to create the ControlPoint object. </param>
        /// <param name="style"> Indicates the style of the ControlPoint object. </param>
        /// <returns> The created ControlPoint object. </returns>
        private SideControlPoint CreateSideControlPoint(int i, int j, AnchorStyles style)
        {
            var rect = ToRect();
            return new SideControlPoint(new Point(rect.X + i * frameSize.Width / 2, rect.Y + j * frameSize.Height / 2),
                                        frameCornerWidth,
                                        controlPointBackColor,
                                        controlPointBorderColor,
                                        style);
        }

        /// <summary>
        /// Creates the ControlPoint objects whenever the Frame object is selected (IsSelected set to true).
        /// </summary>
        protected void CreateControlPoints()
        {
            controlPoints = new ControlPoint[9]; // Creates an array of ControlPoint objects.
            int C = 0;
            var frameRect = ToRect();
            // Creates the top left control point.
            controlPoints[C++] = CreateCornerControlPoint(0, 0);
            // Creates the bottom right control point.
            controlPoints[C++] = CreateCornerControlPoint(1, 1);
            // Creates the top right control point.
            controlPoints[C++] = CreateCornerControlPoint(1, 0);
            // Creates the bottom left control point.            
            controlPoints[C++] = CreateCornerControlPoint(0, 1);
            // Creates the top control point.
            controlPoints[C++] = CreateSideControlPoint(1, 0, AnchorStyles.Top);
            // Creates the bottom control point.
            controlPoints[C++] = CreateSideControlPoint(1, 2, AnchorStyles.Bottom);
            // Creates the left control point.
            controlPoints[C++] = CreateSideControlPoint(0, 1, AnchorStyles.Left);
            // Creates the right control point.
            controlPoints[C++] = CreateSideControlPoint(2, 1, AnchorStyles.Right);
            // Creates the rotate control point.                       
            controlPoints[C++] = new RotateControlPoint(new Point(frameRect.X + frameSize.Width / 2,
                                        frameRect.Y - rotateCirclePadding),
                                        frameCornerWidth,
                                        rotatePointBackColor,
                                        rotatePointBorderColor);
        }

        /// <summary>
        /// Draws the shape conatined in the Frame object to the specified Bitmap object.
        /// </summary>
        /// <param name="bmp"> The Bitmap object that will be drawn into. </param>
        protected void DrawShape(Bitmap bmp)
        {
            var graphics = Graphics.FromImage(bmp);
            DrawShape(graphics);
        }

        public void DrawShape(Graphics graphics)
        {
            var state = graphics.Save();
            var matrix = new Matrix(); // Creates a new matrix for rotating.

            // Rotates the image.
            matrix.RotateAt(Angle, new Point(Location.X + FrameSize.Width / 2, Location.Y + FrameSize.Height / 2));
            graphics.Transform = matrix;

            // Draws the shape.
            DrawShapeIntoGraphics(graphics);
            graphics.Restore(state);
        }

        public abstract void DrawShapeIntoGraphics(Graphics graphics);
       
        public Size FrameSize
        {
            get { return frameSize; }
            set { frameSize = value; }
        }      

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }        

        /// <summary>
        /// Gets the origin of the frame.
        /// </summary>
        /// <returns> The origin of the frame. </returns>
        protected Point GetOrigin()
        {
            return new Point(location.X + frameSize.Width / 2, location.Y + frameSize.Height / 2);
        }

        /// <summary>
        /// Draws the shape in the frame and the frame itself into a specified Bitmap object.
        /// </summary>
        /// <param name="bmp"> The specified Bitmap object. </param>
        protected virtual void DrawFrame(Bitmap bmp)
        {          
            var graphics = Graphics.FromImage(bmp); // Creates a graphics object.
            // Rotates the graphics.
            var origin = GetOrigin();
            var matrix = new Matrix();
            matrix.RotateAt(Angle, origin);
            graphics.Transform = matrix;

            // Draws the rectangle that represents the frame.            
            var frameRect = this.ToRect();
            graphics.DrawRectangle(new Pen(Color.Gray, frameThickness), frameRect);
            // Draws the control points.
            foreach (var cp in controlPoints) cp.DrawToGraphics(graphics);
            // Rotates back.
            origin = GetOrigin();
            matrix.RotateAt(-Angle, origin);
            graphics.Transform = matrix;
        }        

        /// <summary>
        /// Draws the Frame object to the specified Bitmap object.
        /// </summary>
        /// <param name="bmp"> The specified Bitmap object. </param>
        public void Draw(Bitmap bmp)
        {
            if(fakeFrame != null)
            {
                fakeFrame.Draw(bmp);
                fakeFrame = null;
                return;
            }
            DrawShape(bmp);
            if (isSelected) DrawFrame(bmp);            
        }

        /// <summary>
        /// Checks whether a Frame is clicked or not.
        /// </summary>
        /// <param name="mouseLocation"> The location of the mouse pointer. </param>
        /// <returns> True if the Frame is clicked or false otherwise. </returns>
        public bool IsClicked(Point mouseLocation)
        {
            if (IsSelected)
                return IsInsideFrame(mouseLocation);
            else
                return IsInsideShape(mouseLocation);
        }

        /// <summary>
        /// Translate the frame according to a given vector.
        /// </summary>
        /// <param name="x"> The value of the vector. </param>
        /// <param name="y"> The value of the vector. </param>
        public void Translate(int x, int y)
        {
            location = new Point(location.X + x, location.Y + y);
        }

        /// <summary>
        /// Changes the Frame according to the Drag operation of the user.
        /// </summary>
        /// <param name="start"> The start point of the Drag operation. </param>
        /// <param name="end"> The end point of the Drag operation. </param>
        /// <returns> True if the Drag operation affects the Frame, false otherwise. </returns>
        public virtual Point Drag(Point start, Point end)
        {            
            if (!IsSelected)
                throw new Exception("The shape must be shelected before performing the drag operation.");
            if (end == this.Location)
                end = new Point(this.location.X + 1, this.Location.Y + 1);
            var origin = GetOrigin();
            var newStart = Helper.Rotate(start, origin, -angle); // Rotates the two points.
            var newEnd = Helper.Rotate(end, origin, -angle); 

            // If one of the control points corvers the start point.
            for (int i = 0; i < controlPoints.Length; i++)
            {
                if (controlPoints[i].IsCovered(newStart))
                {
                    var ct = i + 1 == controlPoints.Length? controlPoints[i] : controlPoints[i ^ 1]; // Gets the opposite control point that handles the Drag.
                    ct.Transform(newStart, newEnd, this);
                    var A = GetOrigin();
                    var B = Helper.Rotate(A, origin, angle);
                    location = new Point(location.X + B.X - A.X, location.Y + B.Y - A.Y);
                    IsSelected = true;
                    if (ct is RotateControlPoint)
                        return Helper.Rotate(ct.Location, GetOrigin(), angle);
                    else if (ct is SideControlPoint)
                    {
                        Point res;
                        var tmp = (SideControlPoint)ct;
                        if (tmp.Style == AnchorStyles.Left || tmp.Style == AnchorStyles.Right)
                            res = Helper.Rotate(new Point(newEnd.X + B.X - A.X, tmp.Location.Y + B.Y - A.Y), GetOrigin(), angle);
                        else
                            res = Helper.Rotate(new Point(tmp.Location.X + B.X - A.X, newEnd.Y + B.Y - A.Y), GetOrigin(), angle);
                        return res;
                    }                        
                    return end;
                }                
            }

            // If the start point is inside the Frame then it's a translate operation.
            if (IsInsideFrame(start))
            {                                
                Translate(end.X - start.X, end.Y - start.Y);
                IsSelected = true; // Reconstructs the ControlPoint objects.
                return end;
            }            
            //IsSelected = false;
            return NullPoint;
        }                               

        public virtual void Resize(Point location, int width, int height)
        {
            if (width < minimumArea || height < minimumArea)
                return;
            this.Location = location;
            this.frameSize = new Size(width, height);
        }        

        public virtual void RotateAt(Point root, float angle)
        {
            var cur = GetOrigin();
            var next = Helper.Rotate(cur, root, angle);
            this.Translate(next.X - cur.X, next.Y - cur.Y);
            this.angle += angle;
        }

        public virtual void TransformByMatrix(Matrix matrix)
        {
            var tmp = matrix.Clone();
            var arr = new Point[2]
            {
                new Point(this.location.X, this.location.Y),                
                new Point(this.location.X + this.frameSize.Width, this.location.Y + this.frameSize.Height)
            };            
            tmp.RotateAt(this.Angle, GetOrigin(), MatrixOrder.Prepend);
            tmp.TransformPoints(arr);            
            tmp.Reset();
            tmp.RotateAt(-this.Angle, new Point((arr[0].X + arr[1].X) / 2, (arr[0].Y + arr[1].Y) / 2));
            tmp.TransformPoints(arr);
            this.location = new Point(arr[0].X, arr[0].Y);
            this.frameSize = new Size(arr[1].X - arr[0].X, arr[1].Y - arr[0].Y);
        }

        public virtual Point FakeDrag(Point start, Point end)
        {
            fakeFrame = this.Clone();
            return fakeFrame.Drag(start, end);                        
        }

        public abstract Frame Clone();        
    }

    [Serializable]
    class RectFrame : Frame
    {
        public RectFrame() : base() { }
        public RectFrame(int x, int y, int width, int height) : base(x, y, width, height) { }
        public RectFrame(int x, int y, int width, int height, float angle) : base(x, y, width, height, angle) { }
        public RectFrame(RectFrame other) : base(other) { }
        public RectFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }
        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (RectFrame)bnr.Deserialize(stream);
        }

        /// <summary>
        /// Checks whether the specified Point object is inside the shape or not.
        /// </summary>
        /// <param name="location"> The specified Point object. </param>
        /// <returns></returns>
        public override bool IsInsideShape(Point location)
        {
            var origin = GetOrigin();
            var p = Helper.Rotate(location, origin, -Angle);
            return this.ToRect().Contains(p);
        }

        /// <summary>
        /// /// Draws the shape in the Frame object using the specified graphics object.
        /// </summary>
        /// <param name="graphics"> The specified graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {
            // Draws the shape.            
            graphics.FillRectangle(FillBrush, this.ToRect());                        
            graphics.DrawRectangle(OutlinePen, this.ToRect());
        }

        public override Frame Clone()
        {
            return new RectFrame(this);
        }        
    }

    [Serializable]
    class EllipseFrame : Frame
    {
        public static readonly float epsilon = (float)1e-3;
        public EllipseFrame() : base() { }
        public EllipseFrame(int x, int y, int width, int height) : base(x, y, width, height) { }
        public EllipseFrame(int x, int y, int width, int height, float angle) : base(x, y, width, height, angle) { }
        public EllipseFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }
        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (EllipseFrame)bnr.Deserialize(stream);
        }

        public EllipseFrame(EllipseFrame other) : base(other) { }
        /// <summary>
        /// Checks wheter a given point is inside an ellipse shape or not.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point is inside the shape, false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            location = Helper.Rotate(location, GetOrigin(), -this.Angle);
            return EllipseFormula(location) <= 0;            
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {
            // Draws the shape.
            graphics.FillEllipse(FillBrush, this.ToRect());
            graphics.DrawEllipse(OutlinePen, this.ToRect());
        }

        /// <summary>
        /// Calculates the value when passing a given point to the formula of the current ellipse.
        /// </summary>
        /// <param name="p"> The given point. </param>
        /// <returns> The calculated value. </returns>
        protected float EllipseFormula(Point p)
        {
            float x = p.X - this.GetOrigin().X;
            float y = p.Y - this.GetOrigin().Y;
            float a = FrameSize.Width / 2;
            float b = FrameSize.Height / 2;
            return (x * x) / (a * a) + (y * y) / (b * b) - 1;
        }

        public override Frame Clone()
        {
            return new EllipseFrame(this);
        }
    }

    [Serializable]
    class CircleFrame : EllipseFrame
    {        
        public CircleFrame() : base() { }
        public CircleFrame(int x, int y, int radius) : base(x, y, radius, radius) { }
        public CircleFrame(int x, int y, int radius, float angle) : base(x, y, radius, radius, angle) { }
        public CircleFrame(CircleFrame other) : base(other) { }
        public CircleFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }
        /// <summary>
        /// Checks wheter a given point is inside a circle shape or not.
        /// </summary>
        /// <param name="p"> The given point. </param>
        /// <returns> True if the given point is inside the shape, false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            return base.IsInsideShape(location);
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {
            base.DrawShapeIntoGraphics(graphics);
        }

        public override void Resize(Point location, int width, int height)
        {
            base.Resize(location, Math.Min(width, height), Math.Min(width, height));
        }
        public override Frame Clone()
        {
            return new CircleFrame(this);
        }
    }

    [Serializable]
    class HalfEllipseFrame : EllipseFrame
    {        
        public HalfEllipseFrame() : base() { }
        public HalfEllipseFrame(int x, int y, int width, int height) : base(x, y, width, height) { }

        public HalfEllipseFrame(int x, int y, int width, int height, float angle) : base(x, y, width, height, angle) { }

        public HalfEllipseFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }
        public HalfEllipseFrame(HalfEllipseFrame other) : base(other) { }
        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (HalfEllipseFrame)bnr.Deserialize(stream);
        }

        /// <summary>
        /// Check whether a given point lies on the current curve or not.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point lies on the current curve (approximately), false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            location = Helper.Rotate(location, GetOrigin(), -this.Angle);
            for (int i = -mouseArea; i <= mouseArea; i++)
                for (int j = -mouseArea; j <= mouseArea; j++)
                {
                    var p = new Point(location.X + i, location.Y + j);
                    if (Math.Abs(EllipseFormula(p)) <= EllipseFrame.epsilon &&
                        p.X >= this.Location.X + this.FrameSize.Width / 2)
                        return true;
                }
            return false;
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {
            if (this.ToRect().Width == 0 || this.ToRect().Height == 0)
                return;
            graphics.DrawArc(OutlinePen, this.ToRect(), -90, 180);
        }

        public override Frame Clone()
        {
            return new HalfEllipseFrame(this);
        }
    }

    [Serializable]
    class HalfCircleFrame : HalfEllipseFrame
    {        
        public HalfCircleFrame() : base() { }
        public HalfCircleFrame(int x, int y, int radius) : base(x, y, radius, radius) { }
        public HalfCircleFrame(int x, int y, int radius, float angle) : base(x, y, radius, radius, angle) { }
        public HalfCircleFrame(HalfCircleFrame other) : base(other) { }
        public HalfCircleFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }

        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (HalfCircleFrame)bnr.Deserialize(stream);
        }
        public override void Resize(Point location, int width, int height)
        {
            var tmp = Math.Min(width, height);
            base.Resize(location, tmp, tmp);
        }

        public override Frame Clone()
        {
            return new HalfCircleFrame(this);
        }
    }

    [Serializable]
    class QuarEllipseFrame : EllipseFrame
    {        
        public QuarEllipseFrame() : base() { }
        public QuarEllipseFrame(int x, int y, int width, int height) : base(x, y, width, height) { }
        public QuarEllipseFrame(int x, int y, int width, int height, float angle) : base(x, y, width, height, angle) { }
        public QuarEllipseFrame(QuarEllipseFrame other) : base(other) { }
        public QuarEllipseFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }

        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (QuarEllipseFrame)bnr.Deserialize(stream);
        }

        /// <summary>
        /// Check whether a given point lies on the current curve or not.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point lies on the current curve (approximately), false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            location = Helper.Rotate(location, GetOrigin(), -this.Angle);
            for (int i = -mouseArea; i <= mouseArea; i++)
                for (int j = -mouseArea; j <= mouseArea; j++)
                {
                    var p = new Point(location.X + i, location.Y + j);
                    if (Math.Abs(EllipseFormula(p)) <= EllipseFrame.epsilon &&
                        p.X >= this.Location.X + this.FrameSize.Width / 2 &&
                        p.Y <= this.Location.Y + this.FrameSize.Height / 2)
                            return true;
                }
            return false;
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {
            if (this.ToRect().Width == 0 || this.ToRect().Height == 0)
                return;
            graphics.DrawArc(OutlinePen, this.ToRect(), -90, 90);
        }

        public override Frame Clone()
        {
            return new QuarEllipseFrame(this);
        }
    }

    [Serializable]
    class QuarCircleFrame : QuarEllipseFrame
    {
        public QuarCircleFrame() : base() { }
        public QuarCircleFrame(int x, int y, int radius) : base(x, y, radius, radius) { }
        public QuarCircleFrame(int x, int y, int radius, float angle) : base(x, y, radius, radius, angle) { }
        public QuarCircleFrame(QuarCircleFrame other) : base(other) { }
        public QuarCircleFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }

        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (QuarCircleFrame)bnr.Deserialize(stream);
        }
        public override void Resize(Point location, int width, int height)
        {
            var tmp = Math.Min(width, height);
            base.Resize(location, tmp, tmp);
        }

        public override Frame Clone()
        {
            return new QuarCircleFrame(this);
        }
    }

    [Serializable]
    class PolylineFrame : Frame
    {
        protected float epsilon = 5;
        protected List<Point> vertices;        
        private bool isComplete;
        private static int dotWidth = 1;
        private static Color dotBackcolor = SystemColors.Control;        
        public PolylineFrame() : base()
        {
            vertices = new List<Point>();
            isComplete = false;
        }

        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (PolygonFrame)bnr.Deserialize(stream);
        }

        public PolylineFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext)
        {
            isComplete = (bool)info.GetValue("isComplete", typeof(bool));
            vertices = (List<Point>)info.GetValue("vertices", typeof(List<Point>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctext)
        {
            base.GetObjectData(info, ctext);
            info.AddValue("isComplete", isComplete);
            info.AddValue("vertices", vertices);
        }

        public PolylineFrame(PolylineFrame other) : base(other)
        {
            this.vertices = new List<Point>(other.vertices);
            this.isComplete = other.isComplete;
        }

        public PolylineFrame(Point start) : this()
        {
            vertices.Add(start);
        }
        
        public Point LastVertice
        {
            get { return vertices[vertices.Count - 1]; }
            set
            {
                if (isComplete)
                    throw new Exception("Cannot change the vertices! The polygon is complete");                
                vertices.RemoveAt(vertices.Count - 1); // Removes the last vertice.                
                vertices.Add(value);
            }
        }

        public Point FirstVertice
        {
            get { return vertices[0]; }
        }

        public bool IsComplete
        {
            get { return isComplete; }
            set { isComplete = value; }
        }

        /// <summary>
        /// This method is called to indicate that the polyline is completely drawn.
        /// </summary>
        /// <returns> True if the shape is successfully completed, false otherwise. </returns>
        public virtual int SetComplete()
        {
            // If there is not enough vertices to form a polyline then returns false.
            if (vertices.Count < 2)
                return -1; ;
            isComplete = true;
            // Calculates the frame that bounds the polyline.
            var minx = (int)1e9;
            var miny = (int)1e9;
            var maxx = 0;
            var maxy = 0;
            foreach(var v in vertices)
            {
                minx = Math.Min(minx, v.X);
                miny = Math.Min(miny, v.Y);
                maxx = Math.Max(maxx, v.X);
                maxy = Math.Max(maxy, v.Y);
            }
            Location = new Point(minx, miny);
            FrameSize = new Size(maxx - minx, maxy - miny);
            return 0;
        }

        /// <summary>
        /// Adds a new vertice to the polyline.
        /// </summary>
        /// <param name="p"> The new vertice to be added. </param>
        /// <returns> True if the added point completes the polyline. </returns>
        public virtual bool Add(Point p)
        {
            if (isComplete)
                throw new Exception("Cannot add more vertice to a complete shape");
            vertices.Add(p);
            return false;
        }

        /// <summary>
        /// Checks whether a given point is close to a line segment that is represented by two ending points.
        /// </summary>
        /// <param name="start"> The first ending point of the line segment. </param>
        /// <param name="end"> The second ending point of the line segment. </param>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point is close to the line, false otherwise. </returns>
        protected bool IsInsideLine(Point start, Point end, Point location)
        {
            float A = start.Y - end.Y;
            float B = end.X - start.X;
            float C = -(A * start.X + B * start.Y);
            for (int i = -mouseArea; i <= mouseArea; i++)
                for (int j = -mouseArea; j <= mouseArea; j++)
                {
                    var p = new Point(location.X + i, location.Y + j);
                    float distance = (float)(Math.Abs(A * p.X + B * p.Y + C) / Math.Sqrt(A * A + B * B));
                    if (distance <= epsilon)
                        return true;
                }
            return false;
        }

        /// <summary>
        /// Checks whether a given point is close to one of the line segments of the polyline.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point is close to one of the line segments, false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            location = Helper.Rotate(location, GetOrigin(), -this.Angle);
            for(int i = 0; i < vertices.Count - 1; i++)
                if(IsInsideLine(vertices[i], vertices[i + 1], location))
                    return true;
            return false;
        }

        public void DrawPoint(Point location, Graphics graphics)
        {
            var d = new CornerControlPoint(location, dotWidth, dotBackcolor, BorderColor);
            d.DrawToGraphics(graphics);            
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {            
            if (vertices.Count == 1)
                DrawPoint(vertices[0], graphics);
            else
                graphics.DrawLines(OutlinePen, vertices.ToArray());
        }

        /// <summary>
        /// Performs the drag operation of the user.
        /// </summary>
        /// <param name="start"> The start point of the drag operation. </param>
        /// <param name="end"> The end point of the drag operation. </param>
        /// <returns> True if the drag operation affects the shape, false otherwise. </returns>
        public override Point Drag(Point start, Point end)
        {
            if (!isComplete)
                throw new Exception("The polyline must be complete before draging");
            var curRect = this.ToRect(); // Gets the frame before dragging.
            var res = base.Drag(start, end);
            var changedRect = this.ToRect(); // Gets the frame after dragging.
            var matrix = Helper.GetTransformMatrix(curRect, changedRect); // Gets the transformation matrix.            
            // Applies the transformation to the vertices.
            var arr = vertices.ToArray();
            matrix.TransformPoints(arr);
            vertices = new List<Point>(arr);
            return res;
        }

        /// <summary>
        /// Draws the frame that bounds the shape into a given Bitmap object.
        /// </summary>
        /// <param name="bmp"> The given Bitmap object. </param>
        protected override void DrawFrame(Bitmap bmp)
        {
            if (!isComplete)
                throw new Exception("The polyline must be complete before draging");
            base.DrawFrame(bmp);
        }

        public override bool IsSelected
        {
            get => base.IsSelected;
            set
            {
                if (isComplete == false && value == true)
                    throw new Exception("The shape must be complete before being selected");
                base.IsSelected = value;
            }
        }        

        public override void TransformByMatrix(Matrix matrix)
        {
            base.TransformByMatrix(matrix);
            var arr = vertices.ToArray();
            matrix.TransformPoints(arr);
            vertices = new List<Point>(arr);
        }

        public override void RotateAt(Point root, float angle)
        {
            base.RotateAt(root, angle);
            for (int i = 0; i < vertices.Count; i++)
                vertices[i] = Helper.Rotate(vertices[i], root, angle);
        }

        public List<Point> Vertices
        {
            get { return vertices; }
            set { vertices = value; }
        }

        public override Frame Clone()
        {
            return new PolylineFrame(this);            
        }        

        public virtual bool Rewind()
        {
            vertices.RemoveAt(vertices.Count - 1);
            isComplete = false;
            return vertices.Count > 0;
        }
    }

    [Serializable]
    class PolygonFrame : PolylineFrame
    {
        public PolygonFrame() : base() { }
        public PolygonFrame(Point start) : base(start) { }
        public PolygonFrame(PolygonFrame other) : base(other) { }
        public PolygonFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }

        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (PolygonFrame)bnr.Deserialize(stream);
        }        

        /// <summary>
        /// Adds a new vertice to the polygon.
        /// </summary>
        /// <param name="p"> The new vertice. </param>
        /// <returns> True if after being added, the vertices form a complete polygon, false otherwise. </returns>
        public override bool Add(Point p)
        {
            if (IsComplete)
                throw new Exception("Cannot add more vertice to a complete shape");
            var tmp = new CornerControlPoint(vertices[0], 1, Color.Black, Color.Black);
            if (tmp.IsCovered(p))
            {
                if (vertices.Count < 3)
                    throw new Exception("A polygon must have atleast 3 vetices!");
                SetComplete();
                return true;
            }                
            return base.Add(p);
        }

        /// <summary>
        /// Checks wheter a given point is close to one of the line segments of the polygon.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point is close to one of the segments of the polygon, false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            var newLocation = Helper.Rotate(location, GetOrigin(), -this.Angle);
            if (IsInsideLine(LastVertice, FirstVertice, newLocation))
                return true;
            return base.IsInsideShape(location);
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {            
            if(IsComplete)
                graphics.DrawPolygon(OutlinePen, vertices.ToArray());
            else
            {
                DrawPoint(vertices[0], graphics);
                if(vertices.Count > 1)
                    graphics.DrawLines(OutlinePen, vertices.ToArray());
            }            
        }

        /// <summary>
        /// This method is called to indicate that the polyline is completely drawn.
        /// </summary>
        /// <returns> True if the shape is successfully completed, false otherwise. </returns>
        public override int SetComplete()
        {
            // Not enough vertices to form a polygon.
            if (vertices.Count < 3)
                return -1;
            return base.SetComplete();
        }

        public override Frame Clone()
        {
            return new PolygonFrame(this);
        }

        public override bool Rewind()
        {
            if (IsComplete)
            {
                IsComplete = false;
                return true;
            }                
            return base.Rewind();
        }
    }

    [Serializable]
    class LineFrame : PolylineFrame
    {
        public LineFrame() : base() { }
        public LineFrame(Point start, Point end) : this()
        {
            Add(start);
            Add(end);            
        }
        public LineFrame(LineFrame other) : base(other) { }
        public LineFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }
        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (PolylineFrame)bnr.Deserialize(stream);
        }

        public override Frame Clone()
        {
            return new LineFrame(this);
        }

        public override void Resize(Point location, int width, int height)
        {
            throw new Exception("This class doesn't support this method!");
        }
    }
    
    [Serializable]
    class BezierFrame : PolylineFrame
    {
        public BezierFrame() : base() { }
        public BezierFrame(Point start) : base(start) { }
        public BezierFrame(BezierFrame other) : base(other) { }
        public BezierFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }
        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (BezierFrame)bnr.Deserialize(stream);
        }

        public override Frame Clone()
        {
            return new BezierFrame(this);
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {            
            var arr = new List<Point>(vertices);
            var tmp = new List<Point>();
            while((arr.Count - 1) % 3 != 0)
            {
                tmp.Add(arr[arr.Count - 1]);
                arr.RemoveAt(arr.Count - 1);
            }
            graphics.DrawBeziers(OutlinePen, arr.ToArray());
            DrawPoint(arr.Last(), graphics);
            if(tmp.Count > 0)
            {
                tmp.Reverse();
                graphics.DrawLine(OutlinePen, arr[arr.Count - 1], tmp[0]);
                for (int i = 1; i < tmp.Count; i++)
                    graphics.DrawLine(OutlinePen, tmp[i - 1], tmp[i]);
                foreach (var p in tmp)
                {
                    var cp = new CornerControlPoint(p, this.BorderWidth, SystemColors.Control, BorderColor);
                    cp.DrawToGraphics(graphics);
                }
            }            
        }        

        /// <summary>
        /// This method is called to indicate that the polyline is completely drawn.
        /// </summary>
        /// <returns> True if the shape is successfully completed, false otherwise. </returns>
        public override int SetComplete()
        {
            int res = 0;
            while ((vertices.Count - 1) % 3 != 0)
            {
                vertices.RemoveAt(vertices.Count - 1);
                res++;
            }                
            // If the vertices cannot form beziers curve then returns false.
            if (vertices.Count < 4)
                return -1;
            if (base.SetComplete() == -1)
                return -1;
            return res;
        }

        /// <summary>
        /// Checks wheter a given point is close to one of the line segments of the polygon.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point is close to one of the segments of the polygon, false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            if (!IsComplete)
                throw new Exception("The shaple must be complete!");
            location = Helper.Rotate(location, GetOrigin(), -this.Angle);
            for(int i = 3; i < vertices.Count; i += 3) // Iterates through all the curves.
            {
                var P0 = vertices[i - 3];
                var P1 = vertices[i - 2];
                var P2 = vertices[i - 1];
                var P3 = vertices[i];
                float A1 = -P0.X + 3 * P1.X - 3 * P2.X + P3.X;
                float B1 = 3 * P0.X - 6 * P1.X + 3 * P2.X;
                float C1 = -3 * P0.X + 3 * P1.X;
                float D1 = P0.X;
                float A2 = -P0.Y + 3 * P1.Y - 3 * P2.Y + P3.Y;
                float B2 = 3 * P0.Y - 6 * P1.Y + 3 * P2.Y;
                float C2 = -3 * P0.Y + 3 * P1.Y;
                float D2 = P0.Y;
                for (int j = -mouseArea; j <= mouseArea; j++) // Checks for the area around the mouse pointer.
                    for(int k = -mouseArea; k <= mouseArea; k++)
                    {
                        var p = new Point(location.X + j, location.Y + k);
                        float[] X = Helper.SolveCubicEquation(A1, B1, C1, D1 - p.X); // Solves for x and y.
                        float[] Y = Helper.SolveCubicEquation(A2, B2, C2, D2 - p.Y);
                        foreach(var x in X)
                            foreach(var y in Y)
                            {
                                float newX = Helper.Cube(x) * A1 + Helper.Sqr(x) * B1 + x * C1 + D1;
                                float newY = Helper.Cube(y) * A2 + Helper.Sqr(y) * B2 + y * C2 + D2;
                                if (Helper.VectorLeng(new PointF(newX - p.X, newY - p.Y)) <= epsilon)
                                    return true;
                            }
                    }                
            }
            return false;
        }

        public override void Resize(Point location, int width, int height)
        {
            throw new Exception("This class doesn't support this method!");
        }
    }

    [Serializable]
    class ParallelFrame : Frame
    {
        protected ControlPoint pivot;
        protected static readonly Color pivotPointBackcolor = Color.Green;
        protected static readonly Color pivotPointBorderColor = Color.Green; 
        public ParallelFrame() : base() { }
        
        public ParallelFrame(int x, int y, int width, int height) : base(x, y, width, height)
        {
            pivot = new PivotControlPoint(new Point(x + width / 4, y), BorderWidth, pivotPointBackcolor, pivotPointBorderColor);
        }

        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (ParallelFrame)bnr.Deserialize(stream);
        }

        public ParallelFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext)
        {
            pivot = (PivotControlPoint)info.GetValue("pivot", typeof(PivotControlPoint));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctext)
        {
            base.GetObjectData(info, ctext);
            info.AddValue("pivot", typeof(PivotControlPoint));
        }

        public ParallelFrame(ParallelFrame other) : base(other)
        {
            this.pivot = other.pivot.Clone();
        }

        public ParallelFrame(int x, int y, int width, int height, float angle) : this(x, y, width, height)
        {
            this.Angle = angle;
        }

        /// <summary>
        /// Checks wheter a given point is close to one of the line segments of the polygon.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point is close to one of the segments of the polygon, false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            if (this.ToRect().Contains(location) == false) // If the point is outside the frame then returns false.
                return false;
            var A = pivot.Location;
            var B = new Point(this.Location.X + FrameSize.Width - A.X + this.Location.X, this.Location.Y + FrameSize.Height);
            var v1 = new Point(-A.X, FrameSize.Height);
            var v2 = new Point(A.X, -FrameSize.Height);
            return Helper.CrossProduct(new Point(location.X - A.X, location.Y - A.Y), v1) >= 0 &&
                    Helper.CrossProduct(new Point(location.X - B.X, location.Y - B.Y), v2) >= 0;
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {
            var poly = new List<Point>();
            poly.Add(pivot.Location);
            poly.Add(new Point(Location.X + FrameSize.Width, Location.Y));
            poly.Add(new Point(Location.X + FrameSize.Width - pivot.Location.X + Location.X, Location.Y + FrameSize.Height));
            poly.Add(new Point(Location.X, Location.Y + FrameSize.Height));
            // Draws the shape.            
            graphics.FillPolygon(FillBrush, poly.ToArray());                        
            graphics.DrawPolygon(OutlinePen, poly.ToArray());
        }

        /// <summary>
        /// Draws the shape in the frame and the frame itself into a specified Bitmap object.
        /// </summary>
        /// <param name="bmp"> The specified Bitmap object. </param>
        protected override void DrawFrame(Bitmap bmp)
        {            
            base.DrawFrame(bmp);
            var graphics = Graphics.FromImage(bmp);
            var matrix = new Matrix();
            matrix.RotateAt(Angle, GetOrigin());
            graphics.Transform = matrix;
            pivot.DrawToGraphics(graphics);
            graphics.Transform.RotateAt(Angle, GetOrigin());
        }

        /// <summary>
        /// Performs the drag operation of the user.
        /// </summary>
        /// <param name="start"> The start point of the drag operation. </param>
        /// <param name="end"> The end point of the drag operation. </param>
        /// <returns> True if the drag operation affects the shape, false otherwise. </returns>
        public override Point Drag(Point start, Point end)
        {            
            if (!IsSelected)
                throw new Exception("The shape must be shelected before performing the drag operation.");
            var newStart = Helper.Rotate(start, GetOrigin(), -Angle); // Rotates the two points.
            var newEnd = Helper.Rotate(end, GetOrigin(), -Angle);
            if (pivot.IsCovered(newStart))
            {
                var tmp = newEnd.X;
                tmp = Math.Max(tmp, Location.X + 1);
                tmp = Math.Min(tmp, Location.X + FrameSize.Width - 1);
                pivot.Location = new Point(tmp, pivot.Location.Y);
                return Helper.Rotate(pivot.Location, GetOrigin(), this.Angle);
            }
            var curRect = this.ToRect(); // Gets the current frame.
            var res = base.Drag(start, end); // Performs the drag operation.
            var changedRect = this.ToRect(); // Gets the changed frame.            
            var matrix = Helper.GetTransformMatrix(curRect, changedRect); // Gets the transformation matrix.
            var p = new Point[1] { pivot.Location }; 
            matrix.TransformPoints(p); // Applies to the pivot point.
            pivot.Location = p[0];
            return res;
        }

        protected override bool IsInsideFrame(Point p)
        {
            if (pivot.IsCovered(p))
                return true;
            return base.IsInsideFrame(p);
        }        

        public override void TransformByMatrix(Matrix matrix)
        {
            base.TransformByMatrix(matrix);
            var arr = new Point[1] { pivot.Location };
            matrix.TransformPoints(arr);            
            pivot.Location = arr[0];
        }

        public override void RotateAt(Point root, float angle)
        {
            base.RotateAt(root, angle);
            var p = Helper.Rotate(pivot.Location, root, angle);
            p.X = Math.Max(p.X, this.Location.X);
            p.X = Math.Min(p.X, this.Location.X + this.FrameSize.Width);
            p.Y = this.Location.Y;
            pivot.Location = p;
        }

        public override Frame Clone()
        {
            return new ParallelFrame(this);
        }

        public ControlPoint Pivot
        {
            get { return pivot; }
            set { pivot = value; }
        }

        public override void Resize(Point location, int width, int height)
        {
            base.Resize(location, width, height);
            pivot = new PivotControlPoint(new Point(this.Location.X + width / 4, this.Location.Y), BorderWidth, pivotPointBackcolor, pivotPointBorderColor);
        }
    }

    [Serializable]
    class ParabolFrame : LineFrame
    {
        public ParabolFrame() : base() { }
        public ParabolFrame(Point start, Point end) : base(start, end) { }
        public ParabolFrame(ParabolFrame other) : base(other) { }
        public ParabolFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext) { }
        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (ParabolFrame)bnr.Deserialize(stream);
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {
            var parabola = Helper.DrawParabol(FirstVertice, LastVertice);            
            graphics.DrawLines(OutlinePen, parabola.ToArray());            
        }

        public override Frame Clone()
        {
            return new ParabolFrame(this);
        }

        /// <summary>
        /// This method is called to indicate that the polyline is completely drawn.
        /// </summary>
        /// <returns> True if the shape is successfully completed, false otherwise. </returns>
        public override int SetComplete()
        {
            var res = base.SetComplete();                        
            // Calculates the frame that bounds the polyline.            
            Location = new Point(Math.Min(FirstVertice.X, Math.Min(LastVertice.X, FirstVertice.X * 2 - LastVertice.X)), 
                                Math.Min(FirstVertice.Y, LastVertice.Y));
            FrameSize = new Size(Math.Abs(LastVertice.X - FirstVertice.X) * 2, Math.Abs(LastVertice.Y - FirstVertice.Y));
            return res;
        }

        /// <summary>
        /// Checks wheter a given point is close to one of the line segments of the polygon.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point is close to one of the segments of the polygon, false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            var pixels = Helper.DrawParabol(FirstVertice, LastVertice); // Gets all the points of the parabola.
            foreach(var px in pixels)
            {
                for(int i = -mouseArea; i <= mouseArea; i++)
                    for(int j = -mouseArea; j <= mouseArea; j++)
                    {
                        var p = new Point(location.X + i, location.Y + j);
                        if (Helper.VectorLeng(new Point(p.X - px.X, p.Y - px.Y)) <= epsilon)
                            return true;
                    }
            }
            return false;
        }
    }

    [Serializable]
    class HyperbolFrame : Frame
    {
        private readonly float epsilon = 5;        
        private ControlPoint pivot;
        private readonly static int pivotPadding = 20;
        private readonly static Color pivotBorderColor = Color.Green;
        private readonly static Color pivotBackcolor = Color.Green;
        public HyperbolFrame() : base() { }
        public HyperbolFrame(int x, int y, int width, int height) 
            : base(x - 2 * pivotPadding - width, y, (width + pivotPadding) * 2, height)
        {            
            pivot = new PivotControlPoint(new Point(x, y + height / 2), BorderWidth, pivotBackcolor, pivotBorderColor);
        }
        public HyperbolFrame(int x, int y, int width, int height, float angle) : this(x, y, width, height)
        {
            this.Angle = angle;
        }
        public HyperbolFrame(HyperbolFrame other) : base(other)
        {
            this.pivot = other.pivot.Clone();
        }
        public HyperbolFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext)
        {
            pivot = (PivotControlPoint)info.GetValue("pivot", typeof(PivotControlPoint));
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext ctext)
        {
            base.GetObjectData(info, ctext);
            info.AddValue("pivot", pivot);
        }

        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (HyperbolFrame)bnr.Deserialize(stream);
        }        

        /// <summary>
        /// Gets all the points that will be drawn.
        /// </summary>
        /// <returns> An array that contains all the points. </returns>
        /// 
        private Point[] GetAllPoints()
        {
            // Gets the parabola respects to the point (0, 0).
            float a = pivot.Location.X - GetOrigin().X;
            float b = a * FrameSize.Height / 2 / (float)Math.Sqrt(Helper.Sqr(FrameSize.Width / 2) - Helper.Sqr(a));
            var pixels = Helper.DrawHyperbola(a, b, new Point(FrameSize.Width / 2, FrameSize.Height / 2));
            var matrix = new Matrix();
            matrix.Translate(GetOrigin().X, GetOrigin().Y);
            var arr = pixels.ToArray();
            matrix.TransformPoints(arr);
            return arr;
        }

        /// <summary>
        /// Draws the shape inside the current Frame object into a given graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {
            var arr = GetAllPoints();
            var firstHalf = new Point[arr.Length / 2];
            var secondHalf = new Point[arr.Length / 2];
            Array.Copy(arr, 0, firstHalf, 0, arr.Length / 2);
            Array.Copy(arr, arr.Length / 2, secondHalf, 0, arr.Length / 2);
            graphics.DrawLines(OutlinePen, firstHalf);
            graphics.DrawLines(OutlinePen, secondHalf);
        }

        /// <summary>
        /// Checks wheter a given point is close to one of the line segments of the polygon.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point is close to one of the segments of the polygon, false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            location = Helper.Rotate(location, GetOrigin(), -this.Angle);
            var pixels = GetAllPoints();
            foreach (var px in pixels)
            {
                for (int i = -mouseArea; i <= mouseArea; i++)
                    for (int j = -mouseArea; j <= mouseArea; j++)
                    {
                        var p = new Point(location.X + i, location.Y + j);
                        if (Helper.VectorLeng(new Point(p.X - px.X, p.Y - px.Y)) <= epsilon)
                            return true;
                    }
            }
            return false;
        }

        /// <summary>
        /// Draws the shape in the frame and the frame itself into a specified Bitmap object.
        /// </summary>
        /// <param name="bmp"> The specified Bitmap object. </param>
        protected override void DrawFrame(Bitmap bmp)
        {
            base.DrawFrame(bmp);
            var graphics = Graphics.FromImage(bmp);
            var matrix = new Matrix();
            matrix.RotateAt(this.Angle, GetOrigin());
            graphics.Transform = matrix;
            pivot.DrawToGraphics(graphics);
        }

        /// <summary>
        /// Performs the drag operation of the user.
        /// </summary>
        /// <param name="start"> The start point of the drag operation. </param>
        /// <param name="end"> The end point of the drag operation. </param>
        /// <returns> True if the drag operation affects the shape, false otherwise. </returns>
        public override Point Drag(Point start, Point end)
        {
            var newStart = Helper.Rotate(start, GetOrigin(), -Angle);
            var newEnd = Helper.Rotate(end, GetOrigin(), -Angle);
            if(pivot.IsCovered(newStart))
            {
                var tmp = newEnd.X;
                tmp = Math.Max(tmp, GetOrigin().X + 1);
                tmp = Math.Min(tmp, Location.X + FrameSize.Width - 1);
                pivot.Location = new Point(Math.Max(tmp, GetOrigin().X + 1), pivot.Location.Y);
                return Helper.Rotate(pivot.Location, GetOrigin(), Angle);
            }
            var curAngle = this.Angle;
            var curRect = ToRect();
            var res = base.Drag(start, end);
            var changedRect = ToRect();
            var matrix = Helper.GetTransformMatrix(curRect, changedRect);
            var arr = new Point[1] { pivot.Location };
            matrix.TransformPoints(arr);
            pivot.Location = arr[0];
            return res;
        }

        public override void Resize(Point location, int width, int height)
        {
            base.Resize(new Point(location.X - 2 * pivotPadding - width, location.Y), (width + pivotPadding) * 2, height);
            pivot.Location = new Point(location.X, location.Y + height / 2);
        }        
        
        public override void TransformByMatrix(Matrix matrix)
        {
            base.TransformByMatrix(matrix);
            var arr = new Point[1] { pivot.Location };
            matrix.TransformPoints(arr);
            arr[0].X = Math.Max(this.Location.X, arr[0].X);
            arr[0].X = Math.Min(this.Location.X + FrameSize.Width, arr[0].X);
            pivot.Location = arr[0];
        }

        public override void RotateAt(Point root, float angle)
        {
            base.RotateAt(root, angle);
            var p = Helper.Rotate(pivot.Location, root, angle);
            p.X = Math.Max(p.X, this.Location.X + FrameSize.Width / 2 + 1);
            p.X = Math.Min(p.X, this.Location.X + this.FrameSize.Width);
            p.Y = this.Location.Y + this.FrameSize.Height / 2;
            pivot.Location = p;
        }

        public ControlPoint Pivot
        {
            get { return pivot; }
            set { pivot = value; }
        }

        public override Frame Clone()
        {
            return new HyperbolFrame(this);
        }
    }

    [Serializable]
    class TextFrame : RectFrame
    {
        private RichTextBox textBox;
        Font textFont;        
        public TextFrame() : base()
        {
            textBox = null;            
        }
        public TextFrame(int x, int y, int width, int height) : base(x, y, width, height)
        {
            textFont = new Font("Calibri", 10);
            BorderColor = Color.Black;
            textBox = new RichTextBox()
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                Text = "",
                Font = textFont
            };                        
        }       

        public TextFrame(int x, int y, int width, int height, float angle) : this(x, y, width, height)
        {
            this.Angle = angle;
        }
        public TextFrame(TextFrame other) : base(other)
        {
            textFont = other.textFont;
            this.textBox = new RichTextBox()
            {
                Location = other.TextBox.Location,
                Size = other.TextBox.Size,
                Text = other.TextBox.Text,
                Font = other.TextBox.Font,
            };
        }
        public TextFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext)
        {                                    
            var text = (string)info.GetValue("text", typeof(string));
            textFont = (Font)info.GetValue("textFont", typeof(Font));
            textBox = new RichTextBox()
            {
                Location = this.Location,
                Size = this.FrameSize,
                Font = this.textFont,
                Text = text
            };
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctext)
        {
            base.GetObjectData(info, ctext);
            info.AddValue("text", textBox.Text);
            info.AddValue("textFont", textBox.Font);
        }

        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (TextFrame)bnr.Deserialize(stream);
        }

        /// <summary>
        /// Checks whether the specified Point object is inside the shape or not.
        /// </summary>
        /// <param name="location"> The specified Point object. </param>
        /// <returns></returns>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {                        
            graphics.DrawString(textBox.Text, textBox.Font, Brushes.Black, new Rectangle(Location, FrameSize));
        }

        public override void Resize(Point location, int width, int height)
        {
            base.Resize(location, width, height);
            this.textBox.Location = location;
            this.textBox.Size = this.FrameSize;
        }

        /// <summary>
        /// Changes the Frame according to the Drag operation of the user.
        /// </summary>
        /// <param name="start"> The start point of the Drag operation. </param>
        /// <param name="end"> The end point of the Drag operation. </param>
        /// <returns> True if the Drag operation affects the Frame, false otherwise. </returns>
        public override Point Drag(Point start, Point end)
        {            
            var res = base.Drag(start, end);
            var changedRect = ToRect();
            textBox.Location = changedRect.Location;
            textBox.Size = changedRect.Size;
            return res;
        }

        public RichTextBox TextBox
        {
            get { return textBox; }
            set { textBox = value; }
        }

        public Font TextFont
        {
            get { return textFont; }
            set
            {
                textFont = value;
                textBox.Font = value;
            }
        }        

        public override void TransformByMatrix(Matrix matrix)
        {
            base.TransformByMatrix(matrix);
            textBox.Location = this.Location;
            textBox.Size = this.FrameSize;
        }

        public override void RotateAt(Point root, float angle)
        {
            base.RotateAt(root, angle);
            textBox.Location = this.Location;
            textBox.Size = this.FrameSize;
        }

        public override Frame Clone()
        {
            return new TextFrame(this);
        }
    }

    class GroupFrame : Frame
    {
        LinkedList<Frame> frames;
        public GroupFrame() : base() { }
        public GroupFrame(LinkedList<Frame> frames) : base()
        {
            this.frames = frames;
            CalculateFrameSize();
        }

        public GroupFrame(GroupFrame other) : base(other)
        {
            frames = new LinkedList<Frame>();
            foreach (var fr in other.frames)
                frames.AddLast(fr.Clone());
        }

        public override Frame Load(Stream stream)
        {
            var bnr = new BinaryFormatter();
            return (GroupFrame)bnr.Deserialize(stream);
        }

        public GroupFrame(SerializationInfo info, StreamingContext ctext) : base(info, ctext)
        {
            frames = (LinkedList<Frame>)info.GetValue("frames", typeof(LinkedList<Frame>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctext)
        {
            base.GetObjectData(info, ctext);
            info.AddValue("frames", frames);
        }

        public LinkedList<Frame> Frames
        {
            get { return frames; }
            set { frames = value; }
        }

        public override void GetCorners(ref int minX, ref int minY, ref int maxX, ref int maxY)
        {
            minX = minY = (int)1e9;
            maxX = maxY = 0;
            foreach (var fr in frames)
            {
                int a, b, c, d;
                a = b = c = d = 0;
                fr.GetCorners(ref a, ref b, ref c, ref d);
                minX = Math.Min(minX, a);
                minY = Math.Min(minY, b);
                maxX = Math.Max(maxX, c);
                maxY = Math.Max(maxY, d);
            }                
        }

        private void CalculateFrameSize()
        {
            int minX, minY, maxX, maxY;
            minX = minY = maxX = maxY = 0;
            GetCorners(ref minX, ref minY, ref maxX, ref maxY);
            this.Location = new Point(minX, minY);
            this.FrameSize = new Size(maxX - minX, maxY - minY);
        }

        /// <summary>
        /// Check wheter a given point is inside one of the shapes that the current group contains.
        /// </summary>
        /// <param name="location"> The given point. </param>
        /// <returns> True if the given point is inside one of the shapes that the current group contains, false otherwise. </returns>
        public override bool IsInsideShape(Point location)
        {
            location = Helper.Rotate(location, GetOrigin(), -this.Angle);
            foreach(var fr in frames)
            {
                if (fr.IsInsideShape(location))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Draws all the shapes contained in the current group using a given Graphics object.
        /// </summary>
        /// <param name="graphics"> The given graphics object. </param>
        public override void DrawShapeIntoGraphics(Graphics graphics)
        {
            foreach (var fr in frames)                
                fr.DrawShape(graphics);
        }

        public override Point Drag(Point start, Point end)
        {            
            var curAngle = this.Angle;
            var curRect = this.ToRect();
            var res = base.Drag(start, end);
            var changedRect = this.ToRect();
            var matrix = Helper.GetTransformMatrix(curRect, changedRect);
            foreach(var fr in frames)
            {                
                fr.TransformByMatrix(matrix);                
            }
            foreach(var fr in frames)
            {
                fr.RotateAt(GetOrigin(), this.Angle - curAngle);
            }
            return res;
        }       

        public override void TransformByMatrix(Matrix matrix)
        {
            foreach (var fr in frames)
                fr.TransformByMatrix(matrix);
        }

        public override void RotateAt(Point root, float angle)
        {
            foreach (var fr in frames)
                fr.RotateAt(root, angle);
        }

        public override Frame Clone()
        {
            return new GroupFrame(this);            
        }

        public override Color BorderColor
        {
            set
            {
                foreach (var fr in frames)
                    fr.BorderColor = value;
            }
        }

        public override Color ShapeColor
        {
            set
            {
                foreach (var fr in frames)
                    fr.ShapeColor = value;
            }
        }

        public override DashStyle BorderType
        {
            set
            {
                foreach (var fr in frames)
                    fr.BorderType = value;
            }
        }

        public override int BorderWidth
        {
            set
            {
                foreach (var fr in frames)
                    fr.BorderWidth = value;
            }
        }

        public override Bitmap Texture
        {
            set
            {
                foreach (var fr in frames)
                    fr.Texture = value;
            }
        }
    }
}