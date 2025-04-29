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
        public CategoryViewModel InsertCategory(CategoryViewModel newCategory)
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
        /// Agrega una nueva etiqueta a una categoría.
        /// </summary>
        /// <param name="categoryId">ID de la categoría.</param>
        /// <param name="tagName">Modelo de vista de la nueva Tag</param>
        /// <returns>ID de la etiqueta creada.</returns>
        public int AddTagToCategory(int categoryId, TagViewModel newTag)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Verificar que la categoría exista
                Category category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (category == null) return 0;

                // Crear la nueva etiqueta
                Tag Tag = new Tag
                {
                    Name = newTag.Name,
                    Color = "Gray",
                    CategoryId = categoryId
                };

                context.Tags.Add(Tag);
                context.SaveChanges();

                return Tag.Id;
            }
        }

    }

}