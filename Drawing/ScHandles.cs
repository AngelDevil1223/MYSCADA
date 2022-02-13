using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA.Drawing
{
    public class ScHandles
    {
        public const int Size = 3;

        public ScHandles(ScShape shape)
        {

            this.BorderWidth = 4;
            this.SetBounds(shape.Bounds);

        }

        public Rectangle BorderBounds { get; private set; }
        public int BorderWidth { get; set; }
        public bool Locked { get; set; }

        public Rectangle TotalBounds
        {

            get
            {
                return Rectangle.Union(this.TopLeft,
             this.BottomRight);
            }

        }

        internal Rectangle TopLeft
        {

            get
            {
                return new Rectangle(this.BorderBounds.X - Size,
                   this.BorderBounds.Y - Size, 2 * Size + 1,
                   2 * Size + 1);

            }

        }

        internal Rectangle TopRight
        {

            get
            {

                return new Rectangle(this.BorderBounds.Right - Size,
                   this.BorderBounds.Y - Size, 2 * Size + 1,
                   2 * Size + 1);

            }

        }

        internal Rectangle TopMiddle
        {

            get
            {

                return new Rectangle(this.BorderBounds.X +
                   this.BorderBounds.Width / 2 - Size,
                   this.BorderBounds.Y - Size, 2 * Size + 1,
                   2 * Size + 1);

            }

        }

        internal Rectangle MiddleLeft
        {

            get
            {

                return new Rectangle(this.BorderBounds.X - Size,
                   this.BorderBounds.Y + this.BorderBounds.Height / 2 -
                   Size, 2 * Size + 1, 2 * Size + 1);

            }

        }

        internal Rectangle MiddleRight
        {

            get
            {

                return new Rectangle(this.BorderBounds.Right - Size,
                   this.BorderBounds.Y + this.BorderBounds.Height / 2 -
                   Size, 2 * Size + 1, 2 * Size + 1);

            }

        }

        internal Rectangle MiddleMiddle
        {

            get
            {

                return new Rectangle(this.BorderBounds.X +
                   this.BorderBounds.Width / 2 - Size,
                   this.BorderBounds.Y + this.BorderBounds.Height / 2 -
                   Size, 2 * Size + 1, 2 * Size + 1);

            }

        }

        internal Rectangle BottomLeft
        {

            get
            {

                return new Rectangle(this.BorderBounds.X - Size,
                   this.BorderBounds.Bottom - Size, 2 * Size + 1,
                   2 * Size + 1);

            }

        }

        internal Rectangle BottomRight
        {

            get
            {

                return new Rectangle(this.BorderBounds.Right - Size,
                   this.BorderBounds.Bottom - Size, 2 * Size + 1,
                   2 * Size + 1);

            }

        }

        internal Rectangle BottomMiddle
        {

            get
            {

                return new Rectangle(this.BorderBounds.X +
                   this.BorderBounds.Width / 2 - Size,
                   this.BorderBounds.Bottom - Size, 2 * Size + 1,
                   2 * Size + 1);

            }

        }

        internal void SetBounds(Rectangle shape)
        {

            this.BorderBounds = new Rectangle(shape.X -
               this.BorderWidth, shape.Y - this.BorderWidth,
               shape.Width + 2 * this.BorderWidth, shape.Height + 2 *
               this.BorderWidth);

        }

        internal void Draw(Graphics g, bool firstSelection)
        {

            ControlPaint.DrawBorder(g, this.BorderBounds,
               ControlPaint.ContrastControlDark,
               ButtonBorderStyle.Dotted);

            if (this.Locked)
            {

                this.DrawLock(g);

            }

            else
            {
                this.DrawGrabHandle(g, this.TopLeft, firstSelection);
                this.DrawGrabHandle(g, this.TopMiddle,
                   firstSelection);
                this.DrawGrabHandle(g, this.TopRight, firstSelection);
                this.DrawGrabHandle(g, this.MiddleLeft,
                   firstSelection);
                this.DrawGrabHandle(g, this.MiddleRight,
                   firstSelection);
                this.DrawGrabHandle(g, this.BottomLeft,
                   firstSelection);
                this.DrawGrabHandle(g, this.BottomMiddle,
                   firstSelection);
                this.DrawGrabHandle(g, this.BottomRight,
                   firstSelection);

            }
        }

        private void DrawGrabHandle(Graphics g, Rectangle rect,
              bool firstSel)
        {

            if (firstSel)
            {
                var rect1 = rect;
                var rect2 = rect;

                var innerRect = rect;

                innerRect.Inflate(-1, -1);

                rect1.X += 1;
                rect1.Width -= 2;
                rect2.Y += 1;
                rect2.Height -= 2;

                g.FillRectangle(Brushes.Black, rect1);
                g.FillRectangle(Brushes.Black, rect2);
                g.FillRectangle(Brushes.White, innerRect);

            }

            else
            {

                g.FillRectangle(Brushes.Black, rect);

            }

        }

        private void DrawLock(Graphics g)
        {

            var rect = this.TopLeft;

            rect.X -= 1;
            rect.Width -= 1;
            rect.Height -= 2;

            var innerRect = rect;
            innerRect.Inflate(-1, -1);

            g.FillRectangle(Brushes.White, innerRect);
            g.DrawRectangle(Pens.Black, rect);

            var outerHandleRect1 = rect;

            outerHandleRect1.Y -= 2;
            outerHandleRect1.Height = 2;
            outerHandleRect1.Width = 5;
            outerHandleRect1.X += 1;

            var outerHandleRect2 = outerHandleRect1;

            outerHandleRect2.Y -= 1;
            outerHandleRect2.X += 1;
            outerHandleRect2.Width = 3;
            outerHandleRect2.Height = 1;

            var innerHandleRect = outerHandleRect1;

            innerHandleRect.X += 1;
            innerHandleRect.Width = 3;

            g.FillRectangle(Brushes.Black, outerHandleRect1);
            g.FillRectangle(Brushes.Black, outerHandleRect2);
            g.FillRectangle(Brushes.White, innerHandleRect);

        }
    }
}
