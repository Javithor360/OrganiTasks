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
    public partial class CategoriesManagement: Form
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
                    .Where(c => c.Board.Any(b => b.Id == 5))
                    .Select(c => new { c.Id, c.Title })
                    .ToList();

                dgvCategories.DataSource = categories;
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            using (var context = new OrganiTaskDB())
            {
                if (string.IsNullOrEmpty(txtCategoryTitle.Text))
                {
                    MessageBox.Show("Ingrese un nombre para la categoría");
                    return;
                }

                // Por el momento todas las categorías creadas serán asignadas al tablero con Id = 5
                var board = context.Boards.FirstOrDefault(b => b.Id == 5);
                //var board = context.Boards.FirstOrDefault();
                if (board == null)
                {
                    MessageBox.Show("No se encontró el tablero");
                    return;
                }

                var category = new Category
                {
                    Title = txtCategoryTitle.Text,
                    Board = new List<Board> { board }
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

            int categoryId = (int) dgvCategories.SelectedRows[0].Cells["Id"].Value;

            using (var context = new OrganiTaskDB())
            {
                var category = context.Categories
                    .Include("Board")
                    .FirstOrDefault(c => c.Id == categoryId);

                if (category == null)
                {
                    MessageBox.Show("La categoría seleccionada no existe.");
                    return;
                }

                category.Board.Clear(); // Eliminar las referencias en tabla BoardCategory

                context.Categories.Remove(category);
                context.SaveChanges();
                MessageBox.Show("Categoría eliminada correctamente.");
                LoadCategories();
            }
        }
    }
}
