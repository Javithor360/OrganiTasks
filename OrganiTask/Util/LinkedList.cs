using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Util
{
    public class LinkedList<T>
    {
        private class Nodo
        {
            public T Data;
            public Nodo Next;

            public Nodo(T data)
            {
                Data = data;
                Next = null;
            }
        }
        private Nodo head;

        public void Add(T data)
        {
            Nodo newNode = new Nodo(data);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Nodo current = head;
                while(current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public void Remove(T data)
        {
            if (head == null) return;

            if (head.Data.Equals(data))
            {
                {
                    head = head.Next;
                    return;
                }

                Nodo current = head;

                while (current.Next != null && !current.Next.Data.Equals(data))
                {
                    current = current.Next;
                }

                if (current.Next != null)
                {
                    current.Next = current.Next.Next;
                }
            }
        }

    }
}
