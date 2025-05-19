using OrganiTask.Controllers;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util.Collections;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    /// <summary>
    /// Formulario para ver y modificar la información de un tablero.
    /// </summary>
    public partial class DashboardSettings : Form
    {
        private readonly int dashboardId; // ID del tablero 
        private readonly DashboardController controller = new DashboardController(); // Instancia del controlador

        private bool _isEditMode = false; // Bandera para indicar si estamos en modo edición

        public event EventHandler DashboardInfoChanged; // Evento que se dispara cuando se guarda la información del tablero

        public DashboardSettings(int dashboardId)
        {
            InitializeComponent();
            this.dashboardId = dashboardId;
        }

        private void DashboardSettings_Load(object sender, EventArgs e)
        {
            LoadDashboardInfo(); // Al cargar el formulario, inmediatamente cargamos la información del tablero
        }

        private void LoadDashboardInfo()
        {
            DashboardViewModel dvm = controller.LoadDashboardDetails(dashboardId); // Cargamos la información del tablero desde el controlador

            // Determinamos si el tablero existe
            if (dvm == null)
            {
                MessageBox.Show("Error al cargar el tablero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Obtenemos el nombre de usuario del propietario del tablero
            string username = controller.GetUsernameFromDashboardOwnerId(dvm.UserId);

            // Cargamos la información del tablero en los controles
            lblHeader.MaximumSize = new Size(495, 0);
            lblHeader.AutoSize = false;
            lblHeader.AutoEllipsis = true;

            lblHeader.Text = $"Información de {dvm.DashboardTitle}"; // Concatenamos el título del tablero al texto del label
            lblCreatorValue.Text = username ?? "(desconocido)"; // Por cualquier error con el propietario, ponemos "(desconocido)"
            lblDescText.Text = dvm.Description ?? "(sin descripción)"; // Si no hay descripción, ponemos "(sin descripción)"

            LoadCategoriesTable(); // Cargamos la tabla de categorías

            // Definimos la visibilidad de los botones de acción
            btnSave.Visible = false;
            btnCancel.Visible = true;
            btnEdit.Visible = true;
        }

        // Método que se encarga de cargar la tabla de categorías de manera dinámica
        private void LoadCategoriesTable()
        {
            // Limpiamos la tabla de categorías
            tblCategories.Controls.Clear();
            tblCategories.RowStyles.Clear();
            tblCategories.RowCount = 0;

            // Por medio del controlador, obtenemos la lista de categorias del tablero
            OrganiList<CategoryViewModel> columnTitles = controller.GetDashboardCategories(dashboardId);

            int row = 0;

            foreach (CategoryViewModel column in columnTitles)
            {
                // Construimos un Label con el nombre de la categoría
                Label lbl = new Label
                {
                    Text = column.Title,
                    AutoSize = false,
                    Font = new Font("Segoe UI", 10F),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(3),
                    Tag = column.Id,
                    TextAlign = ContentAlignment.MiddleLeft
                };
                tblCategories.RowCount++;
                tblCategories.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tblCategories.Controls.Add(lbl, 0, row);

                // Construimos un Button para editar la categoría
                Button btnView = new Button
                {
                    Text = "Ver",
                    AutoSize = true,
                    Tag = column.Id,
                    Margin = new Padding(3)
                };

                // Asignamos el evento Click al botón
                btnView.Click += (s, e) =>
                {
                    int catId = (int)((Button)s).Tag;
                    CategorySettings categoryView = new CategorySettings(catId, dashboardId);
                    categoryView.CategoryUpdated += (sender, args) =>
                    {
                        LoadCategoriesTable();
                        DashboardInfoChanged?.Invoke(this, EventArgs.Empty);
                    };

                    categoryView.ShowDialog();
                };
                tblCategories.Controls.Add(btnView, 1, row);

                // Construimos un Button para eliminar la categoría
                Button btnDelete = new Button
                {
                    Text = "Eliminar",
                    AutoSize = true,
                    Tag = column.Id,
                    Margin = new Padding(3),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(200, 80, 80)
                };

                // Asignamos el evento Click al botón
                btnDelete.Click += (s, e) =>
                {
                    int catId = (int)((Button)s).Tag;
                    bool isLastCategory = columnTitles.Count == 1;

                    string messageDelete = isLastCategory
                        ? "Esta es la última categoría. \n¿Estás seguro de que deseas eliminarla?"
                        : "¿Estás seguro de que deseas eliminar esta categoría?";

                    DialogResult result = MessageBox.Show(messageDelete, "Eliminar categoría", MessageBoxButtons.YesNo, isLastCategory ? MessageBoxIcon.Warning :  MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        new CategoryController().DeleteCategory(catId); // Llamamos al controlador para eliminar la categoría
                        LoadCategoriesTable(); // Recargamos la tabla de categorías
                        DashboardInfoChanged?.Invoke(this, EventArgs.Empty); // Disparamos el evento de guardado
                    }
                };

                tblCategories.Controls.Add(btnDelete, 2, row);

                row++;
            }

            tblCategories.RowCount++;
            tblCategories.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Finalmente, agregamos como última opción un botón para crear una nueva categoría
            Button btnNew = new Button
            {
                Text = "➕ Nueva categoría",
                AutoSize = true,
                Margin = new Padding(3)
            };

            // Y le asignamos el evento Click
            btnNew.Click += (s, e) =>
            {
                CategorySettings categoryView = new CategorySettings(dashboardId);
                categoryView.CategoryUpdated += (sender, ev) =>
                {
                    LoadCategoriesTable(); // Recargamos la tabla de categorías
                    DashboardInfoChanged?.Invoke(this, EventArgs.Empty); // Disparamos el evento de guardado
                };

                categoryView.ShowDialog();
            };

            tblCategories.Controls.Add(btnNew, 0, row);
            tblCategories.SetColumnSpan(btnNew, 2); // Hacemos que el botón ocupe dos columnas
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Obtenemos los valores de los controles de texto
            string title = txtHeader.Text.Trim();
            string description = txtDescription.Text.Trim();

            // Validamos que el título no esté vacío
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("El título no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Utilizamos el controlador para actualizar la información del tablero
            controller.UpdateDashboard(dashboardId, title, description);
            DashboardInfoChanged?.Invoke(this, EventArgs.Empty); // Disparamos el evento de guardado

            // Para evitar refrescar toda la información del tablero, simplemente actualizamos los labels visualmente
            lblHeader.Text = $"Información de {title}";
            lblDescText.Text = description;
            ExitEditMode(); // Salimos del modo edición
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Condición para determinar si estamos en modo edición
            if (_isEditMode)
            {
                ExitEditMode();
            }
            else // Si no, cerrar
            {
                this.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnterEditMode(); // Entramos en modo edición
        }

        private void EnterEditMode()
        {
            _isEditMode = true;

            string title = lblHeader.Text.Replace("Información de ", "");
            txtHeader.Text = title;
            txtDescription.Text = lblDescText.Text;

            lblHeader.Visible = false;
            lblDescText.Visible = false;

            ToggleCategoryControls();

            txtHeader.Visible = true;
            txtDescription.Visible = true;


            btnSave.Visible = true;
            btnEdit.Visible = false;
        }

        private void ExitEditMode()
        {
            _isEditMode = false;

            lblHeader.Visible = true;
            lblDescText.Visible = true;

            ToggleCategoryControls();

            txtHeader.Visible = false;
            txtDescription.Visible = false;

            btnSave.Visible = false;
            btnEdit.Visible = true;
        }

        private void ToggleCategoryControls()
        {
            foreach (Control c in tblCategories.Controls)
            {
                if (c is Button)
                    c.Visible = !c.Visible;
            }
        }
    }
}
