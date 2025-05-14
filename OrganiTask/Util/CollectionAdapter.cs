using OrganiTask.Util.Collections;
using System.Collections.Generic;

namespace OrganiTask.Util
{
    /// <summary>
    /// Clase que contiene métodos de extensión para adaptar colecciones.
    /// </summary>
    public static class CollectionAdapter
    {
        /// <summary>
        /// Convierte una colección de elementos en una lista doblemente enlazada.
        /// </summary>
        /// <typeparam name="T">Tipo de los elementos de la colección. </typeparam>
        /// <param name="source">Colección de elementos a convertir. </param>
        /// <returns>Lista doblemente enlazada con los elementos de la colección. </returns>
        public static OrganiList<T> ToOrganiList<T>(this IEnumerable<T> source)
        {
            OrganiList<T> result = new OrganiList<T>(); // Lista doblemente enlazada que se retornará.
            if (source != null)
            {
                // Se recorren los elementos de la colección y se agregan a la lista doblemente enlazada.
                foreach (T item in source)
                {
                    result.Add(item);
                }
            }

            // Se retorna la lista doblemente enlazada.
            return result;
        }
    }
}
