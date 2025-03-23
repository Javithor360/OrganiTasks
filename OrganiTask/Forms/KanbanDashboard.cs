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
using System.Text;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    public partial class KanbanDashboard: Form
    {
        private int dashboardId;
        private string categoryTitle;

        public KanbanDashboard(int dashboardId, string categoryTitle)
        {
            InitializeComponent();
            this.dashboardId = dashboardId;
            this.categoryTitle = categoryTitle;
        }

        private void KanbanDashboard_Load(object sender, EventArgs e)
        {
            DashboardController controller = new DashboardController();
            DashboardViewModel model = controller.LoadKanban(dashboardId, categoryTitle);

            if (model == null)
            {
                MessageBox.Show("No se encontró el tablero especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblDashboardTitle.Text = model.DashboardTitle;
            RenderDashboard(model);
        }

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

        private FlowLayoutPanel CreateColumnPanel(string name)
        {
            // 
            FlowLayoutPanel column = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Width = 250,
                Height = flpBoard.Height - 30,
                AutoScroll = true,
                Margin = new Padding(10)
            };

            //
            Label lblTag = new Label()
            {
                Text = name,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            column.Controls.Add(lblTag);

            return column;
        }

        private Panel CreateTaskCard(TaskViewModel task)
        {
            Panel card = new Panel
            {
                Width = 200,
                Height = 80,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5)
            };

            // Título de la tarea
            Label lblTitle = new Label
            {
                Text = task.Title,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(5, 5)
            };
            card.Controls.Add(lblTitle);

            // Descripción (opcional)
            Label lblDesc = new Label
            {
                Text = task.Description,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(5, 25)
            };
            card.Controls.Add(lblDesc);

            return card;
        }
    }
}
