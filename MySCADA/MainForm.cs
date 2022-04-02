using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void NewProject(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "SCADA projects (*.scdproj)|*.scdproj";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "New SCADA Project";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    myStream.Close();
                    ModifyProjectFile(saveFileDialog1.FileName);
                }
            }
        }

        void ModifyProjectFile(string fileName)
        {
            string title = fileName.Split("\\").LastOrDefault();
            var proj = new ScadaProject();
            proj.Name = title.Replace(".scdproj", "");
            proj.Location = fileName.Replace($"\\{title}", "");

            var projectContent = ScadaProject.ToFileFormat(proj);
            File.WriteAllText(fileName, projectContent);
            ScadaProject.ActiveProject = proj;
            proj.FormAdded += Proj_FormAdded;
            Text = $"{proj.Name} [{proj.Location}]";
            treeView1.Nodes.Add(proj.ToNode());
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "SCADA projects (*.scdproj)|*.scdproj|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var folder = openFileDialog.FileName.Replace($"\\{openFileDialog.SafeFileName}", "");
                var proj = ScadaProject.FromXml(openFileDialog.FileName);
                proj.FormAdded += Proj_FormAdded;
                proj.Location = folder;
                ScadaProject.ActiveProject = proj;
                Text = $"{proj.Name} [{proj.Location}]";
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(proj.ToNode());
                treeView1.ExpandAll();
                proj.UserForms.ForEach(x =>
                {
                    tlsDefaultForm.DropDownItems.Add(x.FormName, null);
                });
                tlsDefaultForm.Text = $"Default: {proj.DefaultForm}";
                openFileDialog.Dispose();
            }
        }

        private void Proj_FormAdded()
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(ScadaProject.ActiveProject.ToNode());
            treeView1.ExpandAll();

            ScadaProject.ActiveProject.UserForms.ForEach(x =>
            {
                var current = tlsDefaultForm.DropDownItems.Cast<ToolStripItem>();
                if(!current.Any(c=>c.Text==x.FormName))
                    tlsDefaultForm.DropDownItems.Add(x.FormName, null);
            });
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void tlsNewFrm_Click(object sender, EventArgs e)
        {
            if (ScadaProject.ActiveProject == null)
            {
                MessageBox.Show("You have not opened any project. Open or create a new project.");
                return;
            }

            var frm = new CreateForm();
            frm.Show();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Parent != null && e.Node.Parent.Text == "Custom Forms")
            {
                ctxDelete.Visible = true;
                ctxOpen.Visible = true;
            }
            else
            {
                ctxDelete.Visible = true;
                ctxOpen.Visible = true;
            }
        }

        private void ctxOpen_Click(object sender, EventArgs e)
        {
            var form = ScadaProject.ActiveProject.UserForms
                .FirstOrDefault(x=>x.FormName==treeView1.SelectedNode.Text);
            if (form != null)
            {
                var frm = new UserForm()
                {
                    DesignerFile = form.DesignerFile,
                    FormName = form.FormName,
                    ScriptFile = form.ScriptFile
                };
                var editor = new VisualEditor(frm);
                editor.Show();
            }
        }

        private void ctxDelete_Click(object sender, EventArgs e)
        {

        }

        private void tlsDefaultForm_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var item = e.ClickedItem;
            if (item != null)
            {
                ScadaProject.ActiveProject.DefaultForm = item.Text;
                tlsDefaultForm.Text = $"Default: {item.Text}";
                ScadaProject.ActiveProject.SaveChanges();
            }
        }

        private void tlsRun_Click(object sender, EventArgs e)
        {
            if (ScadaProject.ActiveProject == null)
            {
                MessageBox.Show("Open or create a new project", "Error");
                return;
            }
            if (ScadaProject.ActiveProject.DefaultForm == null)
            {
                MessageBox.Show("Default form not set", "Error");
                return;
            }
            var gen = new FormGenerator();
            var fm = ScadaProject.ActiveProject.GetDefaultForm();
            var form = gen.FromFile(fm);
            form.Show();
        }

        private void tlsTag_Click(object sender, EventArgs e)
        {
            var tag = new Tag();
            tag.Show();
        }
    }
}
