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
    public partial class Tag : Form
    {
        public Tag()
        {
            InitializeComponent();
        }

        private void btnCommParams_Click(object sender, EventArgs e)
        {
            var par = new SimulatorCommunicationParameters();
            par.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var par = new SimulatorCommunicationParameters();
            par.Show();
        }
    }
}
