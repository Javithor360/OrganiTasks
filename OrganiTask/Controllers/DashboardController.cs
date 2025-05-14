using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util;
using OrganiTask.Util.Collections;
using System;
using System.Linq;
using System.Windows.Forms;
using Task = OrganiTask.Entities.Task;

namespace OrganiTask.Controllers
{
    public class DashboardController
    {
        /// <summary>
        /// Carga un tablero Kanban para un tablero y una categoría específica.
        /// </summary>
        /// <param name="dashboardId">Identificador del tablero.</param>
        /// <param name="categoryId">Identificador de la categoría.</param>
        /// <returns>Modelo de vista del tablero Kanban.</returns>
        public DashboardViewModel LoadKanban(int dashboardId, int categoryId)
        {
            // Instanciamos el modelo de vista
            DashboardViewModel viewModel = new DashboardViewModel();

            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Obtenemos el tablero
                Dashboard dashboard = context.Dashboards.FirstOrDefault(d => d.Id == dashboardId);

                if (dashboard == null)
                    return null;

                viewModel.DashboardTitle = dashboard.Name; // Asignamos el título del tablero

                // Obtenemos la categoría para ordenar el tablero
                // Las entidades generadas tienen propiedades de navegación que permiten acceder a las entidades relacionadas
                Category category = context.Categories
                    .Include("Tag") // Cargar las etiquetas de la categoría
                    .FirstOrDefault(c => c.Id == categoryId && c.DashboardId == dashboardId);

                // Si no se encuentra la categoría, se retorna el modelo de vista con la "Columna Sin Etiquetar"
                if (category == null)
                {
                    // Obtener todas las tareas del dashboard
                    OrganiList<Task> allTasks = context.Tasks
                        .Where(t => t.DashboardId == dashboardId)
                        .ToOrganiList();

                    // Crear una columna "Sin Etiquetar" aunque no haya categoría
                    viewModel.Columns.AddLast(createNoTagColum(allTasks)); // Agregar la columna sin etiquetas

                    return viewModel;
                }

                // Para cada etiqueta en la categoría se crea una columna
                foreach (Tag tag in category.Tag)
                {
                    ColumnViewModel column = new ColumnViewModel
                    {
                        Tag = tag,
                        Tasks = new OrganiList<TaskViewModel>(), // Inicializar la lista de tareas
                        ColorColumn = tag.Color
                    };

                    // Obtenemos las tareas que pertenecen al dashboard
                    // y tienen asignada la etiqueta iterada (usando TaskTag)
                    OrganiList<Task> tasksWithTag = context.Tasks
                        .Where(t => t.DashboardId == dashboardId && t.TaskTag.Any(tt => tt.TagId == tag.Id))
                        .ToOrganiList(); // Convertir a lista doblemente enlazada

                    // Iteramos cada task a el modelo de vista de task
                    foreach (Task task in tasksWithTag)
                    {
                        column.Tasks.AddLast(new TaskViewModel
                        {
                            Id = task.Id,
                            Title = task.Title,
                            DashboardId = dashboard.Id,
                            Description = task.Description,
                            StartDate = (DateTime)task.StartDate,
                            EndDate = (DateTime)task.EndDate
                        });
                    }

                    viewModel.Columns.AddLast(column); // Agregar la columna al modelo de vista
                }

                // Realizamos la consulta para obtener dichas tareas que NO poseen ningún
                // TaskTag asociado a la categoría actual
                OrganiList<Task> taskWithoutTag = context.Tasks
                    .Where(t => t.DashboardId == dashboardId && !t.TaskTag.Any(tt => tt.Tag.CategoryId == category.Id))
                    .ToOrganiList();

                // Ahora, creamos una columna adicional para aquellas tareas que no tienen
                // etiqueta asignada en esta categoría particular
                viewModel.Columns.AddLast(createNoTagColum(taskWithoutTag, category.Id)); // Agregar la columna de tareas sin etiqueta al modelo de vista
            }

            return viewModel;
        }

        private ColumnViewModel createNoTagColum(OrganiList<Task> tasks, int categoryId = -1)
        {
            ColumnViewModel noTagColumn = new ColumnViewModel
            {
                Tag = new Tag
                {
                    Id = -1, // ID ficticio para la columna sin etiqueta
                    Name = "Sin Etiquetar", // Título de la columna
                    CategoryId = categoryId, // Asignar la categoría,
                },
                Tasks = new OrganiList<TaskViewModel>(), // Inicializar la lista de tareas
                ColorColumn = "Gray"
            };

            // Iteramos cada task a el modelo de vista de task
            foreach (Task task in tasks)
            {
                noTagColumn.Tasks.AddLast(new TaskViewModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    StartDate = (DateTime)task.StartDate,
                    EndDate = (DateTime)task.EndDate
                });
            }

            return noTagColumn;
        }

        /// <summary>
        /// Carga los detalles de un tablero.
        /// </summary>
        /// <param name="dashboardId">Identificador del tablero.</param>
        /// <returns>Modelo de vista del tablero.</returns>
        public DashboardViewModel LoadDashboardDetails(int dashboardId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscar el tablero por su ID
                Dashboard dashboard = context.Dashboards.FirstOrDefault(d => d.Id == dashboardId);
                if (dashboard == null) return null;

                // Crear y retornar el modelo de vista
                return new DashboardViewModel
                {
                    Id = dashboard.Id,
                    DashboardTitle = dashboard.Name,
                    Description = dashboard.Description,
                    UserId = dashboard.UserId
                };
            }
        }

        /// <summary>
        /// Crea un nuevo tablero.
        /// </summary>
        /// <param name="newDashboard">Modelo de vista del nuevo tablero.</param>
        /// <returns>Modelo de vista del tablero Kanban.</returns>
        public DashboardViewModel CreateDashboard(DashboardViewModel newDashboard)
        {
            // Bloque using para liberación de contexto al finalizar bloque de codigo
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Crear la entidad a partir del modelo de la vista del tablero
                Dashboard dashboard = new Dashboard
                {
                    Name = newDashboard.DashboardTitle,
                    Description = newDashboard.Description,
                    UserId = newDashboard.UserId
                };

                context.Dashboards.Add(dashboard); // Agregar el tablero al contexto
                context.SaveChanges(); // Guardar los cambios

                newDashboard.Id = dashboard.Id; // Asignar el ID generado al modelo de la vista

                return newDashboard;
            }
        }

        /// <summary>
        /// Devuelve el título de la primera categoría de un tablero
        /// </summary>
        /// <param name="dashboardId">Identificador del tablero.</param>
        /// <returns>Título de la primera categoría</returns>   
        public CategoryViewModel GetDefaultCategory(int dashboardId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                CategoryViewModel firstCategory = context.Categories
                    .Where(c => c.DashboardId == dashboardId)
                    .OrderBy(c => c.Title)
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id, // Asignamos ID
                        Title = c.Title, // Asignamos título
                        // Los demás campos quedan nulos ya que no se necesitan
                    })
                    .FirstOrDefault();

                return firstCategory ?? new CategoryViewModel
                {
                    Id = -1, // ID ficticio para la categoría por defecto
                    Title = "" // Título por defecto
                };
            }
        }

        /// <summary>
        /// Obtiene las categorías asociadas a un tablero específico.
        /// </summary>
        /// <param name="dashboardId">Identificador del tablero.</param>
        /// <returns>Lista categorías relacionadas al tablero.</returns>
        public OrganiList<CategoryViewModel> GetDashboardCategories(int dashboardId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Obtener las categorías asociadas al tablero
                return context.Categories
                    .Where(c => c.DashboardId == dashboardId)
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id, // Asignamos ID
                        Title = c.Title, // Asignamos título
                        // Los demás campos quedan nulos ya que no se necesitan
                    })
                    .ToOrganiList();
            }
        }

        /// <summary>
        /// Obtiene el nombre de usuario del propietario del tablero a partir de su ID.
        /// </summary>
        /// <param name="ownerId">ID del propietario del tablero.</param>
        /// <returns>Nombre de usuario del propietario.</returns>
        public string GetUsernameFromDashboardOwnerId(int ownerId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                return context.Users
                    .Where(u => u.Id == ownerId)
                    .Select(u => u.Username)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Actualiza el nombre y la descripción de un tablero existente.
        /// </summary>
        /// <param name="dashboardId">Identificador del tablero.</param>
        /// <param name="newName">Nuevo nombre del tablero.</param>
        /// <param name="newDescription">Nueva descripción del tablero.</param>
        public void UpdateDashboard(int dashboardId, string newName, string newDescription)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscar el tablero por su ID
                Dashboard dashboard = context.Dashboards.FirstOrDefault(d => d.Id == dashboardId);

                // Si no se encuentra el tablero, mostramos un mensaje de error
                if (dashboard == null)
                {
                    MessageBox.Show("Tablero no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Actualizar el nombre y la descripción del tablero
                dashboard.Name = newName;
                dashboard.Description = newDescription;
                context.SaveChanges(); // Guardar los cambios en la base de datos
            }
        }

        /// <summary>
        /// Elimina un tablero existente y todos sus elementos relacionados.
        /// </summary>
        /// <param name="dashboardId">Identificador del tablero.</param>
        public void DeleteDashboard(int dashboardId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscar el tablero por su ID
                Dashboard dashboard = context.Dashboards.FirstOrDefault(d => d.Id == dashboardId);

                // Si no se encuentra el tablero, mostramos un mensaje de error
                if (dashboard == null)
                {
                    MessageBox.Show("Tablero no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Elimina los elementos relacionados

                // Eliminamos las tareas asociadas al tablero   
                OrganiList<Task> tasksDashboard = context.Tasks
                    .Where(t => t.DashboardId == dashboardId)
                    .ToOrganiList();

                foreach (Task task in tasksDashboard)
                {
                    // Eliminar relaciones TaskTag
                    OrganiList<TaskTag> taskTags = context.TaskTags
                        .Where(tt => tt.TaskId == task.Id)
                        .ToOrganiList();

                    context.TaskTags.RemoveRange(taskTags); // Eliminar las relaciones TaskTag
                    context.Tasks.Remove(task); // Eliminar la tarea
                }

                // Eliminar las categorias relacionadas
                OrganiList<Category> categories = context.Categories
                    .Where(c => c.DashboardId == dashboardId)
                    .ToOrganiList();

                foreach (Category category in categories)
                {
                    // Elimina las etiquetas asociadas a la categoría
                    OrganiList<Tag> tags = context.Tags
                        .Where(t => t.CategoryId == category.Id)
                        .ToOrganiList();

                    foreach (Tag tag in tags)
                    {
                        // Eliminar relaciones TaskTag
                        OrganiList<TaskTag> taskTags = context.TaskTags
                            .Where(tt => tt.TagId == tag.Id)
                            .ToOrganiList();
                        context.TaskTags.RemoveRange(taskTags); // Eliminar las relaciones TaskTag

                        context.Tags.Remove(tag); // Eliminar la etiqueta
                    }

                    // Eliminar la categoria
                    context.Categories.Remove(category);
                }

                // Por ultimo eliminar el tablero
                context.Dashboards.Remove(dashboard); // Eliminar el tablero

                // Guardar los cambios en la base de datos
                context.SaveChanges();

                //        // Luego, eliminamos las relaciones entre las etiquetas y las tareas
                //        foreach (int tagId in tagIds)
                //        {
                //            DeleteTag(tagId); // Eliminar cada etiqueta asociada a la categoría
                //        }

                //        // Ahora eliminamos la categoría en sí
                //        Category category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
                //        if (category != null)
                //        {
                //            context.Categories.Remove(category); // Eliminar la categoría
                //            context.SaveChanges(); // Guardar los cambios
                //        }
            }
        }

        /// <summary>
        /// Carga los datos de una categoría por medio de su ID
        /// </summary>
        /// <param name="categoryId">Identificador de la categoría.</param>
        /// <returns>Modelo de vista de la categoría.</returns>    
        public CategoryViewModel LoadCategoryById(int categoryId)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscamos la categoría por su ID
                CategoryViewModel cat = context.Categories
                    .Where(c => c.Id == categoryId)
                    .Select(c => new CategoryViewModel // Seleccionamos los campos necesarios
                    {
                        Id = c.Id,
                        Title = c.Title,
                    })
                    .FirstOrDefault();

                // Si no se encuentra la categoría, retornamos un modelo vacío
                return cat ?? new CategoryViewModel { Id = categoryId, Title = string.Empty };
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
        /// Crea una nueva categoría asociada a un tablero específico.
        /// </summary>
        /// <param name="category">Modelo de vista de la categoría a crear.</param>
        public void CreateCategory(CategoryViewModel category)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                Category cat = new Category
                {
                    Title = category.Title,
                    DashboardId = category.DashboardId
                };
                context.Categories.Add(cat);
                context.SaveChanges();
                category.Id = cat.Id;
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
                    DeleteTag(tagId); // Eliminar cada etiqueta asociada a la categoría
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
                OrganiList<CategoryViewModel> categories = GetDashboardCategories(dashboardId);

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
