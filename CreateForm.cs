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

            var frm = new UserForm()
            {
                FormName = txtFormName.Text,
                DesignerFile = $"{txtFormName.Text}.frx",
                ScriptFile = $"{txtFormName.Text}.cs"
            };
            CreatePhysicalFile(frm);
            ScadaProject.ActiveProject.UserForms.Add(frm);
            var text = ScadaProject.ToFileFormat(ScadaProject.ActiveProject);
            File.WriteAllText($"{ScadaProject.ActiveProject.Location}\\{ScadaProject.ActiveProject.Name}.scdproj", text);
            ScadaProject.ActiveProject.RaiseEvent();
            var editor = new VisualEditor(frm);
            editor.Show();
            Close();
        }

        void CreatePhysicalFile(UserForm frm)
        {
            string loc = $"{ScadaProject.ActiveProject.Location}\\UserForms";

            FileInfo info = new FileInfo($"{loc}\\{frm.DesignerFile}");
            info.Directory.Create();

            File.WriteAllText($"{loc}\\{frm.DesignerFile}", "");
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
    }
}
