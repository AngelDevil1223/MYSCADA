using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySCADA.Drawing
{
    public class ScPolygon : ScShape
    {
        public ScPolygon(Point loc) : base(loc) { }

        public override string ShapeName()
        {
            return "Polygon";
        }
        public override void Draw(Graphics g)
        {
            using (var brush = new SolidBrush(BackColor))
            {
                Point p1 = new Point(50, 50);
                Point p2 = new Point(100, 25);
                Point p3 = new Point(200, 5);
                Point p4 = new Point(250, 50);
                Point p5 = new Point(300, 100);
                Point p6 = new Point(350, 200);
                Point p7 = new Point(250, 250);
                Point[] curvePoints = { p1, p2, p3, p4, p5, p6, p7 };
                g.DrawPolygon(new Pen(brush,2), curvePoints);
            }
        }
    }
}
