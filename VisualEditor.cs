using MySCADA.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class VisualEditor : Form
    {
        public VisualEditor()
        {
            InitializeComponent();
        }

        private void scCanvas1_SelectedShapeChanged(object sender, EventArgs e)
        {
            ScShape s = scCanvas1.SelectedShape;
            propertyGrid1.SelectedObject = s;
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            Add(new ScCircle(Point.Empty));
        }

        private void Add(ScShape shape)
        {
            scCanvas1.Shapes.Add(shape);
            scCanvas1.Invalidate();
        }
    }
}
