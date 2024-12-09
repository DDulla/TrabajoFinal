using System;
using UnityEngine;

public class SimpleLinkedList<T>
{
    public class Node 
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    private Node Head { get; set; }
    private int Count { get; set; }

    public void InsertSort(T value, Comparison<T> comparison)
    {
        Node newNode = new Node(value);
        if (Head == null || comparison(Head.Value, value) < 0)
        {
            newNode.Next = Head;
            Head = newNode;
        }
        else
        {
            Node current = Head;
            while (current.Next != null && comparison(current.Next.Value, value) > 0)
            {
                current = current.Next;
            }
            newNode.Next = current.Next;
            current.Next = newNode;
        }
        Count++;
    }

    public void RemoveLast()
    {
        if (Head == null) return;
        if (Head.Next == null)
        {
            Head = null;
            Count--;
            return;
        }

        Node current = Head;
        while (current.Next.Next != null)
        {
            current = current.Next;
        }
        current.Next = null;
        Count--;
    }

    public Node GetHead()
    {
        return Head;
    }

    public int GetCount()
    {
        return Count;
    }
}
