using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace sketch_2d
{
    class Helper
    {
        public static float Epsilon = (float)1e-6;
        /// <summary>
        /// Rotates a given point according to a given angle and a given origin.
        /// </summary>
        /// <param name="p"> The point that needs to be rotated. </param>
        /// <param name="origin"> The given origin. </param>
        /// <param name="angle"> The given angle (in degree). </param>
        /// <returns> The rotated point. </returns>
        public static Point Rotate(Point p, Point origin, float angle)
        {            
            var res = new Point(p.X - origin.X, p.Y - origin.Y);
            float rad = (float)Math.PI / 180 * angle;
            res = new Point((int)(Math.Cos(rad) * res.X - Math.Sin(rad) * res.Y),
                            (int)(Math.Sin(rad) * res.X + Math.Cos(rad) * res.Y));
            res = new Point(res.X + origin.X, res.Y + origin.Y);
            return res;
        }

        /// <summary>
        /// Calculates the length of a given vector.
        /// </summary>
        /// <param name="v"> The given vector. </param>
        /// <returns> The length of the given vector. </returns>
        public static double VectorLeng(PointF v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y);
        }

        public static float Sqr(float x)
        {
            return x * x;
        }

        public static float Cube(float x)
        {
            return x * x * x;
        }

        public static float Cbrt(float x)
        {
            return (x > 0? 1 : -1) * (float)Math.Pow(Math.Abs(x), 1.0 / 3);
        }

        /// <summary>
        /// Solve a cubic equation in the form ax^3 + bx^2 + c + d = 0.
        /// </summary>
        /// <param name="a"> The coeff of the equation. </param>
        /// <param name="b"> The coeff of the equation. </param>
        /// <param name="c"> The coeff of the equation. </param>
        /// <param name="d"> The coeff of the equation. </param>
        /// <returns> An array contains the roots. </returns>
        public static float[] SolveCubicEquation(float a, float b, float c, float d)
        {
            var res = new List<float>();
            b /= a;
            c /= a;
            d /= a;
            a = b;
            b = c;
            c = d;
            float p = b - Sqr(a) / 3;
            float q = Cube(a / 3) * 2 - a * b / 3 + c;
            float delta = Sqr(q / 2) + Cube(p / 3);
            if(delta > Epsilon)
            {
                res.Add(Cbrt(-q / 2 + (float)Math.Sqrt(delta)) + Cbrt(-q / 2 - ((float)Math.Sqrt(delta))) - a / 3);
            }
            else if(Math.Abs(delta) <= Epsilon)
            {
                res.Add(-2 * Cbrt(q / 2) - a / 3);
                res.Add(Cbrt(q / 2) - a / 3);
            }
            else
            {
                float t = (float)(1.0 / 3 * Math.Asin(Math.Sqrt(3) * 3 * q / 2 / Cube((float)Math.Sqrt(-p))));
                float k = (float)(2.0 / Math.Sqrt(3) * Math.Sqrt(-p));
                res.Add(k * (float)Math.Sin(t) - a / 3);
                res.Add(-k * (float)Math.Sin(t + Math.PI / 3) - a / 3);
                res.Add(k * (float)Math.Cos(t + Math.PI / 6) - a / 3);
            }
            return res.ToArray();
        }


        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        /// <param name="A"> The first vector. </param>
        /// <param name="B"> The second vector. </param>
        /// <returns> The cross product. </returns>
        public static int CrossProduct(Point A, Point B)
        {
            return A.X * B.Y - A.Y * B.X;
        }

        /// <summary>
        /// Gets a matrix that transforms a rectagle to another rectangle.
        /// </summary>
        /// <param name="A"> The first rectangle. </param>
        /// <param name="B"> The second rectangle. </param>
        /// <returns> The transformation matrix. </returns>
        public static Matrix GetTransformMatrix(Rectangle A, Rectangle B)
        {
            var res = new Matrix();
            res.Translate(-A.X, -A.Y, MatrixOrder.Append);
            res.Scale(((float)B.Width) / A.Width, ((float)B.Height) / A.Height, MatrixOrder.Append);
            res.Translate(B.X, B.Y, MatrixOrder.Append);
            return res;
        }

        /// <summary>
        /// Gets the points represent a parabola that goes through two given points.
        /// Using Midpoint algorithm.
        /// </summary>
        /// <param name="center"> The first point. </param>
        /// <param name="border"> The second point. </param>
        /// <returns> A list of points that represents the parabola. </returns>
        public static List<Point> DrawParabol(Point center, Point border)
        {
            var res = new List<Point>();            
            border.X -= center.X; // Translate border to the center.
            border.Y -= center.Y;
            if (border.X < 0) border.X *= -1;
            bool reflected = false;
            if (border.Y < 0)
            {
                reflected = true;
                border.Y *= -1;
            }
            int x = 0;
            int y = 0;
            long p = border.Y * 2 - (long)Helper.Sqr(border.X);
            while (border.Y * (1 + 2 * x) <= Helper.Sqr(border.X) && x <= border.X && y <= border.Y) // Draws the first part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + 2 * border.Y * (2 * x + 3);
                else
                {
                    p = p + 2 * border.Y * (2 * x + 3) - (long)Helper.Sqr(border.X) * 2;
                    y++;
                }
                x++;
            }
            p = (long)Helper.Sqr(2 * border.X) * (y + 1) - (long)Helper.Sqr(2 * x + 1) * border.Y;
            while (x <= border.X && y <= border.Y) // Draws the second part.
            {
                res.Add(new Point(x, y));
                if (p <= 0) p = p + (long)Helper.Sqr(2 * border.X);
                else
                {
                    p = p + (long)Helper.Sqr(2 * border.X) - 8 * border.Y * (x + 1);
                    x++;
                }
                y++;
            }            
            res.Reverse();
            for(int i = res.Count - 1; i >= 0; i--)            
                res.Add(new Point(-res[i].X, res[i].Y));
            if (reflected) // Reflects the Parabol to its ordinary.
            {
                for (int i = 0; i < res.Count; i++)
                    res[i] = new Point(res[i].X, -res[i].Y);
            }
            // Translates back.
            var matrix = new Matrix();
            matrix.Translate(center.X, center.Y);
            var arr = res.ToArray();
            matrix.TransformPoints(arr);
            return new List<Point>(arr);
        }

        /// <summary>
        /// Draws a Hyperbola x^2/a^2 - y^2/b^2 = 1.        
        /// The center of the curve is (0, 0).
        /// </summary>
        /// <param name="a"> The first parameter. </param>
        /// <param name="b"> The second parameter. </param>
        /// <param name="endPoint"> The point that ends the curve. </param>
        /// <returns> A list of points that represent the curve. </returns>
        public static List<Point> DrawHyperbola(float a, float b, Point endPoint)
        {
            var res = new List<Point>();            
            int x = (int)a;
            int y = 0;
            float xSquare = Helper.Sqr(a);
            float p = Helper.Sqr(2 * a) - Helper.Sqr(b) - Helper.Sqr(2 * b) * a;
            while (Helper.Sqr(a) * y <= Helper.Sqr(b) * x && x <= endPoint.X && y <= endPoint.Y) // Draws the first part.
            {
                res.Add(new Point(x, y));
                xSquare += Helper.Sqr(a) / Helper.Sqr(b) * (2 * y + 1);
                x = (int)Math.Round(Math.Sqrt(xSquare));
                y++;
            }
            double ySquare = Helper.Sqr(y);
            while (x <= endPoint.X && y <= endPoint.Y) // Draws the second part.
            {
                res.Add(new Point(x, y));
                ySquare += ((double)Helper.Sqr(b)) / Helper.Sqr(a) * (2 * x + 1);
                y = (int)Math.Round(Math.Sqrt(ySquare));
                x++;
            }
            // Reflects the drawn part to make the whole Hyperbol.
            var N = res.Count;
            res.Reverse();
            for(int i = N - 1; i >= 0; i--)
            {
                res.Add(new Point(res[i].X, -res[i].Y));                
            }
            N = res.Count;
            for(int i = 0; i < N; i++)
            {
                res.Add(new Point(-res[i].X, res[i].Y));                
            }
            // Moves the Hyperbol to the center.            
            return res;
        }

        public static Point Translate(Point A, int x, int y)
        {
            return new Point(A.X + x, A.Y + y);
        }

        public static Rectangle TransformRectangle(Rectangle rect, Matrix matrix)
        {
            var arr = new Point[2] { rect.Location, new Point(rect.X + rect.Width, rect.Y + rect.Height) };
            matrix.TransformPoints(arr);
            return new Rectangle(arr[0], new Size(arr[1].X - arr[0].X, arr[1].Y - arr[0].Y));
        }
    }
}
