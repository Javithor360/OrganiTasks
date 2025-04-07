using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Entities.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; } // Identificador de la etiqueta
        public string Name { get; set; } // Nombre de la etiqueta
        public string Description { get; set; } // Descripción de la etiqueta
        public string Color { get; set; } // Color de la etiqueta
        public int CategoryId { get; set; } // Identificador de la categoría

        public override string ToString()
        {
            return Name;
        }
    }
}
