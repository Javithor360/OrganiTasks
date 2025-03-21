using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Util
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }        
    }
    public class DoubleLinkledList<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int count;

        public int Count => count;

        public DoubleLinkledList()
        {
            head = null;
            tail= null;
            count = 0;
        }

        public void AddFirst(T value)
        {
            Node<T> newNode = new Node<T>(value);
            {
                if (head == null)
                    head = tail = newNode;
                else
                {
                    newNode.Next = head;
                    head.Previous = newNode;
                    head = newNode;
                }
                count++;
        }   }

        public void AddLast(T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (head == null)
                head = tail = newNode;
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
            count++;
        }

        public void BulkAdd(IEnumerable<T> values)
        {
            foreach(var value in values)
            {
                AddLast(value);
            }
        }
        public Node<T> Find(T value)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                    return current;
                current = current.Next;
            }
            return null;
        }

        public void Remove(Node<T> node)
        {
            if (node == null) return;

            if(node==head) head = node.Next;
            if(node== tail) tail = node.Previous;
            if(node.Previous!= null) node.Previous.Next = node.Next;
            if(node.Next != null) node.Next.Previous = node.Previous;
            count--;
        }
        public void ModifyAt(int position, T value)
        {
            if (position < 0 || position >= count) return;
            Node<T> current = head;
            for (int i = 0; i < position; i++)
                current = current.Next;
            current.Value = value;
        }

        public void Move(int fromPosition, int toPosition)
        {
            if (fromPosition < 0 || fromPosition >= count || toPosition < 0 || toPosition >= count || fromPosition == toPosition)
                return;

            Node<T> current = head;
            for (int i = 0; i < fromPosition; i++)
                current = current.Next;

            Remove(current);
            Insert(toPosition, current.Value);
        }

        public void Insert(int position, T value)
        {
            if (position < 0 || position > count) return;
            if (position == 0) { AddFirst(value); return; }
            if (position == count) { AddLast(value); return; }

            Node<T> current = head;
            for (int i = 0; i < position; i++)
                current = current.Next;

            Node<T> newNode = new Node<T>(value);
            newNode.Next = current;
            newNode.Previous = current.Previous;
            current.Previous.Next = newNode;
            current.Previous = newNode;
            count++;
        }

        public void Sort()
        {
            if (count <= 1) return;
            bool swapped;
            do
            {
                swapped = false;
                Node<T> current = head;
                while (current.Next != null)
                {
                    if (current.Value.CompareTo(current.Next.Value) > 0)
                    {
                        T temp = current.Value;
                        current.Value = current.Next.Value;
                        current.Next.Value = temp;
                        swapped = true;
                    }
                    current = current.Next;
                }
            } while (swapped);
        }

        public void Clear()
        {
            head = tail = null;
            count = 0;
        }

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

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }


}


    

