using OrganiTask.Controllers;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Entities;
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
using Label = System.Windows.Forms.Label;

namespace OrganiTask.Forms.Test
{
    public partial class Main : Form
    {
        SessionManager session = SessionManager.Instance;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!session.IsLoggedIn)
                ReloadLoginForm();
        }

        private void LoadUserInfo()
        {
            lblUsername.Text = session.CurrentUser.Username;
            lblUserEmail.Text = session.CurrentUser.Email;
        }

        private void LoadDashBoards()
        {
            MainController controller = new MainController();  // Instanciar el controlador
            MainViewModel model = controller.LoadUserDashboards(session.CurrentUser.Id); // Cargar los tableros
            panelContent.Controls.Clear();

            if (model.DashboardPreviews.Count == 0)
            {
                Panel cardNoData = CreateCardNoData();
                panelContent.Controls.Add(cardNoData);
                return;
            }

            Panel panelContainer = new Panel();
            panelContent.Controls.Add(panelContainer);

            foreach (DashboardViewModel dashboard in model.DashboardPreviews)
            {
                Panel dashboardCard = CreateDashboardCard(dashboard);
                panelContainer.Controls.Add(dashboardCard);
            }
        }

        private Panel CreateCardNoData()
        {
            // Crear panelNoData dinámicamente
            Panel panelNoData = new Panel
            {
                Name = "panelNoData",
                Size = new Size(300, 150),
                Location = new Point(700, 350),
                Anchor = AnchorStyles.None,
                Visible = true,
            };

            Label lblNoData = new Label
            {
                Name = "lblNoData",
                Text = "No tienes tableros disponibles",
                AutoSize = false,
                Size = new Size(280, 30),
                Location = new Point(10, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular)
            };

            Button btnCreateDashboard = new Button
            {
                Name = "btnCreateDashboard",
                Text = "CREAR TABLERO",
                Size = new Size(120, 35),
                Location = new Point(90, 80),
                BackColor = Color.FromArgb(41, 128, 185),
                TextAlign= ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold, GraphicsUnit.Point) // Se aplicó la misma fuente
            };
            btnCreateDashboard.FlatAppearance.BorderSize = 1; // Mantener el borde del botón

            // Agregar controles al panel
            panelNoData.Controls.Add(lblNoData);
            panelNoData.Controls.Add(btnCreateDashboard);

            return panelNoData;
        }

        // Método para crear una tarjeta visual para cada dashboard
        private Panel CreateDashboardCard(DashboardViewModel dashboard)
        {
            // Creamos un panel para la tarjeta
            Panel card = new Panel
            {
                Width = 300,
                Height = 200,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Padding = new Padding(10),
                BackColor = Color.White
            };

            // Etiqueta con el nombre del dashboard
            Label lblName = new Label
            {
                Text = dashboard.DashboardTitle,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            // Etiqueta con la descripción
            //Label lblDescription = new Label
            //{
            //    Text = dashboard.Description,
            //    Font = new Font("Segoe UI", 10),
            //    AutoSize = true,
            //    MaximumSize = new Size(280, 50),
            //    Location = new Point(10, 40)
            //};

            //// Etiqueta con el conteo de tareas
            //Label lblTaskCount = new Label
            //{
            //    Text = $"Tareas: {dashboard.TaskCount}",
            //    Font = new Font("Segoe UI", 9),
            //    AutoSize = true,
            //    Location = new Point(10, 100)
            //};

            //// Etiqueta con la fecha de última modificación
            //Label lblLastModified = new Label
            //{
            //    Text = $"Última modificación: {dashboard.LastModified.ToString("dd/MM/yyyy HH:mm")}",
            //    Font = new Font("Segoe UI", 9),
            //    AutoSize = true,
            //    Location = new Point(10, 120)
            //};

            // FlowLayoutPanel para mostrar las categorías disponibles
            FlowLayoutPanel categoriesPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Width = 180,
                Height = 15,
                Location = new Point(10, 150),
                WrapContents = true
            };

            // Agregamos labels para cada categoría
            //foreach (string category in dashboard.Categories)
            //{
            //    Label lblCategory = new Label
            //    {
            //        Text = category,
            //        Font = new Font("Segoe UI", 8),
            //        AutoSize = true,
            //        BackColor = Color.LightGray,
            //        Padding = new Padding(5, 3, 5, 3),
            //        Margin = new Padding(2)
            //    };
            //    categoriesPanel.Controls.Add(lblCategory);
            //}

            // Botón para abrir el dashboard
            //Button btnOpen = new Button
            //{
            //    Text = "Abrir",
            //    Size = new Size(70, 30),
            //    Location = new Point(220, 10),
            //    BackColor = Color.FromArgb(0, 122, 204),
            //    ForeColor = Color.White,
            //    FlatStyle = FlatStyle.Flat,
            //    Tag = dashboard.Id  // Guardamos el ID del dashboard en el Tag
            //};
            //btnOpen.Click += BtnOpenDashboard_Click;  // Asignamos el evento Click

            // Agregamos todos los controles a la tarjeta
            card.Controls.Add(lblName);
            //card.Controls.Add(lblDescription);
            //card.Controls.Add(lblTaskCount);
            //card.Controls.Add(lblLastModified);
            //card.Controls.Add(categoriesPanel);
            //card.Controls.Add(btnOpen);

            return card;
        }

        // Navigation ============================================================================ |

        private void ReloadLoginForm()
        {
            this.Hide();

            Login loginForm = new Login(this);
            loginForm.ShowDialog(); // Pausa la siguiente ejecución de código

            if (!session.IsLoggedIn)
                Environment.Exit(1); // Ciere forzoso de la aplicación
            else
            {
                this.Show();
                LoadUserInfo();
                LoadDashBoards();
            }
        }

        // Events ============================================================================ |

        private void buttonOLD_Click(object sender, EventArgs e)
        {
            KanbanDashboard kanban = new KanbanDashboard(1, "Status");
            kanban.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            session.Logout();
            ReloadLoginForm();
        }
    }
}
