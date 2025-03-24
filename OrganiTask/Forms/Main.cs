using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OrganiTask.Entities;
using OrganiTask.Util;
using OrganiTask.Forms.Test;


/*
 * CONSIDERACIONES IMPORTANTES PARA TODO EL PROYECTO:
 * - Nombres de atributos, clases y métodos SIEMPRE en inglés
 * - Comentarios sobre el código e indicaciones en español
 */

/*
 * Nota sobre las entidades:
 * De momento las entidades usan la clase List y HashSet para las relaciones de uno a muchos y muchos a muchos.
 * Pero, se van a reemplazar posteriormente por las clases personalizadas de ListaEnlazada
 */

namespace OrganiTask.Forms
{
    public partial class Main: Form
    {

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //if(!SessionManager.Instance.IsLoggedIn)
            //{
            //    this.Hide();
            //    //MessageBox.Show("Sin Sessión");

            //    Login loginForm = new Login();
            //    loginForm.ShowDialog(); // Pausa la siguiente ejecución de código

            //    if(!SessionManager.Instance.IsLoggedIn)
            //    {
            //        Environment.Exit(1); // Ciere forzoso de la aplicación
            //    }
            //    else
            //        this.Show();
            //}

            // Cargar datos solo si hay sesión
            Refresh();
            label1.Text = SessionManager.Instance.CurrentUser.Username;
        }

        private new void Refresh()
        {
            using (var context = new OrganiTaskDB())
            {
                // de manera temporal, obtenemos todos los usuarios
                var users = context.Users.Include(u => u.Dashboard).Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Email,
                    NumeroTableros = u.Dashboard.Count()
                }).ToList();

                // y los cargamos en el DataGridView con la información recopilada
                dgvData.DataSource = users;
            }
        }

        // Eventos =================================================================== | 

        private void btnMainTest_Click(object sender, EventArgs e)
        {
            KanbanDashboard kanban = new KanbanDashboard(1, "Status");
            kanban.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CategoriesManagement categoriesManagement = new CategoriesManagement();
            categoriesManagement.Show();
        }
    }
}
