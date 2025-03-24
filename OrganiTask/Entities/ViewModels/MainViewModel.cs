using OrganiTask.Util.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Entities.ViewModels
{
    // <sumary>
    // Clase que representa el modelo de vista de a pantalla principal
    // con múltiples dashboards
    // </sumary>
    public class MainViewModel
    {
        public int UserId { get; set; }                     // ID del usuario
        public string UserName { get; set; }                // Nombre del usuario
        public string Email {  get; set; }                  // Email del usuario
        public OrganiList<DashboardViewModel> DashboardPreviews { get; set; } = new OrganiList<DashboardViewModel>();   // Lista de dashboards
    }

}
