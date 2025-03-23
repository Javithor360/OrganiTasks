using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Util.Collections
{
    /// <summary>
    /// Clase que maneja los nodos de la lista
    /// </summary>
    /// <typeparam name="T">Tipo de dato que contendrá el nodo</typeparam>
    public class Node<T>
    {
        public T Value { get; set; } // Tipo de dato genérico y valor que contendrá el nodo
        public Node<T> Next { get; set; } // Instancia del siguiente nodo
        public Node<T> Previous { get; set; } // Instancia del nodo anterior

        /// <summary>
        /// Constructor default de la clase
        /// </summary>
        /// <param name="value">Valor que almacenará la instancia actual del nodo</param>
        public Node(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }
    }
}
