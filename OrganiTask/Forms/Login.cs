using OrganiTask.Entities;
using OrganiTask.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            User user = AuthService.Login(username, password);

            if (user != null)
            {
                MessageBox.Show("Login exitoso", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                Main mainForm = new Main();
                mainForm.Show();
                
                this.Hide();
            } else
            {
                MessageBox.Show("Credenciales incorrectas","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
