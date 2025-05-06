using OrganiTask.Controllers;
using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Forms.Controls;
using OrganiTask.Forms.Test;
using OrganiTask.Util;
using OrganiTask.Util.Collections;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OrganiTask.Forms
{
    /// <summary>
    /// Formulario para visualizar un tablero Kanban.
    /// </summary>
    public partial class KanbanDashboard : Form
    {
        // Propiedades para almacenar el identificador del tablero y el título de la categoría
        private int dashboardId;
        private CategoryViewModel selectedCategory; // default "Status"

        // Propiedad para controlar la visibilidad de la columna "Sin Etiquetar"
        private bool showHiddenColumn = false;

        // Propiedades auxiliares para controlar el drag y click de las tarjetas
        private bool _dragStarted = false; // Bandera para indicar si se ha iniciado el arrastre
        private Point _dragStartPoint; // Punto de inicio del arrastre
        private Timer _dragTimer; // Temporizador para actualizar la posición del formulario de arrastre

        // Formulario que muestra la tarjeta arrastrada
        private DragForm _dragForm;

        public int SourceTagId { get; set; } // ID de la etiqueta de origen de la tarjeta arrastrada

        DashboardController controller = new DashboardController(); // Instancia del controlador de tableros

        // Constructor del formulario requiere identificar el tablero y la categoría con la que se ordenarán las tareas
        public KanbanDashboard(int dashboardId)
        {
            InitializeComponent();
            this.dashboardId = dashboardId;
        }

        // Evento de carga del formulario
        private void KanbanDashboard_Load(object sender, EventArgs e)
        {
            selectedCategory = controller.GetDefaultCategory(dashboardId); // Obtenemos la categoría por defecto del tablero
            RefreshDashboard(); // Cargamos el tablero al iniciar el formulario

            this.WindowState = FormWindowState.Maximized; // maximiza la ventana.
            //this.FormBorderStyle = FormBorderStyle.None; // elimina la barra de título y los bordes.
            this.Bounds = Screen.PrimaryScreen.Bounds; // asegura que ocupe toda la pantalla
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
                    Panel taskCard = CreateTaskCard(task, column.Tag.Id, column.ColorColumn); // Creamos la tarjeta
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
        private TaskCardPanel CreateTaskCard(TaskViewModel task, int tagId, string ColorColumn)
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

            Color baseColor = ColorUtil.ParseColor(ColorColumn);

            // Título de la tarea
            Label lblTitle = new Label
            {
                Text = task.Title,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorUtil.IsDarkColor(baseColor) ? Color.White : Color.Black,
                AutoSize = false, // Controlar el ancho
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top, // Hacer que el ancho ocupe todo el panel
                Height = 25,
                Location = new Point(0, 0),
                AutoEllipsis = true
            };

            card.Controls.Add(lblTitle); // Agregamos el título a la tarjeta

            // Descripción de la tarea
            Label lblDesc = new Label
            {
                Text = task.Description,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                AutoSize = false,
                Height = 60,
                Width = 235,
                Location = new Point(5, 30),
                AutoEllipsis = true,
                ForeColor = ColorUtil.IsDarkColor(baseColor) ? Color.White : Color.Black,
                BackColor = Color.Transparent,
            };

            if(task.Description.Length > 0)
            {
                // Línea divisora
                Panel separator = new Panel
                {
                    Height = 1,
                    Location = new Point(15, 100),
                    BackColor = ColorUtil.IsDarkColor(baseColor) ? Color.White : Color.Black,
                };

                card.Controls.Add(separator);
            }

            card.Controls.Add(lblDesc); // Agregamos la descripción a la tarjeta

            return card; // Retornamos la tarjeta
        }

        private void RefreshDashboard()
        {
            // Cargar el modelo del tablero
            DashboardViewModel model = controller.LoadKanban(dashboardId, selectedCategory.Id);

            // Verificar si la categoría seleccionada sigue existiendo
            bool categoryExists = controller.CategoryExists(dashboardId, selectedCategory.Id);

            // Si la categoría ya no existe, obtener la categoría por defecto
            if (!categoryExists)
            {
                // Obtenemos la categoría por defecto o la primera disponible
                selectedCategory = controller.GetDefaultCategory(dashboardId);

                // Si no hay ninguna categoría disponible, mostrar mensaje
                if (selectedCategory == null || selectedCategory.Id == -1)
                {
                    btnShowHidden.Visible = false;
                    showHiddenColumn = true;
                } else
                {
                    model = controller.LoadKanban(dashboardId, selectedCategory.Id);
                    RenderDashboard(model);
                }
            }

            if (model == null) // Mostrar error si no se encuentra el tablero
            {
                MessageBox.Show("No se encontró el tablero especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Configurar la visibilidad del botón "Sin Etiquetar"
            if (model.Columns != null && model.Columns.Any() && model.Columns.First != null && model.Columns.First.Tag != null && model.Columns.First.Tag.Id == -1)
            {
                btnShowHidden.Visible = false;
                showHiddenColumn = true;
            }
            else
                btnShowHidden.Visible = true;

            // Actualizar el título del tablero
            lblDashboardTitle.Text = model.DashboardTitle;

            // Renderizar el tablero
            RenderDashboard(model);

            // Asegurarnos de que el combo de ordenamiento está oculto cuando refrescamos
            RevertSortControl();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            // Obtener la lista de categorías del tablero
            OrganiList<CategoryViewModel> categories = controller.GetDashboardCategories(dashboardId);

            // Verificar si hay categorías reales (excluyendo la de Id -1)
            bool hasRealCategories = categories.Count > 0 && categories.Any(c => c.Id != -1);

            if (!hasRealCategories)
            {
                MessageBox.Show("¡Agrega una categoría y sus etiquetas para comenzar a filtrar!", "Alerta", MessageBoxButtons.OK);
                return; // Salimos de la función sin mostrar el combo box
            }

            // Ahora reordenamos las categorías colocando la seleccionada al inicio
            categories = ReorderCategories(categories, selectedCategory);

            btnSort.Visible = false; // Ocultamos el botón de ordenamiento  
            cboSort.Items.Clear(); // Limpiamos los elementos del combo box 

            // Indicamos al combobox que se usará para mostrar las categorías
            cboSort.DisplayMember = nameof(CategoryViewModel.Title);
            cboSort.ValueMember = nameof(CategoryViewModel.Id);
            cboSort.Visible = true;

            // Añadimos solo categorías válidas
            var validCategories = categories.Where(c => c.Id != -1).ToArray();
            if (validCategories.Length > 0)
            {
                cboSort.Items.AddRange(validCategories);
                cboSort.SelectedIndex = 0;
                cboSort.Focus();
                cboSort.DroppedDown = true;
            }
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
            details.TaskDeleted += EventRefreshDashboard; // Evento para cuando se elimina una tarea
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

            // Validamos que no sea nulo
            if (selected != null)
            {
                bool changed = selected.Id != selectedCategory.Id;

                selectedCategory = selected; // Asignamos la nueva categoría
                RefreshDashboard(); // Recargamos el tablero SIEMPRE, incluso si es la misma categoría
            }
            else
            {
                // En caso de selección nula, intentamos obtener la categoría por defecto
                selectedCategory = controller.GetDefaultCategory(dashboardId);
                RefreshDashboard();
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
            details.TaskDeleted += EventRefreshDashboard; // Evento para cuando se elimina una tarea

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
        private OrganiList<CategoryViewModel> ReorderCategories(OrganiList<CategoryViewModel> categories, CategoryViewModel selected)
        {
            OrganiList<CategoryViewModel> reordered = new OrganiList<CategoryViewModel>();

            // Ubicamos la categoría actual al inicio
            reordered.AddLast(selected);

            // Agregamos el resto de las categorías
            foreach (CategoryViewModel category in categories)
            {
                if (category.Id != selected.Id)
                {
                    reordered.AddLast(category);
                }
            }

            return reordered;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            DashboardSettings settings = new DashboardSettings(dashboardId); // Mostrar configuración del tablero
            settings.DashboardInfoChanged += EventRefreshDashboard;
            settings.ShowDialog();
        }

        private void btnDashboardBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Cerrar el formulario
        }
    }
}
