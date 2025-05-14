using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util;
using OrganiTask.Util.Collections;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OrganiTask.Controllers
{
    /// <summary>
    /// Controlador de categorías.
    /// Se utiliza para gestiones relacionadas con las categorías.
    /// </summary>
    public class CategoryController
    {
        /// <summary>
        /// Inserta una nueva categoría.
        /// </summary>
        /// <param name="newCategory">Modelo de vista de la nueva categoría.</param>
        public CategoryViewModel CreateCategory(CategoryViewModel newCategory)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Creamos la entidad a partir del modelo de vista de la categoría
                Category category = new Category
                {
                    Title = newCategory.Title,
                    DashboardId = newCategory.DashboardId
                };

                context.Categories.Add(category); // Agregamos la categoría al contexto
                context.SaveChanges(); // Guardamos los cambios

                newCategory.Id = category.Id; // Asignamos el identificador de la categoría al modelo de vista

                return newCategory; // Retornamos el modelo de vista de la categoría
            }
        }

        /// <summary>
        /// Actualiza los datos de una categoría existente.
        /// </summary>
        /// <param name="category">Modelo de vista de la categoría a actualizar.</param>
        public void UpdateCategory(CategoryViewModel category)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                Category cat = context.Categories.FirstOrDefault(c => c.Id == category.Id);

                if (cat == null) return;

                cat.Title = category.Title;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Elimina una categoría y todas las etiquetas asociadas a ella.
        /// </summary>
        /// <param name="categoryId">Identificador de la categoría a eliminar.</param>
        public void DeleteCategory(int categoryId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Primero obtenemos todas las etiquetas asociadas a la categoría
                OrganiList<int> tagIds = context.Tags
                    .Where(t => t.CategoryId == categoryId)
                    .Select(t => t.Id)
                    .ToOrganiList();

                // Luego, eliminamos las relaciones entre las etiquetas y las tareas
                foreach (int tagId in tagIds)
                {
                    new TagController().DeleteTag(tagId); // Eliminar cada etiqueta asociada a la categoría
                }

                // Ahora eliminamos la categoría en sí
                Category category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (category != null)
                {
                    context.Categories.Remove(category); // Eliminar la categoría
                    context.SaveChanges(); // Guardar los cambios
                }
            }
        }

        /// <summary>
        /// Verifica si una categoría específica existe en el tablero
        /// </summary>
        /// <param name="dashboardId">ID del tablero</param>
        /// <param name="categoryId">ID de la categoría a verificar</param>
        /// <returns>True si la categoría existe, False en caso contrario</returns>
        public bool CategoryExists(int dashboardId, int categoryId)
        {
            try
            {
                // Obtener todas las categorías del tablero
                OrganiList<CategoryViewModel> categories = new DashboardController().GetDashboardCategories(dashboardId);

                // Verificar si la categoría con el ID específico existe
                return categories != null && categories.Any(c => c.Id == categoryId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar si existe la categoría: {ex.Message}",
                    "Hubo un error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }

}