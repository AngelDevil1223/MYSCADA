using MySCADA.Controls;
using MySCADA.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public class FormGenerator
    {
        public Form CreateFromPanel(Panel p,string formName)
        {
            Form frm = new Form();
            frm.Name = formName;
            frm.Text = formName;
            foreach (var c in p.Controls)
            {
                dynamic cd = c;
                if (cd.Name == "MainPanel")
                {
                    var panel = ((ScCanvas)c).Clone();
                    frm.Size = panel.Size;
                    frm.BackColor = panel.BackColor;
                    break;
                }
            }
            foreach (var ctrl in p.Controls)
            {
                var b = new Button();
                dynamic sc = ctrl;
                if (sc.Name != "MainPanel")
                {
                    var tp = ctrl.GetType();
                    switch (tp.Name)
                    {
                        case nameof(ScArc):
                            frm.Controls.Add(((ScArc)ctrl).Clone());
                            break;
                        case nameof(ScCircle):
                            frm.Controls.Add(((ScCircle)ctrl).Clone());
                            break;
                        case nameof(ScLine):
                            frm.Controls.Add(((ScLine)ctrl).Clone());
                            break;
                        case nameof(ScPolygon):
                            frm.Controls.Add(((ScPolygon)ctrl).Clone());
                            break;
                        case nameof(ScRectangle):
                            frm.Controls.Add(((ScRectangle)ctrl).Clone());
                            break;
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
                }
            }
            return frm;
        }
        public Form FromFile(UserForm f)
        {
            var panel = CreateComponents(f);
            var form = CreateFromPanel(panel, f.FormName);
            return form;
        }

        Panel CreateComponents(UserForm form)
        {
            var pnControls = new Panel();
            var assembly = Assembly.GetAssembly(typeof(Button));

            var elements = ScadaProject.ActiveProject.ReadForm(form.DesignerFile);
            var types = assembly.GetTypes();
            foreach (var e in elements.FormElements)
            {
                var tp = types.FirstOrDefault(x => x.Name == e.Type);
                if (tp != null)
                {
                    var props = tp.GetProperties();
                    var events = tp.GetEvents();
                    dynamic ctrl = Activator.CreateInstance(tp);
                    ctrl.BringToFront();
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
            return pnControls;
        }

    }
}
