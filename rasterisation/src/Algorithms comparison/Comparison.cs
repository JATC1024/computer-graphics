using System.Collections;
using System.Collections.Generic;
using System.IO;
using Graphics_drawing;
using System;
using Point = Graphics_drawing.Point;

class Comparison
{
    static Point RandomPoint(Random rd, int maxCoor)
    {
        return new Point(rd.Next(0, maxCoor), rd.Next(0, maxCoor));
    }

    static double LineDistance(Point A, Point B, List<Point> pixels)
    {
        double res = 0;
        long a = A.y - B.y;
        long b = B.x - A.x;
        long c = -(a * A.x + b * A.y);
        foreach (var p in pixels)
            res += Helper.Sqr(a * p.x + b * p.y + c);
        return res;
    }

    static double CircleDistance(Point center, int radius, List<Point> pixels)
    {
        double res = 0;
        foreach (var p in pixels)
            res += Math.Abs(Helper.Sqr(p.x - center.x) + Helper.Sqr(p.y - center.y) - Helper.Sqr(radius));
        return res;
    }

    static double EllipseDistance(Point center, int a, int b, List<Point> pixels)
    {
        double res = 0;
        foreach (var p in pixels)
            res += Math.Abs(((double)Helper.Sqr(p.x)) / Helper.Sqr(a) + ((double)Helper.Sqr(p.y)) / Helper.Sqr(b) - 1);
        return res;
    }

    static void Main()
    {
        int MaxWidth = 1000;
        int maxObjectsNumber = 10000;
        var sw = new StreamWriter("Record.txt");
        var ddaDrawer = new DDA(MaxWidth, MaxWidth);
        var bresenhamDrawer = new Bresenham(MaxWidth, MaxWidth);
        var midpointDrawer = new MidPoint(MaxWidth, MaxWidth);
        var random = new Random();


        // Compares the lines drawn by the three algorithms.
        sw.WriteLine("2D drawing algorithms compared by Line objects:");
        sw.WriteLine("Each algorithm will drawn " + maxObjectsNumber + " Lines. The algorithms are mapped with a value \"f\" >= 0.");
        sw.WriteLine("The smaller the f, the more precise the algorithm.");
        double fdda = 0, fbre = 0, fmid = 0;
        for(int i = 0; i < maxObjectsNumber; i++)
        {
            var A = RandomPoint(random, MaxWidth);
            var B = RandomPoint(random, MaxWidth);
            var l1 = ddaDrawer.DrawLine(A, B);
            var l2 = bresenhamDrawer.DrawLine(A, B);
            var l3 = midpointDrawer.DrawLine(A, B);
            fdda += LineDistance(A, B, l1);
            fbre += LineDistance(A, B, l2);
            fmid += LineDistance(A, B, l3);
        }
        fdda /= maxObjectsNumber;
        fbre /= maxObjectsNumber;
        fmid /= maxObjectsNumber;
        sw.WriteLine("DDA: " + fdda);
        sw.WriteLine("Bresenham: " + fbre);
        sw.WriteLine("MidPoint: " + fmid);
        sw.WriteLine();

        // Compares the circles drawn by the algorithms.
        sw.WriteLine("2D drawing algorithms compared by Circle objects:");
        sw.WriteLine("Each algorithm will drawn " + maxObjectsNumber + " Circles. The algorithms are mapped with a value \"f\" >= 0.");
        sw.WriteLine("The smaller the f, the more precise the algorithm.");
        fdda = 0;  fbre = 0; fmid = 0;
        for (int i = 0; i < maxObjectsNumber; i++)
        {
            var A = RandomPoint(random, MaxWidth);
            int radius = random.Next(0, MaxWidth);
            var l1 = ddaDrawer.DrawCircle(A, radius);
            var l2 = bresenhamDrawer.DrawCircle(A, radius);
            var l3 = midpointDrawer.DrawCircle(A, radius);
            fdda += CircleDistance(A, radius, l1);
            fbre += CircleDistance(A, radius, l2);
            fmid += CircleDistance(A, radius, l3);            
        }
        fdda /= maxObjectsNumber;
        fbre /= maxObjectsNumber;
        fmid /= maxObjectsNumber;
        sw.WriteLine("DDA: " + fdda);
        sw.WriteLine("Bresenham: " + fbre);
        sw.WriteLine("MidPoint: " + fmid);


        // Compares the Ellipses drawn by the algorithms.
        sw.WriteLine("2D drawing algorithms compared by Ellipse objects:");
        sw.WriteLine("Each algorithm will drawn " + maxObjectsNumber + " Ellipses. The algorithms are mapped with a value \"f\" >= 0.");
        sw.WriteLine("The smaller the f, the more precise the algorithm.");
        fdda = 0; fbre = 0; fmid = 0;
        for (int i = 0; i < maxObjectsNumber; i++)
        {
            var A = RandomPoint(random, MaxWidth);
            int a = random.Next(1, MaxWidth - 1);
            int b = random.Next(1, MaxWidth - 1);
            var l1 = ddaDrawer.DrawEllipse(A, a, b);
            var l2 = bresenhamDrawer.DrawEllipse(A, a, b);
            var l3 = midpointDrawer.DrawEllipse(A, a, b);
            fdda += EllipseDistance(A, a, b, l1);
            fbre += EllipseDistance(A, a, b, l2);
            fmid += EllipseDistance(A, a, b, l3);
        }
        fdda /= maxObjectsNumber;
        fbre /= maxObjectsNumber;
        fmid /= maxObjectsNumber;
        sw.WriteLine("DDA: " + fdda);
        sw.WriteLine("Bresenham: " + fbre);
        sw.WriteLine("MidPoint: " + fmid);

        sw.Close();
    }
}