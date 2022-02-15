using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySCADA.Drawing
{
    public class ScRectangle : ScShape
    {
        public ScRectangle(Point loc) : base(loc)
        {
        }

        public override string ShapeName()
        {
            return "Rectangle";
        }
        public override void Draw(Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(b, Bounds);
                g.DrawRectangle(Pens.Black, Bounds);
            }
        }
    }
}
