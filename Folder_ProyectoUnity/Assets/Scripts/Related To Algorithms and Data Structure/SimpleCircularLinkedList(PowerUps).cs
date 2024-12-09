using System;
using System.Collections.Generic;

public class SimpleCircularLinkedList<T>
{
    public class Node
    {
        public T Data;
        public Node Next;

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    private Node head;
    private Node tail;
    private Node current;

    public SimpleCircularLinkedList()
    {
        head = null;
        tail = null;
        current = null;
    }

    public void Add(T data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
            newNode.Next = head;
        }
        else
        {
            tail.Next = newNode;
            tail = newNode;
            tail.Next = head;
        }
    }

    public T GetNext()
    {
        if (current == null)
        {
            current = head;
        }
        else
        {
            current = current.Next;
        }
        return current.Data;
    }

    public void Reset()
    {
        current = head;
    }
}
