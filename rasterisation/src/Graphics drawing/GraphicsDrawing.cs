using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_drawing
{
    /// <summary>
    /// A helper class.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Swaps two elements.
        /// </summary>
        /// <typeparam name="T"> The type of the elements. </typeparam>
        /// <param name="first"> The first element. </param>
        /// <param name="second"> The second element. </param>
        public static void Swap<T>(ref T first, ref T second)
        {
            T tmp = first;
            first = second;
            second = tmp;
        }

        public static long Sqr(long x)
        {
            return x * x;
        }
    }

    /// <summary>
    /// A struct that represents a point.
    /// </summary>
    public struct Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// An operator that compares two Points.
        /// </summary>
        /// <param name="first"> The first Point. </param>
        /// <param name="second">The second Point. </param>
        /// <returns> True if the two points are equal, false otherwise. </returns>
        public static bool operator ==(Point first, Point second)
        {
            return first.x == second.x && first.y == second.y;
        }

        /// <summary>
        /// An operator that compares two Points.
        /// </summary>
        /// <param name="first"> The first Point. </param>
        /// <param name="second"> The second Point. </param>
        /// <returns> True if the two points are not equal, false otherwise. </returns>
        public static bool operator !=(Point first, Point second)
        {
            return !(first == second);
        }

        override public bool Equals(object obj)
        {
            return this == (Point)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// A class that represents a drawing tool.
    /// </summary>
    public abstract class Drawer
    {
        protected int width; // The width of the drawing place.
        protected int height; // The height of the drawing place.
        static public int MaxRepresentPoints = 1000; // The greatest number of points could be drawn.
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="width"> The initial width. </param>
        /// <param name="height"> The initial height. </param>
        public Drawer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            
            get { return height; }
        }

        /// <summary>
        /// A method that checks if a given point is outside the grid or not.
        /// </summary>
        /// <param name="P"> The given point </param>
        /// <returns> true if the given point is outside the grid, false otherwise. </returns>
        protected bool CheckOutSide(Point P)
        {
            return P.x < 0 || P.x >= width || P.y < 0 || P.y >= height;
        }

        public abstract List<Point> DrawLine(Point A, Point B);
        public abstract List<Point> DrawCircle(Point A, int radius);
        public abstract List<Point> DrawEllipse(Point A, int a, int b);
        public abstract List<Point> DrawParabol(Point A, Point B);
        public abstract List<Point> DrawHyperbol(Point A, int a, int b);

        /// <summary>
        /// A method that reflects a list of Points by the lines y = x and y = -x.
        /// </summary>
        /// <param name="pixels"> The given list. </param>
        /// <returns> The result list. </returns>
        protected List<Point> Reflect4(List<Point> pixels)
        {
            var res = new List<Point>();            
            foreach(var px in pixels)
            {
                res.Add(px);
                res.Add(new Point(-px.x, px.y));
                res.Add(new Point(px.x, -px.y));
                res.Add(new Point(-px.x, -px.y));
            }
            return res;
        }

        /// <summary>
        /// A method that reflects a list of Points by the lines y = x, y = -x, y = 0 and x = 0.
        /// </summary>
        /// <param name="pixels"> The given list of Points. </param>
        /// <returns> The result list. </returns>
        protected List<Point> Reflect8(List<Point> pixels)
        {
            var res = new List<Point>();
            foreach(var px in pixels)
            {
                res.Add(px);
                res.Add(new Point(-px.x, px.y));
                res.Add(new Point(px.x, -px.y));
                res.Add(new Point(-px.x, -px.y));
                res.Add(new Point(px.y, px.x));
                res.Add(new Point(-px.y, px.x));
                res.Add(new Point(px.y, -px.x));
                res.Add(new Point(-px.y, -px.x));
            }
            return res;
        }

        /// <summary>
        /// A methods that translates a list of Points by an origin Point.
        /// </summary>
        /// <param name="pixels"> The given list of Points. </param>
        /// <param name="A"> The given origin Point. </param>
        /// <returns> The result list. </returns>
        protected List<Point> Translate(List<Point> pixels, Point A)
        {
            var res = new List<Point>();
            foreach (var px in pixels)
                res.Add(new Point(px.x + A.x, px.y + A.y));
            return res;
        }
    }

    /// <summary>
    /// A class that provides drawing methods on objects such as line, circle, Ellipse and hyperbol,
    /// using Bresenham algorithm.
    /// </summary>
    public class Bresenham : Drawer
    {
        /// <summary>
        /// Default constructor.
        /// Inherited from the base constructor.
        /// </summary>
        /// <param name="row"> The initial number of rows. </param>
        /// <param name="col"> The initial number of columns. </param>
        public Bresenham(int row, int col) : base(row, col) { }


        /// <summary>
        /// A method that draws a line that contains two given points.
        /// </summary>
        /// <param name="A"> The first given point. </param>
        /// <param name="B"> The second given point. </param>
        /// <returns> A list of points that represents the line.
        /// If one of the given points doesn't lie in the grid then returns an empty list.
        /// Otherwise a list of Points is returned.
        /// </returns>
        public override List<Point> DrawLine(Point A, Point B)
        {
            var res = new List<Point>(); // The result.
            if (CheckOutSide(A) || CheckOutSide(B))
                return res;             
            /// Bresenham algorithm.
            if (A.x > B.x) Helper.Swap<Point>(ref A, ref B);
            bool isReflected = false; // A flag that is set when B is reflected by y=A.y.
            if(A.y > B.y)
            {
                isReflected = true;
                B.y = A.y * 2 - B.y;
            }
            int Dy = Math.Abs(A.y - B.y);
            int Dx = Math.Abs(A.x - B.x);
            bool steep = false;
            if(Dy > Dx) // The slope is greater than 1 then draws with y.
            {
                Helper.Swap(ref Dy, ref Dx);
                Helper.Swap(ref A.x, ref A.y);
                Helper.Swap(ref B.x, ref B.y);
                steep = true;
            }
            int p = Dy * 2 - Dx;                        
            int x = A.x;
            int y = A.y;
            res.Add(new Point(x, y));
            while (x < B.x)
            {
                if (p < 0) p += Dy * 2;
                else
                {
                    p += (Dy - Dx) * 2;
                    y++;
                }
                x++;
                res.Add(new Point(x, y));
            }
            if(isReflected) // The line is reflected by y = A.y.
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].x, A.y * 2 - res[i].y);
            }            
            if(steep)
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].y, res[i].x);
            }
            return res;
        }

        /// <summary>
        /// A method that draws a circle. The center and radius of the circle are given.
        /// </summary>
        /// <param name="center"> The center of the circle. </param>
        /// <param name="radius"> The radius of the circle. </param>
        /// <returns> If the center of the circle is outside the grid then returns an empty list.
        /// Otherwise a list of Points is returned.
        /// </returns>
        public override List<Point> DrawCircle(Point center, int radius)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || radius > Math.Max(width, height)) // If the center is out of the grid or the radius is too large then does nothing.
                return res;            
            int x = 0;
            int y = radius;
            int p = 5 - 4 * radius;
            while(x <= y) // Draws 1/8 of the circle.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + 12 + 8 * x;
                else
                {                    
                    p = 20 + 8 * x - 8 * y + p;
                    y--;
                }
                x++;
            }
            // Refects the arc that is drawn.
            res = Reflect8(res);
            // Moves the circle to the center.
            res = Translate(res, center);            
            return res;
        }

        /// <summary>
        /// A method that draws an Ellipse. Then center and the two lengths are given,
        /// Using Bresenham algorithm
        /// </summary>
        /// <param name="center"> The center of the Ellipse. </param>
        /// <param name="a"> The first length of the Ellipse. </param>
        /// <param name="b"> The second length of the Ellipse. </param>
        /// <returns> The list of Points that represents the Ellipse. </returns>
        public override List<Point> DrawEllipse(Point center, int a, int b)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || Math.Max(a, b) > Math.Max(width, height)) // The Ellipse is too large for the grid.
                return res;
            for(int i = 0; i < 2; i++)
            {
                int x = 0;
                int y = b;
                long p = Helper.Sqr(a) - Helper.Sqr(2 * a) * b + Helper.Sqr(2 * b);
                while (Helper.Sqr(b) * x <= Helper.Sqr(a) * y)
                {
                    if (i == 0)
                        res.Add(new Point(x, y));
                    else
                        res.Add(new Point(y, x));
                    if (p <= 0) p = Helper.Sqr(b) * (8 * x + 12) + p;
                    else
                    {
                        p = Helper.Sqr(a) * 8 * (1 - y) + Helper.Sqr(b) * (8 * x + 12) + p;
                        y--;
                    }
                    x++;
                }
                Helper.Swap<int>(ref a, ref b);
            }
            // Reflects the drawn part to make the whole ellipse.
            res = Reflect4(res);
            // Moves the Ellipse to the center.
            res = Translate(res, center);
            return res;
        }

        /// <summary>
        /// A method that draws a parabol using Bresenham algorithm.
        /// </summary>
        /// <param name="center"> The center of the Parabol. </param>
        /// <param name="border"> A Point that belongs to the Parabol. </param>
        /// <returns> A list of Points that represents the Parabol. </returns>
        public override List<Point> DrawParabol(Point center, Point border)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || CheckOutSide(border)) // The center is outside the grid.
                return res;
            border.x -= center.x; // Translate border to the center.
            border.y -= center.y;
            if (border.x < 0) border.x *= -1;
            bool reflected = false;
            if (border.y < 0)
            {
                reflected = true;
                border.y *= -1;
            }
            int x = 0;
            int y = 0;
            long p = border.y * 2 - Helper.Sqr(border.x);
            while(border.y * (1 + 2 * x) <= Helper.Sqr(border.x)) // Draws the first part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + 2 * border.y * (2 * x + 3);
                else
                {
                    p = p + 2 * border.y * (2 * x + 3) - Helper.Sqr(border.x) * 2;
                    y++;
                }
                x++;
            }
            p = Helper.Sqr(2 * border.x) * (y + 1) - Helper.Sqr(2 * x + 1) * border.y;
            while(x + y <= MaxRepresentPoints) // Draws the second part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + Helper.Sqr(2 * border.x);
                else
                {
                    p = p + Helper.Sqr(2 * border.x) - 8 * border.y * (x + 1);
                    x++;
                }
                y++;
            }
            int tmp = res.Count;
            for (int i = 0; i < tmp; i++)
                res.Add(new Point(-res[i].x, res[i].y));
            if (reflected) // Reflects the Parabol to its ordinary.
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].x, -res[i].y);
            }
            // Translates back.
            res = Translate(res, center);
            return res;
        }

        /// <summary>
        /// A methods that draws a Hyperbol x^2/a^2 - y^2/b^2 = 1 with given a, b and the center, using Bresenham algorithm.
        /// </summary>
        /// <param name="center"> The center of the Hyperbol. </param>
        /// <param name="a"> The given a. </param>
        /// <param name="b"> The given b. </param>
        /// <returns> A list of Points that represents the Hyperbol. </returns>
        public override List<Point> DrawHyperbol(Point center, int a, int b)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || Math.Max(a, b) > Math.Max(width, height)) // The Hyperbol is too large for the grid.
                return res;
            if (CheckOutSide(center))
                return res;
            int x = a;
            int y = 0;
            long p = Helper.Sqr(2 * a) - Helper.Sqr(b) - Helper.Sqr(2 * b) * a;
            while(Helper.Sqr(a) * y <= Helper.Sqr(b) * x && y <= MaxRepresentPoints) // Draws the first part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + Helper.Sqr(2 * a) * (2 * y + 3);
                else
                {
                    p = p + Helper.Sqr(2 * a) * (2 * y + 3) - Helper.Sqr(b) * 8 * (x + 1);
                    x++;
                }
                y++;
            }
            p = Helper.Sqr(2 * b * x) - Helper.Sqr(2 * b * a) - Helper.Sqr(a * (2 * y + 1));
            while(x + y <= MaxRepresentPoints) // Draws the second part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + Helper.Sqr(2 * b) * (2 * x + 1);
                else
                {
                    p = p + Helper.Sqr(2 * b) * (2 * x + 1) - Helper.Sqr(a) * (y + 1) * 8;
                    y++;
                }
                x++;
            }
            // Reflects the drawn part to make the whole Hyperbol.
            res = Reflect4(res);
            // Moves the Hyperbol to the center.
            res = Translate(res, center);
            return res;
        }
    }


    /// <summary>
    /// A class that provides drawing methods on objects such as line, circle, Ellipse and hyperbol,
    /// using MidPoint algorithm.
    /// </summary>
    public class MidPoint : Drawer
    {        
        public MidPoint(int row, int col) : base(row, col) { }

        /// <summary>
        /// A method that draws a line that contains two given points.
        /// </summary>
        /// <param name="A"> The first given point. </param>
        /// <param name="B"> The second given point. </param>
        /// <returns> A list of points that represents the line.
        /// If one of the given points doesn't lie in the grid then returns an empty list.
        /// Otherwise a list of Points is returned.
        /// </returns>
        public override List<Point> DrawLine(Point A, Point B)
        {
            var res = new List<Point>(); // The result.
            if (CheckOutSide(A) || CheckOutSide(B))
                return res;
            /// Bresenham algorithm.
            if (A.x > B.x) Helper.Swap<Point>(ref A, ref B);
            bool isReflected = false; // A flag that is set when B is reflected by y=A.y.
            if (A.y > B.y)
            {
                isReflected = true;
                B.y = A.y * 2 - B.y;
            }
            int Dy = Math.Abs(A.y - B.y);
            int Dx = Math.Abs(A.x - B.x);
            bool steep = false;
            if (Dy > Dx) // The slope is greater than 1 then draws with y.
            {
                Helper.Swap(ref Dy, ref Dx);
                Helper.Swap(ref A.x, ref A.y);
                Helper.Swap(ref B.x, ref B.y);
                steep = true;
            }
            int p = Dy * 2 - Dx;
            int x = A.x;
            int y = A.y;
            res.Add(new Point(x, y));
            while (x < B.x)
            {
                if (p < 0) p += Dy * 2;
                else
                {
                    p += (Dy - Dx) * 2;
                    y++;
                }
                x++;
                res.Add(new Point(x, y));
            }
            if (isReflected) // The line is reflected by y = A.y.
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].x, A.y * 2 - res[i].y);
            }
            if (steep)
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].y, res[i].x);
            }
            return res;
        }

        /// <summary>
        /// A method that draws a circle. The center and radius of the circle are given,
        /// using Midpoint algorithm.
        /// </summary>
        /// <param name="center"> The center of the circle. </param>
        /// <param name="radius"> The radius of the circle. </param>
        /// <returns> If the center of the circle is outside the grid then returns an empty list.
        /// Otherwise a list of Points is returned.
        /// </returns>
        public override List<Point> DrawCircle(Point center, int radius)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || radius > Math.Max(width, height)) // The circle is too large for the grid.
                return res;
            int x = 0;
            int y = radius;
            int p = 1 - radius;
            while (x <= y) // Draws 1/8 of the circle.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + 2 * (x + 1) + 1;
                else
                {
                    y--;
                    p = p + 2 * (x + 1) + 1 - 2 * y;
                }
                x++;
            }
            // Refects the arc that is drawn.
            res = Reflect8(res);
            // Moves the circle to the center.
            res = Translate(res, center);
            return res;
        }

        /// <summary>
        /// A method that draws an Ellipse. Then center and the two lengths are given,
        /// using Midpoint algorithm
        /// </summary>
        /// <param name="center"> The center of the Ellipse. </param>
        /// <param name="a"> The first length of the Ellipse. </param>
        /// <param name="b"> The second length of the Ellipse. </param>
        /// <returns> The list of Points that represents the Ellipse. </returns>
        public override List<Point> DrawEllipse(Point center, int a, int b)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || Math.Max(a, b) > Math.Max(width, height)) // Out of the grid.
                return res;            
            for(int i = 0; i < 2; i++) // Draws the first part and the second part of the ellipse.
            {
                int x = 0;
                int y = b;
                long p = Helper.Sqr(2 * b) + Helper.Sqr(a) - Helper.Sqr(2 * a) * b;
                while (Helper.Sqr(b) * x <= Helper.Sqr(a) * y)
                {
                    if (i == 0) // The first part.
                        res.Add(new Point(x, y));
                    else // The second part.
                        res.Add(new Point(y, x));
                    if (p <= 0) p = p + Helper.Sqr(b) * (8 * x + 12);
                    else
                    {
                        p = p + Helper.Sqr(b) * (8 * x + 12) + Helper.Sqr(a) * 8 * (1 - y);
                        y--;
                    }
                    x++;
                }
                Helper.Swap(ref a, ref b);
            }            
            int tmp = res.Count;
            // Reflects the drawn part to make the whole ellipse.
            res = Reflect4(res);
            // Moves to the center.
            res = Translate(res, center);
            return res;
        }

        /// <summary>
        /// A method that draws a parabol using MidPoint algorithm.
        /// </summary>
        /// <param name="center"> The center of the Parabol. </param>
        /// <param name="border"> A Point that belongs to the Parabol. </param>
        /// <returns> A list of Points that represents the Parabol. </returns>
        public override List<Point> DrawParabol(Point center, Point border)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || CheckOutSide(border)) // The center is outside the grid.
                return res;
            border.x -= center.x; // Translate border to the center.
            border.y -= center.y;
            if (border.x < 0) border.x *= -1;
            bool reflected = false;
            if (border.y < 0)
            {
                reflected = true;
                border.y *= -1;
            }
            int x = 0;
            int y = 0;
            long p = border.y * 2 - Helper.Sqr(border.x);
            while (border.y * (1 + 2 * x) <= Helper.Sqr(border.x)) // Draws the first part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + 2 * border.y * (2 * x + 3);
                else
                {
                    p = p + 2 * border.y * (2 * x + 3) - Helper.Sqr(border.x) * 2;
                    y++;
                }
                x++;
            }
            p = Helper.Sqr(2 * border.x) * (y + 1) - Helper.Sqr(2 * x + 1) * border.y;
            while (x + y <= MaxRepresentPoints) // Draws the second part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + Helper.Sqr(2 * border.x);
                else
                {
                    p = p + Helper.Sqr(2 * border.x) - 8 * border.y * (x + 1);
                    x++;
                }
                y++;
            }
            int tmp = res.Count;
            for (int i = 0; i < tmp; i++)
                res.Add(new Point(-res[i].x, res[i].y));
            if (reflected) // Reflects the Parabol to its ordinary.
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].x, -res[i].y);
            }
            // Translates back.
            res = Translate(res, center);
            return res;
        }

        /// <summary>
        /// A methods that draws a Hyperbol x^2/a^2 - y^2/b^2 = 1 with given a, b and the center, using MidPoint algorithm.
        /// </summary>
        /// <param name="center"> The center of the Hyperbol. </param>
        /// <param name="a"> The given a. </param>
        /// <param name="b"> The given b. </param>
        /// <returns> A list of Points that represents the Hyperbol. </returns>
        public override List<Point> DrawHyperbol(Point center, int a, int b)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || Math.Max(a, b) > Math.Max(width, height)) // The Hyperbol is too large for the grid.
                return res;
            if (CheckOutSide(center))
                return res;
            int x = a;
            int y = 0;
            long p = Helper.Sqr(2 * a) - Helper.Sqr(b) - Helper.Sqr(2 * b) * a;
            while (Helper.Sqr(a) * y <= Helper.Sqr(b) * x && y <= MaxRepresentPoints) // Draws the first part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + Helper.Sqr(2 * a) * (2 * y + 3);
                else
                {
                    p = p + Helper.Sqr(2 * a) * (2 * y + 3) - Helper.Sqr(b) * 8 * (x + 1);
                    x++;
                }
                y++;
            }
            p = Helper.Sqr(2 * b * x) - Helper.Sqr(2 * b * a) - Helper.Sqr(a * (2 * y + 1));
            while (x + y <= MaxRepresentPoints) // Draws the second part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + Helper.Sqr(2 * b) * (2 * x + 1);
                else
                {
                    p = p + Helper.Sqr(2 * b) * (2 * x + 1) - Helper.Sqr(a) * (y + 1) * 8;
                    y++;
                }
                x++;
            }
            // Reflects the drawn part to make the whole Hyperbol.
            res = Reflect4(res);
            // Moves the Hyperbol to the center.
            res = Translate(res, center);
            return res;
        }
    }

    public class DDA : Drawer
    {
        public DDA(int width, int height) : base(width, height) { }

        /// <summary>
        /// A method that draws a line that contains two given points.
        /// </summary>
        /// <param name="A"> The first given point. </param>
        /// <param name="B"> The second given point. </param>
        /// <returns> A list of points that represents the line.
        /// If one of the given points doesn't lie in the grid then returns an empty list.
        /// Otherwise a list of Points is returned.
        /// </returns>
        public override List<Point> DrawLine(Point A, Point B)
        {
            var res = new List<Point>(); // The result.
            if (CheckOutSide(A) || CheckOutSide(B))
                return res;                        
            if (A.x > B.x) Helper.Swap<Point>(ref A, ref B);
            bool isReflected = false; // A flag that is set when B is reflected by y=A.y.
            if (A.y > B.y)
            {
                isReflected = true;
                B.y = A.y * 2 - B.y;
            }
            int Dy = Math.Abs(A.y - B.y);
            int Dx = Math.Abs(A.x - B.x);
            bool steep = false;
            if (Dy > Dx) // The slope is greater than 1 then draws with y.
            {
                Helper.Swap(ref Dy, ref Dx);
                Helper.Swap(ref A.x, ref A.y);
                Helper.Swap(ref B.x, ref B.y);
                steep = true;
            }            
            int x = A.x;
            double y = A.y;
            double m = ((double)(Dy)) / Dx;
            while (x <= B.x)
            {
                res.Add(new Point(x, (int)Math.Round(y)));
                y += m;
                x++;                
            }
            if (isReflected) // The line is reflected by y = A.y.
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].x, A.y * 2 - res[i].y);
            }
            if (steep)
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].y, res[i].x);
            }
            return res;
        }

        /// <summary>
        /// A method that draws a circle. The center and radius of the circle are given,
        /// using DDA algorithm.
        /// </summary>
        /// <param name="center"> The center of the circle. </param>
        /// <param name="radius"> The radius of the circle. </param>
        /// <returns> If the center of the circle is outside the grid then returns an empty list.
        /// Otherwise a list of Points is returned.
        /// </returns>
        public override List<Point> DrawCircle(Point center, int radius)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || radius > Math.Max(width, height)) // The circle is too large for the grid.
                return res;
            int x = 0;
            int y = radius;
            double ySquare = Helper.Sqr(radius);            
            while (x <= y) // Draws 1/8 of the circle.
            {
                res.Add(new Point(x, y));                
                ySquare -= 2 * x + 1;
                y = (int)Math.Round(Math.Sqrt(ySquare));
                x++;
            }
            // Refects the arc that is drawn.
            res = Reflect8(res);
            // Moves the circle to the center.
            res = Translate(res, center);
            return res;
        }

        /// <summary>
        /// A method that draws an Ellipse. Then center and the two lengths are given,
        /// using DDA algorithm.
        /// </summary>
        /// <param name="center"> The center of the Ellipse. </param>
        /// <param name="a"> The first length of the Ellipse. </param>
        /// <param name="b"> The second length of the Ellipse. </param>
        /// <returns> The list of Points that represents the Ellipse. </returns>
        public override List<Point> DrawEllipse(Point center, int a, int b)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || Math.Max(a, b) > Math.Max(width, height)) // Out of the grid.
                return res;
            for (int i = 0; i < 2; i++) // Draws the first part and the second part of the ellipse.
            {
                int x = 0;
                int y = b;
                double ySquare = Helper.Sqr(b);
                while (Helper.Sqr(b) * x <= Helper.Sqr(a) * y)
                {
                    if (i == 0) // The first part.
                        res.Add(new Point(x, y));
                    else // The second part.
                        res.Add(new Point(y, x));
                    ySquare -= ((double)Helper.Sqr(b)) / Helper.Sqr(a) * (2 * x + 1);
                    y = (int)Math.Sqrt(ySquare);
                    x++;
                }
                Helper.Swap(ref a, ref b);
            }
            int tmp = res.Count;
            // Reflects the drawn part to make the whole ellipse.
            res = Reflect4(res);
            // Moves to the center.
            res = Translate(res, center);
            return res;
        }

        /// <summary>
        /// A method that draws a parabol using DDA algorithm.
        /// </summary>
        /// <param name="center"> The center of the Parabol. </param>
        /// <param name="border"> A Point that belongs to the Parabol. </param>
        /// <returns> A list of Points that represents the Parabol. </returns>
        public override List<Point> DrawParabol(Point center, Point border)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || CheckOutSide(border)) // The center is outside the grid.
                return res;
            border.x -= center.x; // Translate border to the center.
            border.y -= center.y;
            if (border.x < 0) border.x *= -1;
            bool reflected = false;
            if (border.y < 0)
            {
                reflected = true;
                border.y *= -1;
            }
            int x = 0;
            double y = 0;
            double A = ((double)border.y) / Helper.Sqr(border.x);
            long p = border.y * 2 - Helper.Sqr(border.x);
            while ((1 + 2 * x) * border.y <= Helper.Sqr(border.x)) // Draws the first part.
            {
                res.Add(new Point(x, (int)Math.Round(y)));
                y += A * (2 * x + 1);
                x++;
            }
            double xSquare = Helper.Sqr(x);
            while (x + y <= MaxRepresentPoints) // Draws the second part.
            {
                res.Add(new Point(x, (int)Math.Round(y)));
                xSquare += 1.0 / A;
                x = (int)Math.Round(Math.Sqrt(xSquare));
                y++;
            }
            int tmp = res.Count;
            for (int i = 0; i < tmp; i++)
                res.Add(new Point(-res[i].x, res[i].y));
            if (reflected) // Reflects the Parabol to its ordinary.
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].x, -res[i].y);
            }
            // Translates back.
            res = Translate(res, center);
            return res;
        }

        /// <summary>
        /// A methods that draws a Hyperbol x^2/a^2 - y^2/b^2 = 1 with given a, b and the center, using DDA algorithm.
        /// </summary>
        /// <param name="center"> The center of the Hyperbol. </param>
        /// <param name="a"> The given a. </param>
        /// <param name="b"> The given b. </param>
        /// <returns> A list of Points that represents the Hyperbol. </returns>
        public override List<Point> DrawHyperbol(Point center, int a, int b)
        {
            var res = new List<Point>();
            if (CheckOutSide(center) || Math.Max(a, b) > Math.Max(width, height)) // The Hyperbol is too large for the grid.
                return res;
            if (CheckOutSide(center))
                return res;
            int x = a;
            int y = 0;
            double xSquare = Helper.Sqr(a);
            long p = Helper.Sqr(2 * a) - Helper.Sqr(b) - Helper.Sqr(2 * b) * a;
            while (Helper.Sqr(a) * y <= Helper.Sqr(b) * x && y <= MaxRepresentPoints) // Draws the first part.
            {
                res.Add(new Point(x, y));
                xSquare += ((double)Helper.Sqr(a)) / Helper.Sqr(b) * (2 * y + 1);
                x = (int)Math.Round(Math.Sqrt(xSquare));
                y++;
            }
            double ySquare = Helper.Sqr(y);
            while (x + y <= MaxRepresentPoints) // Draws the second part.
            {
                res.Add(new Point(x, y));
                ySquare += ((double)Helper.Sqr(b)) / Helper.Sqr(a) * (2 * x + 1);
                y = (int)Math.Round(Math.Sqrt(ySquare));
                x++;
            }
            // Reflects the drawn part to make the whole Hyperbol.
            res = Reflect4(res);
            // Moves the Hyperbol to the center.
            res = Translate(res, center);
            return res;
        }
    }
}
