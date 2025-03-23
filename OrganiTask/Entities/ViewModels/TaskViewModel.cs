using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Entities.ViewModels
{
    /// <summary>
    /// Clase que representa el modelo de vista de una tarea.
    /// </summary>
    public class TaskViewModel
    {
        public int Id { get; set; } // Identificador de la tarea
        public string Title { get; set; } // Título de la tarea
        public string Description { get; set; } // Descripción de la tarea
    }
}
