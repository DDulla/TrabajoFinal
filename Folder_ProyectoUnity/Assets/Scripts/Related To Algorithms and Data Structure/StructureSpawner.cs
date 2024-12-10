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

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            var node = graph.AddNode(child);
            graphNodes.Add(node);
            StartCoroutine(SpawnObjects(node));
        }

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

            for (int i = nodeTransform.childCount - 1; i >= 0; i--)
            {
                Destroy(nodeTransform.GetChild(i).gameObject);
            }

            GameObject randomObject = spawnableObjects[Random.Range(0, spawnableObjects.Count)];
            GameObject spawnedObject = Instantiate(randomObject, nodeTransform.position, randomObject.transform.rotation, nodeTransform);

            // Ajustar la escala del objeto instanciado a su escala original
            spawnedObject.transform.localScale = randomObject.transform.localScale;
        }
    }
}
