using MySCADA.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Text;
using MySCADA.Controls;

namespace MySCADA
{
    public partial class VisualEditor : Form
    {
        const int DRAG_HANDLE_SIZE = 7;
        int mouseX, mouseY;
        Control SelectedControl;
        List<Control> SelectedControls = new();
        Control copiedControl;
        Direction direction;
        Point newLocation;
        Size newSize;
        string[] gParam = null;
        Bitmap MemoryImage;
        bool cutCheck = false;
        bool copyCheck = false;
        private ToolTip tt;
        int x;
        int y;

        UserForm form;

        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr AddFontMemResourceEx(byte[] pbFont, int cbFont, IntPtr pdv, out uint pcFonts);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern bool RemoveFontMemResourceEx(IntPtr fh);

        static private IntPtr m_fh = IntPtr.Zero;
        static private PrivateFontCollection m_pfc = null;
        string[] gParamPop = null;
        List<string> ControlNames = new();
        enum Direction
        {
            NW,
            N,
            NE,
            W,
            E,
            SW,
            S,
            SE,
            None
        }
        private string FontToString(Font font)
        {
            return font.FontFamily.Name + ":" + font.Size + ":" + (int)font.Style;
        }

        private Font StringToFont(string font)
        {
            string[] parts = font.Split(':');
            if (parts.Length != 3)
                throw new ArgumentException("Not a valid font string", "font");

            Font loadedFont = new(parts[0], float.Parse(parts[1]), (FontStyle)int.Parse(parts[2]));
            return loadedFont;
        }

        private void control_Click(object sender, EventArgs e)
        {
            //if (rdoMessage.Checked == true)
            //{
            //    RunTimeCodeGenerate(txtCode.Text.Trim());
            //}
            //else if (rdoDataTable.Checked == true)
            //{
            //    RunTimeCodeGenerate_ReturnTypeDataTable(txtCode.Text.Trim());
            //}
            //else if (rdoXML.Checked == true)
            //{
            //    RunTimeCodeGenerate_ReturnTypeDataTable(txtCode.Text.Trim());
            //}
            //else if (rdoDatabase.Checked == true)
            //{
            //    RunTimeCodeGenerate_ReturnTypeDataTable(txtCode.Text.Trim());
            //}
        }

        private void control_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            Cursor = Cursors.SizeAll;
            Control control = (Control)sender;
            tt = new ToolTip();
            tt.InitialDelay = 0;
            tt.IsBalloon = true;
            tt.Show(control.Name.ToString(), control);
        }
        private void control_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            tt.Dispose();
            timer1.Start();
        }
        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var ctrl = (Control)sender;

                pnControls.Invalidate();
                SelectedControl = (Control)sender;
                if (SelectedControls.Contains(ctrl))
                    SelectedControls.Remove(ctrl);
                else
                    SelectedControls.Add((Control)sender);
                Control control = (Control)sender;
                mouseX = -e.X;
                mouseY = -e.Y;
                control.Invalidate();
                DrawControlBorder(sender);
                propertyGrid1.SelectedObject = SelectedControl;
            }
        }
        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control control = (Control)sender;
                Point nextPosition = new();
                nextPosition = pnControls.PointToClient(MousePosition);
                nextPosition.Offset(mouseX, mouseY);
                control.Location = nextPosition;
                Invalidate();
            }
        }
        private void control_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control control = (Control)sender;
                Cursor.Clip = System.Drawing.Rectangle.Empty;
                control.Invalidate();
                DrawControlBorder(control);
            }
        }
        private void DrawControlBorder(object sender)
        {
            Control control = (Control)sender;

            Rectangle Border = new(
                new Point(control.Location.X - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE / 2),
                new Size(control.Size.Width + DRAG_HANDLE_SIZE,
                    control.Size.Height + DRAG_HANDLE_SIZE));
            Rectangle NW = new(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle N = new(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle NE = new(
                new Point(control.Location.X + control.Width,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle W = new(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle E = new(
                new Point(control.Location.X + control.Width,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle SW = new(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle S = new(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle SE = new(
                new Point(control.Location.X + control.Width,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));

            Graphics g = pnControls.CreateGraphics();
            ControlPaint.DrawBorder(g, Border, Color.Gray, ButtonBorderStyle.Dotted);
            ControlPaint.DrawGrabHandle(g, NW, true, true);
            ControlPaint.DrawGrabHandle(g, N, true, true);
            ControlPaint.DrawGrabHandle(g, NE, true, true);
            ControlPaint.DrawGrabHandle(g, W, true, true);
            ControlPaint.DrawGrabHandle(g, E, true, true);
            ControlPaint.DrawGrabHandle(g, SW, true, true);
            ControlPaint.DrawGrabHandle(g, S, true, true);
            ControlPaint.DrawGrabHandle(g, SE, true, true);
            g.Dispose();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                Point pos = pnControls.PointToClient(MousePosition);
                if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X) &&
                    (pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y))
                {
                    direction = Direction.NW;
                    Cursor = Cursors.SizeNWSE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE))
                {
                    direction = Direction.SE;
                    Cursor = Cursors.SizeNWSE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2) &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                    pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y)
                {
                    direction = Direction.N;
                    Cursor = Cursors.SizeNS;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2) &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE)
                {
                    direction = Direction.S;
                    Cursor = Cursors.SizeNS;
                }
                else if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2))
                {
                    direction = Direction.W;
                    Cursor = Cursors.SizeWE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2))
                {
                    direction = Direction.E;
                    Cursor = Cursors.SizeWE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE) &&
                    (pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y))
                {
                    direction = Direction.NE;
                    Cursor = Cursors.SizeNESW;
                }
                else if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X + DRAG_HANDLE_SIZE) &&
                    (pos.Y >= SelectedControl.Location.Y + SelectedControl.Height - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE))
                {
                    direction = Direction.SW;
                    Cursor = Cursors.SizeNESW;
                }
                else
                {
                    Cursor = Cursors.Default;
                    direction = Direction.None;
                }
            }
            else
            {
                direction = Direction.None;
                Cursor = Cursors.Default;
            }
        }

        public VisualEditor(UserForm f)
        {
            InitializeComponent();
            form = f;
            pnControls.ControlAdded += (s, e) =>
            {
                pnControls.ContextMenuStrip = contextMenuStrip1;
            };
            pnControls.BackgroundImage = Properties.Resources.dotedback;
            LoadComponents();
        }
        void LoadComponents()
        {
            var assembly = Assembly.GetAssembly(typeof(Button));
            var drawing = Assembly.GetAssembly(typeof(ScCircle)).GetTypes();
            var elements = ScadaProject.ActiveProject.ReadForm(form.DesignerFile);
            var types = assembly.GetTypes();

            //if (elements.FormElements.Count == 0)
            //{
            //    var panel = new ScCanvas()
            //    {
            //        Size = new Size(Width = 249, Height = 67)
            //    };
            //    panel.Name = "MainPanel";
            //    panel.MouseEnter += new EventHandler(control_MouseEnter);
            //    panel.MouseLeave += new EventHandler(control_MouseLeave);
            //    panel.MouseDown += new MouseEventHandler(control_MouseDown);
            //    panel.MouseMove += new MouseEventHandler(control_MouseMove);
            //    panel.MouseUp += new MouseEventHandler(control_MouseUp);
            //    panel.Click += new EventHandler(control_Click);
            //    panel.BackgroundImage = Properties.Resources.dotedback;
            //    pnControls.Controls.Add(panel);
            //    panel.SendToBack();
            //}
            foreach (var e in elements.FormElements)
            {
                var tp = GetElementType(types, drawing, e.Type);
                if (tp != null)
                {
                    var props = tp.GetProperties();
                    var events = tp.GetEvents();
                    dynamic ctrl = Activator.CreateInstance(tp);
                    ctrl.BringToFront();
                    ctrl.MouseEnter += new EventHandler(control_MouseEnter);
                    ctrl.MouseLeave += new EventHandler(control_MouseLeave);
                    ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
                    ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
                    ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
                    ctrl.Click += new EventHandler(control_Click);
                    foreach (var a in e.Attributes)
                    {
                        var prop = props.FirstOrDefault(y => y.Name == a.Name);
                        if (prop != null && a.Value != null)
                        {
                            try
                            {
                                prop.SetValue(ctrl, Convert.ChangeType(a.Value, prop.PropertyType));
                            }
                            catch (Exception) { }
                        }
                    }
                    if (ctrl.Name == "MainPanel")
                    {
                        //ctrl.BackgroundImage = Properties.Resources.dotedback;
                    }
                    else
                    {
                        pnControls.Controls.Add(ctrl);
                    }
                }
            }
        }
        Type GetElementType(Type[] types,Type[] drawing,string type)
        {
            return types.FirstOrDefault(x => x.Name == type) ?? drawing.FirstOrDefault(x => x.Name == type);
        }
        void SaveForm()
        {
            var frm = new ScadaForm();
            foreach (var c in pnControls.Controls)
            {
                var properties = c.GetType().GetProperties();
                var events = c.GetType().GetEvents();
                var el = new FElement()
                {
                    Type = c.GetType().Name,
                    Attributes = properties.Select(x => new FElementAttribute()
                    {
                        Name = x.Name,
                        Type = x.PropertyType.Name,
                        Value = x.GetValue(c)?.ToString()
                    }).ToList(),
                    Events = events.Where(x => x.GetRaiseMethod() != null).Select(e => new FElementEvent()
                    {
                        Event = e.Name,
                        Handler = e.GetRaiseMethod().Name
                    }).ToList()
                };
                frm.FormElements.Add(el);
            }
            var str = JsonConvert.SerializeObject(frm);


            string loc = $"{ScadaProject.ActiveProject.Location}\\UserForms";

            FileInfo info = new($"{loc}\\{form.DesignerFile}");
            info.Directory.Create();

            File.WriteAllText($"{loc}\\{form.DesignerFile}", str);
            MessageBox.Show("Changes committed successfully", "Success");
        }

        private void pnControls_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedControl != null)
                DrawControlBorder(SelectedControl);
            foreach(var c in SelectedControls)
            {
                DrawControlBorder(c);
            }
            timer1.Start();
        }

        private void toolButton_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string btnName = "btn_" + randNumber;

            Button ctrl = new();
            ctrl.Location = new Point(50, 150);
            ctrl.Name = btnName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.Text = "Button";
            ctrl.BringToFront();
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += new EventHandler(control_Click);
            pnControls.Controls.Add(ctrl);
        }

        private void toolLabel_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string LableName = "Lbl_" + randNumber;
            Label ctrl = new();
            ctrl.Location = new Point(30, 130);
            ctrl.Name = LableName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.Text = "Label1";
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.BringToFront();
            pnControls.Controls.Add(ctrl);
        }

        private void toolCheckbox_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string chkName = "chk_" + randNumber;

            CheckBox ctrl = new();
            ctrl.Location = new Point(120, 140);
            ctrl.Name = chkName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.BringToFront();
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);

            pnControls.Controls.Add(ctrl);
        }

        private void toolAutocomplete_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string ComboName = "cbo_" + randNumber;

            ComboBox ctrl = new();
            ctrl.Location = new Point(160, 180);
            ctrl.Name = ComboName;
            ctrl.BringToFront();
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.Text = "Select";

            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            // ctrl.Click += new EventHandler(control_Click);

            pnControls.Controls.Add(ctrl);
        }

        private void toolTextBox_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string textName = "txt_" + randNumber;

            TextBox ctrl = new();
            ctrl.Location = new Point(20, 190);
            ctrl.Name = textName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.Text = "Text box";

            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            pnControls.Controls.Add(ctrl);
        }

        private void toolRadioButton_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string radioName = "rdo_" + randNumber;

            RadioButton ctrl = new();
            ctrl.Location = new Point(200, 260);
            ctrl.Name = radioName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.Text = "Radio";
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            pnControls.Controls.Add(ctrl);
        }

        private void toolDatePicker_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string DatetimeName = "dte_" + randNumber;

            DateTimePicker ctrl = new();
            ctrl.Location = new Point(70, 130);
            ctrl.Name = DatetimeName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.Text = DateTime.Now.ToString();
            ctrl.BringToFront();
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);

            pnControls.Controls.Add(ctrl);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Form();
            form.Controls.Add(pnControls);
            form.Text = "Test form";
            form.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void VisualEditor_Load(object sender, EventArgs e)
        {
            ControlList.objDGVBind.Clear();
        }

        private void tlsSaveChanges_Click(object sender, EventArgs e)
        {
            SaveForm();
        }

        private void tlsViewCode_Click(object sender, EventArgs e)
        {
            var editor = new CodeEditor(form.ScriptFile);
            editor.Show();
        }

        private void ctxCopy_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                copiedControl = SelectedControl;
                if (copiedControl != null)
                {
                    cutCheck = false;
                    copyCheck = true;
                    ctxPaste.Enabled = true;
                }
            }
        }

        private void ctxCut_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                copiedControl = SelectedControl;
                cutCheck = true;
                ctxPaste.Enabled = true;
            }

            if (SelectedControl != null)
            {
                pnControls.Controls.Remove(SelectedControl);
                propertyGrid1.SelectedObject = null;
                pnControls.Invalidate();
            }
        }

        private void ctxBringToFront_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                SelectedControl.BringToFront();
            }
        }

        private void ctxSendToBack_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                SelectedControl.SendToBack();
                foreach(Control c in pnControls.Controls)
                {
                    if(c.Name != "MainPanel")
                    {
                        c.SendToBack();
                    }
                }
            }
        }

        private void ctxDeleteSelected_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null&&SelectedControl.Name!= "MainPanel")
            {
                pnControls.Controls.Remove(SelectedControl);
                propertyGrid1.SelectedObject = null;
                pnControls.Invalidate();
            }
        }

        private void ctxPaste_Click(object sender, EventArgs e)
        {
            if (copiedControl != null)
            {
                PasteNewControl();
                //control.Invalidate();
                if (copyCheck == true)
                {
                    ctxPaste.Enabled = true;
                }
                if (cutCheck == true)
                {
                    ctxPaste.Enabled = false;
                    cutCheck = false;
                }
            }
        }

        private void PasteNewControl()
        {
            try
            {

                Random rnd = new();
                int randNumber = rnd.Next(1, 1000);
                string newControlsName = copiedControl.Name + "_" + randNumber;

                switch (copiedControl.GetType().ToString())
                {
                    case "System.Windows.Forms.PictureBox":
                        try
                        {
                            PictureBox pic = copiedControl as PictureBox;
                            PictureBox ctrl = new();
                            ctrl.Name = newControlsName;
                            ctrl.SendToBack();
                            ctrl.BackColor = copiedControl.BackColor;
                            ctrl.ForeColor = copiedControl.ForeColor;
                            ctrl.Image = pic.Image;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        catch (Exception ex)
                        {
                        }

                        break;
                    case "System.Windows.Forms.DataGridView":
                        {
                            DataGridView ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.Label":
                        {
                            Label ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.Button":
                        {
                            System.Drawing.Color myBackColor = new();
                            myBackColor = System.Drawing.ColorTranslator.FromHtml(gParam[8]);
                            Button ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.ComboBox":
                        {
                            ComboBox ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.ListBox":
                        {
                            ListBox ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.Panel":
                        {
                            Panel ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            ctrl.SendToBack();
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.NumericUpDown":
                        {
                            NumericUpDown ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.TreeView":
                        {
                            TreeView ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.DateTimePicker":
                        {
                            DateTimePicker ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.TextBox":
                        {
                            TextBox ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.RadioButton":
                        {
                            RadioButton ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.CheckBox":
                        {
                            CheckBox ctrl = new();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        void SetControlDefaults(Control ctrl)
        {
            ctrl.Location = new Point(copiedControl.Location.X + 10, copiedControl.Location.Y + 10);
            ctrl.Text = copiedControl.Text;
            ctrl.Font = copiedControl.Font;
            ctrl.BackColor = copiedControl.BackColor;
            ctrl.ForeColor = copiedControl.ForeColor;
            ctrl.Size = copiedControl.Size;
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
        }

        private void ctxDeleteAll_Click(object sender, EventArgs e)
        {
            foreach(Control ctrl in pnControls.Controls)
            {
                if(ctrl.Name!= "MainPanel")
                {
                    pnControls.Controls.Remove(ctrl);
                }
            }
            pnControls.Controls.Clear();
            propertyGrid1.SelectedObject = null;
            pnControls.Invalidate();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                Point pos = pnControls.PointToClient(MousePosition);
                if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X) &&
                    (pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y))
                {
                    direction = Direction.NW;
                    Cursor = Cursors.SizeNWSE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE))
                {
                    direction = Direction.SE;
                    Cursor = Cursors.SizeNWSE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2) &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                    pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y)
                {
                    direction = Direction.N;
                    Cursor = Cursors.SizeNS;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2) &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE)
                {
                    direction = Direction.S;
                    Cursor = Cursors.SizeNS;
                }
                else if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2))
                {
                    direction = Direction.W;
                    Cursor = Cursors.SizeWE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2))
                {
                    direction = Direction.E;
                    Cursor = Cursors.SizeWE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE) &&
                    (pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y))
                {
                    direction = Direction.NE;
                    Cursor = Cursors.SizeNESW;
                }
                else if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X + DRAG_HANDLE_SIZE) &&
                    (pos.Y >= SelectedControl.Location.Y + SelectedControl.Height - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE))
                {
                    direction = Direction.SW;
                    Cursor = Cursors.SizeNESW;
                }
                else
                {
                    Cursor = Cursors.Default;
                    direction = Direction.None;
                }
            }
            else
            {
                direction = Direction.None;
                Cursor = Cursors.Default;
            }
        }

        private void tlsRun_Click(object sender, EventArgs e)
        {
            var generator = new FormGenerator();
            Form frm = generator.CreateFromPanel(pnControls, form.FormName);
            frm.Show();
        }

        private void toolImage_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string textName = "img_" + randNumber;

            PictureBox ctrl = new();
            ctrl.Location = new Point(20, 190);
            ctrl.Name = textName;
            ctrl.BackColor = SystemColors.InactiveBorder;
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            pnControls.Controls.Add(ctrl);
        }

        private void tlsCircle_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string btnName = "sccle_" + randNumber;

            ScCircle ctrl = new();
            ctrl.Location = new Point(50, 150);
            ctrl.Name = btnName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += new EventHandler(control_Click);
            pnControls.Controls.Add(ctrl);
            ctrl.BringToFront();
        }
        private void tlsRectangle_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string btnName = "rect_" + randNumber;

            ScRectangle ctrl = new();
            ctrl.Location = new Point(50, 150);
            ctrl.Name = btnName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.BringToFront();
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += new EventHandler(control_Click);
            pnControls.Controls.Add(ctrl);
        }

        private void tlsArc_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string btnName = "arc_" + randNumber;

            ScArc ctrl = new();
            ctrl.Location = new Point(50, 150);
            ctrl.Name = btnName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += new EventHandler(control_Click);
            pnControls.Controls.Add(ctrl);
            ctrl.BringToFront();
            pnControls.Invalidate();
        }

        private void tlsLine_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string btnName = "ln_" + randNumber;

            ScLine ctrl = new();
            ctrl.Location = new Point(50, 150);
            ctrl.Name = btnName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.BringToFront();
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += new EventHandler(control_Click);
            pnControls.Controls.Add(ctrl);
        }

        private void tlsPolygon_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string btnName = "polyg_" + randNumber;

            ScPolygon ctrl = new();
            ctrl.Location = new Point(50, 150);
            ctrl.Name = btnName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += new EventHandler(control_Click);
            pnControls.Controls.Add(ctrl);
            ctrl.BringToFront();
        }

        private void tlsConnector_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string btnName = "sconn_" + randNumber;

            ScLine ctrl = new();
            ctrl.Location = new Point(50, 150);
            ctrl.Name = btnName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += new EventHandler(control_Click);
            pnControls.Controls.Add(ctrl);
            ctrl.BringToFront();
        }

        private void tlsEllipseSegment_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string btnName = "arc_" + randNumber;

            ScArc ctrl = new();
            ctrl.Location = new Point(50, 150);
            ctrl.Name = btnName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += new EventHandler(control_Click);
            pnControls.Controls.Add(ctrl);
            ctrl.BringToFront();
            pnControls.Invalidate();
        }

        private void tlsPieSegment_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string btnName = "arc_" + randNumber;

            ScArc ctrl = new();
            ctrl.Location = new Point(50, 150);
            ctrl.Name = btnName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += new EventHandler(control_Click);
            pnControls.Controls.Add(ctrl);
            ctrl.BringToFront();
            pnControls.Invalidate();
        }

        private void toolListBox_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string chkName = "lst_" + randNumber;

            ListBox ctrl = new();
            ctrl.Location = new Point(120, 140);
            ctrl.Name = chkName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.BringToFront();
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);

            pnControls.Controls.Add(ctrl);
        }

        private void toolTab_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string chkName = "tab_" + randNumber;

            TabControl ctrl = new();
            var page = new TabPage()
            {
                Name = "Tab1",
                Text = "Tab1"
            };
            ctrl.TabPages.Add(page);
            ctrl.Location = new Point(120, 140);
            ctrl.Name = chkName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);

            pnControls.Controls.Add(ctrl);
            ctrl.BringToFront();
        }

        private void toolComboBox_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            int randNumber = rnd.Next(1, 1000);
            string chkName = "cmb_" + randNumber;

            ComboBox ctrl = new();
            ctrl.Location = new Point(120, 140);
            ctrl.Name = chkName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.BringToFront();
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);

            pnControls.Controls.Add(ctrl);
        }

        private void pnControls_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedControl != null && e.Button == MouseButtons.Left)
            {
                timer1.Stop();
                Invalidate();

                if (SelectedControl.Height < 20)
                {
                    SelectedControl.Height = 20;
                    direction = Direction.None;
                    Cursor = Cursors.Default;
                    return;
                }
                else if (SelectedControl.Width < 20)
                {
                    SelectedControl.Width = 20;
                    direction = Direction.None;
                    Cursor = Cursors.Default;
                    return;
                }

                //get the current mouse position relative the the app
                Point pos = pnControls.PointToClient(MousePosition);

                #region resize the control in 8 directions
                if (direction == Direction.NW)
                {
                    //north west, location and width, height change
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(SelectedControl.Size.Width - (newLocation.X - SelectedControl.Location.X),
                        SelectedControl.Size.Height - (newLocation.Y - SelectedControl.Location.Y));
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.SE)
                {
                    //south east, width and height change
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(SelectedControl.Size.Width + (newLocation.X - SelectedControl.Size.Width - SelectedControl.Location.X),
                        SelectedControl.Height + (newLocation.Y - SelectedControl.Height - SelectedControl.Location.Y));
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.N)
                {
                    //north, location and height change
                    newLocation = new Point(SelectedControl.Location.X, pos.Y);
                    newSize = new Size(SelectedControl.Width,
                        SelectedControl.Height - (pos.Y - SelectedControl.Location.Y));
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.S)
                {
                    //south, only the height changes
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(SelectedControl.Width,
                        pos.Y - SelectedControl.Location.Y);
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.W)
                {
                    //west, location and width will change
                    newLocation = new Point(pos.X, SelectedControl.Location.Y);
                    newSize = new Size(SelectedControl.Width - (pos.X - SelectedControl.Location.X),
                        SelectedControl.Height);
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.E)
                {
                    //east, only width changes
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(pos.X - SelectedControl.Location.X,
                        SelectedControl.Height);
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.SW)
                {
                    //south west, location, width and height change
                    newLocation = new Point(pos.X, SelectedControl.Location.Y);
                    newSize = new Size(SelectedControl.Width - (pos.X - SelectedControl.Location.X),
                        pos.Y - SelectedControl.Location.Y);
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.NE)
                {
                    //north east, location, width and height change
                    newLocation = new Point(SelectedControl.Location.X, pos.Y);
                    newSize = new Size(pos.X - SelectedControl.Location.X,
                        SelectedControl.Height - (pos.Y - SelectedControl.Location.Y));
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                #endregion
            }
        }
    }
}
