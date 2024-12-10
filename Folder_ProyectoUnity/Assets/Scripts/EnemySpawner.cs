using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float spawnInterval = 5f;
    public List<Transform> spawnPoints;
    public Transform playerTransform;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                Transform spawnPoint = spawnPoints[i];

                if (spawnPoint != null)
                {
                    GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                    GameObject spawnedEnemy = Instantiate(randomEnemy, spawnPoint.position, randomEnemy.transform.rotation);
                    EnemyCarController enemyCarController = spawnedEnemy.GetComponent<EnemyCarController>();
                    enemyCarController.SetPlayerTransform(playerTransform);
                }
            }
        }
    }
}
