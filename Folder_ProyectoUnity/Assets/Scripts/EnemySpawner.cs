using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public List<GameObject> enemyPrefabs;
    public float spawnInterval = 5f;
    public List<Transform> spawnPoints;
    public Transform playerTransform;
    private Coroutine spawnCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnEnemies());
        }
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
