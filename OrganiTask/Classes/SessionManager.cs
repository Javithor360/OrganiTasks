using Microsoft.Win32;
using OrganiTask.Entities;
using System;

// Clase "SingleTon"
// Una sola instancia durante el ciclo de vida de la aplicación
public class SessionManager
{
    // "_" Se utiliza para denotar campos privados
    private static SessionManager _instance; // Campo Privado
    private User _currentUser; // Campo Privado

    // Constructor vacío
    public SessionManager() { }

    public static SessionManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = new SessionManager();

            return _instance;
        }
    }

    public User CurrentUser
    {
        get { return _currentUser; }
        private set { _currentUser = value; }
    }

    public bool IsLoggedIn => CurrentUser != null;

    public void Login(User user)
    {
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}
