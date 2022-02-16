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
                //var fl = File.ReadAllText(openFileDialog.FileName);
                Stream stream = null;
                if ((stream = openFileDialog.OpenFile()) != null)
                {
                    var folder = openFileDialog.FileName.Replace($"\\{openFileDialog.SafeFileName}", "");
                    var proj = ScadaProject.FromXml(stream);
                    proj.Location = folder;
                    ScadaProject.ActiveProject = proj;
                    Text = $"{proj.Name} [{proj.Location}]";
                    treeView1.Nodes.Add(proj.ToNode());
                }
            }
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
            var frm = new VisualEditor();
            frm.Show();
        }
    }
}
