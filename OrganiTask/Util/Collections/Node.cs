namespace OrganiTask.Util.Collections
{
    /// <summary>
    /// Clase nodo para la lista doblemente enlazada que contiene referencias al siguiente y anterior nodo
    /// </summary>
    /// <typeparam name="T">Tipo de elementos en el nodo</typeparam>
    public class Node<T>
    {
        public T Value { get; set; } // Valor que almacena el nodo
        public Node<T> Next { get; set; } // Referencia al siguiente nodo en la lista
        public Node<T> Previous { get; set; } // Referencia al nodo anterior en la lista

        /// <summary>
        /// Constructor que crea un nodo con un valor específico
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
