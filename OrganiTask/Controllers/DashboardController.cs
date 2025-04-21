using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Forms;
using OrganiTask.Util;
using OrganiTask.Util.Collections;
using System;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Task = OrganiTask.Entities.Task;

namespace OrganiTask.Controllers
{
    public class DashboardController
    {
        /// <summary>
        /// Carga un tablero Kanban para un tablero y una categoría específica.
        /// Por defecto carga la categoría "Status".
        /// </summary>
        /// <param name="dashboardId">Identificador del tablero.</param>
        /// <param name="categoryTitle">Título de la categoría.</param>
        /// <returns>Modelo de vista del tablero Kanban.</returns>
        public DashboardViewModel LoadKanban(int dashboardId, string categoryTitle = "Status")
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
                    .FirstOrDefault(c => c.Title == categoryTitle && c.DashboardId == dashboardId);

                if (category == null)
                    return viewModel; // Si no se encuentra la categoría, se retorna el modelo de vista sin columnas

                // Para cada etiqueta en la categoría se crea una columna
                foreach (Tag tag in category.Tag)
                {
                    ColumnViewModel column = new ColumnViewModel
                    {
                        Tag = tag,
                        Tasks = new OrganiList<TaskViewModel>() // Inicializar la lista de tareas
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
                            Description = task.Description,
                            StartDate = (DateTime)task.StartDate,
                            EndDate = (DateTime)task.EndDate
                        });
                    }

                    viewModel.Columns.AddLast(column); // Agregar la columna al modelo de vista
                }

                // Ahora, creamos una columna adicional para aquellas tareas que no tienen
                // etiqueta asignada en esta categoría particular
                ColumnViewModel noTagColumn = new ColumnViewModel
                {
                    Tag = new Tag {
                        Id = -1, // ID ficticio para la columna sin etiqueta
                        Name = "Sin Etiquetar", // Título de la columna
                        CategoryId = category.Id, // Asignar la categoría
                    },
                    Tasks = new OrganiList<TaskViewModel>() // Inicializar la lista de tareas
                };

                // Realizamos la consulta para obtener dichas tareas que NO poseen ningún
                // TaskTag asociado a la categoría actual
                OrganiList<Task> taskWithoutTag = context.Tasks
                    .Where(t => t.DashboardId == dashboardId && !t.TaskTag.Any(tt => tt.Tag.CategoryId == category.Id))
                    .ToOrganiList();

                // Iteramos cada task a el modelo de vista de task
                foreach (Task task in taskWithoutTag)
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

                viewModel.Columns.AddLast(noTagColumn); // Agregar la columna de tareas sin etiqueta al modelo de vista
            }

            return viewModel;
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
        public void CreateDashboard(DashboardViewModel newDashboard)
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
            }
        }


        /// <summary>
        /// Modificar la información un tablero existente.
        /// </summary>
        /// <param name="updatedDashboard">Modelo de vista del tablero actualizado.</param>
        public void UpdateDashboard(DashboardViewModel updatedDashboard)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscar el tablero por su ID
                var dashboard = context.Dashboards.FirstOrDefault(d => d.Id == updatedDashboard.Id);
                if (dashboard == null) return;

                // Actualizar los datos del tablero
                dashboard.Name = updatedDashboard.DashboardTitle;
                dashboard.Description = updatedDashboard.Description;

                // Guardar los cambios
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Devuelve el título de la primera categoría de un tablero,
        /// o "Status" si no hay ninguna.
        /// </summary>
        /// <param name="dashboardId">Identificador del tablero.</param>
        /// <returns>Título de la primera categoría o "Status".</returns>   
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
                    Title = "Status" // Título por defecto
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
    }
}
 