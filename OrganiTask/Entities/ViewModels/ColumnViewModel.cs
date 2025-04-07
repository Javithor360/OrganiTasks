using OrganiTask.Util.Collections;

namespace OrganiTask.Entities.ViewModels
{
    /// <summary>
    /// Clase que representa el modelo de vista de una columna de tareas.
    /// </summary>
    public class ColumnViewModel
    {
        public Tag Tag { get; set; } // Identificador de la etiqueta
        public OrganiList<TaskViewModel> Tasks { get; set; } = new OrganiList<TaskViewModel>(); // Lista de tareas de la columna
    }
}
