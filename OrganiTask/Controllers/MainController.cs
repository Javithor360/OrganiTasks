using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util;
using OrganiTask.Util.Collections;
using System.Linq;

namespace OrganiTask.Controllers
{
    /// <summary>
    /// Controlador para la pantalla principal, maneja la carga de dashboards del usuario.
    /// </summary>
    public class MainController
    {
        /// <summary>
        /// Carga todos los dashboards que pertenecen al usuario actual.
        /// </summary>
        /// <param name="userId">ID del Usuario</param>
        /// <returns>Modelo de vista con la información de los dashboards del usuario.</returns>
        public MainViewModel LoadUserDashboards(int userId)
        {
            // Instanciamos el modelo de vista
            MainViewModel viewModel = new MainViewModel();

            // Obtenemos la sessión actual
            SessionManager session = SessionManager.Instance;

            // Si no hay un usuario logeado, retornamos un modelo vacío
            if(!session.IsLoggedIn)
                return viewModel;

            // Asignamos la información del usuario al modelo
            viewModel.UserId = userId;
            viewModel.UserName = session.CurrentUser.Username;
            viewModel.Email = session.CurrentUser.Email;

            // Obtenemos los dashboards
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Obtenemos todos los dashboards del usuario actual
                OrganiList<Dashboard> userDashboards = context.Dashboards
                    .Where(d => d.UserId == userId)
                    .ToOrganiList();

                foreach (Dashboard dashboard in userDashboards)
                {
                    viewModel.DashboardPreviews.AddLast(new DashboardViewModel
                    {
                        Id = dashboard.Id,
                        DashboardTitle = dashboard.Name
                    });
                }
            }

            return viewModel;
        }
    }
}
 