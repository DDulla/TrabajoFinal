
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    public List<GameObject> spawnableObjects;
    public float spawnInterval = 10f;
    private Graph<Transform> graph;
    private List<Graph<Transform>.Node> graphNodes;

    void Start()
    {
        graph = new Graph<Transform>();
        graphNodes = new List<Graph<Transform>.Node>();

        // Añadir nodos al grafo y crear los objetos
        foreach (Transform child in transform)
        {
            var node = graph.AddNode(child);
            graphNodes.Add(node);
            StartCoroutine(SpawnObjects(node));
        }

        // Añadir aristas entre nodos (enlace simple para ejemplo)
        for (int i = 0; i < graphNodes.Count - 1; i++)
        {
            graph.AddEdge(graphNodes[i], graphNodes[i + 1]);
        }
    }

    private IEnumerator SpawnObjects(Graph<Transform>.Node node)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Transform nodeTransform = node.Data;
            foreach (Transform child in nodeTransform)
            {
                Destroy(child.gameObject); // Eliminar el objeto anterior
            }

            GameObject randomObject = spawnableObjects[Random.Range(0, spawnableObjects.Count)];
            Instantiate(randomObject, nodeTransform.position, Quaternion.identity, nodeTransform);
        }
    }
}
