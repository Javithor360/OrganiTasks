using OrganiTask.Entities.ViewModels;
using OrganiTask.Entities;
using OrganiTask.Util.Collections;
using OrganiTask.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Controllers
{
    /// <summary>
    /// Controlador de etiquetas.
    /// </summary>
    public class TagController
    {
        /// <summary>
        /// Crea una nueva etiqueta y la asocia a una categoría.
        /// </summary>
        /// <param name="tagVm">Modelo de vista de la etiqueta a crear.</param>
        public void CreateTag(TagViewModel tagVm)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Creamos el objeto Tag y lo añadimos al contexto
                Tag tag = new Tag
                {
                    Name = tagVm.Name,
                    Color = tagVm.Color,
                    CategoryId = tagVm.CategoryId
                };
                context.Tags.Add(tag);
                context.SaveChanges(); // Guardamos los cambios en la base de datos
                tagVm.Id = tag.Id; // Asignamos el ID generado a la vista modelo
            }
        }

        /// <summary>
        /// Actualiza los datos de una etiqueta existente.
        /// </summary>
        /// <param name="updateTag">Modelo de vista de la etiqueta a actualizar.</param>
        public void UpdateTag(TagViewModel updateTag)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscamos la etiqueta por su ID
                Tag tag = context.Tags.FirstOrDefault(t => t.Id == updateTag.Id);
                if (tag == null) return; // Si no se encuentra la etiqueta, no hacemos nada

                // Actualizamos los datos de la etiqueta
                tag.Name = updateTag.Name;
                tag.Color = updateTag.Color;
                context.SaveChanges(); // Guardar los cambios
            }
        }

        /// <summary>
        /// Elimina una etiqueta y sus relaciones con las tareas.
        /// </summary>
        /// <param name="tagId">Identificador de la etiqueta a eliminar.</param>
        public void DeleteTag(int tagId)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscamos las relaciones entre la etiqueta y las tareas
                OrganiList<TaskTag> links = context.TaskTags.Where(tt => tt.TagId == tagId).ToOrganiList();
                if (links.Any())
                    context.TaskTags.RemoveRange(links); // Eliminar los enlaces de la etiqueta a las tareas

                // Una vez eliminada las relaciones, eliminamos la etiqueta
                Tag tag = context.Tags.FirstOrDefault(t => t.Id == tagId);
                if (tag != null)
                    context.Tags.Remove(tag); // Eliminar la etiqueta

                context.SaveChanges(); // Guardar los cambios
            }
        }

        /// <summary>
        /// Obtiene las etiquetas asociadas a una categoría específica.
        /// </summary>
        /// <param name="categoryId">Identificador de la categoría.</param>
        /// <returns>Lista de etiquetas asociadas a la categoría.</returns>
        public OrganiList<TagViewModel> GetTagsForCategory(int categoryId)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                return context.Tags
                    .Where(t => t.CategoryId == categoryId)
                    .Select(t => new TagViewModel
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Color = t.Color,
                        CategoryId = t.CategoryId
                    })
                    .ToOrganiList();
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
