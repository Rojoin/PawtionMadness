using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public UnityEvent activateWinScreenChannel;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyInvokeChannel onEnemySpawnChannel;
    [SerializeField] private float timeUntilWin = 5f;
    private List<GameObject> enemySpawned;
    private ObjectPool<GameObject> enemyPool;

    private EnemyFactory enemyFactory = new EnemyFactory();

    private void Awake()
    {
        enemySpawned = new List<GameObject>();
        onEnemySpawnChannel.Subscribe(addNewEnemy);
    }

    private void OnDestroy()
    {
        onEnemySpawnChannel.Unsubscribe(addNewEnemy);
    }
    private void Update()
    {
        if (!AreEnemiesAlive())
        {
            Invoke(nameof(WinGame), timeUntilWin);
        }
    }

    private void addNewEnemy(List<EnemySO> probList) 
    {
        Transform spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
        EnemySO enemyType = probList[Random.Range(0, probList.Count)];
        enemySpawned.Add(enemyFactory.SpawnNewEnemy(spawnPosition, enemyType, transform));
    }

    private void WinGame()
    {
        activateWinScreenChannel.Invoke();
    }

    private bool AreEnemiesAlive()
    {
        foreach (GameObject enemy in enemySpawned)
        {
            if (enemy)
            {
                return true;
            }
        }

        return false;
    }
}
