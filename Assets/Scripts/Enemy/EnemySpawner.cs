using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject baseEnemy;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private WaveSO[] waveList;
    private List<EnemySO> probList = new List<EnemySO>();

    [SerializeField] private float spawnTime;
    private float spawnTimer;
    private bool delayTimer;

    private int actualWave = 0;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnTime)
        {
            if (delayTimer) 
            {
                delayTimer = false;
                //spawnTimer = waveList[actualWave].spawnTime;
            }

            foreach (EnemyTypeProb newEnemyType in waveList[actualWave].enemyTypes)
            {
                for (int i = 0; i < newEnemyType.probability; i++)
                {
                    probList.Add(newEnemyType.Type);
                }
            }

            SpawnNewEnemy();
            waveList[actualWave].enemyCount++;

            if (waveList[actualWave].activeWave && waveList[actualWave].enemyCount <= waveList[actualWave].totalEnemyWave)
            {
                actualWave++;
                spawnTimer = waveList[actualWave].delayAfterWave;
                delayTimer = true;
            }
            else if (waveList[actualWave].enemyCount <= waveList[actualWave].totalEnemyBeforeWave)
            {
                waveList[actualWave].activeWave = true;
            }


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
