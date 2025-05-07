using OrganiTask.Forms;
using OrganiTask.Util.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiTask.Util
{
    /// <summary>
    /// Clase para gestionar las instancias abiertas de dashboards
    /// </summary>
    public class DashboardManager
    {
        // Instancia Singleton del gestor de dashboards
        private static DashboardManager _instance;

        // OrganiList que almacena los dashboards abiertos
        private OrganiList<DashboardInstance> _openDashboards;

        /// <summary>
        /// Obtiene la instancia única del gestor de dashboards
        /// </summary>
        public static DashboardManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DashboardManager();
                return _instance;
            }
        }

        /// <summary>
        /// Constructor privado para el patrón Singleton
        /// </summary>
        private DashboardManager()
        {
            _openDashboards = new OrganiList<DashboardInstance>();
        }

        /// <summary>
        /// Abre un dashboard o activa uno existente si ya está abierto
        /// </summary>
        /// <param name="dashboardId">ID del dashboard a abrir</param>
        /// <returns>La instancia del dashboard abierto</returns>
        public KanbanDashboard OpenDashboard(int dashboardId)
        {
            // Buscar si el dashboard ya está abierto
            Node<DashboardInstance> existingNode = null;

            foreach (var node in _openDashboards)
            {
                if (node.DashboardId == dashboardId)
                {
                    existingNode = _openDashboards.Find(node);
                    break;
                }
            }

            // Si ya está abierto, activar el formulario
            if (existingNode != null)
            {
                KanbanDashboard existingDashboard = existingNode.Value.DashboardForm;

                // Verificar si la ventan sigue abierta o fue cerrada
                if (existingDashboard != null && !existingDashboard.IsDisposed) {
                    // Activar la ventana existente
                    if(existingDashboard.WindowState == FormWindowState.Minimized)
                        existingDashboard.WindowState = FormWindowState.Normal;
                    existingDashboard.Activate();
                    return existingDashboard;
                }
                else
                {
                    // La ventana fue cerrada, eliminar el registro
                    _openDashboards.Remove(existingNode);
                }
            }

            // Crear nueva instancia del dashboard
            KanbanDashboard newDashboard = new KanbanDashboard(dashboardId);

            // Registrar el evento FormClosed para eliminar el dashboard de la lista cuando se cierre
            newDashboard.FormClosed += (sender, e) => {
                RemoveDashboard(dashboardId);
            };

            // Agregar a la lista de dashboards abiertos
            _openDashboards.AddLast(new DashboardInstance(dashboardId, newDashboard));

            // Mostrar el dashboard
            newDashboard.Show();

            return newDashboard;
        }

        /// <summary>
        /// Elimina un dashboard de la lista de dashboards abiertos
        /// </summary>
        /// <param name="dashboardId">ID del dashboard a eliminar</param>
        public void RemoveDashboard(int dashboardId)
        {
            // Recorrer la lista y buscar el dashboard con el ID especificado
            foreach (var node in _openDashboards)
            {
                if (node.DashboardId == dashboardId)
                {
                    // Encontramos la instancia, ahora obtenemos el nodo y lo eliminamos
                    Node<DashboardInstance> nodeToRemove = _openDashboards.Find(node);
                    
                    if (nodeToRemove != null)
                        _openDashboards.Remove(nodeToRemove);

                    break;
                }
            }
        }

    }
  

    /// <summary>
    /// Clase que representa una instancia de dashboard abierta
    /// </summary>
    public class DashboardInstance
    {
        public int DashboardId { get; private set; } // ID del dashboard
        public KanbanDashboard DashboardForm { get; private set; } // Formulario del dashboard

        /// <summary>
        /// Constructor de la instancia de dashboard
        /// <param name="id">ID del dashboard</param>
        /// <para name="form">Formulario del dashboard</para>
        public DashboardInstance(int id, KanbanDashboard form)
        {
            DashboardId = id;
            DashboardForm = form;
        }

        /// <summary>
        /// Compara si dos instancias de dashboard son iguales basándose en su ID
        /// </summary>
        /// <param name="obj">Objeto a comparar</param>
        /// <returns>True si los ID's son iguales</returns>
        public override bool Equals(object obj)
        {
            if(obj is DashboardInstance other)
            {
                return this.DashboardId == other.DashboardId;
            }
            return false;
        }

        /// <summary>
        /// Obtiene el código hash de la instancia
        /// </summary>
        /// <returns>Código hash basado en el ID del dashboard</returns>
        public override int GetHashCode()
        {
            return DashboardId.GetHashCode();
        }
    }
}
