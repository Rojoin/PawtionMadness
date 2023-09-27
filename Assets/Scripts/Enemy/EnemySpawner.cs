using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject baseEnemy;
    [SerializeField]  private UnityEvent activateWinScreenChannel;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private WaveSO[] waveList;
    private List<EnemySO> probList = new List<EnemySO>();
    private List<GameObject> enemySpawned;

    private float spawnTimer;
    private float spawnTime;
    private bool delayTimer;

    private int enemyCount;
    private bool activeWave;
    private int actualWave = 0;

    private void Awake()
    {
        enemySpawned = new List<GameObject>();
        spawnTime = waveList[actualWave].newSpawnTime;
    }

    private void Update()
    {
        if (actualWave < waveList.Length)
        {
            spawnTimer += Time.deltaTime;


            if (spawnTimer > spawnTime)
            {
                if (activeWave)
                {
                    spawnTime = waveList[actualWave].SpawnTimeInWave;
                }

                if (delayTimer)
                {
                    delayTimer = false;
                    activeWave = false;
                    actualWave++;
                    if (actualWave < waveList.Length)
                    {
                        spawnTime = waveList[actualWave].newSpawnTime;
                    }

                    enemyCount = 0;
                }

                foreach (EnemyTypeProb newEnemyType in waveList[actualWave].enemyTypes)
                {
                    for (int i = 0; i < newEnemyType.probability; i++)
                    {
                        probList.Add(newEnemyType.Type);
                    }
                }

                SpawnNewEnemy();
                enemyCount++;

                spawnTimer = 0;
                if (activeWave && enemyCount >= waveList[actualWave].totalEnemyWave)
                {
                    spawnTime = waveList[actualWave].delayAfterWave;
                    delayTimer = true;
                    enemyCount = 0;
                    Debug.Log("Next Wave");
                }
                else if (!activeWave && enemyCount >= waveList[actualWave].totalEnemyBeforeWave)
                {
                    activeWave = true;
                    spawnTime = waveList[actualWave].delayBeforeWave;
                    enemyCount = 0;
                    Debug.Log("Wave Incoming!");
                }
            }
        }
        else if (!AreEnemiesAlive())
        {
            activateWinScreenChannel.Invoke();
        }
    }

    private bool AreEnemiesAlive()
    {
        foreach (var enemy in enemySpawned)
        {
            if (enemy)
                return true;
        }

        return false;
    }

    private void SpawnNewEnemy()
    {
        Transform spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var type = probList[Random.Range(0, probList.Count)];
        GameObject newEnemy = Instantiate(type.asset, spawnPosition.transform.position, spawnPoints[0].rotation);
        Debug.Log("New Enemy");
        enemySpawned.Add(newEnemy);
    }
}