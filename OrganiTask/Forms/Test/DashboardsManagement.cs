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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiTask.Forms.Test
{
    public partial class DashboardsManagement : Form
    {
        public event EventHandler DashboardStored;
        int userId { get; set; } // ID del usuario que está logueado
        // Instancia del controlador
        private DashboardController dashboardController = new DashboardController();

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
            if (string.IsNullOrEmpty(txtDashboardTitle.Text))
            {
                MessageBox.Show("Ingrese un nombre para el tablero");
                return;
            }

            var newDashboard = new DashboardViewModel
            {
                DashboardTitle = txtDashboardTitle.Text,
                Description = "Descripción del tablero",
                UserId = userId 
            };

            dashboardController.CreateDashboard(newDashboard);
            MessageBox.Show("Tablero creado correctamente.");

            // Trigger de evento que notifica sobre la creación del tablero
            DashboardStored?.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        private void chkCreateDefaultCategory_CheckedChanged(object sender, EventArgs e)
        {
            SetCategoryControlsState(chkCreateDefaultCategory.Checked);
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

        private void btnUpdateDashboard_Click(object sender, EventArgs e)
        {
            //if (dgvDashboards.SelectedRows.Count == 0)
            //{
            //    MessageBox.Show("Seleccione un tablero para actualizar.");
            //    return;
            //}

            //int dashboardId = (int)dgvDashboards.SelectedRows[0].Cells["Id"].Value;

            //var updatedDashboard = new DashboardViewModel
            //{
            //    Id = dashboardId,
            //    Name = txtDashboardName.Text,
            //    Description = txtDashboardDescription.Text
            //};

            //_dashboardController.UpdateDashboard(updatedDashboard);
            //MessageBox.Show("Tablero actualizado correctamente.");
            //LoadDashboards();
        }

        //private void btnAddCategory_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtCategoryTitle.Text))
        //    {
        //        MessageBox.Show("Ingrese un nombre para la categoría");
        //        return;
        //    }

        //    using (var context = new OrganiTaskDB())
        //    {

        //        // Por el momento todas las categorías creadas serán asignadas al tablero con Id = 2
        //        var dashboard = context.Dashboards.FirstOrDefault(b => b.Id == 2);
        //        //var board = context.Boards.FirstOrDefault();

        //        var category = new Category
        //        {
        //            Title = txtCategoryTitle.Text,
        //            Dashboard = dashboard
        //        };

        //        context.Categories.Add(category);
        //        context.SaveChanges();
        //        MessageBox.Show("Categoría añadida correctamente.");
        //        LoadCategories();
        //    }
        //}

        //private void btnUpdateCategory_Click(object sender, EventArgs e)
        //{
        //    if (dgvCategories.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("Seleccione una categoría para actualizar");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtCategoryTitle.Text))
        //    {
        //        MessageBox.Show("Ingrese un nombre para la categoría");
        //        return;
        //    }

        //    int categoryId = (int)dgvCategories.SelectedRows[0].Cells["Id"].Value;

        //    using (var context = new OrganiTaskDB())
        //    {
        //        var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
        //        if (category == null)
        //        {
        //            MessageBox.Show("La categoría seleccionada no existe.");
        //            return;
        //        }

        //        category.Title = txtCategoryTitle.Text;
        //        context.SaveChanges();
        //        MessageBox.Show("Categoría actualizada correctamente.");
        //        LoadCategories();
        //    }
        //}

        //private void btnDeleteCategory_Click(object sender, EventArgs e)
        //{
        //    if (dgvCategories.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("Seleccione una categoría para eliminar");
        //        return;
        //    }

        //    int categoryId = (int)dgvCategories.SelectedRows[0].Cells["Id"].Value;

        //    using (var context = new OrganiTaskDB())
        //    {
        //        var category = context.Categories
        //            .Include("Tag") // Incluimos las etiquetas para eliminar las etiquetas asociadas a la categoría
        //            .FirstOrDefault(c => c.Id == categoryId);

        //        if (category == null)
        //        {
        //            MessageBox.Show("La categoría seleccionada no existe.");
        //            return;
        //        }

        //        // Eliminar las relaciones TaskTag asociadas a las Tags de la categoría a eliminar
        //        var tagIds = category.Tag.Select(t => t.Id).ToList();
        //        var taskTags = context.TaskTags.Where(tt => tagIds.Contains(tt.Tag.Id)).ToList();
        //        context.TaskTags.RemoveRange(taskTags);

        //        // Eliminar las Tags asociadas a la categoría
        //        context.Tags.RemoveRange(category.Tag);

        //        // Eliminar la categoría
        //        context.Categories.Remove(category);
        //        context.SaveChanges();

        //        MessageBox.Show("Categoría eliminada junto a sus etiquetas fueron eliminadas correctamente.");
        //        LoadCategories();
        //    }
        //}

        //private void btnAddCategory_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtCategoryTitle.Text))
        //    {
        //        MessageBox.Show("Ingrese un nombre para la categoría");
        //        return;
        //    }

        //    using (var context = new OrganiTaskDB())
        //    {

        //        // Por el momento todas las categorías creadas serán asignadas al tablero con Id = 2
        //        var dashboard = context.Dashboards.FirstOrDefault(b => b.Id == 2);
        //        //var board = context.Boards.FirstOrDefault();

        //        var category = new Category
        //        {
        //            Title = txtCategoryTitle.Text,
        //            Dashboard = dashboard
        //        };

        //        context.Categories.Add(category);
        //        context.SaveChanges();
        //        MessageBox.Show("Categoría añadida correctamente.");
        //        LoadCategories();
        //    }
        //}

        //private void btnUpdateCategory_Click(object sender, EventArgs e)
        //{
        //    if (dgvCategories.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("Seleccione una categoría para actualizar");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtCategoryTitle.Text))
        //    {
        //        MessageBox.Show("Ingrese un nombre para la categoría");
        //        return;
        //    }

        //    int categoryId = (int) dgvCategories.SelectedRows[0].Cells["Id"].Value;

        //    using (var context = new OrganiTaskDB())
        //    {
        //        var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
        //        if (category == null)
        //        {
        //            MessageBox.Show("La categoría seleccionada no existe.");
        //            return;
        //        }

        //        category.Title = txtCategoryTitle.Text;
        //        context.SaveChanges();
        //        MessageBox.Show("Categoría actualizada correctamente.");
        //        LoadCategories();
        //    }
        //}

        //private void btnDeleteCategory_Click(object sender, EventArgs e)
        //{
        //    if (dgvCategories.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("Seleccione una categoría para eliminar");
        //        return;
        //    }

        //    int categoryId = (int)dgvCategories.SelectedRows[0].Cells["Id"].Value;

        //    using (var context = new OrganiTaskDB())
        //    {
        //        var category = context.Categories
        //            .Include("Tag") // Incluimos las etiquetas para eliminar las etiquetas asociadas a la categoría
        //            .FirstOrDefault(c => c.Id == categoryId);

        //        if (category == null)
        //        {
        //            MessageBox.Show("La categoría seleccionada no existe.");
        //            return;
        //        }

        //        // Eliminar las relaciones TaskTag asociadas a las Tags de la categoría a eliminar
        //        var tagIds = category.Tag.Select(t => t.Id).ToList();
        //        var taskTags = context.TaskTags.Where(tt => tagIds.Contains(tt.Tag.Id)).ToList();
        //        context.TaskTags.RemoveRange(taskTags);

        //        // Eliminar las Tags asociadas a la categoría
        //        context.Tags.RemoveRange(category.Tag);

        //        // Eliminar la categoría
        //        context.Categories.Remove(category);
        //        context.SaveChanges();

        //        MessageBox.Show("Categoría eliminada junto a sus etiquetas fueron eliminadas correctamente.");
        //        LoadCategories();
        //    }
        //}

        //private void dgvCategories_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (dgvCategories.SelectedRows.Count > 0)
        //    {
        //        int categoryId = (int) dgvCategories.SelectedRows[0].Cells["Id"].Value;
        //        using (var context = new OrganiTaskDB())
        //        {
        //            var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);

        //            if (category != null)
        //            {
        //                txtCategoryTitle.Text = category.Title;
        //            }
        //        }
        //    }
        //}
    }
}
