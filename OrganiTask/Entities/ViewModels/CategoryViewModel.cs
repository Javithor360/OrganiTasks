using OrganiTask.Util.Collections;

namespace OrganiTask.Entities.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; } // Identificador de la categoría
        public string Title { get; set; } // Título de la categoría
        public int DashboardId { get; set; } // Identificador del tablero al que pertenece la categoría
        public TagViewModel AssignedTag { get; set; } // Etiqueta asignada a la tarea en esta categoría
        public OrganiList<TagViewModel> TagList { get; set; } = new OrganiList<TagViewModel>(); // Lista de etiquetas de la categoría
    }
}
