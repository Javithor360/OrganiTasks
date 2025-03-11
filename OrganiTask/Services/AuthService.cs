using OrganiTask.Entities;
using OrganiTask.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiTask.Services
{
    public class AuthService
    {
        public static User Login(string username, string password)
        {
            using (var context = new OrganiTaskDB())
            {
                // Buscar al usuario por su nombre de usuario
                var user = context.Users.SingleOrDefault(u => u.Username == username);

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user;
                }

                return null;
            }
        }
    }
}
