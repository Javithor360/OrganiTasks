using OrganiTask.Entities;
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
    public partial class CategoriesManagement : Form
    {
        public CategoriesManagement()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            using (var context = new OrganiTaskDB())
            {
                var categories = context.Categories
                    .Where(c => c.Dashboard.Id == 2)
                    .Select(c => new { c.Id, c.Title })
                    .ToList();

                dgvCategories.DataSource = categories;
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryTitle.Text))
            {
                MessageBox.Show("Ingrese un nombre para la categoría");
                return;
            }

            using (var context = new OrganiTaskDB())
            {

                // Por el momento todas las categorías creadas serán asignadas al tablero con Id = 2
                var dashboard = context.Dashboards.FirstOrDefault(b => b.Id == 2);
                //var board = context.Boards.FirstOrDefault();

                var category = new Category
                {
                    Title = txtCategoryTitle.Text,
                    Dashboard = dashboard
                };

                context.Categories.Add(category);
                context.SaveChanges();
                MessageBox.Show("Categoría añadida correctamente.");
                LoadCategories();
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoría para actualizar");
                return;
            }

            if (string.IsNullOrEmpty(txtCategoryTitle.Text))
            {
                MessageBox.Show("Ingrese un nombre para la categoría");
                return;
            }

            int categoryId = (int) dgvCategories.SelectedRows[0].Cells["Id"].Value;

            using (var context = new OrganiTaskDB())
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (category == null)
                {
                    MessageBox.Show("La categoría seleccionada no existe.");
                    return;
                }

                category.Title = txtCategoryTitle.Text;
                context.SaveChanges();
                MessageBox.Show("Categoría actualizada correctamente.");
                LoadCategories();
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoría para eliminar");
                return;
            }

            int categoryId = (int)dgvCategories.SelectedRows[0].Cells["Id"].Value;

            using (var context = new OrganiTaskDB())
            {
                var category = context.Categories
                    .Include("Tag") // Incluimos las etiquetas para eliminar las etiquetas asociadas a la categoría
                    .FirstOrDefault(c => c.Id == categoryId);

                if (category == null)
                {
                    MessageBox.Show("La categoría seleccionada no existe.");
                    return;
                }

                // Eliminar las relaciones TaskTag asociadas a las Tags de la categoría a eliminar
                var tagIds = category.Tag.Select(t => t.Id).ToList();
                var taskTags = context.TaskTags.Where(tt => tagIds.Contains(tt.Tag.Id)).ToList();
                context.TaskTags.RemoveRange(taskTags);

                // Eliminar las Tags asociadas a la categoría
                context.Tags.RemoveRange(category.Tag);

                // Eliminar la categoría
                context.Categories.Remove(category);
                context.SaveChanges();

                MessageBox.Show("Categoría eliminada junto a sus etiquetas fueron eliminadas correctamente.");
                LoadCategories();
            }
        }

        private void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count > 0)
            {
                int categoryId = (int) dgvCategories.SelectedRows[0].Cells["Id"].Value;
                using (var context = new OrganiTaskDB())
                {
                    var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);

                    if (category != null)
                    {
                        txtCategoryTitle.Text = category.Title;
                    }
                }
            }
        }
    }
}
