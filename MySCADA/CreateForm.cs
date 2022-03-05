using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MySCADA
{
    public partial class CreateForm : Form
    {
        public CreateForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveForm();
        }

        void SaveForm()
        {
            if (string.IsNullOrEmpty(txtFormName.Text))
            {
                MessageBox.Show("Type file name", "File name");
                return;
            }
            if (Regex.IsMatch(txtFormName.Text, "[^0-9a-zA-Z]+"))
            {
                MessageBox.Show("Invalid characters", "File name");
                return;
            }
            if (char.IsDigit(txtFormName.Text[0]))
            {
                MessageBox.Show("File name must begin with letter", "File name");
                return;
            }
            var existing = ScadaProject.ActiveProject.UserForms.Any(x => x.FormName == txtFormName.Text);
            if (existing)
            {
                MessageBox.Show("A form with that name already exists", "File name");
                return;
            }
            var frm = new UserForm()
            {
                FormName = txtFormName.Text,
                DesignerFile = $"{txtFormName.Text}.frx",
                ScriptFile = $"{txtFormName.Text}.cs"
            };
            CreatePhysicalFile(frm);
            ScadaProject.ActiveProject.UserForms.Add(frm);
            ScadaProject.ActiveProject.SaveChanges();
            var editor = new VisualEditor(frm);
            editor.Show();
            Close();
        }

        void CreatePhysicalFile(UserForm frm)
        {
            string loc = $"{ScadaProject.ActiveProject.Location}\\UserForms";

            FileInfo info = new FileInfo($"{loc}\\{frm.DesignerFile}");
            info.Directory.Create();

            if (cmbReuse.SelectedItem != null && (string)cmbReuse.SelectedItem != "(none)")
            {
                var fileContents = ScadaProject.ActiveProject.ReadRawForm($"{(string)cmbReuse.SelectedItem}.frx");
                File.WriteAllText($"{loc}\\{frm.DesignerFile}", fileContents);
            }
            else
            {
                File.WriteAllText($"{loc}\\{frm.DesignerFile}", "");
            }
            File.WriteAllText($"{loc}\\{frm.ScriptFile}", CreateCodeFile(frm.FormName));
        }

        string CreateCodeFile(string className)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;\nusing System.Linq;\nusing System.Text;\nusing System.Collections.Generic;\n");
            builder.AppendLine($"namespace {ScadaProject.ActiveProject.Name}");
            builder.AppendLine("{");
            builder.AppendLine($"\tpublic class {className}" + "\n\t{\n");
            builder.AppendLine("\t}\n}\n");
            return builder.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtFormName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveForm();
            }
        }

        private void CreateForm_Load(object sender, EventArgs e)
        {
            ScadaProject.ActiveProject.UserForms.ForEach(f =>
            {
                cmbReuse.Items.Add(f.FormName);
            });
        }
    }
}
