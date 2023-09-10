using Enemy;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject baseEnemy;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyTypePorb[] enemyTypes;
    private List<EnemyType> probList = new List<EnemyType>();

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
        GameObject newEnemy = Instantiate(baseEnemy, spawnPosition.transform.position, spawnPoints[0].rotation);
        newEnemy.GetComponent<BaseEnemy>().type = probList[Random.Range(0, probList.Count)];
    }
}

[System.Serializable]
public class EnemyTypePorb
{
    public EnemyType Type;
    [Range(1.0f, 10.0f)]
    public int probability;
}