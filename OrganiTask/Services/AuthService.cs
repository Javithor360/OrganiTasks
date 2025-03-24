using OrganiTask.Entities;
using OrganiTask.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiTask.Services
{
    public class AuthService
    {
        public static Boolean CheckUserDataExist(string username, string email)
        {
            using (var context = new OrganiTaskDB())
            {
                return context.Users.Any(u => u.Username == username || u.Email == email);
            }
        }

        public static bool Login(string username, string password)
        {
            using (var context = new OrganiTaskDB())
            {
                var user = context.Users.SingleOrDefault(u => u.Username == username);
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    // Actualizar la sesión actual
                    SessionManager.Instance.Login(user);
                    // 
                    return true;
                }
                return false;
            }
        }

        // Async/Await para operaciones de bases de datos - ?
        public async Task<User> LoginAsync(string username, string password)
        {
            using (var context = new OrganiTaskDB())
            {
                var user = await context.Users.SingleOrDefaultAsync(u => u.Username == username);
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user;
                }
                return null;
            }
        }

        public static User Register(string username, string password, string email )
        {
            if (CheckUserDataExist(username, email))
                return null; // O lanzar una excepción

            using (var context = new OrganiTaskDB())
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                var newUser = new User
                {
                    Username = username,
                    Password = hashedPassword,
                    Email = email,
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                return newUser;
            }
        }
        public static void Logout()
        {
            SessionManager.Instance.Logout();
        }
    }
}
