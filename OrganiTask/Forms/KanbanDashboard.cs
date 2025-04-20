using OrganiTask.Controllers;
using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Forms.Controls;
using OrganiTask.Util.Collections;
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

        // Propiedad para controlar la visibilidad de la columna "Sin Etiquetar"
        private bool showHiddenColumn = false;

        // Propiedades auxiliares para controlar el drag y click de las tarjetas
        private bool _dragStarted = false; // Bandera para indicar si se ha iniciado el arrastre
        private Point _dragStartPoint; // Punto de inicio del arrastre
        private Timer _dragTimer; // Temporizador para actualizar la posición del formulario de arrastre

        // Formulario que muestra la tarjeta arrastrada
        private DragForm _dragForm;

        public int SourceTagId { get; set; } // ID de la etiqueta de origen de la tarjeta arrastrada

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
            RefreshDashboard(); // Cargamos el tablero al iniciar el formulario
        }

        // Método para dibujar el tablero
        private void RenderDashboard(DashboardViewModel model)
        {
            // Limpiar columnas
            flpBoard.Controls.Clear();

            // Dibujamos dinámicamente una columna por cada elemento en la lista de columnas
            foreach (ColumnViewModel column in model.Columns)
            {
                // Si la columna es "Sin Etiquetar", verificamos si se debe mostrar
                if (column.Tag.Id == -1)
                {
                    // Si no se debe mostrar, continuamos con la siguiente iteración
                    if (!showHiddenColumn)
                        continue;
                }

                // Si no, creamos la columna
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
        private KanbanColumnPanel CreateColumnPanel(Tag tag)
        {
            // Instanciamos un panel de columna para la categoría
            KanbanColumnPanel column = new KanbanColumnPanel(tag);
            column.ColumnUpdated += EventRefreshDashboard; // Evento para actualizar el tablero al soltar una tarjeta

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
        private void RefreshDashboard()
        {
            DashboardController controller = new DashboardController();
            DashboardViewModel model = controller.LoadKanban(dashboardId, categoryTitle);

            if (model == null) // Mostrar error si no se encuentra el tablero
            {
                MessageBox.Show("No se encontró el tablero especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblDashboardTitle.Text = model.DashboardTitle;
            RenderDashboard(model);
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            btnSort.Visible = false; // Ocultamos el botón de ordenamiento  
            cboSort.Items.Clear(); // Limpiamos los elementos del combo box 

            // Instanciamos el controlador
            DashboardController controller = new DashboardController();
            OrganiList<CategoryViewModel> reordered = ReorderCategories(controller.GetDashboardCategories(dashboardId), categoryTitle); // Reordenamos las categorías

            cboSort.DisplayMember = nameof(CategoryViewModel.Title);
            cboSort.ValueMember = nameof(CategoryViewModel.Id);

            cboSort.Items.AddRange(reordered.ToArray()); // Agregamos los títulos al combo box
            if (reordered.Count > 0)
                cboSort.SelectedIndex = 0;

            cboSort.Visible = true; // Mostramos el combo box
            cboSort.Focus(); // Focamos el combo box
            cboSort.DroppedDown = true; // Abrimos el combo box 
        }

        // Evento para manejar el click en el botón de agregar tarea
        private void btnNewTask_Click(object sender, EventArgs e)
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
            details.TaskUpdated += EventRefreshDashboard; // Evento para cuando se actualiza una tarea
            details.ShowDialog(); // Mostrar el formulario de detalles
        }

        private void btnShowHidden_Click(object sender, EventArgs e)
        {
            showHiddenColumn = !showHiddenColumn; // Alternar la visibilidad de la columna "Sin Etiquetar"
            btnShowHidden.Text = showHiddenColumn ? "🔎 Esconder ocultos" : "🔎 Mostrar ocultos"; // Cambiar el texto del botón
            RefreshDashboard(); // Recargar el tablero
        }

        /*
         * EVENT LISTENERS AUXILIARES
         */

        // Evento para manejar el cambio de selección en el combo box
        // Se usa ChangeCommited para evitar que se ejecute al abrir el combo
        private void cboSort_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CategoryViewModel selected = cboSort.SelectedItem as CategoryViewModel; // Obtenemos el elemento seleccionado

            // Validamos que no sea nulo y que sea diferente al la categoría actual
            if (selected != null && selected.Title != categoryTitle)
            {
                categoryTitle = selected.Title; // Asignamos la nueva categoría
                RefreshDashboard(); // Recargamos el tablero
            }

            RevertSortControl(); // Revertimos el control de ordenamiento
        }

        // Evento para manejar el cierre del combo box
        private void cboSort_DropDownClosed(object sender, EventArgs e)
        {
            RevertSortControl(); // Revertimos el control de ordenamiento
        }

        // Evento para manejar la pérdida de foco del combo box
        private void cboSort_LostFocus(object sender, EventArgs e)
        {
            RevertSortControl(); // Revertimos el control de ordenamiento
        }

        /*
         * EVENT LISTENERS PERSONALIZADOS
         */

        // Evento para manejar el click en la tarjeta
        private void Card_ClickEvent(TaskViewModel task)
        {
            TaskDetails details = new TaskDetails(task, dashboardId); // Mostrar detalles de la tarea
            details.TaskUpdated += EventRefreshDashboard; // Evento para cuando se actualiza una tarea

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
                SourceTagId = card.CurrentTagId; // Guardamos el ID de la etiqueta de origen
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
                    _dragForm = new DragForm(bitmap); // Asignamos la instancia de DragForm al bitmap
                    _dragForm.Location = new Point(screenPos.X - bitmap.Width / 2, screenPos.Y - bitmap.Height / 2);
                    _dragForm.Show(); // Mostrar el formulario de arrastre

                    StartDragTimer(); // Iniciar el temporizador para actualizar la posición del formulario de arrastre

                    try
                    {
                        card.DoDragDrop(card.TaskData, DragDropEffects.Move); // Iniciamos el arrastre de la tarjeta
                    }
                    finally
                    {
                        StopDragTimer(); // Detenemos el temporizador al finalizar el arrastre

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

        private void EventRefreshDashboard(object sender, EventArgs e)
        {
            // Recargamos el tablero cuando se actualiza una columna
            RefreshDashboard();
        }

        // Método para iniciar el temporizador que actualiza la posición del formulario de arrastre
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

        // Método para detener el temporizador que actualiza la posición del formulario de arrastre
        private void StopDragTimer()
        {
            if (_dragTimer != null)
            {
                _dragTimer.Stop();
                _dragTimer.Dispose();
                _dragTimer = null;
            }
        }

        // Método para revertir el control de ordenamiento
        private void RevertSortControl()
        {
            cboSort.Visible = false; // Ocultamos el combo box
            btnSort.Visible = true; // Mostramos el botón de ordenamiento

            cboSort.Items.Clear(); // Limpiamos los elementos del combo box
        }

        // Reordenar la lista de categorías poniendo la categoría actual al frente
        private OrganiList<CategoryViewModel> ReorderCategories(OrganiList<CategoryViewModel> original, string currentTitle)
        {
            OrganiList<CategoryViewModel> reordered = new OrganiList<CategoryViewModel>();

            // Reordenamos la lista de categorías poniendo la categoría actual al frente
            foreach (var cat in original)
            {
                if (cat.Title == currentTitle)
                {
                    reordered.AddLast(cat);
                    break;
                }
            }

            // Agregamos el resto de las categorías
            foreach (var cat in original)
            {
                if (cat.Title != currentTitle)
                    reordered.AddLast(cat);
            }

            // Retornamos la lista reordenada
            return reordered;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            DashboardSettings settings = new DashboardSettings(dashboardId); // Mostrar configuración del tablero
            settings.ShowDialog();
        }
    }
}
