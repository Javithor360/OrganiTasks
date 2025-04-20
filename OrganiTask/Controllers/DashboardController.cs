using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Forms;
using OrganiTask.Util;
using OrganiTask.Util.Collections;
using System;
using System.Linq;
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
                            StartDate = (DateTime) task.StartDate,
                            EndDate = (DateTime) task.EndDate
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
            using(OrganiTaskDB context = new OrganiTaskDB())
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
            using(OrganiTaskDB context = new OrganiTaskDB())
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
            using(OrganiTaskDB context = new OrganiTaskDB())
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
        /// Obtiene los nombres de los títulos para cada columna del tablero.
        /// </summary>
        /// <param name="dashboardId">Identificador del tablero.</param>
        /// <returns>Lista de títulos de columnas.</returns>
        public OrganiList<string> GetColumnTitles(int dashboardId)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                return context.Categories
                    .Where(c => c.DashboardId == dashboardId) // Filtrar por el ID del tablero
                    .OrderBy(c => c.Title) // Ordenar por el título de la categoría 
                    .Select(c => c.Title) // Seleccionar solo el título 
                    .ToOrganiList(); // Convertir a lista doblemente enlazada
            }
        }
    }


}
 