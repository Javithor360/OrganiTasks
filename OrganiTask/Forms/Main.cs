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
        private const int CARD_WIDTH = 300;
        private const int CARD_HEIGHT = 200;
        private const int CARD_MARGIN = 20;
        private const int CARDS_PER_ROW = 3;

        //private DashboardController dashboardController = new DashboardController();

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

            // Creación de FlowLayoutPanel para mostrar tarjetas en estructura de grid
            FlowLayoutPanel flowPanel = new FlowLayoutPanel
            {
                Width = panelContent.Width - 20, // Margin
                Height = panelContent.Height - 20,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Location = new Point(10, 10),
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            panelContent.Controls.Add(flowPanel);

            // Crear siempre primer tarjeta para nuevos tableros
            Panel createNewCard = CreateNewDashboardCard();
            flowPanel.Controls.Add(createNewCard);

            // Agregar el resto de tableros
            foreach (DashboardViewModel dashboard in model.DashboardPreviews)
            {
                Panel dashboardCard = CreateDashboardCard(dashboard);
                flowPanel.Controls.Add(dashboardCard);
            }

        }

        private Panel CreateNewDashboardCard()
        {
            Panel card = new Panel
            {
                Width = CARD_WIDTH,
                Height = CARD_HEIGHT,
                BorderStyle = BorderStyle.None, 
                Margin = new Padding(CARD_MARGIN),
                BackColor = System.Drawing.Color.White,
                Cursor = Cursors.Hand
            };

            // Estilo dotted
            card.Paint += (sender, e) => {
                using (Pen pen = new Pen(System.Drawing.Color.FromArgb(41, 128, 185)))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            Label lblPlus = new Label
            {
                Text = "+",
                Font = new Font("Segoe UI", 48, FontStyle.Regular),
                ForeColor = System.Drawing.Color.FromArgb(41, 128, 185),
                Size = new Size(80, 80),
                Location = new Point((card.Width - 80) / 2, (card.Height - 120) / 2),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblCreate = new Label
            {
                Text = "Crear nuevo tablero",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = System.Drawing.Color.FromArgb(41, 128, 185),
                AutoSize = false,
                Size = new Size(card.Width - 20, 30),
                Location = new Point(10, lblPlus.Bottom + 5),
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.Add(lblPlus);
            card.Controls.Add(lblCreate);

            card.Click += BtnCreateDashboard_Click;
            foreach (Control control in card.Controls)
            {
                control.Click += (s, e) => BtnCreateDashboard_Click(card, e);
            }

            return card;
        }

        // Método para crear una tarjeta visual para cada dashboard
        private Panel CreateDashboardCard(DashboardViewModel dashboard)
        {
            // Creamos un panel para la tarjeta
            Panel card = new Panel
            {
                Width = CARD_WIDTH,
                Height = CARD_HEIGHT,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(CARD_MARGIN),
                Padding = new Padding(10),
                BackColor = System.Drawing.Color.White,
                Tag = dashboard.Id  // Guardamos el ID del dashboard en el Tag
            };

            // Efecto Hover
            card.MouseEnter += (s, e) => card.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            card.MouseLeave += (s, e) => card.BackColor = System.Drawing.Color.White;

            // Etiqueta con el nombre del dashboard
            Label lblName = new Label
            {
                Text = dashboard.DashboardTitle,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            // Etiqueta con la descripción
            Label lblDescription = new Label
            {
                Text = dashboard.Description,
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                MaximumSize = new Size(card.Width - 100, 50),
                Location = new Point(10, 40)
            };

            // Botón para eliminar el dashboard
            Button btnDelete = new Button
            {
                Text = "Eliminar",
                Size = new Size(70, 30),
                Location = new Point(card.Width - 90, 90),
                BackColor = System.Drawing.Color.FromArgb(244, 47, 47),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            btnDelete.FlatAppearance.BorderSize = 0;

            // Botón para abrir el dashboard
            Button btnOpen = new Button
            {
                Text = "Abrir",
                Size = new Size(70, 30),
                Location = new Point(card.Width - 90, 125),
                BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            btnOpen.FlatAppearance.BorderSize = 0;

            // Botón para editar el dashboard
            Button btnEdit = new Button
            {
                Text = "🛠️",
                Size = new Size(70, 30),
                Location = new Point(card.Width - 90, 160),
                BackColor = System.Drawing.Color.FromArgb(244, 47, 113),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            btnEdit.FlatAppearance.BorderSize = 0;

            // Evento de click para la tarjeta completa
            card.Click += (s, e) => HandleDashboardClick(dashboard.Id);
            lblName.Click += (s, e) => HandleDashboardClick(dashboard.Id);
            lblDescription.Click += (s, e) => HandleDashboardClick(dashboard.Id);
            btnOpen.Click += (s, e) => HandleDashboardClick(dashboard.Id);
            btnEdit.Click += (s, e) => HandleEditDashboardClick(dashboard.Id);
            btnDelete.Click += (s, e) => HandleDeleteDashboardClick(dashboard.Id);

            //btnOpen.Click += (s, e) => MessageBox.Show($"ID del tablero: {dashboard.Id}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //btnOpen.Click += (s, e) => MessageBox.Show($"Descripción del tablero: {dashboard.Description}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Agregar controles al panel
            card.Controls.Add(lblName);
            card.Controls.Add(lblDescription);
            card.Controls.Add(btnOpen);
            card.Controls.Add(btnEdit);
            card.Controls.Add(btnDelete);

            return card;
        }

        private void HandleEditDashboardClick(int dashboardId)
        {
            DashboardSettings settingsDashboard = new DashboardSettings(dashboardId); // Mostrar configuración del tablero
            settingsDashboard.DashboardInfoChanged += (s, args) => LoadDashBoards();
            settingsDashboard.ShowDialog();
        }

        private void HandleDeleteDashboardClick(int dashboardId)
        {
            DialogResult confirmResult = MessageBox.Show(
                "¿Deseas eliminar este tablero y todos sus elementos? (Categorias, Etiquetas y Tareas)",
                "Eliminando",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult == DialogResult.Yes)
            {

            }
        }

        private void HandleDashboardClick(int dashboardId)
        {
            //KanbanDashboard kanban = new KanbanDashboard(dashboardId, "Status");
            KanbanDashboard kanban = new KanbanDashboard(dashboardId);
            kanban.Show();
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
        private void BtnCreateDashboard_Click(object sender, EventArgs e)
        {
            DashboardsManagement createDashboardForm = new DashboardsManagement(session.CurrentUser.Id);
            createDashboardForm.DashboardStored += (s, args) => LoadDashBoards(); // Suscribirse al evento
            createDashboardForm.ShowDialog(); // Mostrar el formulario como modal
        }

        private void buttonOLD_Click(object sender, EventArgs e)
        {
            KanbanDashboard kanban = new KanbanDashboard(1);
            kanban.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            session.Logout();
            ReloadLoginForm();
        }

        // Window Resize ===================================================================== |
        private void Main_Resize(object sender, EventArgs e)
        {
            // Reload dashboards to adjust layout when form is resized
            if (session.IsLoggedIn)
            {
                LoadDashBoards();
            }
        }
    }
}
