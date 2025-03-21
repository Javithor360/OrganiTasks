using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Util
{
    public class Nodo
    {
        public string Dato;
        public Nodo Siguiente;

        public Nodo(string dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
    public class ListaEnlazada
    {
        private Nodo cabeza;

        public void Agregar(String dato)
        {
            Nodo nuevo = new Nodo(dato);
                if(cabeza  == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
                       
        }

        public void AgregarVarios(List<string> datos)
        {
            foreach (var dato in datos)
            {
                Agregar(dato);
            }
        }

        public void Eliminar(string dato)
        {
            if (cabeza == null) return;

            if(cabeza.Dato == dato)
            {
                cabeza = cabeza.Siguiente;
                return;
            }

            Nodo actual = cabeza;
            while (actual.Siguiente != null && actual.Siguiente.Dato != dato)
            {
                actual = actual.Siguiente;
            }

            if(actual.Siguiente != null)
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
            }
        }
        public void Modificar(string Datoviejo, string nuevoDato)
        {
            Nodo actual = cabeza;
            while(actual !=null)
            {
                if (actual.Dato == Datoviejo)
                {
                    actual.Dato = nuevoDato;
                    break;
                }
                actual = actual.Siguiente;
            }

        }

        public void Ordenar()
        {
            if (cabeza == null || cabeza.Siguiente == null) return;

            Nodo actual, siguiente;
            string temp;
            bool swapped;

            do
            {
                swapped = false;
                actual = cabeza;

                while (actual.Siguiente != null)
                {
                    siguiente = actual.Siguiente;

                    if(string.Compare(actual.Dato, siguiente.Dato) > 0)
                    {
                        temp = actual.Dato;
                        actual.Dato= siguiente.Dato;
                        siguiente.Dato = temp;
                        swapped = true;
                    }
                    actual = actual.Siguiente;
                }
            }
            while(swapped);
        }
        public List<string> ObtenerLista()
        {
            List<string> tareas = new List<string>();
            Nodo actual = cabeza;
            while(actual != null)
            {
                tareas.Add(actual.Dato);
                actual = actual.Siguiente;
            }
            return tareas;
        }

        
    }

    
}
