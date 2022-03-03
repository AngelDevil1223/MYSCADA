using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySCADA.Drawing
{
    internal static class Ext
    {
        public static Point Add(this Point Point1, Point Point2)
        {

            return new Point(Point1.X + Point2.X, Point1.Y +
               Point2.Y);

        }

        public static Point Subtract(this Point Point1, Point Point2)
        {

            return Point1.Add(Point2.Negate());

        }

        public static Point Negate(this Point Point1)
        {

            return new Point(-Point1.X, -Point1.Y);

        }

        public static Point Floor(this Point Point1, int Grid)
        {

            var X = Point1.X / Grid;
            var Y = Point1.Y / Grid;

            return new Point(X * Grid, Y * Grid);

        }

        public static bool Between(this int Location1, int Location2,
           int Location3)
        {
            if (Location2 > Location3) return (Location3
               <= Location1 && Location2 > Location1);
            if (Location2 < Location3) return (Location2
               <= Location1 && Location3 > Location1);

            return false;
        }

    }
    public class lnSnap
    {
        public lnSnap(int X1, int Y1, int X2, int Y2,
           Color LineColor)
        {
            this.X1 = X1;
            this.Y1 = Y1;
            this.X2 = X2;
            this.Y2 = Y2;

            this.Color = LineColor;
        }

        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public Color Color { get; set; }

        public void Draw(Graphics g)
        {

            using (var p = new Pen(this.Color))
            {
                g.DrawLine(p, this.X1, this.Y1, this.X2, this.Y2);
            }
        }
    }
}
