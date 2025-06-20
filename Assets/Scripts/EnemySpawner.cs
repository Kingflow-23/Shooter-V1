using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab; 
    public float spawnInterval;
    private float nextSpawnInterval;
    public float spawnAreaRadius = 20f; 
    
    void Start()
    {
        nextSpawnInterval = spawnInterval;
    }

    void Update()
    {
        nextSpawnInterval -= Time.deltaTime;
        if (nextSpawnInterval > 0f)
        {
            return;
        }
        nextSpawnInterval = spawnInterval;

        Vector3 spawnLocation = spawnAreaRadius * Random.insideUnitCircle.normalized;
        Instantiate(enemyPrefab, new Vector3(spawnLocation.x, 1.5f, spawnLocation.y), Quaternion.identity);
    }
}
