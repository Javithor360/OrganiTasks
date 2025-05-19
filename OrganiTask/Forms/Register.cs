using OrganiTask.Controllers.Services;
using OrganiTask.Entities;
using System;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    public partial class Register : Form
    {
        private Login loginForm; // Manejar referencia de loginForm para no crear nuevas instancias

        public Register(Login loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();
            string email = textEmail.Text.Trim();

            User user = AuthService.Register(username, password, email);

            if (user != null)
            {
                MessageBox.Show("Registro exitoso", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                closeForm();
            } else
                MessageBox.Show("Usuario o Email ya registrado 🚨", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void linkSignInAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            closeForm();
        }

        private void closeForm()
        {
            this.Close();
            loginForm.Show();
        }
    }
}
