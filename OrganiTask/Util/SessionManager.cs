using OrganiTask.Entities;

namespace OrganiTask.Util
{
    /// <summary>
    /// Clase singleton para manejar la sesión del usuario.
    /// </summary>
    public class SessionManager
    {
        // "_" Se utiliza para denotar campos privados
        private static SessionManager _instance; // Campo Privado
        private User _currentUser; // Campo Privado

        /// <summary>
        /// Constructor vacío.
        /// </summary>
        public SessionManager() { }

        /// <summary>
        /// Propiedad estática para obtener la instancia de la clase.
        /// </summary>
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionManager();

                return _instance;
            }
        }

        /// <summary>
        /// Propiedad para obtener el usuario actual.
        /// </summary>
        public User CurrentUser
        {
            get { return _currentUser; }
            private set { _currentUser = value; }
        }

        /// <summary>
        /// Propiedad para verificar si el usuario está logueado.
        /// </summary>
        public bool IsLoggedIn => CurrentUser != null;

        /// <summary>
        /// Método para loguear al usuario.
        /// </summary>
        /// <param name="user">Usuario a loguear. </param>
        public void Login(User user)
        {
            CurrentUser = user;
        }

        /// <summary>
        /// Método para desloguear al usuario.
        /// </summary>
        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
