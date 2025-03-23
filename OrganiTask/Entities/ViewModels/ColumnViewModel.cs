using OrganiTask.Util.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Entities.ViewModels
{
    /// <summary>
    /// Clase que representa el modelo de vista de una columna de tareas.
    /// </summary>
    public class ColumnViewModel
    {
        public string TagName { get; set; } // Nombre de la etiqueta
        public OrganiList<TaskViewModel> Tasks { get; set; } = new OrganiList<TaskViewModel>(); // Lista de tareas de la columna
    }
}
