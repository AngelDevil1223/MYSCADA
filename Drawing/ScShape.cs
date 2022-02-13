using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MySCADA.Drawing
{
    public abstract class ScShape
    {
        protected ScShape() : this(Point.Empty)
        {

        }

        protected ScShape(Point loc)
        {

            this.MinimumSize = new Size(50, 50);
            this.Bounds = new Rectangle(loc, this.DefaultSize);
            this.BackColor = Color.White;
            this.Locked = false;

        }

        public enum HitStatus
        {

            None,
            Drag,
            ResizeTopLeft,
            ResizeTopRight,
            ResizeBottomLeft,
            ResizeBottomRight,
            ResizeLeft,
            ResizeTop,
            ResizeRight,
            ResizeBottom

        }

        public event EventHandler LocationChanged;
        public event EventHandler SizeChanged;
        public event EventHandler AppearanceChanged;

        protected virtual void OnLocationChanged(EventArgs e)
        {

            if (LocationChanged != null)
                this.LocationChanged(this, e);

        }

        protected virtual void OnSizeChanged(EventArgs e)
        {

            if (this.SizeChanged != null) this.SizeChanged(this, e);

        }

        protected virtual void OnAppearanceChanged(EventArgs e)
        {

            if (this.AppearanceChanged != null)
                this.AppearanceChanged(this, e);

        }
        private string name = String.Empty;
        public string Name
        {

            get { return name; }

            set
            {

                name = value;

            }

        }

        private Rectangle bounds;
        [Browsable(false)]
        public Rectangle Bounds
        {

            get { return bounds; }

            set
            {

                bounds = value;
                this.GrabHandles.SetBounds(value);

            }

        }

        [XmlIgnore]
        public Point Location
        {

            get { return this.Bounds.Location; }

            set
            {

                if (this.Bounds.Location == value) return;
                Rectangle rect = this.Bounds;
                rect.Location = value;
                this.Bounds = rect;
                this.OnLocationChanged(EventArgs.Empty);

            }
        }

        [XmlIgnore]
        public Size Size
        {

            get { return this.Bounds.Size; }

            set
            {

                if (this.Bounds.Size == value) return;
                Rectangle rect = this.Bounds;

                rect.Size = value;
                this.Bounds = rect;
                this.OnSizeChanged(EventArgs.Empty);

            }
        }

        private bool locked;

        public virtual bool Locked
        {

            get { return locked; }

            set
            {

                locked = value;
                this.GrabHandles.Locked = value;

            }
        }

        private ScHandles grabhandles;
        [XmlIgnore]
        internal ScHandles GrabHandles
        {

            get
            {

                if (grabhandles == null) grabhandles = new
                      ScHandles(this);
                return grabhandles;

            }

        }

        private Size minsize;
        [Browsable(false)]
        public Size MinimumSize
        {

            get { return minsize; }

            set
            {

                if (value.Width < 0 || value.Height < 0)

                    minsize = value;
            }

        }

        internal Point MoveStart { get; set; }

        [XmlIgnore]
        protected virtual Size DefaultSize
        {

            get { return new Size(150, 150); }

        }

        [XmlIgnore]
        public Color BackColor { get; set; }

        [Browsable(false)]
        public int XmlBackColor
        {

            get { return this.BackColor.ToArgb(); }
            set { this.BackColor = Color.FromArgb(value); }

        }

        [Browsable(false)]
        [XmlIgnore]
        public ContextMenuStrip ContextMenuStrip { get; set; }

        public abstract void Draw(Graphics g);

        internal virtual void DrawGrabHandles(Graphics g, bool first)
        {

            this.GrabHandles.Draw(g, first);

        }

        public void Move(Point newLocation)
        {

            if (this.Locked) return;
            this.Location = newLocation;

        }

        public void Resize(HitStatus hitStatus, int x, int y)
        {

            if (this.Locked) return;

            switch (hitStatus)
            {

                case HitStatus.ResizeBottomLeft:
                    this.ResizeBottomLeft(x, y);
                    break;

                case HitStatus.ResizeBottomRight:
                    this.ResizeBottomRight(x, y);
                    break;

                case HitStatus.ResizeTopLeft:
                    this.ResizeTopLeft(x, y);
                    break;

                case HitStatus.ResizeTopRight:
                    this.ResizeTopRight(x, y);
                    break;

                case HitStatus.ResizeLeft:
                    this.ResizeLeft(x, y);
                    break;

                case HitStatus.ResizeRight:
                    this.ResizeRight(x, y);
                    break;

                case HitStatus.ResizeTop:
                    this.ResizeTop(x, y);
                    break;

                case HitStatus.ResizeBottom:
                    this.ResizeBottom(x, y);
                    break;

            }

        }

        private void ResizeBottomLeft(int x, int y)
        {

            Rectangle oldBounds = this.Bounds;
            int newTop = oldBounds.Top;

            int newLeft = x;
            int newWidth = oldBounds.Right - x;
            int newHeight = y - oldBounds.Top;

            if (newWidth < this.MinimumSize.Width)
            {

                newWidth = this.MinimumSize.Width;
                newLeft = oldBounds.Right - newWidth;

            }

            if (newHeight < this.MinimumSize.Height)
            {

                newHeight = this.MinimumSize.Height;

            }

            this.Bounds = new Rectangle(newLeft, newTop, newWidth,
               newHeight);
        }

        private void ResizeBottomRight(int x, int y)
        {

            Rectangle oldBounds = this.Bounds;

            int newTop = oldBounds.Top;
            int newLeft = oldBounds.Left;
            int newWidth = x - newLeft;
            int newHeight = y - oldBounds.Top;


            if (newWidth < this.MinimumSize.Width)
            {

                newWidth = this.MinimumSize.Width;

            }

            if (newHeight < this.MinimumSize.Height)
            {

                newHeight = this.MinimumSize.Height;

            }

            this.Bounds = new Rectangle(newLeft, newTop, newWidth,
               newHeight);

        }

        private void ResizeTopLeft(int x, int y)
        {

            Rectangle oldBounds = this.Bounds;

            int newTop = y;
            int newLeft = x;
            int newWidth = oldBounds.Right - x;
            int newHeight = oldBounds.Bottom - y;

            if (newWidth < this.MinimumSize.Width)
            {

                newWidth = this.MinimumSize.Width;
                newLeft = oldBounds.Right - newWidth;

            }

            if (newHeight < this.MinimumSize.Height)
            {

                newHeight = this.MinimumSize.Height;
                newTop = oldBounds.Bottom - newHeight;

            }

            this.Bounds = new Rectangle(newLeft, newTop, newWidth,
               newHeight);

        }

        private void ResizeTopRight(int x, int y)
        {

            Rectangle oldBounds = this.Bounds;

            int newTop = y;
            int newLeft = oldBounds.Left;
            int newWidth = x - newLeft;
            int newHeight = oldBounds.Bottom - y;

            if (newWidth < this.MinimumSize.Width)
            {

                newWidth = this.MinimumSize.Width;

            }
            if (newHeight < this.MinimumSize.Height)
            {

                newHeight = this.MinimumSize.Height;
                newTop = oldBounds.Bottom - newHeight;

            }

            this.Bounds = new Rectangle(newLeft, newTop, newWidth,
               newHeight);
        }

        private void ResizeTop(int x, int y)
        {

            Rectangle oldBounds = this.Bounds;

            int newTop = y;
            int newLeft = oldBounds.Left;
            int newWidth = oldBounds.Width;
            int newHeight = oldBounds.Bottom - y;

            if (newHeight < this.MinimumSize.Height)
            {

                newHeight = this.MinimumSize.Height;
                newTop = oldBounds.Bottom - newHeight;

            }

            this.Bounds = new Rectangle(newLeft, newTop, newWidth,
               newHeight);
        }

        private void ResizeLeft(int x, int y)
        {

            Rectangle oldBounds = this.Bounds;

            int newTop = oldBounds.Top;
            int newLeft = x;
            int newWidth = oldBounds.Right - x;
            int newHeight = oldBounds.Height;

            if (newWidth < this.MinimumSize.Width)
            {

                newWidth = this.MinimumSize.Width;
                newLeft = oldBounds.Right - newWidth;

            }

            this.Bounds = new Rectangle(newLeft, newTop, newWidth,
               newHeight);

        }

        private void ResizeRight(int x, int y)
        {

            Rectangle oldBounds = this.Bounds;

            int newTop = oldBounds.Top;
            int newLeft = oldBounds.Left;
            int newWidth = x - newLeft;
            int newHeight = oldBounds.Height;

            if (newWidth < this.MinimumSize.Width)
            {

                newWidth = this.MinimumSize.Width;

            }

            this.Bounds = new Rectangle(newLeft, newTop, newWidth,
               newHeight);

        }

        private void ResizeBottom(int x, int y)
        {

            Rectangle oldBounds = this.Bounds;

            int newTop = oldBounds.Top;
            int newLeft = oldBounds.Left;
            int newWidth = oldBounds.Width;
            int newHeight = y - oldBounds.Top;

            if (newHeight < this.MinimumSize.Height)
            {

                newHeight = this.MinimumSize.Height;

            }

            this.Bounds = new Rectangle(newLeft, newTop, newWidth,
               newHeight);

        }

        public HitStatus HitTest(Point loc)
        {

            if (this.GrabHandles.TotalBounds.Contains(loc))
            {

                if (this.GrabHandles.TopLeft.Contains(loc))
                    return HitStatus.ResizeTopLeft;

                else if (this.GrabHandles.TopRight.Contains(loc))
                    return HitStatus.ResizeTopRight;

                else if (this.GrabHandles.BottomLeft.Contains(loc))
                    return HitStatus.ResizeBottomLeft;

                else if (this.GrabHandles.BottomRight.Contains(loc))
                    return HitStatus.ResizeBottomRight;

                if (Rectangle.Union(this.GrabHandles.TopLeft,
                      this.GrabHandles.TopRight).Contains(loc))
                    return HitStatus.ResizeTop;

                else if (Rectangle.Union(this.GrabHandles.TopRight,
                      this.GrabHandles.BottomRight).Contains(loc))
                    return HitStatus.ResizeRight;

                else if (Rectangle.Union(this.GrabHandles.BottomRight,
                      this.GrabHandles.BottomLeft).Contains(loc))
                    return HitStatus.ResizeBottom;

                else if (Rectangle.Union(this.GrabHandles.BottomLeft,
                      this.GrabHandles.TopLeft).Contains(loc))
                    return HitStatus.ResizeLeft;

                return HitStatus.Drag;

            }

            else
            {

                return HitStatus.None;

            }
        }

        public virtual string ShapeName()
        {

            return this.GetType().Name;

        }
    }
}
