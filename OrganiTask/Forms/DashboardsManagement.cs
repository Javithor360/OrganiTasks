using OrganiTask.Controllers;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util;
using System;
using System.Windows.Forms;

namespace OrganiTask.Forms.Test
{
    public partial class DashboardsManagement : Form
    {
        public event EventHandler DashboardStored;
        int userId { get; set; } // ID del usuario que está logueado
        bool defaultCategories = true; // Indica si se crearán categorías por defecto

        // Instancia del controlador
        private DashboardController dashboardController = new DashboardController();
        private CategoryController categoryController = new CategoryController();

        public DashboardsManagement(int IdUser)
        {
            InitializeComponent();
            SetCategoryControlsState(false);
            userId = IdUser;
        }

        private void btnSaveDashboard_Click(object sender, EventArgs e)
        {
            DashboardViewModel currentDashboard = null;

            if (string.IsNullOrEmpty(txtDashboardTitle.Text))
            {
                MessageBox.Show("Ingrese un nombre para el tablero",
                   "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DashboardViewModel newDashboard = new DashboardViewModel
            {
                DashboardTitle = txtDashboardTitle.Text,
                Description = txtDashboardDescription.Text.Trim(),
                UserId = userId
            };

            // Validar campos extra si lo amerita
            if (defaultCategories && !validDefaultValues()) return;

            currentDashboard = dashboardController.CreateDashboard(newDashboard);

            if (currentDashboard == null)
            {
                MessageBox.Show("Error al crear el tablero.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mensajes de exito o error.
            MessageBox.Show("Tablero creado correctamente.");

            if (validDefaultValues())
                createDefaultValues(currentDashboard.Id);

            // Trigger de evento que notifica sobre la creación del tablero
            DashboardStored?.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        private void chkCreateDefaultCategory_CheckedChanged(object sender, EventArgs e)
        {
            SetCategoryControlsState(chkCreateDefaultCategory.Checked);
            defaultCategories = chkCreateDefaultCategory.Checked;
        }

        private void SetCategoryControlsState(bool enabled)
        {
            lblCategoryName.Enabled = enabled;
            txtCategoryName.Enabled = enabled;
            lblTagName.Enabled = enabled;
            txtTagName.Enabled = enabled;
            btnAddTag.Enabled = enabled;
            listBoxTags.Enabled = enabled;

            listBoxTags.Items.Clear();

            txtCategoryName.Text = "Estado";

            listBoxTags.Items.Add("Sin iniciar");
            listBoxTags.Items.Add("En progreso");
            listBoxTags.Items.Add("Finalizada");
        }

        private bool validDefaultValues()
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text))
            {
                MessageBox.Show("El nombre de la categoría es obligatorio",
                   "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (listBoxTags.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos una etiqueta para la categoría.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void createDefaultValues(int dashboardId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Crear la categoría
                CategoryViewModel category = new CategoryViewModel
                {
                    Title = txtCategoryName.Text,
                    DashboardId = dashboardId
                };

                CategoryViewModel currentCategory = categoryController.InsertCategory(category);

                // Crear las etiquetas
                foreach (TagViewModel item in listBoxTags.Items)
                {
                    TagViewModel tag = new TagViewModel
                    {
                        Name = item.ToString(),
                        CategoryId = currentCategory.Id
                    };

                    categoryController.AddTagToCategory(currentCategory.Id, tag);
                }

                MessageBox.Show("Se ha creado su categoría y etiquetas correspondientes.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Método para validar el nombre de la etiqueta y agregarla
        private void btnAddTag_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTagName.Text))
            {
                MessageBox.Show("El nombre de la etiqueta es obligatorio",
                   "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listBoxTags.Items.Add(txtTagName.Text);
            txtTagName.Clear();
        }

        // Método para eliminar una etiqueta al hacer doble clic en ella
        private void listBoxTags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxTags.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show($"¿Desea eliminar la etiqueta '{listBoxTags.SelectedItem}'?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    listBoxTags.Items.Remove(listBoxTags.SelectedItem);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
