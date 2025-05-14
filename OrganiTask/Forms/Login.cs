using OrganiTask.Entities;
using OrganiTask.Controllers.Services;
using System;
using System.Windows.Forms;
using OrganiTask.Util;

namespace OrganiTask.Forms
{
    public partial class Login : Form
    {
        private Test.Main mainTestForm; // Manejar referencia de mainForm para no crear nuevas instancias

        public Login(Test.Main mainTestForm)
        {
            InitializeComponent();
            this.mainTestForm = mainTestForm;
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
                MessageBox.Show($"¡Bienvenido, {loggedInUser.Username}!", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
