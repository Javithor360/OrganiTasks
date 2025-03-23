using OrganiTask.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiTask.Forms.Test
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!SessionManager.Instance.IsLoggedIn)
            {
                this.Hide();
                //MessageBox.Show("Sin Sessión");

                Login loginForm = new Login(this);
                loginForm.ShowDialog(); // Pausa la siguiente ejecución de código

                if (!SessionManager.Instance.IsLoggedIn)
                    Environment.Exit(1); // Ciere forzoso de la aplicación
                else
                    this.Show();
            }

            // Cargar datos solo si hay sesión
            Refresh();
            lblUsername.Text = SessionManager.Instance.CurrentUser.Username;
        }

        private void buttonOLD_Click(object sender, EventArgs e)
        {
            KanbanDashboard kanban = new KanbanDashboard(1, "Status");
            kanban.Show();
        }
    }
}
