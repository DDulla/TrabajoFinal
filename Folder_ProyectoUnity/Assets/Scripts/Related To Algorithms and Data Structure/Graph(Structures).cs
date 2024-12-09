using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<T>
{
    public class Node
    {
        public T Data;
        public List<Node> Neighbors;

        public Node(T data)
        {
            Data = data;
            Neighbors = new List<Node>();
        }
    }

    private List<Node> nodes;

    public Graph()
    {
        nodes = new List<Node>();
    }

    public Node AddNode(T data)
    {
        Node newNode = new Node(data);
        nodes.Add(newNode);
        return newNode;
    }

    public void AddEdge(Node node1, Node node2)
    {
        if (!node1.Neighbors.Contains(node2))
        {
            node1.Neighbors.Add(node2);
        }
        if (!node2.Neighbors.Contains(node1))
        {
            node2.Neighbors.Add(node1);
        }
    }

    public List<Node> GetNodes()
    {
        return nodes;
    }
}

