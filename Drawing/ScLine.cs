using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySCADA.Drawing
{
    public class ScLine : ScShape
    {
        public ScLine(Point loc) : base(loc) { }

        public override string ShapeName()
        {
            return "Line";
        }
        public override void Draw(Graphics g)
        {
            using (var brush = new SolidBrush(BackColor))
            {
                //Pen pen = new Pen(BackColor);
                //g.FillPath(brush,Bounds)

                //// Draws the line
                //g.DrawLine(pen, pt1, pt2);
            }
        }
    }
}
