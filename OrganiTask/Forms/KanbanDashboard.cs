using OrganiTask.Entities;
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
        private int selectedCategoryId;

        public KanbanDashboard(int dashboardId, int selectedCategoryId)
        {
            InitializeComponent();
            this.dashboardId = dashboardId;
            this.selectedCategoryId = selectedCategoryId;
        }

        private void KanbanDashboard_Load(object sender, EventArgs e)
        {
            using (var context = new OrganiTaskDB())
            {
                // Obtenemos la instancia del tablero
                var dashboard = context.Dashboards.FirstOrDefault(b => b.Id == dashboardId);
                lblDashboardTitle.Text = dashboard.Name;

                // Hacemos la búsqueda por el ID del tablero y el ID de la categoría seleccionada
                var categories = context.Categories
                    .Include("Tag") // Incluimos las etiquetas
                    .FirstOrDefault(c => c.Id == selectedCategoryId && c.DashboardId == dashboardId);

                if (categories == null)
                {
                    MessageBox.Show($"La categoría seleccionada no pudo ser encontrada en el tablero");
                    return;
                }

                // Ahora, para etiqueta creamos una columna
                foreach (var tag in categories.Tag)
                {
                    FlowLayoutPanel columnPanel = CreateColumnPanel(tag.Name);

                    // Buscamos las tareas que tengan la etiqueta evaluada
                    var tasksWithTag = context.Tasks
                        .Where(t => t.DashboardId == dashboardId && t.TaskTag.Any(tt => tt.TagId == tag.Id))
                        .ToList();

                    // Crear una tarjeta para cada tarea
                    foreach (var task in tasksWithTag)
                    {
                        var card = CreateTaskCard(task);
                        columnPanel.Controls.Add(card);
                    }

                    flpBoard.Controls.Add(columnPanel);
                }

            }
        }

        private FlowLayoutPanel CreateColumnPanel(string name)
        {
            // 
            FlowLayoutPanel column = new FlowLayoutPanel();
            column.FlowDirection = FlowDirection.TopDown;
            column.Width = 250;
            column.Height = 200;
            column.AutoScroll = true;
            column.Margin = new Padding(10);

            //
            Label lblTag = new Label();
            lblTag.Text = name;
            lblTag.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTag.AutoSize = true;
            column.Controls.Add(lblTag);

            return column;
        }

        private Panel CreateTaskCard(Task task)
        {
            Panel card = new Panel();
            card.Width = 200;
            card.Height = 80;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(5);

            // Título de la tarea
            Label lblTitle = new Label();
            lblTitle.Text = task.Title;
            lblTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(5, 5);
            card.Controls.Add(lblTitle);

            // Descripción (opcional)
            Label lblDesc = new Label();
            lblDesc.Text = task.Description;
            lblDesc.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(5, 25);
            card.Controls.Add(lblDesc);

            return card;
        }
    }
}
