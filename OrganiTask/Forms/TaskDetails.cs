﻿using OrganiTask.Entities.ViewModels;
using OrganiTask.Controllers;
using OrganiTask.Util;
using OrganiTask.Util.Collections;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    public partial class TaskDetails : Form
    {
        // Propiedades para almacenar el modelo de vista de la tarea y el identificador del tablero
        private TaskViewModel task; // Modelo de vista de la tarea
        private int dashboardId; // Identificador del tablero
        private bool isEditMode = false; // Indica si el formulario está en modo edición

        public event EventHandler TaskUpdated; // Evento para notificar que la tarea ha sido actualizada
        public event EventHandler TaskDeleted; // Evento para notificar que la tarea ha sido eliminada

        // Campos para los controles de edición
        private TextBox txtTitle; // TextBox para el título
        private TextBox txtDesc; // TextBox para la descripción
        private DateTimePicker dtpStart; // DateTimePicker para las fechas
        private DateTimePicker dtpEnd; // DateTimePicker para las fechas
        private OrganiList<ComboBox> comboBoxes = new OrganiList<ComboBox>(); // Lista de ComboBoxes para las etiquetas

        // Instancia del controlador
        private TaskController taskController = new TaskController();

        // Constructor del formulario requiere un modelo de vista de tarea y el identificador del tablero
        public TaskDetails(TaskViewModel task, int dashboardId)
        {
            InitializeComponent();
            this.task = task;
            this.dashboardId = dashboardId;
        }

        // Evento de carga del formulario
        private void TaskDetails_Load(object sender, EventArgs e)
        {
            if (task.Id != 0)
            {
                task = taskController.LoadTaskDetails(task.Id, dashboardId); // Cargar la tarea
            }
            RenderTask(); // Dibujar la tarea
        }

        // Método para dibujar la tarea
        private void RenderTask()
        {
            // Limpiamos el contenido de la tabla
            tblDetails.Controls.Clear();
            tblDetails.RowCount = 0;
            tblDetails.RowStyles.Clear();

            // Mostrar la tarea en modo vista o edición
            if (!isEditMode)
            {
                RenderTaskInViewMode(); // Modo vista
                // Determinar visibilidad de botones
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                txtBoxDesc.ReadOnly = true;
            }
            else
            {
                RenderTaskInEditMode(); // Modo edición
                // Determinar visibilidad de botones
                txtBoxDesc.ReadOnly = false;
                btnEdit.Visible = false;
                btnSave.Visible = true;
                btnDelete.Visible = false;
                btnCancel.Visible = true;
            }
        }

        // Método para dibujar la tarea en modo vista
        private void RenderTaskInViewMode()
        {
            // Definir el título y descripción de la tarea y mostrarlos
            lblTitle.Text = task.Title;
            lblTitle.Visible = true;
            txtBoxDesc.Text = task.Description;
            txtBoxDesc.Visible = true;

            LoadCategoriesViewMode(); // Cargamos las categorías de la tarea en modo vista

            // Agregamos las fechas de inicio y fin de la tarea a la tabla sin controles de edición
            AddRowWithLabel("Fecha Inicio:", task.StartDate.ToString("dd/MM/yyyy") ?? "");
            AddRowWithLabel("Fecha Fin:", task.EndDate.ToString("dd/MM/yyyy") ?? "");
        }

        // Método para dibujar la tarea en modo edición
        private void RenderTaskInEditMode()
        {
            // Limpiar la tabla antes de agregar filas
            tblDetails.Controls.Clear();
            tblDetails.RowCount = 0;
            tblDetails.RowStyles.Clear();

            // Ocultamos los labels de título y descripción
            lblTitle.Visible = false;
            txtBoxDesc.Visible = false;

            // Agregamos filas a la tabla con TextBoxes para editar el título y descripción
            AddRowWithTextBox("Título:", task.Title, out txtTitle);
            AddRowWithTextBox("Descripción:", task.Description, out txtDesc, multiline: true);

            // Cargar las categorías en modo edición
            LoadCategoriesEditMode();

            // Agregamos filas a la tabla con DateTimePickers para las fechas de inicio y fin
            AddRowWithDate("Fecha Inicio:", task.StartDate, out dtpStart);
            AddRowWithDate("Fecha Fin:", task.EndDate, out dtpEnd);
        }

        // Método para cargar las categorías de la tarea en modo vista
        private void LoadCategoriesViewMode()
        {
            OrganiList<CategoryViewModel> categories = taskController.LoadTaskCategories(task.Id, dashboardId); // Cargar las etiquetas de la tarea

            foreach (CategoryViewModel category in categories)
            {
                string tagName = category.AssignedTag != null ? category.AssignedTag.Name : "";
                string tagColor = category.AssignedTag != null ? category.AssignedTag.Color : "";
                AddRowWithLabel($"{category.Title}:", tagName, tagColor);
            }
        }

        // Método para cargar las categorías de la tarea en modo edición
        private void LoadCategoriesEditMode()
        {
            // Usamos el controlador para obtener las categorías de la tarea
            OrganiList<CategoryViewModel> categories = taskController.LoadTaskCategories(task.Id, dashboardId);

            // Limpiamos los ComboBoxes
            comboBoxes.Clear();

            // Para cada categoría, creamos un ComboBox con las etiquetas
            foreach (CategoryViewModel category in categories)
            {
                ComboBox cmb = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Width = 150
                };

                // Agregamos una opción para no tener etiqueta
                cmb.Items.Add(new TagViewModel { Id = 0, Name = "" });
                cmb.SelectedIndex = 0; // Por defecto, no hay etiqueta seleccionada

                // Ahora, agregamos las etiquetas de la categoría al ComboBox
                foreach (TagViewModel tag in category.TagList)
                {
                    int index = cmb.Items.Add(tag); // Agregamos la etiqueta al ComboBox y guardamos su índice

                    if (category.AssignedTag != null && tag.Id == category.AssignedTag.Id)
                    {
                        cmb.SelectedIndex = index; // Si la etiqueta es la actual, la seleccionamos
                    }
                }

                cmb.Tag = category.Id; // Guardamos el Id de la categoría en el Tag del ComboBox
                comboBoxes.Add(cmb); // Agregamos el ComboBox a la lista de ComboBoxes
                // Agregamos la fila a la tabla con el título de la categoría y el ComboBox
                AddRowWithControl(category.Title + ":", cmb);
            }
        }

        /*
         * A continuación se listan los métodos auxiliares para agregar filas a la tabla de detalles
         * con distintos tipos de controles para mostrar y editar la información de la tarea.
         */

        // Método para agregar una fila a la tabla con un label y un valor de texto sin edición
        private void AddRowWithLabel(string labelText, string valueText = "", string color = null)
        {
            // Incrementamos RowCount en 1
            int rowIndex = tblDetails.RowCount;
            tblDetails.RowCount = rowIndex + 1;

            // Definimos el estilo de la fila
            tblDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Creamos el label para la categoría
            Label lblKey = new Label
            {
                AutoSize = false,
                Size = new Size(190, 25),
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 2, 5, 2),
                Text = labelText,
                AutoEllipsis = true
            };

            // Creamos el label para la etiqueta
            Label lblValue = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 2, 0, 2),
                Text = valueText
            };

            // Si hay un color, lo aplicamos al label de la etiqueta
            if (!string.IsNullOrEmpty(valueText) && !string.IsNullOrWhiteSpace(color))
            {
                lblValue.ForeColor = ColorUtil.IsDarkColor(ColorUtil.ParseColor(color)) ? Color.White : Color.Black;
                lblValue.BackColor = ColorUtil.ParseColor(color);
                lblValue.Padding = new Padding(2, 1, 2, 1);
            }

            // Agregamos los labels a la tabla
            tblDetails.Controls.Add(lblKey, 0, rowIndex);
            tblDetails.Controls.Add(lblValue, 1, rowIndex);
        }

        // Método para agregar una fila a la tabla con un TextBox como control de edición
        private void AddRowWithTextBox(string labelText, string defaultValue, out TextBox txt, bool multiline = false)
        {
            // Incrementamos RowCount en 1
            int rowIndex = tblDetails.RowCount;
            tblDetails.RowCount++;

            // Definimos el estilo de la fila
            tblDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Creamos el label para la categoría
            Label lblKey = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Text = labelText,
                Margin = new Padding(0, 4, 10, 4)
            };

            // Creamos el TextBox para el valor
            txt = new TextBox
            {
                Text = defaultValue,
                Multiline = multiline,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 4, 0, 4),
                Height = multiline ? 100 : 20
            };

            // Agregamos los controles a la tabla
            tblDetails.Controls.Add(lblKey, 0, rowIndex);
            tblDetails.Controls.Add(txt, 1, rowIndex);
        }

        // Método para agregar una fila a la tabla con un control personalizado
        private void AddRowWithControl(string labelText, Control control)
        {
            // Incrementamos RowCount en 1
            int rowIndex = tblDetails.RowCount;
            tblDetails.RowCount++;

            // Definimos el estilo de la fila
            tblDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Creamos el label para la categoría
            Label lblKey = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Text = labelText,
                Margin = new Padding(0, 4, 10, 4)
            };

            // Configuramos el control personalizado
            control.Dock = DockStyle.Left;
            control.Margin = new Padding(0, 4, 0, 4);

            // Agregamos los controles a la tabla
            tblDetails.Controls.Add(lblKey, 0, rowIndex);
            tblDetails.Controls.Add(control, 1, rowIndex);
        }

        // Método para agregar una fila a la tabla con un DateTimePicker como control de edición
        private void AddRowWithDate(string labelText, DateTime? defaultDate, out DateTimePicker dtp)
        {
            // Incrementamos RowCount en 1
            int rowIndex = tblDetails.RowCount;
            tblDetails.RowCount++;

            // Definimos el estilo de la fila
            tblDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Creamos el label para la categoría
            Label lblKey = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Text = labelText,
                Margin = new Padding(0, 4, 10, 4)
            };

            // Creamos el DateTimePicker para el valor
            dtp = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short, // Por el momento, solo fecha aunque se puede cambiar con Tiempo
                Dock = DockStyle.Left,
                Margin = new Padding(0, 4, 0, 4)
            };

            if (defaultDate.HasValue) // Si hay una fecha por defecto, la asignamos
                dtp.Value = defaultDate.Value;
            else // Si no, asignamos la fecha actual
                dtp.Value = DateTime.Today;

            // Agregamos los controles a la tabla
            tblDetails.Controls.Add(lblKey, 0, rowIndex);
            tblDetails.Controls.Add(dtp, 1, rowIndex);
        }

        /*
         * Métodos de eventos de los botones
         */

        // Evento de clic en el botón de edición
        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetEditMode(true); // Cambiar a modo edición
        }

        // Evento de clic en el botón de guardar
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validar que la fecha de finalización no sea menor a la fecha de inicio
            if (dtpEnd.Value < dtpStart.Value)
            {
                MessageBox.Show("La fecha de finalización no puede ser anterior a la fecha de inicio.", 
                    "Error de validación", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return;
            }

            // Actualizamos la tarea con los nuevos valores
            task.Title = txtTitle.Text;
            task.Description = txtDesc.Text;
            task.StartDate = dtpStart.Value;
            task.EndDate = dtpEnd.Value;

            // Ahora procesamos las etiquetas por cada categoría editada
            OrganiList<TagViewModel> updatedTags = new OrganiList<TagViewModel>();
            foreach (ComboBox cmb in comboBoxes)
            {
                TagViewModel selected = cmb.SelectedItem as TagViewModel;
                if (selected != null && selected.Id > 0)
                {
                    selected.CategoryId = (int)cmb.Tag;
                    updatedTags.AddLast(selected);
                }
            }

            if (task.Id == 0)
            {
                taskController.InsertTask(task, updatedTags); // Crear la tarea
            }
            else
            {
                taskController.UpdateTask(task, updatedTags); // Actualizar la tarea
            }

            // Cambiamos a modo vista y volvemos a dibujar la tarea
            TaskUpdated?.Invoke(this, EventArgs.Empty);
            SetEditMode(false); // Cambiar a modo vista
        }

        // Evento de clic en el botón de cancelar
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (task.Id != 0)
            {
                SetEditMode(false); // Cambiar a modo vista
            }
            else
            {
                this.Close(); // Cerrar el formulario si no hay tarea
            }
        }

        /*
         * Métodos complementarios
         */

        // Método para cambiar el modo de edición del formulario
        public void SetEditMode(bool editMode)
        {
            isEditMode = editMode;
            RenderTask();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool taskDeleted = taskController.DeleteTask(task.Id);

            if (taskDeleted) this.Close();
            TaskDeleted?.Invoke(this, EventArgs.Empty); // Notificar que la tarea ha sido eliminada
        }
    }
}
