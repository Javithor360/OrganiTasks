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
    public partial class Register : Form
    {
        public Register()
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
                mainForm.ShowDialog();
                
                this.Visible = true;
            } else
            {
                MessageBox.Show("Credenciales incorrectas","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkSignInAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login loginForm = new Login();
            this.Hide();
            loginForm.Show();
        }
    }
}
