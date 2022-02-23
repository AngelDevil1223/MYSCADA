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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LogUser();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LogUser();
            }
        }
        void LogUser()
        {
            if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Invalid credentials", "Error");
                return;
            }
            if (txtUsername.Text.ToUpper() != "ADMIN" || txtPassword.Text != "Admin2022")
            {
                MessageBox.Show("Invalid credentials", "Error");
                return;
            }
            var form = new MainForm();
            form.Show();
            Hide();
        }
    }
}
