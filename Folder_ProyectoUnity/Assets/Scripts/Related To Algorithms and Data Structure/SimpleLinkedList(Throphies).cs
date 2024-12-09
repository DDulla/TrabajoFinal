
public class SimpleLinkedListForThrophies<T>
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
    private int count;

    public SimpleLinkedListForThrophies()
    {
        head = null;
        count = 0;
    }

    public void Add(T data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }

    public int GetCount()
    {
        return count;
    }

    public Node GetHead()
    {
        return head;
    }
}

