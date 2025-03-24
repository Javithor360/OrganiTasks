using BCrypt.Net;
using OrganiTask.Entities;
using OrganiTask.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiTask.Controllers.Services
{
    /// <summary>
    /// Servicio de autenticación de usuarios.
    /// </summary>
    public class AuthService
    {
        /// <summary>
        /// Verifica si un nombre de usuario ya existe en la base de datos.
        /// </summary>
        /// <param name="username">Nombre de usuario a verificar</param>
        /// <returns><c>true</c> si el nombre de usuario ya existe, <c>false</c> en caso contrario.</returns>
        public static bool CheckUserNameExist(string username)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscar un usuario con el nombre de usuario proporcionado
                User user = context.Users.SingleOrDefault(u => u.Username == username);

                if (user != null)
                {
                    return true; // El nombre de usuario ya existe
                }
            }

            return false; // El nombre de usuario no existe
        }

        /// <summary>
        /// Método para loguear a un usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <returns><c>true</c> si el login es exitoso, <c>false</c> en caso contrario.</returns>
        public static bool Login(string username, string password)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscar un usuario con el nombre de usuario proporcionado
                User user = context.Users.SingleOrDefault(u => u.Username == username);

                // Verificar si el usuario existe y la contraseña es correcta
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    // Actualizar la sesión actual
                    SessionManager.Instance.Login(user);
                    return true; // Login exitoso
                }
                return false; // Login fallido
            }
        }

        /// <summary>
        /// Async/Await para operaciones de bases de datos - ?
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <returns>Usuario logueado</returns>
        public async Task<User> LoginAsync(string username, string password)
        {
            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Buscar un usuario con el nombre de usuario proporcionado
                User user = await context.Users.SingleOrDefaultAsync(u => u.Username == username);

                // Verificar si el usuario existe y la contraseña es correcta
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user; // Retornar el usuario logueado
                }
                return null; // Retornar null si el login falla
            }
        }

        /// <summary>
        /// Método para registrar un nuevo usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <param name="email">Correo electrónico</param>
        /// <returns>Nuevo usuario registrado</returns>
        public static User Register(string username, string password, string email )
        {
            // Verificar si el nombre de usuario ya existe
            if (CheckUserNameExist(username))
                return null; // O lanzar una excepción

            // Usamos un bloque using para asegurarnos de que el contexto se libere al finalizar
            using (OrganiTaskDB context = new OrganiTaskDB())
            {
                // Hashear la contraseña
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                // Construir el nuevo usuario
                User newUser = new User
                {
                    Username = username,
                    Password = hashedPassword, // Guardar la contraseña hasheada
                    Email = email,
                };

                context.Users.Add(newUser); // Agregar el nuevo usuario al contexto
                context.SaveChanges(); // Guardar los cambios en la base de datos

                return newUser; // Retornar el nuevo usuario
            }
        }

        /// <summary>
        /// Método para cerrar la sesión del usuario.
        /// </summary>
        public static void Logout()
        {
            SessionManager.Instance.Logout(); // Cerrar la sesión del usuario
        }
    }
}
