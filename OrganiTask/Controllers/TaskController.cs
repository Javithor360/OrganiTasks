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
    /// Controlador de tareas.
    /// Se utiliza para gestiones relacionadas con las tareas.
    /// </summary>
    public class TaskController
    {
        /// <summary>
        /// Carga los datos de la tarea y sus relaciones, transformándolos en un TaskViewModel.
        /// </summary>
        /// <param name="taskId">Identificador de la tarea.</param>
        /// <param name="dashboardId">Identificador del tablero al que pertenece la tarea.</param>
        /// <returns>Modelo de vista de la tarea.</returns>
        public TaskViewModel LoadTaskDetails(int taskId, int dashboardId)
        {
            TaskViewModel taskVM = null; // Inicializar el modelo de vista de la tarea

            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Obtenemos la tarea que coincida con el identificador y el tablero
                Task task = context.Tasks.FirstOrDefault(t => t.Id == taskId && t.DashboardId == dashboardId);

                if (task != null) // Si se encuentra la tarea
                {
                    // Asignamos los valores de la tarea al modelo de vista
                    taskVM = new TaskViewModel
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Description = string.IsNullOrEmpty(task.Description)
                        ? "Sin descripción... Agrega una para más detalles"
                        : task.Description,
                        StartDate = (DateTime)task.StartDate,
                        EndDate = (DateTime)task.EndDate
                    };
                }
            }
            return taskVM; // Retornar el modelo de vista de la tarea o null si no se encuentra
        }

        /// <summary>
        /// Carga las relaciones de etiquetas para la tarea: para cada categoría del tablero,
        /// obtiene la etiqueta asignada (si existe) y las devuelve como una lista de objetos TagViewModel.
        /// Este método no retorna los nombres de las categorías.
        /// </summary>
        /// <param name="taskId">Identificador de la tarea.</param>
        /// <param name="dashboardId">Identificador del tablero al que pertenece la tarea.</param>
        /// <returns>Lista de modelos de vista de etiquetas.</returns>
        public OrganiList<TagViewModel> LoadTaskTags(int taskId, int dashboardId)
        {
            OrganiList<TagViewModel> tagsVM = new OrganiList<TagViewModel>(); // Inicializar la lista de modelos de vista de etiquetas

            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Obtenenemos todas las categorías que pertenecen al tablero
                OrganiList<Category> categories = context.Categories
                    .Where(c => c.DashboardId == dashboardId)
                    .OrderBy(c => c.Title)
                    .ToOrganiList();

                foreach (Category category in categories)
                {
                    Tag tag = (
                     from ttag in context.TaskTags
                     join tg in context.Tags on ttag.TagId equals tg.Id
                     where ttag.TaskId == taskId && tg.CategoryId == category.Id
                     select tg
                    ).FirstOrDefault();

                    TagViewModel tagVM = new TagViewModel
                    {
                        Id = tag?.Id ?? 0,
                        Name = tag?.Name ?? "",
                        Color = tag?.Color ?? "",
                        CategoryId = category.Id
                    };

                    tagsVM.Add(tagVM); // Agregar el modelo de vista de la etiqueta a la lista
                }
            }

            return tagsVM; // Retornar la lista de modelos de vista de etiquetas
        }

        /// <summary>
        /// Carga las categorías de la tarea como CategoryViewModel.
        /// Cada CategoryViewModel incluye el título de la categoría, la etiqueta asignada (si existe)
        /// y las etiquetas disponibles para esa categoría.
        /// </summary>
        /// <param name="taskId">Identificador de la tarea.</param>
        /// <param name="dashboardId">Identificador del tablero al que pertenece la tarea.</param>
        /// <returns>Lista de modelos de vista de categorías.</returns>
        public OrganiList<CategoryViewModel> LoadTaskCategories(int taskId, int dashboardId)
        {
            // Inicializar la lista de modelos de vista de categorías
            OrganiList<CategoryViewModel> categoryVM = new OrganiList<CategoryViewModel>();

            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Obtenemos todas las categorías que pertenecen al tablero ordenadas
                OrganiList<Category> categories = context.Categories
                    .Where(c => c.DashboardId == dashboardId)
                    .OrderBy(c => c.Title)
                    .ToOrganiList();

                // Para cada categoría, obtenemos la etiqueta asignada a la tarea (si existe)
                foreach (Category category in categories)
                {
                    Tag tag = (
                        from ttag in context.TaskTags
                        join tg in context.Tags on ttag.TagId equals tg.Id
                        where ttag.TaskId == taskId && tg.CategoryId == category.Id
                        select tg
                    ).FirstOrDefault();

                    // Solo creamos al CategoryViewModel si la tarea tiene una etiqueta asignada
                    //if (tag != null && !string.IsNullOrEmpty(tag.Name))
                    //{
                    // Crear el modelo de vista de la categoría
                    CategoryViewModel categoryViewModel = new CategoryViewModel
                    {
                        Id = category.Id,
                        Title = category.Title,
                        AssignedTag = tag != null ?
                        new TagViewModel
                        {
                            Id = tag.Id,
                            Name = tag.Name,
                            Color = tag.Color,
                            CategoryId = category.Id,
                        } : new TagViewModel(),
                    };

                    // Obtener las etiquetas disponibles para la categoría
                    OrganiList<Tag> availableTags = context.Tags
                        .Where(t => t.CategoryId == category.Id)
                        .OrderBy(t => t.Name)
                        .ToOrganiList();

                    // Para cada etiqueta disponible, crear un TagViewModel y agregarlo a la lista de etiquetas
                    foreach (Tag t in availableTags)
                    {
                        categoryViewModel.TagList.AddLast(new TagViewModel
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Color = t.Color,
                            CategoryId = t.CategoryId
                        });
                    }
                    categoryVM.Add(categoryViewModel);
                    //}
                }
            }
            return categoryVM;
        }

        /// <summary>
        /// Inserta una nueva tarea y sus relaciones de etiquetas.
        /// </summary>
        /// <param name="newTask">Modelo de vista de la nueva tarea.</param>
        /// <param name="tags">Lista de modelos de vista de etiquetas.</param>
        public void InsertTask(TaskViewModel newTask, OrganiList<TagViewModel> tags)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Creamos la entidad a partir del modelo de vista de la tarea
                Task task = new Task
                {
                    Title = newTask.Title,
                    Description = newTask.Description,
                    StartDate = newTask.StartDate,
                    EndDate = newTask.EndDate,
                    DashboardId = newTask.DashboardId
                };

                context.Tasks.Add(task); // Agregamos la tarea al contexto
                context.SaveChanges(); // Guardamos los cambios

                newTask.Id = task.Id; // Asignamos el identificador de la tarea al modelo de vista

                // Para cada TagViewModel, solo si se ha seleccionado una etiqueta, creamos una nueva TaskTag
                foreach (TagViewModel tagVM in tags)
                {
                    if (tagVM.Id > 0)
                    {
                        TaskTag newTT = new TaskTag
                        {
                            TaskId = newTask.Id,
                            TagId = tagVM.Id
                        };
                        context.TaskTags.Add(newTT); // Agregamos la nueva TaskTag
                    }
                }
                context.SaveChanges(); // Guardamos los cambios
            }
        }

        /// <summary>
        /// Actualiza la tarea y las relaciones de etiquetas.
        /// Se encarga de actualizar el título, descripción, fechas y rehacer las relaciones de TaskTag.
        /// </summary>
        /// <param name="updatedTask">Modelo de vista de la tarea actualizada.</param>
        /// <param name="updatedTags">Lista de modelos de vista de etiquetas actualizadas.</param>
        public void UpdateTask(TaskViewModel updatedTask, OrganiList<TagViewModel> updatedTags)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                Task newTask = context.Tasks.FirstOrDefault(t => t.Id == updatedTask.Id); // Obtener la tarea a actualizar
                if (newTask == null) return; // Si no se encuentra la tarea, no se actualiza

                // Si no, actualizar los valores de la tarea
                newTask.Title = updatedTask.Title;
                newTask.Description = updatedTask.Description;
                newTask.StartDate = updatedTask.StartDate;
                newTask.EndDate = updatedTask.EndDate;
                context.SaveChanges();

                // Eliminamos todas las relaciones TaskTag para la tarea
                OrganiList<TaskTag> taskTagsToDelete = context.TaskTags
                    .Where(tt => tt.TaskId == updatedTask.Id)
                    .ToOrganiList();

                foreach (TaskTag tt in taskTagsToDelete)
                {
                    context.TaskTags.Remove(tt);
                }
                context.SaveChanges();

                // Para cada TagViewModel actualizado, solo si se ha 
                // seleccionado una etiqueta, se crea una nueva TaskTag
                foreach (TagViewModel tagVM in updatedTags)
                {
                    if (tagVM.Id > 0) // Si el identificador es mayor a cero
                    {
                        TaskTag newTaskTag = new TaskTag
                        {
                            TaskId = updatedTask.Id,
                            TagId = tagVM.Id
                        };
                        context.TaskTags.Add(newTaskTag); // Agregar la nueva TaskTag
                    }
                }
                context.SaveChanges(); // Guardar los cambios
            }
        }

        /// <summary>
        /// Elimina la tarea.
        /// </summary>
        /// <param name="taskId">Id de la tarea a eliminar</param>
        public bool DeleteTask(int taskId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                Task task = context.Tasks.FirstOrDefault(t => t.Id == taskId);

                // Si la tarea no existe
                if (task == null) return false;

                context.Tasks.Remove(task);
                context.SaveChanges(); // Guardar los cambios en la base de datos
            }

            return true;
        }

        /// <summary>
        /// Obtiene el ID de la categoría asociada a una etiqueta por medio del ID de la etiqueta.
        /// </summary>
        /// <param name="tagId">ID de la etiqueta.</param>
        /// <returns>ID de la categoría asociada a la etiqueta.</returns>
        public int GetCategoryIdFromTagId(int tagId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Obtener la categoría asociada a la etiqueta
                Tag tag = context.Tags.FirstOrDefault(t => t.Id == tagId);
                if (tag != null)
                {
                    return tag.CategoryId; // Retornar el identificador de la categoría
                }
                return 0; // Retornar cero si no se encuentra la etiqueta
            }
        }

        /// <summary>
        /// Actualiza la relación entre una tarea y una etiqueta en una categoría específica por medio de sus ID.
        /// </summary>
        /// <param name="taskId">ID de la tarea.</param>
        /// <param name="newTagId">ID de la nueva etiqueta.</param>
        /// <param name="categoryId">ID de la categoría.</param>
        public void UpdateTagCategoryForTask(int taskId, int newTagId, int categoryId)
        {
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Primeramente eliminamos cualquier relación existente para la tarea en la categoría
                OrganiList<TaskTag> existing = context.TaskTags
                    .Where(tt => tt.TaskId == taskId && tt.Tag.CategoryId == categoryId)
                    .ToOrganiList();

                context.TaskTags.RemoveRange(existing);

                // Ahora, si el ID de la nueva etiqueta es válido, creamos la relación
                if (newTagId > 0)
                {
                    TaskTag newTT = new TaskTag
                    {
                        TaskId = taskId,
                        TagId = newTagId
                    };
                    context.TaskTags.Add(newTT); // Agregar la nueva relación
                }

                context.SaveChanges(); // Guardar los cambios
            }
        }
    }
}
