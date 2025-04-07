using OrganiTask.Util.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Entities.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; } // Identificador de la categoría
        public string Title { get; set; } // Título de la categoría
        public TagViewModel AssignedTag { get; set; } // Etiqueta asignada a la tarea en esta categoría
        public OrganiList<TagViewModel> TagList { get; set; } = new OrganiList<TagViewModel>(); // Lista de etiquetas de la categoría
    }
}
