using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA.Drawing
{
    public class ScCanvas : Panel
    {
        private Point ptStart;

        private ScShape.HitStatus hit = ScShape.HitStatus.None;

        private bool blnMouseDown;

        private List<lnSnap> snapLines;

        private bool Snapping;

        private List<ScShape> colClickedShapes;

        private Point ptLastClick;

        private int iShapeIterator;

        private ShpCollection shapes;

        private List<ScShape> selectedshapes;

        public int BorderSnapDist
        {
            get;
            set;
        }

        public SnapModes Mode
        {
            get;
            set;
        }

        [Browsable(false)]
        public ScShape SelectedShape
        {
            get
            {
                ScShape item;
                if ((this.SelectedShapes == null ? true : this.SelectedShapes.Count <= 0))
                {
                    item = null;
                }
                else
                {
                    item = this.SelectedShapes[0];
                }
                return item;
            }
        }

        public List<ScShape> SelectedShapes
        {
            get
            {
                return this.selectedshapes;
            }
        }

        public int ShapeAlignDist
        {
            get;
            set;
        }

        public ShpCollection Shapes
        {
            get
            {
                return this.shapes;
            }
        }

        public int ShapeSnapDist
        {
            get;
            set;
        }

        public ScCanvas()
        {
            this.selectedshapes = new List<ScShape>();
            this.SetShapes(new ShpCollection(this));
            base.DoubleBuffered = true;
            this.ShapeAlignDist = 15;
            this.BorderSnapDist = 25;
            this.ShapeSnapDist = 15;
            this.Mode = SnapModes.SnapLines;
        }

        public void AddOrRemoveSelection(ScShape shape)
        {
            if (shape != null)
            {
                if (this.SelectedShapes == null)
                {
                    this.selectedshapes = new List<ScShape>();
                }
                if (!this.SelectedShapes.Contains(shape))
                {
                    this.SelectedShapes.Insert(0, shape);
                }
                else
                {
                    this.SelectedShapes.Remove(shape);
                }
                this.OnSelectedShapeChanged(EventArgs.Empty);
                base.Invalidate();
            }
        }

        private void AlignBottom(ScShape shpfixed, ScShape moving)
        {
            lnSnap snap;
            int x = moving.Location.X;
            int bottom = shpfixed.Bounds.Bottom;
            Rectangle bounds = moving.Bounds;
            moving.Move(new Point(x, bottom - bounds.Height));
            if (moving.Bounds.X <= shpfixed.Bounds.X)
            {
                int left = moving.Bounds.Left;
                int num = moving.Bounds.Bottom;
                int right = shpfixed.Bounds.Right;
                bounds = moving.Bounds;
                snap = new lnSnap(left, num, right, bounds.Bottom, Color.Blue);
            }
            else
            {
                int left1 = shpfixed.Bounds.Left;
                int bottom1 = moving.Bounds.Bottom;
                int right1 = moving.Bounds.Right;
                bounds = moving.Bounds;
                snap = new lnSnap(left1, bottom1, right1, bounds.Bottom, Color.Blue);
            }
            this.snapLines.Add(snap);
        }

        private void AlignLeft(ScShape shpfixed, ScShape moving)
        {
            lnSnap snap;
            int left = shpfixed.Bounds.Left;
            Rectangle bounds = moving.Bounds;
            moving.Move(new Point(left, bounds.Y));
            if (moving.Bounds.Y <= shpfixed.Bounds.Y)
            {
                int num = moving.Bounds.Left;
                int top = moving.Bounds.Top;
                int left1 = moving.Bounds.Left;
                bounds = shpfixed.Bounds;
                snap = new lnSnap(num, top, left1, bounds.Bottom, Color.Blue);
            }
            else
            {
                int num1 = moving.Bounds.Left;
                int top1 = shpfixed.Bounds.Top;
                int left2 = moving.Bounds.Left;
                bounds = moving.Bounds;
                snap = new lnSnap(num1, top1, left2, bounds.Bottom, Color.Blue);
            }
            this.snapLines.Add(snap);
        }

        private void AlignRight(ScShape shpfixed, ScShape moving)
        {
            lnSnap snap;
            int right = shpfixed.Bounds.Right;
            Rectangle bounds = moving.Bounds;
            int width = right - bounds.Width;
            bounds = moving.Bounds;
            moving.Move(new Point(width, bounds.Y));
            if (moving.Bounds.Y <= shpfixed.Bounds.Y)
            {
                int num = moving.Bounds.Right;
                int top = moving.Bounds.Top;
                int right1 = moving.Bounds.Right;
                bounds = shpfixed.Bounds;
                snap = new lnSnap(num, top, right1, bounds.Bottom, Color.Blue);
            }
            else
            {
                int num1 = moving.Bounds.Right;
                int top1 = shpfixed.Bounds.Top;
                int right2 = moving.Bounds.Right;
                bounds = moving.Bounds;
                snap = new lnSnap(num1, top1, right2, bounds.Bottom, Color.Blue);
            }
            this.snapLines.Add(snap);
        }

        private bool AlignShape(int shapeLoc, int otherLoc)
        {
            bool flag = shapeLoc.Between(otherLoc + this.ShapeAlignDist, otherLoc - this.ShapeAlignDist);
            return flag;
        }

        private void AlignShapes(ScShape moving, ScShape shpfixed)
        {
            this.Snapping = true;
            if (this.AlignShape(moving.Bounds.Top, shpfixed.Bounds.Top))
            {
                this.AlignTop(shpfixed, moving);
            }
            if (this.AlignShape(moving.Bounds.Bottom, shpfixed.Bounds.Bottom))
            {
                this.AlignBottom(shpfixed, moving);
            }
            if (this.AlignShape(moving.Bounds.Left, shpfixed.Bounds.Left))
            {
                this.AlignLeft(shpfixed, moving);
            }
            if (this.AlignShape(moving.Bounds.Right, shpfixed.Bounds.Right))
            {
                this.AlignRight(shpfixed, moving);
            }
            this.Snapping = false;
        }

        private void AlignTop(ScShape shpfixed, ScShape moving)
        {
            lnSnap snap;
            int x = moving.Bounds.X;
            Rectangle bounds = shpfixed.Bounds;
            moving.Move(new Point(x, bounds.Top));
            if (moving.Bounds.X <= shpfixed.Bounds.X)
            {
                int left = moving.Bounds.Left;
                int top = moving.Bounds.Top;
                int right = shpfixed.Bounds.Right;
                bounds = moving.Bounds;
                snap = new lnSnap(left, top, right, bounds.Top, Color.Blue);
            }
            else
            {
                int num = shpfixed.Bounds.Left;
                int top1 = moving.Bounds.Top;
                int right1 = moving.Bounds.Right;
                bounds = moving.Bounds;
                snap = new lnSnap(num, top1, right1, bounds.Top, Color.Blue);
            }
            this.snapLines.Add(snap);
        }

        private void DoHitTest(Point pt)
        {
            if (this.SelectedShape == null)
            {
                this.hit = ScShape.HitStatus.None;
            }
            else
            {
                this.hit = this.SelectedShape.HitTest(pt);
            }
            this.SetCursor();
        }

        private void HandleClick(Point pt)
        {
            ScShape item;
            if (pt != this.ptLastClick)
            {
                this.colClickedShapes = this.ShapesAtPoint(pt);
                this.iShapeIterator = 0;
                if (this.colClickedShapes.Count > 0)
                {
                    item = this.colClickedShapes[0];
                }
                else
                {
                    item = null;
                }
                this.Selection(item);
            }
            else if (this.colClickedShapes.Count > 0)
            {
                this.Selection(this.colClickedShapes[this.iShapeIterator]);
                this.iShapeIterator = (this.iShapeIterator + 1) % this.colClickedShapes.Count;
            }
        }

        public void InvalidateRect(Rectangle rect)
        {
            rect.Inflate(1, 1);
            base.Invalidate(rect);
        }

        public void InvalidateShape(ScShape shape)
        {
            this.InvalidateRect(shape.Bounds);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((e.Button == MouseButtons.Left ? true : e.Button == MouseButtons.Right))
            {
                this.blnMouseDown = true;
                if (Control.ModifierKeys != Keys.Control)
                {
                    this.HandleClick(e.Location);
                }
                else
                {
                    this.AddOrRemoveSelection(this.ShapeAtPoint(e.Location));
                }
                if (this.SelectedShape != null)
                {
                    int x = e.Location.X;
                    Point location = this.SelectedShape.Location;
                    int num = x - location.X;
                    int y = e.Location.Y;
                    location = this.SelectedShape.Location;
                    this.ptStart = new Point(num, y - location.Y);
                }
                this.DoHitTest(e.Location);
                base.Invalidate();
                if ((e.Button != MouseButtons.Right || this.SelectedShape == null ? false : this.SelectedShape.ContextMenuStrip != null))
                {
                    this.SelectedShape.ContextMenuStrip.Show(base.PointToScreen(e.Location));
                }
                this.ptLastClick = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!this.blnMouseDown)
            {
                this.DoHitTest(e.Location);
            }
            else
            {
                List<Rectangle> old = new List<Rectangle>();
                if (this.SelectedShape != null)
                {
                    if (this.hit == ScShape.HitStatus.Drag)
                    {
                        List<Point> locs = new List<Point>();
                        for (int i = 1; i < this.SelectedShapes.Count; i++)
                        {
                            ScShape shape = this.SelectedShapes[i];
                            locs.Add(this.SelectedShape.Location.Subtract(shape.Location));
                            old.Add(shape.GrabHandles.TotalBounds);
                        }
                        old.Add(this.SelectedShape.GrabHandles.TotalBounds);
                        Point newLoc = e.Location.Subtract(this.ptStart);
                        if (this.Mode == SnapModes.SnapToGrid)
                        {
                            newLoc = newLoc.Floor(10);
                        }
                        this.SelectedShape.Move(newLoc);
                        this.InvalidateShape(this.SelectedShape);
                        for (int i = 1; i < this.SelectedShapes.Count; i++)
                        {
                            ScShape shape = this.SelectedShapes[i];
                            old.Add(shape.GrabHandles.TotalBounds);
                            shape.Location = this.SelectedShape.Location.Subtract(locs[i - 1]);
                            this.InvalidateShape(shape);
                        }
                    }
                    else if (this.hit != ScShape.HitStatus.None)
                    {
                        Rectangle oldRect = this.SelectedShape.GrabHandles.TotalBounds;
                        old.Add(oldRect);
                        this.SelectedShape.Resize(this.hit, e.X, e.Y);
                        this.InvalidateShape(this.SelectedShape);
                        Rectangle newRect = this.SelectedShape.GrabHandles.TotalBounds;
                        int dx = newRect.Width - oldRect.Width;
                        for (int i = 1; i < this.SelectedShapes.Count; i++)
                        {
                            ScShape shape = this.SelectedShapes[i];
                            old.Add(shape.GrabHandles.TotalBounds);
                            Size oldSize = shape.Size;
                            oldSize.Width = oldSize.Width + dx;
                            if (oldSize.Width < shape.MinimumSize.Width)
                            {
                                oldSize.Width = shape.MinimumSize.Width;
                            }
                            shape.Size = oldSize;
                            this.InvalidateShape(shape);
                        }
                    }
                }
                foreach (Rectangle rect in old)
                {
                    this.InvalidateRect(rect);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.blnMouseDown = false;
            if (this.snapLines != null)
            {
                this.snapLines.Clear();
            }
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.Mode == SnapModes.SnapToGrid)
            {
                this.PaintGrid(e.Graphics);
            }
            else if (this.Mode == SnapModes.SnapLines)
            {
                if ((this.snapLines == null ? false : this.snapLines.Count > 0))
                {
                    foreach (lnSnap snapLine in this.snapLines)
                    {
                        snapLine.Draw(e.Graphics);
                    }
                }
            }
            foreach (ScShape s in this.Shapes)
            {
                s.Draw(e.Graphics);
            }
            e.Graphics.SmoothingMode = SmoothingMode.Default;
            foreach (ScShape s in this.SelectedShapes)
            {
                s.DrawGrabHandles(e.Graphics, this.SelectedShapes.IndexOf(s) == 0);
            }
        }

        protected virtual void OnSelectedShapeChanged(EventArgs e)
        {
            EventHandler eventHandler = this.SelectedShapeChanged;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
            else
            {
            }
        }

        private void PaintGrid(Graphics g)
        {
            for (int x = 0; x < base.Width; x += 10)
            {
                for (int y = 0; y < base.Height; y += 10)
                {
                    g.DrawLine(Pens.LightGray, 0, y, base.Width, y);
                    g.DrawLine(Pens.LightGray, x, 0, x, base.Height);
                }
            }
        }

        internal void RegisterShapeEvents(ScShape s)
        {
            s.LocationChanged += new EventHandler(this.ShapeLocationChanged);
            s.SizeChanged += new EventHandler(this.ShapeSizeChanged);
        }

        public void Selection(ScShape shape)
        {
            if (shape != null)
            {
                if (this.SelectedShapes == null)
                {
                    this.selectedshapes = new List<ScShape>();
                }
                if (!this.SelectedShapes.Contains(shape))
                {
                    this.selectedshapes = new List<ScShape>();
                    this.SelectedShapes.Add(shape);
                }
                else
                {
                    this.SelectedShapes.Remove(shape);
                    this.SelectedShapes.Insert(0, shape);
                }
                this.OnSelectedShapeChanged(EventArgs.Empty);
                base.Invalidate();
            }
            else
            {
                this.selectedshapes = new List<ScShape>();
                this.OnSelectedShapeChanged(EventArgs.Empty);
            }
        }

        private void SetCursor()
        {
            if ((this.SelectedShape == null ? false : this.SelectedShape.Locked))
            {
                this.Cursor = Cursors.Default;
            }
            else if (this.hit == ScShape.HitStatus.Drag)
            {
                this.Cursor = Cursors.SizeAll;
            }
            else if (this.hit == ScShape.HitStatus.ResizeBottom | this.hit == ScShape.HitStatus.ResizeTop)
            {
                this.Cursor = Cursors.SizeNS;
            }
            else if (this.hit == ScShape.HitStatus.ResizeLeft | this.hit == ScShape.HitStatus.ResizeRight)
            {
                this.Cursor = Cursors.SizeWE;
            }
            else if (this.hit == ScShape.HitStatus.ResizeBottomLeft | this.hit == ScShape.HitStatus.ResizeTopRight)
            {
                this.Cursor = Cursors.SizeNESW;
            }
            else if (!(this.hit == ScShape.HitStatus.ResizeBottomRight | this.hit == ScShape.HitStatus.ResizeTopLeft))
            {
                this.Cursor = Cursors.Default;
            }
            else
            {
                this.Cursor = Cursors.SizeNWSE;
            }
        }

        private void SetShapes(ShpCollection colShapes)
        {
            this.shapes = colShapes;
            base.Invalidate();
        }

        private ScShape ShapeAtPoint(Point pt)
        {
            ScShape item;
            List<ScShape> shape = this.ShapesAtPoint(pt);
            if (shape.Count > 0)
            {
                item = shape[0];
            }
            else
            {
                item = null;
            }
            return item;
        }

        internal void ShapeLocationChanged(object sender, EventArgs e)
        {
            ScShape shape = (ScShape)sender;
            if ((this.Mode != SnapModes.SnapLines ? false : this.SelectedShapes.IndexOf(shape) == 0))
            {
                this.Snap(shape);
            }
        }

        private List<ScShape> ShapesAtPoint(Point pt)
        {
            List<ScShape> list = (
                from ScShape s in this.Shapes
                where s.GrabHandles.TotalBounds.Contains(pt)
                orderby this.Shapes.IndexOf(s) descending
                select s).ToList<ScShape>();
            return list;
        }

        internal void ShapeSizeChanged(object sender, EventArgs e)
        {
            ScShape shape = (ScShape)sender;
            if ((this.Mode != SnapModes.SnapLines ? false : this.SelectedShapes.IndexOf(shape) == 0))
            {
                this.Snap(shape);
            }
        }

        private void Snap(ScShape shape)
        {
            Func<ScShape, bool> func = null;
            if (!this.Snapping)
            {
                this.snapLines = new List<lnSnap>();
                ShpCollection shapes = this.Shapes;
                Func<ScShape, bool> func1 = func;
                if (func1 == null)
                {
                    Func<ScShape, bool> func2 = (ScShape s) => (s == shape ? false : !this.SelectedShapes.Contains(s));
                    Func<ScShape, bool> func3 = func2;
                    func = func2;
                    func1 = func3;
                }
                foreach (ScShape _ScShape in shapes.Where<ScShape>(func1))
                {
                    this.AlignShapes(shape, _ScShape);
                    this.SnapShapes(shape, _ScShape);
                }
                this.SnapCanvas(shape);
                base.Invalidate();
            }
        }

        private void SnapBottom(ScShape shpfixed, ScShape moving)
        {
            int x;
            int num = moving.Location.X;
            Rectangle bounds = shpfixed.Bounds;
            moving.Move(new Point(num, bounds.Bottom + this.ShapeSnapDist));
            if (moving.Location.X >= shpfixed.Location.X)
            {
                int left = moving.Bounds.Left;
                bounds = shpfixed.Bounds;
                x = (left + bounds.Right) / 2;
            }
            else
            {
                int left1 = shpfixed.Bounds.Left;
                bounds = moving.Bounds;
                x = (left1 + bounds.Right) / 2;
            }
            int top = moving.Bounds.Top;
            bounds = shpfixed.Bounds;
            lnSnap snapLine = new lnSnap(x, top, x, bounds.Bottom, Color.Purple);
            this.snapLines.Add(snapLine);
        }

        private void SnapCanvas(ScShape shape)
        {
            this.Snapping = true;
            if (this.SnapCanvas(shape.Bounds.Top, 0))
            {
                this.SnapCanvasTop(shape);
            }
            if (this.SnapCanvas(shape.Bounds.Bottom, base.Height))
            {
                this.SnapCanvasBottom(shape);
            }
            if (this.SnapCanvas(shape.Bounds.Left, 0))
            {
                this.SnapCanvasLeft(shape);
            }
            if (this.SnapCanvas(shape.Bounds.Right, base.Width))
            {
                this.SnapCanvasRight(shape);
            }
            this.Snapping = false;
        }

        private bool SnapCanvas(int shapeLoc, int canvasLoc)
        {
            bool flag = shapeLoc.Between(canvasLoc - this.BorderSnapDist, canvasLoc + this.BorderSnapDist);
            return flag;
        }

        private void SnapCanvasBottom(ScShape shape)
        {
            int x = shape.Location.X;
            int height = base.Height - this.BorderSnapDist;
            Size size = shape.Size;
            shape.Move(new Point(x, height - size.Height));
            int num = shape.Location.X;
            size = shape.Size;
            int center = num + size.Width / 2;
            Rectangle bounds = shape.Bounds;
            lnSnap snapLine = new lnSnap(center, bounds.Bottom, center, base.Height, Color.Purple);
            this.snapLines.Add(snapLine);
        }

        private void SnapCanvasLeft(ScShape shape)
        {
            shape.Move(new Point(this.BorderSnapDist, shape.Location.Y));
            int center = shape.Location.Y + shape.Size.Height / 2;
            Rectangle bounds = shape.Bounds;
            lnSnap snapLine = new lnSnap(0, center, bounds.Left, center, Color.Purple);
            this.snapLines.Add(snapLine);
        }

        private void SnapCanvasRight(ScShape shape)
        {
            int width = base.Width - this.BorderSnapDist;
            Size size = shape.Size;
            shape.Move(new Point(width - size.Width, shape.Location.Y));
            int y = shape.Location.Y;
            size = shape.Size;
            int center = y + size.Height / 2;
            Rectangle bounds = shape.Bounds;
            lnSnap snapLine = new lnSnap(bounds.Right, center, base.Width, center, Color.Purple);
            this.snapLines.Add(snapLine);
        }

        private void SnapCanvasTop(ScShape shape)
        {
            Point location = shape.Location;
            shape.Move(new Point(location.X, this.BorderSnapDist));
            int center = shape.Location.X + shape.Size.Width / 2;
            Rectangle bounds = shape.Bounds;
            lnSnap snapLine = new lnSnap(center, 0, center, bounds.Top, Color.Purple);
            this.snapLines.Add(snapLine);
        }

        private void SnapLeft(ScShape shpfixed, ScShape moving)
        {
            int y;
            int left = shpfixed.Bounds.Left;
            Rectangle bounds = moving.Bounds;
            moving.Move(new Point(left - bounds.Width - this.ShapeSnapDist, moving.Location.Y));
            if (moving.Location.Y >= shpfixed.Location.Y)
            {
                int bottom = shpfixed.Bounds.Bottom;
                bounds = moving.Bounds;
                y = (bottom + bounds.Top) / 2;
            }
            else
            {
                int num = moving.Bounds.Bottom;
                bounds = shpfixed.Bounds;
                y = (num + bounds.Top) / 2;
            }
            int right = moving.Bounds.Right;
            bounds = shpfixed.Bounds;
            lnSnap snapLine = new lnSnap(right, y, bounds.Left, y, Color.Purple);
            this.snapLines.Add(snapLine);
        }

        private void SnapRight(ScShape shpfixed, ScShape moving)
        {
            int y;
            Rectangle bounds = shpfixed.Bounds;
            moving.Move(new Point(bounds.Right + this.ShapeSnapDist, moving.Location.Y));
            if (moving.Location.Y >= shpfixed.Location.Y)
            {
                int bottom = shpfixed.Bounds.Bottom;
                bounds = moving.Bounds;
                y = (bottom + bounds.Top) / 2;
            }
            else
            {
                int num = moving.Bounds.Bottom;
                bounds = shpfixed.Bounds;
                y = (num + bounds.Top) / 2;
            }
            int left = moving.Bounds.Left;
            bounds = shpfixed.Bounds;
            lnSnap snapLine = new lnSnap(left, y, bounds.Right, y, Color.Purple);
            this.snapLines.Add(snapLine);
        }

        private bool SnapShape(int shapeLoc, int shapeDim, int otherLoc, int otherDim)
        {
            return (shapeLoc.Between(otherLoc, otherLoc + otherDim) ? true : otherLoc.Between(shapeLoc, shapeLoc + shapeDim));
        }

        private void SnapShapes(ScShape moving, ScShape shpfixed)
        {
            this.Snapping = true;
            if (this.SnapShape(moving.Bounds.Left, moving.Bounds.Width, shpfixed.Bounds.Left, shpfixed.Bounds.Width))
            {
                if (moving.Bounds.Bottom < shpfixed.Bounds.Top)
                {
                    if (moving.Bounds.Bottom.Between(shpfixed.Bounds.Top - this.ShapeSnapDist, shpfixed.Bounds.Top + this.ShapeSnapDist))
                    {
                        this.SnapTop(shpfixed, moving);
                    }
                }
                else if (moving.Bounds.Top > shpfixed.Bounds.Bottom)
                {
                    if (moving.Bounds.Top.Between(shpfixed.Bounds.Bottom - this.ShapeSnapDist, shpfixed.Bounds.Bottom + this.ShapeSnapDist))
                    {
                        this.SnapBottom(shpfixed, moving);
                    }
                }
            }
            if (this.SnapShape(moving.Bounds.Top, moving.Bounds.Height, shpfixed.Bounds.Top, shpfixed.Bounds.Height))
            {
                if (moving.Bounds.Right < shpfixed.Bounds.Left)
                {
                    if (moving.Bounds.Right.Between(shpfixed.Bounds.Left - this.ShapeSnapDist, shpfixed.Bounds.Left + this.ShapeSnapDist))
                    {
                        this.SnapLeft(shpfixed, moving);
                    }
                }
                else if (moving.Bounds.Left > shpfixed.Bounds.Right)
                {
                    if (moving.Bounds.Left.Between(shpfixed.Bounds.Right - this.ShapeSnapDist, shpfixed.Bounds.Right + this.ShapeSnapDist))
                    {
                        this.SnapRight(shpfixed, moving);
                    }
                }
            }
            this.Snapping = false;
        }

        private void SnapTop(ScShape shpfixed, ScShape moving)
        {
            int x;
            int num = moving.Location.X;
            int top = shpfixed.Bounds.Top;
            Rectangle bounds = moving.Bounds;
            moving.Move(new Point(num, top - bounds.Height - this.ShapeSnapDist));
            if (moving.Location.X >= shpfixed.Location.X)
            {
                int left = moving.Bounds.Left;
                bounds = shpfixed.Bounds;
                x = (left + bounds.Right) / 2;
            }
            else
            {
                int left1 = shpfixed.Bounds.Left;
                bounds = moving.Bounds;
                x = (left1 + bounds.Right) / 2;
            }
            int bottom = moving.Bounds.Bottom;
            bounds = shpfixed.Bounds;
            lnSnap snap = new lnSnap(x, bottom, x, bounds.Top, Color.Purple);
            this.snapLines.Add(snap);
        }

        internal void UnregisterShapeEvents(ScShape s)
        {
            s.LocationChanged -= new EventHandler(this.ShapeLocationChanged);
            s.SizeChanged -= new EventHandler(this.ShapeSizeChanged);
        }

        public event EventHandler SelectedShapeChanged;
    }
    public enum SnapModes
    {
        None,
        SnapLines,
        SnapToGrid
    }
}
