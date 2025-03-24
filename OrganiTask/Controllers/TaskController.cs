﻿using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util;
using OrganiTask.Util.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                        Description = task.Description,
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
    }
}
