using OrganiTask.Controllers;
using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrganiTask.Forms.Controls
{
    /// <summary>
    /// Clase que representa un panel de columna en el tablero Kanban.
    /// Encapsula la lógica para manejar el arrastre y la colocación de tareas en la columna.
    /// </summary>
    public class KanbanColumnPanel : FlowLayoutPanel
    {
        public Tag Column { get; set; } // Propiedad para almacenar la etiqueta

        // Referencia al controlador de tareas
        public TaskController TaskController { get; set; } = new TaskController();

        // Evento para indicar al formulario que se actualice tras un drop
        public event EventHandler ColumnUpdated;

        // Constructor que recibe una etiqueta y la asigna al panel para estilizarlo
        public KanbanColumnPanel(Tag tag)
        {
            this.Column = tag;

            // Configuración estándar del panel
            this.FlowDirection = FlowDirection.TopDown;
            this.WrapContents = false;
            this.AutoScroll = true;
            this.Width = 250;
            this.Height = 1000;
            this.Margin = new Padding(8);
            this.AllowDrop = true;
            this.BackColor = GetColorColumn(tag.Color);

            // Contenedor para el título
            Panel headerPanel = new Panel
            {
                Height = 30, // Altura fija para el título
                Width = 250,
                Dock = DockStyle.None,
                BackColor = Color.Transparent // Fondo transparente
            };

            // Label con el nombre de la columna
            Label lblTag = new Label
            {
                Text = tag.Name,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoEllipsis = true
            };

            headerPanel.Controls.Add(lblTag); // Agregar el label al contenedor
            this.Controls.Add(headerPanel); // Agregar el contenedor al panel principal

            headerPanel.BringToFront();

            // Suscribir los eventos de drag y drop
            this.DragEnter += ColumnDragEnter;
            this.DragDrop += ColumnDragDrop;
        }


        private Color GetColorColumn(string colorString)
        {
            Color currentColor;

            if (string.IsNullOrEmpty(colorString))
                return Color.FromArgb(135, Color.Gray); // Default color

            if(colorString.StartsWith("#"))
            {
                // Si es hexadecimal
                currentColor = ColorTranslator.FromHtml(colorString);
            }else
            {
                currentColor = Color.FromName(colorString);
            }

            // Aplicar opacidad 
            return Color.FromArgb(135, currentColor);
        }

        // Método que se llama cuando se arrastra una tarea sobre la columna
        private void ColumnDragEnter(object sender, DragEventArgs e)
        {
            // Comprobamos que el panel sea una tarea
            if (e.Data.GetDataPresent(typeof(TaskViewModel)))
            {
                e.Effect = DragDropEffects.Move; // Permitimos el arrastre
            }
            else // Si no es una tarea, bloqueamos el arrastre
            {
                e.Effect = DragDropEffects.None;
            }
        }
        
        // Método que se llama cuando se suelta una tarea sobre la columna
        private void ColumnDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TaskViewModel)))
            {
                // Obtenemos la tarea arrastrada
                TaskViewModel draggedTask = (TaskViewModel)e.Data.GetData(typeof(TaskViewModel));

                // Obtenemos el formulario del tablero para actualizarlo
                KanbanDashboard dashboardForm = this.FindForm() as KanbanDashboard;

                // Si el formulario no es nulo y la tarea arrastrada no tiene la misma etiqueta que la columna
                if (dashboardForm != null && dashboardForm.SourceTagId != this.Column.Id)
                {
                    // Actualizamos la tarea en la base de datos
                    TaskController.UpdateTagCategoryForTask(draggedTask.Id, this.Column.Id, this.Column.CategoryId);
                    ColumnUpdated?.Invoke(this, EventArgs.Empty); // Disparamos el evento para actualizar el formulario
                }
            }
        }
    }
}
