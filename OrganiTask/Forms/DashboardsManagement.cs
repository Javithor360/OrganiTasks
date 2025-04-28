using OrganiTask.Controllers;
using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util;
using System;
using System.Data;
using System.Linq;
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
            userId = IdUser;
        }

        private void LoadCategories()
        {
            using (var context = new OrganiTaskDB())
            {
                var categories = context.Categories
                    .Where(c => c.Dashboard.Id == 2)
                    .Select(c => new { c.Id, c.Title })
                    .ToList();

                //dgvCategories.DataSource = categories;
            }
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

            var newDashboard = new DashboardViewModel
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

            if (defaultCategories && validDefaultValues()) 
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
            // Enable or disable category controls based on the checkbox state
            lblCategoryName.Enabled = enabled;
            txtCategoryName.Enabled = enabled;
            lblTagName.Enabled = enabled;
            txtTagName.Enabled = enabled;
            btnAddTag.Enabled = enabled;
            listBoxTags.Enabled = enabled;

            // If creating default category, set default values
            if (enabled)
            {
                txtCategoryName.Text = "Status";

                // Clear and add default tags if the list is empty
                if (listBoxTags.Items.Count == 0)
                {
                    listBoxTags.Items.Add("Sin iniciar");
                    listBoxTags.Items.Add("En progreso");
                    listBoxTags.Items.Add("Finalizada");
                }
            }
            else
            {
                txtCategoryName.Text = string.Empty;
            }
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
            using (var context = new OrganiTaskDB())
            {
                // Crear la categoría
                var category = new CategoryViewModel
                {
                    Title = txtCategoryName.Text,
                    DashboardId = dashboardId
                };

                var currentCategory = categoryController.InsertCategory(category);

                // Crear las etiquetas
                foreach (var item in listBoxTags.Items)
                {
                    var tag = new TagViewModel
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
    }
}
