using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySCADA.Drawing
{
    public class ScCircle : ScShape
    {
        public ScCircle(Point loc) : base(loc)
        {
        }

        public override string ShapeName()
        {
            return "Circle";
        }

        public override void Draw(Graphics g)
        {
            using (var b = new SolidBrush(this.BackColor))
            {
                g.FillEllipse(b, this.Bounds);
                g.DrawEllipse(Pens.Black, this.Bounds);
            }
        }
    }
}
