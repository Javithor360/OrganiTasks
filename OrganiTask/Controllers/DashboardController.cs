using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Forms;
using OrganiTask.Util;
using OrganiTask.Util.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                if (dashboard == null) return null;

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
                        TagName = tag.Name,
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
                            Description = task.Description
                        });
                    }

                    viewModel.Columns.AddLast(column); // Agregar la columna al modelo de vista
                }
            }

            return viewModel;
        }
    }
}
 