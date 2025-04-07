using OrganiTask.Controllers;
using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    /// <summary>
    /// Formulario para visualizar un tablero Kanban.
    /// </summary>
    public partial class KanbanDashboard: Form
    {
        // Propiedades para almacenar el identificador del tablero y el título de la categoría
        private int dashboardId;
        private string categoryTitle; // default "Status"

        // Constructor del formulario requiere identificar el tablero y la categoría con la que se ordenarán las tareas
        public KanbanDashboard(int dashboardId, string categoryTitle)
        {
            InitializeComponent();
            this.dashboardId = dashboardId;
            this.categoryTitle = categoryTitle;
        }

        // Evento de carga del formulario
        private void KanbanDashboard_Load(object sender, EventArgs e)
        {
            DashboardController controller = new DashboardController(); // Instanciar el controlador
            DashboardViewModel model = controller.LoadKanban(dashboardId, categoryTitle); // Cargar el tablero

            if (model == null) // Mostrar error si no se encuentra el tablero
            {
                MessageBox.Show("No se encontró el tablero especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblDashboardTitle.Text = model.DashboardTitle; // Asignar el título del tablero a la etiqueta
            RenderDashboard(model); // Dibujar el tablero
        }

        // Método para dibujar el tablero
        private void RenderDashboard(DashboardViewModel model)
        {
            // Limpiar columnas
            flpBoard.Controls.Clear();

            // Dibujamos dinámicamente una columna por cada elemento en la lista de columnas
            foreach (ColumnViewModel column in model.Columns)
            {
                FlowLayoutPanel columnPanel = CreateColumnPanel(column.TagName);

                // Dibujamos dinámicamente una tarjeta por cada tarea en la lista de tareas
                foreach (TaskViewModel task in column.Tasks)
                {
                    Panel taskCard = CreateTaskCard(task); // Creamos la tarjeta
                    columnPanel.Controls.Add(taskCard); // Agregamos la tarjeta a la columna
                }

                // Agregamos la columna al panel principal
                flpBoard.Controls.Add(columnPanel);
            }
        }

        // Método para crear una columna por cada categoría en el tablero
        private FlowLayoutPanel CreateColumnPanel(string name)
        {
            // Creamos un panel de flujo para la columna
            FlowLayoutPanel column = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Width = 250,
                Height = flpBoard.Height - 30,
                AutoScroll = true,
                Margin = new Padding(10)
            };

            // Etiqueta con el nombre de la categoría
            Label lblTag = new Label()
            {
                Text = name,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            column.Controls.Add(lblTag); // Agregamos la etiqueta al panel

            return column; // Retornamos la columna
        }

        // Método para crear una tarjeta por cada tarea en la columna
        private Panel CreateTaskCard(TaskViewModel task)
        {
            // Creamos un panel para la tarjeta
            Panel card = new Panel
            {
                Width = 200,
                Height = 80,
                Cursor = Cursors.Hand,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
            };

            // Título de la tarea
            Label lblTitle = new Label
            {
                Text = task.Title,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(5, 5)
            };
            card.Controls.Add(lblTitle); // Agregamos el título a la tarjeta

            // Descripción de la tarea
            Label lblDesc = new Label
            {
                Text = task.Description,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(5, 25)
            };
            card.Controls.Add(lblDesc); // Agregamos la descripción a la tarjeta

            // Agregar evento de clic a todos los controles de la tarjeta
            // para que funcione al hacer clic en cualquier parte de la tarjeta
            card.Click += (object sender, EventArgs e) => Card_ClickEvent(task);
            lblTitle.Click += (object sender, EventArgs e) => Card_ClickEvent(task);
            lblDesc.Click += (object sender, EventArgs e) => Card_ClickEvent(task);

            return card; // Retornamos la tarjeta
        }

        private void Card_ClickEvent(TaskViewModel task)
        {
            TaskDetails details = new TaskDetails(task, dashboardId); // Mostrar detalles de la tarea
            details.TaskUpdated += (s, e) => // Evento para actualizar la tarea
            {
                KanbanDashboard_Load(s, e);
            };

            details.ShowDialog();
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            TaskViewModel newTask = new TaskViewModel
            {
                Id = 0,
                Title = "",
                Description = "",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                DashboardId = this.dashboardId
            };

            TaskDetails details = new TaskDetails(newTask, dashboardId); // Mostrar detalles de la tarea
            details.SetEditMode(true); // Habilitar modo de edición
            details.TaskUpdated += (s, ev) => // Evento para actualizar la tarea
            {
                KanbanDashboard_Load(s, ev);
            };
            details.ShowDialog();
        }   
    }
}
