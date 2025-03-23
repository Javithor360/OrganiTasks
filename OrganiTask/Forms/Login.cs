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
        private Main mainForm; // Manejar referencia de mainForm para no crear nuevas instancias

        public Login(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if(username == "" || password == "")
            {
                MessageBox.Show("Ingresa tus credenciales","Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mostrar algún indicador de carga
            buttonLogin.Enabled = false;
            bool loginSuccessfull = AuthService.Login(username, password);

            // Mostrar algún indicador de carga
            buttonLogin.Enabled = true;

            if (loginSuccessfull)
            {
                User loggedInUser = SessionManager.Instance.CurrentUser;
                MessageBox.Show($"Login exitoso. Bienvenido, {loggedInUser.Username}", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Cierra el Login y Main se vuelve a mostrar
            } else
            {
                MessageBox.Show("Credenciales incorrectas","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register registerForm = new Register(this);
            this.Hide();
            registerForm.ShowDialog();
            this.Show();
        }
    }
}
