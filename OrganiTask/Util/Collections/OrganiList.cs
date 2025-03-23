using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Util.Collections
{
    /// <summary>
    /// Representación de una lista doblemente enlazada como estructura de dato dinámica lineal
    /// </summary>
    /// <typeparam name="T">Tipo de dato que tendrán los elementos de la lista</typeparam>
    public class OrganiList<T> : ICollection<T>
    {
        private Node<T> head; // Nodo inicial de la lista
        private Node<T> tail; // Nodo final de la lista
        private int count; // Cantidad de elementos en la lista

        // Getter del primer elemento de la lista
        public T First => head.Value;

        // Getter del último elemento de la lista
        public T Last => tail.Value;

        // Getter de la cantidad de elementos en la lista
        public int Count => count;

        // Getter que indica si la lista está vacía
        public bool IsEmpty => count == 0;

        // Getter que indica si la lista es de solo lectura
        public bool IsReadOnly => false;

        /// <summary>
        /// Constructor default de la clase que inicializa sus elementos vacíos
        /// </summary>
        public OrganiList()
        {
            head = null;
            tail= null;
            count = 0;
        }

        /// <summary>
        /// Agrega un elemento al inicio de la lista
        /// </summary>
        /// <param name="value">Valor que se desea agregar a la lista</param>
        /// <returns>Nodo que se agregó a la lista</returns>
        public Node<T> AddFirst(T value)
        {
            // Se crea un nuevo nodo con el valor deseado
            Node<T> newNode = new Node<T>(value);
            if (IsEmpty)
                head = tail = newNode; // Si la lista está vacía, el nuevo nodo es el único nodo
            else
            {
                // Si la lista no está vacía, se agrega el nuevo nodo al inicio de la lista
                newNode.Next = head; // Temporalmente, el siguiente nodo del nuevo nodo es el nodo inicial
                head.Previous = newNode; // El nodo anterior del nodo inicial es el nuevo nodo
                head = newNode; // El nuevo nodo es el nodo inicial
            }

            count++; // Se incrementa la cantidad de elementos en la lista
            return newNode; // Se devuelve el nodo insertado
        }

        /// <summary>
        /// Agrega un elemento al final de la lista
        /// </summary>
        /// <param name="value">Valor que se desea agregar a la lista</param>
        /// <returns>Nodo que se agregó a la lista</returns>
        public Node<T> AddLast(T value)
        {
            // Se crea un nuevo nodo con el valor deseado
            Node<T> newNode = new Node<T>(value);
            if (IsEmpty)
                head = tail = newNode; // Si la lista está vacía, el nuevo nodo es el único nodo
            else
            {
                // Si la lista no está vacía, se agrega el nuevo nodo al final de la lista
                tail.Next = newNode; // Temporalmente, el siguiente nodo del nodo final es el nuevo nodo
                newNode.Previous = tail; // El nodo anterior del nuevo nodo es el nodo final
                tail = newNode; // El nuevo nodo es el nodo final
            }

            count++; // Se incrementa la cantidad de elementos en la lista
            return newNode; // Se devuelve el nodo insertado
        }

        /// <summary>
        /// Método de ICollection que agrega un elemento a la lista sin retornar valor
        /// </summary>
        /// <param name="item">Elemento que se desea agregar a la lista</param>
        public void Add(T item)
        {
            AddLast(item);
        }

        /// <summary>
        /// Inserta un elemento en una posición específica de la lista
        /// </summary>
        /// <param name="position">Posición en la que se desea insertar el elemento</param>
        /// <param name="value">Valor que se desea insertar en la lista</param>
        /// <returns>Nodo que se insertó en la lista</returns>
        public Node<T> Insert(int position, T value)
        {
            // Si la posición es inválida, no se hace nada
            if (position < 0 || position > count) 
                return null;

            // Si la posición es 0, se agrega al inicio de la lista
            if (position == 0) 
                return AddFirst(value); 

            // Si la posición es igual a la cantidad de elementos, se agrega al final de la lista
            if (position == count)
                return AddLast(value);

            // Nodo temporal que recorrerá la lista empezando por el nodo inicial
            Node<T> current = head;

            // Se recorre la lista hasta llegar a la posición deseada
            for (int i = 0; i < position; i++)
                current = current.Next; // Se avanza al siguiente nodo

            // Se crea un nuevo nodo con el valor deseado
            Node<T> newNode = new Node<T>(value);
            newNode.Next = current; // El siguiente nodo del nuevo nodo es el nodo actual
            newNode.Previous = current.Previous; // El nodo anterior del nuevo nodo es el nodo anterior del nodo actual
            current.Previous.Next = newNode; // El siguiente nodo del nodo anterior del nodo actual es el nuevo nodo
            current.Previous = newNode; // El nodo anterior del nodo actual es el nuevo nodo
            count++; // Se incrementa la cantidad de elementos en la lista

            return newNode; // Se devuelve el nodo insertado
        }

        /// <summary>
        /// Agrega múltiples elementos al final de la lista
        /// </summary>
        /// <param name="values">Valores que se desean agregar a la lista</param>
        public void BulkAdd(IEnumerable<T> values)
        {
            // Se recorren los elementos y se agregan a la lista
            foreach (var value in values)
            {
                AddLast(value); // Se agrega el valor al final de la lista
            }
        }

        /// <summary>
        /// Elimina el primer elemento de la lista
        /// </summary>
        /// <returns>Nodo que se eliminó de la lista</returns>
        public Node<T> RemoveFirst()
        {
            // Si la lista está vacía, no se hace nada
            if (IsEmpty) 
                return null;

            Node<T> node = head; // Nodo que se eliminará de la lista
            head = head.Next; // El nodo inicial es el siguiente nodo

            // Si el nodo inicial no es nulo, el nodo anterior del nodo inicial es nulo
            if (head != null) 
                head.Previous = null;

            count--; // Se decrementa la cantidad de elementos en la lista
            return node; // Se devuelve el nodo eliminado
        }

        /// <summary>
        /// Elimina el último elemento de la lista
        /// </summary>
        /// <returns>Nodo que se eliminó de la lista</returns>
        public Node<T> RemoveLast()
        {
            // Si la lista está vacía, no se hace nada
            if (IsEmpty)
                return null;
            Node<T> node = tail; // Nodo que se eliminará de la lista
            tail = tail.Previous; // El nodo final es el nodo anterior
            // Si el nodo final no es nulo, el siguiente nodo del nodo final es nulo
            if (tail != null)
                tail.Next = null;

            count--; // Se decrementa la cantidad de elementos en la lista
            return node; // Se devuelve el nodo eliminado
        }

        /// <summary>
        /// Elimina un elemento de la lista
        /// </summary>
        /// <param name="node">Nodo que se desea eliminar de la lista</param>
        public Node<T> Remove(Node<T> node)
        {
            // Si el nodo es nulo, no se hace nada
            if (node == null) return null;

            // Si el nodo es el inicial, el siguiente nodo es el nuevo inicial
            if (node == head) 
                head = node.Next;

            // Si el nodo es el final, el nodo anterior es el nuevo final
            if (node == tail) 
                tail = node.Previous;

            // Si el nodo anterior no es nulo, el siguiente nodo del nodo anterior es el siguiente nodo del nodo actual
            if (node.Previous != null) 
                node.Previous.Next = node.Next;

            // Si el siguiente nodo no es nulo, el nodo anterior del siguiente nodo es el nodo anterior del nodo actual
            if (node.Next != null) 
                node.Next.Previous = node.Previous;

            count--; // Se decrementa la cantidad de elementos en la lista
            return node; // Se devuelve el nodo eliminado
        }

        /// <summary>
        /// Método de ICollection que elimina un elemento de la lista retornando booleano
        /// </summary>
        /// <param name="item">Elemento que se desea eliminar de la lista</param>
        /// <returns>Verdadero si se eliminó el elemento, falso en caso contrario</returns>
        public bool Remove(T item)
        {
            Node<T> node = Find(item); // Se busca el nodo que contiene el valor
            if (node == null) return false; // Si no se encuentra el nodo, no se hace nada
            Remove(node); // Se elimina el nodo de la lista
            return true; // Se indica que se eliminó el nodo
        }

        /// <summary>
        /// Elimina un elemento de la lista en una posición específica
        /// </summary>
        /// <param name="position">Posición del elemento que se desea eliminar</param>
        /// <returns>Nodo que se eliminó de la lista</returns>
        public Node<T> RemoveAt(int position)
        {
            // Si la posición es inválida, no se hace nada
            if (position < 0 || position >= count) 
                return null;

            // Nodo temporal que recorrerá la lista empezando por el nodo inicial
            Node<T> current = head;

            // Se recorre la lista hasta llegar a la posición deseada
            for (int i = 0; i < position; i++)
            {
                current = current.Next; // Se avanza al siguiente nodo
            }

            // Se elimina el nodo actual de la lista y se devuelve inmediatamente
            return Remove(current);
        }

        /// <summary>
        /// Modica un elemento de la lista en una posición específica
        /// </summary>
        /// <param name="position">Posición del elemento que se desea modificar</param>
        /// <param name="value">Nuevo valor que se desea asignar al elemento</param>
        public void ModifyAt(int position, T value)
        {
            // Si la posición es inválida, no se hace nada
            if (position < 0 || position >= count) return;

            // Nodo temporal que recorrerá la lista empezando por el nodo inicial
            Node<T> current = head;

            // Se recorre la lista hasta llegar a la posición deseada
            for (int i = 0; i < position; i++)
                current = current.Next; // Se avanza al siguiente nodo
            current.Value = value; // Se modifica el valor del nodo actual
        }

        /// <summary>
        /// Mueve un elemento de una posición a otra
        /// </summary>
        /// <param name="fromPosition">Posición del elemento que se desea mover</param>
        /// <param name="toPosition">Nueva posición del elemento</param>
        public void Move(int fromPosition, int toPosition)
        {
            // Si las posiciones son inválidas o iguales, no se hace nada
            if (fromPosition < 0 || fromPosition >= count || toPosition < 0 || toPosition >= count || fromPosition == toPosition)
                return;

            // Nodo temporal que recorrerá la lista empezando por el nodo inicial
            Node<T> current = head;

            // Se recorre la lista hasta llegar a la posición deseada
            for (int i = 0; i < fromPosition; i++)
                current = current.Next; // Se avanza al siguiente nodo

            Remove(current); // Se elimina el nodo actual de la lista
            Insert(toPosition, current.Value); // Se inserta el nodo en la nueva posición
        }

        /// <summary>
        /// Ordena los elementos de la lista
        /// NOTA: Este método solo funciona con tipos de datos que implementen la interfaz IComparable
        /// </summary>
        //public void Sort()
        //{
        //    // Si la cantidad de elementos es menor o igual a 1, no se hace nada
        //    if (count <= 1) return;

        //    // Variable que indica si se realizaron intercambios
        //    bool swapped;

        //    // Se realiza el ordenamiento de la lista en forma de burbuja
        //    do
        //    {
        //        swapped = false; // Se inicializa la variable en falso
        //        Node<T> current = head; // Nodo temporal que recorrerá la lista empezando por el nodo inicial

        //        // Se recorre la lista comparando los elementos y realizando intercambios si es necesario
        //        while (current.Next != null)
        //        {
        //            // Si el valor del nodo actual es mayor al valor del siguiente nodo, se intercambian los valores
        //            if (current.Value.CompareTo(current.Next.Value) > 0)
        //            {
        //                T temp = current.Value; // Variable temporal para almacenar el valor del nodo actual
        //                current.Value = current.Next.Value; // El valor del nodo actual es el valor del siguiente nodo
        //                current.Next.Value = temp; // El valor del siguiente nodo es el valor temporal
        //                swapped = true; // Se indica que se realizó un intercambio
        //            }
        //            current = current.Next; // Se avanza al siguiente nodo
        //        }
        //    } while (swapped); // Mientras se realicen intercambios, se sigue recorriendo la lista
        //}

        /// <summary>
        /// Busca un elemento en la lista y devuelve el nodo que lo contiene
        /// </summary>
        /// <param name="value">Valor que se desea buscar en la lista</param>
        /// <returns>Nodo que contiene el valor buscado</returns>
        public Node<T> Find(T value)
        {
            // Se crea un nodo temporal que recorrerá la lista
            Node<T> current = head;
            // Mientras el nodo actual no sea nulo se recorre la lista
            while (current != null)
            {
                // Si el valor del nodo actual es igual al valor buscado, se devuelve el nodo actual
                if (current.Value.Equals(value))
                    return current;
                current = current.Next; // Se avanza al siguiente nodo
            }
            return null; // Si no se encuentra el valor, se devuelve nulo
        }

        /// <summary>
        /// Busca un elemento en la lista por posición y devuelve el nodo que lo contiene
        /// </summary>
        /// <param name="position">Posición del elemento que se desea buscar</param>
        /// <returns>Nodo que contiene el valor buscado</returns>
        public Node<T> FindAt(int position)
        {
            // Si la posición es inválida, no se hace nada
            if (position < 0 || position >= count)
                return null;

            // Nodo temporal que recorrerá la lista empezando por el nodo inicial
            Node<T> current = head;

            // Se recorre la lista hasta llegar a la posición deseada
            for (int i = 0; i < position; i++)
                current = current.Next; // Se avanza al siguiente nodo

            return current; // Se devuelve el nodo encontrado
        }

        /// <summary>
        /// Verifica si un elemento se encuentra en la lista
        /// </summary>
        /// <param name="value">Valor que se desea buscar en la lista</param>
        /// <returns>Verdadero si el valor se encuentra en la lista, falso en caso contrario</returns>
        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        /// <summary>
        /// Elimina todos los elementos de la lista
        /// </summary>
        public void Clear()
        {
            head = tail = null;
            count = 0;
        }

        /// <summary>
        /// Convierte la lista en un arreglo
        /// </summary>
        /// <returns>Arreglo con los elementos de la lista</returns>
        public T[] ToArray()
        {
            T[] array = new T[count]; // Se crea un arreglo con la cantidad de elementos en la lista
            Node<T> current = head; // Nodo temporal que recorrerá la lista empezando por el nodo inicial
            int i = 0; // Índice del arreglo

            // Mientras el nodo actual no sea nulo se recorre la lista
            while (current != null)
            {
                array[i++] = current.Value; // Se agrega el valor del nodo actual al arreglo
                current = current.Next; // Se avanza al siguiente nodo
            }

            return array; // Se devuelve el arreglo con los elementos de la lista
        }

        /// <summary>
        /// Método de ICollection que copia los elementos de la lista a un arreglo
        /// </summary>
        /// <param name="array">Arreglo al que se copiarán los elementos de la lista</param>
        /// <param name="arrayIndex">Índice en el que se iniciará la copia de los elementos</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            // Si el arreglo es nulo, el índice es inválido o no hay espacio suficiente, no se hace nada
            if (array == null || arrayIndex < 0 || (array.Length - arrayIndex < count)) return;

            Node<T> current = head; // Nodo temporal que recorrerá la lista empezando por el nodo inicial
            // Mientras el nodo actual no sea nulo se recorre la lista
            while (current != null)
            {
                array[arrayIndex++] = current.Value; // Se agrega el valor del nodo actual al arreglo
                current = current.Next; // Se avanza al siguiente nodo
            }
        }

        /// <summary>
        /// Muestra los elementos de la lista
        /// NOTA: Este método es solo para fines de depuración y solo funciona en consola
        /// </summary>
        public void ShowElements()
        {
            Node<T> current = head;
            while (current != null)
            {
                Console.Write(current.Value + " <-> ");
                current = current.Next;
            }
            Console.WriteLine("null");
        }

        /// <summary>
        /// Implementación del método GetEnumerator de la interfaz IEnumerable para poder recorrer los elementos de la lista
        /// </summary>
        /// <returns>Elemento de la lista</returns>
        public IEnumerator<T> GetEnumerator()
        {
            // Nodo temporal que recorrerá la lista empezando por el nodo inicial
            Node<T> current = head;

            // Mientras el nodo actual no sea nulo se recorre la lista
            while (current != null)
            {
                yield return current.Value; // Se devuelve el valor del nodo actual y se pausa la ejecución
                current = current.Next; // Se avanza al siguiente nodo
            }
        }

        // Getter de la interfaz IEnumerable para poder recorrer los elementos de la lista y que sea compatible con foreach
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}