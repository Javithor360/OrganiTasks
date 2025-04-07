using OrganiTask.Controllers;
using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Forms.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    /// <summary>
    /// Formulario para visualizar un tablero Kanban.
    /// </summary>
    public partial class KanbanDashboard : Form
    {
        // Propiedades para almacenar el identificador del tablero y el título de la categoría
        private int dashboardId;
        private string categoryTitle; // default "Status"

        // Propiedades auxiliares para controlar el drag y click de las tarjetas
        private Point _dragStartPoint; // Punto de inicio del arrastre
        private bool _dragStarted = false; // Bandera para indicar si se ha iniciado el arrastre
        private int _sourceTagId = 0; // ID de la etiqueta de origen (columna)
        private DragForm _dragForm;
        private Timer _dragTimer;

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
                FlowLayoutPanel columnPanel = CreateColumnPanel(column.Tag);

                // Dibujamos dinámicamente una tarjeta por cada tarea en la lista de tareas
                foreach (TaskViewModel task in column.Tasks)
                {
                    Panel taskCard = CreateTaskCard(task, column.Tag.Id); // Creamos la tarjeta
                    columnPanel.Controls.Add(taskCard); // Agregamos la tarjeta a la columna
                }

                // Agregamos la columna al panel principal
                flpBoard.Controls.Add(columnPanel);
            }
        }

        // Método para crear una columna por cada categoría en el tablero
        private FlowLayoutPanel CreateColumnPanel(Tag tag)
        {
            // Creamos un panel de flujo para la columna
            FlowLayoutPanel column = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Width = 250,
                Height = flpBoard.Height - 30,
                Margin = new Padding(10),
                AllowDrop = true,
                Tag = tag.Id
            };

            // Evento para manejar el arrastre y caída de tareas en la columna
            column.DragEnter += ColumnDragEnter;
            column.DragDrop += ColumnDragDrop;

            // Etiqueta con el nombre de la categoría
            Label lblTag = new Label()
            {
                Text = tag.Name,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            column.Controls.Add(lblTag); // Agregamos la etiqueta al panel

            return column; // Retornamos la columna
        }

        // Método para crear una tarjeta por cada tarea en la columna
        private TaskCardPanel CreateTaskCard(TaskViewModel task, int tagId)
        {
            // Instanciamos un panel de tarjeta para la tarea
            TaskCardPanel card = new TaskCardPanel
            {
                TaskData = task,
                CurrentTagId = tagId,
            };

            // Adherimos los eventos de click, arrastre y soltar del mouse
            card.MouseDown += Card_MouseDown;
            card.MouseMove += Card_MouseMove;
            card.MouseUp += Card_MouseUp;

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

            return card; // Retornamos la tarjeta
        }

        // Evento para manejar el click en el botón de agregar tarea
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
            details.TaskUpdated += Details_TaskUpdated; // Evento para cuando se actualiza una tarea
            details.ShowDialog(); // Mostrar el formulario de detalles
        }

        /*
         * EVENT LISTENERS PERSONALIZADOS
         */

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

        private void ColumnDragDrop(object sender, DragEventArgs e)
        {
            TaskController taskController = new TaskController(); // Instanciar el controlador de tareas

            if (e.Data.GetDataPresent(typeof(TaskViewModel)))
            {
                // Obtenemos la tarea arrastrada
                TaskViewModel draggedTask = (TaskViewModel)e.Data.GetData(typeof(TaskViewModel));

                int destinationTagId = (int)((FlowLayoutPanel)sender).Tag; // Obtenemos el ID de la etiqueta de destino

                // Ejecutamos si y solo si el tag (columna) arrastrada no es la misma que la de destino
                if (_sourceTagId != destinationTagId)
                {
                    // Obtenemos el ID de la categoría de la etiqueta de destino
                    int categoryId = taskController.GetCategoryIdFromTagId(destinationTagId);

                    // Actualizamos la tarea en la base de datos
                    taskController.UpdateTagCategoryForTask(draggedTask.Id, destinationTagId, categoryId);

                    KanbanDashboard_Load(sender, e); // Recargamos el tablero
                }
            }
        }

        // Evento para manejar el click en la tarjeta
        private void Card_ClickEvent(TaskViewModel task)
        {
            TaskDetails details = new TaskDetails(task, dashboardId); // Mostrar detalles de la tarea
            details.TaskUpdated += Details_TaskUpdated; // Evento para cuando se actualiza una tarea

            details.ShowDialog(); // Mostramos el formulario de detalles
        }

        // Eventos para manejar el arrastre
        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            // Comprobamos que el botón izquierdo del mouse haya sido presionado
            if (e.Button == MouseButtons.Left)
            {
                TaskCardPanel card = sender as TaskCardPanel; // Obtenemos la tarjeta con sus datos
                _dragStartPoint = e.Location; // Capturamos el punto de inicio del arrastre
                _dragStarted = false; // Reiniciamos la bandera de arrastre
                _sourceTagId = card.CurrentTagId; // Guardamos el ID de la etiqueta de origen
            }
        }

        // Evento para manejar el movimiento del mouse
        private void Card_MouseMove(object sender, MouseEventArgs e)
        {
            // Comprobamos que el botón izquierdo del mouse haya sido presionado y que el arrastre no haya comenzado
            if (e.Button == MouseButtons.Left && !_dragStarted)
            {
                // Comprobamos si el movimiento del mouse es mayor al tamaño de arrastre
                // Para hacer esto, comparamos la distancia entre el punto de inicio y el punto actual
                // Si la distancia es mayor al tamaño de arrastre, iniciamos el arrastre
                if (Math.Abs(e.X - _dragStartPoint.X) >= SystemInformation.DragSize.Width ||
                    Math.Abs(e.Y - _dragStartPoint.Y) >= SystemInformation.DragSize.Height)
                {
                    _dragStarted = true; // Marcamos que el arrastre ha comenzado
                    TaskCardPanel card = sender as TaskCardPanel; // Obtenemos la tarjeta con sus datos

                    // Capturar la imagen de la tarjeta
                    Bitmap bitmap = new Bitmap(card.Width, card.Height);
                    card.DrawToBitmap(bitmap, new Rectangle(0, 0, card.Width, card.Height));

                    // Crear DragForm con la imagen capturada
                    Point screenPos = card.PointToScreen(e.Location);
                    _dragForm = new DragForm(bitmap);
                    _dragForm.Location = new Point(screenPos.X - bitmap.Width / 2, screenPos.Y - bitmap.Height / 2);
                    _dragForm.Show(); // Mostrar el formulario de arrastre

                    StartDragTimer();

                    try
                    {
                        card.DoDragDrop(card.TaskData, DragDropEffects.Move); // Iniciamos el arrastre de la tarjeta
                    }
                    finally
                    {
                        StopDragTimer();
                        // Ocultamos el formulario de arrastre al soltar
                        if (_dragForm != null)
                        {
                            _dragForm.Close();
                            _dragForm.Dispose();
                            _dragForm = null; // Limpiamos la referencia
                        }
                    }
                }
            }
        }

        // Evento para manejar el soltar el mouse
        private void Card_MouseUp(object sender, MouseEventArgs e)
        {
            // Verificamos que el arrastre no haya comenzado
            if (!_dragStarted)
            {
                // Si el arrastre no ha comenzado, significa que el usuario hizo click en la tarjeta
                TaskCardPanel card = sender as TaskCardPanel;
                // Llama al evento click para mostrar los detalles de la tarea
                Card_ClickEvent(card.TaskData);
            }
        }

        // Evento para manejar la actualización de la vista de tareas
        private void Details_TaskUpdated(object sender, EventArgs e)
        {
            // Recargamos el tablero cuando se actualiza una tarea
            KanbanDashboard_Load(sender, e);
        }

        private void StartDragTimer()
        {
            _dragTimer = new Timer();
            _dragTimer.Interval = 20; // Actualiza cada 20 ms
            _dragTimer.Tick += (s, e) =>
            {
                if (_dragForm != null)
                {
                    Point pos = Cursor.Position;
                    _dragForm.Location = new Point(pos.X - _dragForm.Width / 2, pos.Y - _dragForm.Height / 2);
                }
            };
            _dragTimer.Start();
        }

        private void StopDragTimer()
        {
            if (_dragTimer != null)
            {
                _dragTimer.Stop();
                _dragTimer.Dispose();
                _dragTimer = null;
            }
        }
    }
}
