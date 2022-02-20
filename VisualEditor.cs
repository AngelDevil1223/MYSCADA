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

namespace MySCADA
{
    public partial class VisualEditor : Form
    {
        const int DRAG_HANDLE_SIZE = 7;
        int mouseX, mouseY;
        Control SelectedControl;
        Control copiedControl;
        Direction direction;
        Point newLocation;
        Size newSize;
        string[] gParam = null;
        Bitmap MemoryImage;
        String xmlFileName = "";
        String xmlFileName_Query = "";
        bool cutCheck = false;
        bool copyCheck = false;
        private ToolTip tt;

        UserForm form;

        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr AddFontMemResourceEx(byte[] pbFont, int cbFont, IntPtr pdv, out uint pcFonts);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern bool RemoveFontMemResourceEx(IntPtr fh);

        static private IntPtr m_fh = IntPtr.Zero;
        static private PrivateFontCollection m_pfc = null;
        string[] gParamPop = null;
        List<String> ControlNames = new List<String>();
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

            Font loadedFont = new Font(parts[0], float.Parse(parts[1]), (FontStyle)int.Parse(parts[2]));
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
                pnControls.Invalidate();  //unselect other control
                SelectedControl = (Control)sender;
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
                Point nextPosition = new Point();
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

            Rectangle Border = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE / 2),
                new Size(control.Size.Width + DRAG_HANDLE_SIZE,
                    control.Size.Height + DRAG_HANDLE_SIZE));
            Rectangle NW = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle N = new Rectangle(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle NE = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle W = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle E = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle SW = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle S = new Rectangle(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle SE = new Rectangle(
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
        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            Rectangle rect = new Rectangle(0, 0, pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
            pnl.Invalidate();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                Point pos = pnControls.PointToClient(MousePosition);
                //check if the mouse cursor is within the drag handle
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
            LoadComponents();
        }
        void LoadComponents()
        {
            var assembly = Assembly.GetAssembly(typeof(Button));

            var elements = ScadaProject.ActiveProject.ReadForm(form.DesignerFile);
            var types = assembly.GetTypes();
            if (elements.FormElements.Count == 0)
            {
                var panel = new Panel()
                {
                    Size = new Size(Width = 249, Height = 67)
                };
                panel.Name = "MainPanel";
                panel.BringToFront();
                panel.MouseEnter += new EventHandler(control_MouseEnter);
                panel.MouseLeave += new EventHandler(control_MouseLeave);
                panel.MouseDown += new MouseEventHandler(control_MouseDown);
                panel.MouseMove += new MouseEventHandler(control_MouseMove);
                panel.MouseUp += new MouseEventHandler(control_MouseUp);
                panel.Click += new EventHandler(control_Click);
                panel.BackColor = SystemColors.InactiveCaption;
                pnControls.Controls.Add(panel);
            }
            foreach (var e in elements.FormElements)
            {
                var tp = types.FirstOrDefault(x => x.Name == e.Type);
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
                    pnControls.Controls.Add(ctrl);
                }
            }
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

            FileInfo info = new FileInfo($"{loc}\\{form.DesignerFile}");
            info.Directory.Create();

            File.WriteAllText($"{loc}\\{form.DesignerFile}", str);
            MessageBox.Show("Changes committed successfully", "Success");
        }

        private void pnControls_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedControl != null)
                DrawControlBorder(SelectedControl);
            timer1.Start();
        }

        private void toolButton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int randNumber = rnd.Next(1, 1000);
            String btnName = "btn_" + randNumber;

            Button ctrl = new Button();
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
            Random rnd = new Random();
            int randNumber = rnd.Next(1, 1000);
            String LableName = "Lbl_" + randNumber;
            Label ctrl = new Label();
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
            Random rnd = new Random();
            int randNumber = rnd.Next(1, 1000);
            String chkName = "chk_" + randNumber;

            CheckBox ctrl = new CheckBox();
            ctrl.Location = new Point(120, 140);
            ctrl.Name = chkName;
            ctrl.Font = new System.Drawing.Font("NativePrinterFontA", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ctrl.Text = "YourCheckBox";
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
            Random rnd = new Random();
            int randNumber = rnd.Next(1, 1000);
            String ComboName = "cbo_" + randNumber;

            ComboBox ctrl = new ComboBox();
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
            Random rnd = new Random();
            int randNumber = rnd.Next(1, 1000);
            String textName = "txt_" + randNumber;

            TextBox ctrl = new TextBox();
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
            Random rnd = new Random();
            int randNumber = rnd.Next(1, 1000);
            String radioName = "rdo_" + randNumber;

            RadioButton ctrl = new RadioButton();
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
            Random rnd = new Random();
            int randNumber = rnd.Next(1, 1000);
            String DatetimeName = "dte_" + randNumber;

            DateTimePicker ctrl = new DateTimePicker();
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

                Random rnd = new Random();
                int randNumber = rnd.Next(1, 1000);
                String newControlsName = copiedControl.Name + "_" + randNumber;

                switch (copiedControl.GetType().ToString())
                {
                    case "System.Windows.Forms.PictureBox":
                        try
                        {
                            PictureBox pic = copiedControl as PictureBox;
                            PictureBox ctrl = new PictureBox();
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
                            DataGridView ctrl = new DataGridView();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.Label":
                        {
                            Label ctrl = new Label();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.Button":
                        {
                            System.Drawing.Color myBackColor = new System.Drawing.Color();
                            myBackColor = System.Drawing.ColorTranslator.FromHtml(gParam[8]);
                            Button ctrl = new Button();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.ComboBox":
                        {
                            ComboBox ctrl = new ComboBox();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.ListBox":
                        {
                            ListBox ctrl = new ListBox();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.Panel":
                        {
                            Panel ctrl = new Panel();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            ctrl.SendToBack();
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.NumericUpDown":
                        {
                            NumericUpDown ctrl = new NumericUpDown();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.TreeView":
                        {
                            TreeView ctrl = new TreeView();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.DateTimePicker":
                        {
                            DateTimePicker ctrl = new DateTimePicker();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.TextBox":
                        {
                            TextBox ctrl = new TextBox();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.RadioButton":
                        {
                            RadioButton ctrl = new RadioButton();
                            ctrl.Name = newControlsName;
                            SetControlDefaults(ctrl);
                            pnControls.Controls.Add(ctrl);
                        }
                        break;
                    case "System.Windows.Forms.CheckBox":
                        {
                            CheckBox ctrl = new CheckBox();
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
            Form frm = new Form();
            frm.Text = form.FormName;
            foreach (var c in pnControls.Controls)
            {
                dynamic cd = c;
                if (cd.Name == "MainPanel")
                {
                    var panel = ((Panel)c).Clone();
                    frm.Size = panel.Size;
                    frm.BackColor = panel.BackColor;
                    break;
                }
            }
            foreach (var ctrl in pnControls.Controls)
            {
                var b = new Button();
                //var ctrl = ComponentCopy.Clone(c);
                dynamic sc = ctrl;
                if(sc.Name!= "MainPanel")
                {
                    var tp = ctrl.GetType();
                    switch (tp.Name)
                    {
                        case nameof(Button):
                            frm.Controls.Add(((Button)ctrl).Clone());
                            break;
                        case nameof(ComboBox):
                            frm.Controls.Add(((ComboBox)ctrl).Clone());
                            break;
                        case nameof(Label):
                            frm.Controls.Add(((Label)ctrl).Clone());
                            break;
                        case nameof(TextBox):
                            frm.Controls.Add(((TextBox)ctrl).Clone());
                            break;
                        case nameof(RadioButton):
                            frm.Controls.Add(((RadioButton)ctrl).Clone());
                            break;
                        case nameof(DateTimePicker):
                            frm.Controls.Add(((DateTimePicker)ctrl).Clone());
                            break;
                        case nameof(CheckBox):
                            frm.Controls.Add(((CheckBox)ctrl).Clone());
                            break;
                        case nameof(PictureBox):
                            frm.Controls.Add(((PictureBox)ctrl).Clone());
                            break;
                    }
                    //if (panel != null)
                    //{
                    //    switch (tp.Name)
                    //    {
                    //        case nameof(Button):
                    //            panel.Controls.Add((Button)ctrl);
                    //            break;
                    //        case nameof(ComboBox):
                    //            panel.Controls.Add((ComboBox)ctrl);
                    //            break;
                    //        case nameof(Label):
                    //            panel.Controls.Add((Label)ctrl);
                    //            break;
                    //        case nameof(TextBox):
                    //            panel.Controls.Add((TextBox)ctrl);
                    //            break;
                    //        case nameof(RadioButton):
                    //            panel.Controls.Add((RadioButton)ctrl);
                    //            break;
                    //        case nameof(DateTimePicker):
                    //            panel.Controls.Add((DateTimePicker)ctrl);
                    //            break;
                    //        case nameof(CheckBox):
                    //            panel.Controls.Add((CheckBox)ctrl);
                    //            break;
                    //        case nameof(PictureBox):
                    //            panel.Controls.Add((PictureBox)ctrl);
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                    //    switch (tp.Name)
                    //    {
                    //        case nameof(Button):
                    //            frm.Controls.Add((Button)ctrl);
                    //            break;
                    //        case nameof(ComboBox):
                    //            frm.Controls.Add((ComboBox)ctrl);
                    //            break;
                    //        case nameof(Label):
                    //            frm.Controls.Add((Label)ctrl);
                    //            break;
                    //        case nameof(TextBox):
                    //            frm.Controls.Add((TextBox)ctrl);
                    //            break;
                    //        case nameof(RadioButton):
                    //            frm.Controls.Add((RadioButton)ctrl);
                    //            break;
                    //        case nameof(DateTimePicker):
                    //            frm.Controls.Add((DateTimePicker)ctrl);
                    //            break;
                    //        case nameof(CheckBox):
                    //            frm.Controls.Add((CheckBox)ctrl);
                    //            break;
                    //        case nameof(PictureBox):
                    //            frm.Controls.Add((PictureBox)ctrl);
                    //            break;
                    //    }
                    //}
                }
            }
            frm.Show();
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
