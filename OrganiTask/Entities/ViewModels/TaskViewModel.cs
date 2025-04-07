using System;

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
        public DateTime StartDate { get; set; } // Fecha de inicio de la tarea
        public DateTime EndDate { get; set; } // Fecha de vencimiento de la tarea
        public int DashboardId { get; set; } // Identificador del tablero al que pertenece la tarea
    }
}
