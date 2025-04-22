using OrganiTask.Util.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Entities.ViewModels
{
    /// <summary>
    /// Clase que representa el modelo de vista de un tablero Kanban.
    /// </summary>
    public class DashboardViewModel
    {
        public int Id { get; set; }                         // ID del tablero
        public string DashboardTitle { get; set; }          // Título del tablero
        public string Description { get; set; }             // Descripción de los tablero
        public int UserId { get; set; }                     // ID del usuario
        public OrganiList<ColumnViewModel> Columns { get; set; } = new OrganiList<ColumnViewModel>(); // Lista de columnas del tablero
    }
}
