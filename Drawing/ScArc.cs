using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySCADA.Drawing
{
    public class ScArc : ScShape
    {
        public ScArc(Point loc) : base(loc) { }

        public override string ShapeName()
        {
            return "Arc";
        }
        public override void Draw(Graphics g)
        {
            using(var brush=new SolidBrush(BackColor))
            {
                g.DrawArc(new Pen(BackColor), Bounds,0F, -180F);
            }
        }
    }
}
