
using Enemy;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject baseEnemy;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyTypePorb[] enemyTypes;
    private List<EnemySO> probList = new List<EnemySO>();

    [SerializeField] private float spawnTime;
    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnTime)
        {
            foreach (EnemyTypePorb newEnemyType in enemyTypes)
            {
                for (int i = 0; i < newEnemyType.probability; i++)
                {
                    probList.Add(newEnemyType.Type);
                }
            }

            SpawnNewEnemy();
            spawnTimer -= spawnTime;
        }
    }

    private void SpawnNewEnemy()
    {
        Transform spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var type = probList[Random.Range(0, probList.Count)];
        GameObject newEnemy = Instantiate(type.asset, spawnPosition.transform.position, spawnPoints[0].rotation);
    }
}

[System.Serializable]
public class EnemyTypePorb
{
    public EnemySO Type;
    [Range(1.0f, 10.0f)]
    public int probability;
}