using Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public UnityEvent activateWinScreenChannel;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyInvokeChannel onEnemySpawnChannel;
    [SerializeField] private BoolChannelSO OnWaveFinishChannel;
    [SerializeField] private float timeUntilWin = 5f;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private VoidChannelSO OnCheatKillEnemy;

    private List<GameObject> enemySpawned;
    [SerializeField] private List<EnemySO> EnemiesSO = new();
    private Dictionary<string, ObjectPool<GameObject>> EnemyPoolById = new();
    [SerializeField] private int EnemyPoolSize;

    private bool wavesEnded;
    private EnemyFactory enemyFactory = new EnemyFactory();

    private void Awake()
    {
        OnCheatKillEnemy.Subscribe(KillAllEnemies);
        wavesEnded = false;
        OnWaveFinishChannel.Subscribe(ChangeWaveState);

        enemySpawned = new List<GameObject>();
        onEnemySpawnChannel.Subscribe(addNewEnemy);

        foreach (var e in EnemiesSO)
        {
            EnemyPoolById.Add(e.id, new ObjectPool<GameObject>(() => Instantiate(e.asset.gameObject, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, EnemyPoolSize, 100));
        }
    }

    private void OnDestroy()
    {
        onEnemySpawnChannel.Unsubscribe(addNewEnemy);
        OnWaveFinishChannel.Unsubscribe(ChangeWaveState);
    }
    private void Update()
    {
        if (!AreEnemiesAlive() && wavesEnded)
        {
            Invoke(nameof(WinGame), timeUntilWin);
        }
    }

    private void addNewEnemy(List<EnemySO> probList)
    {
        Transform spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
        EnemySO enemyType = probList[Random.Range(0, probList.Count)];

        var pool = EnemyPoolById[enemyType.id];
        if (pool == null)
        {
            Debug.LogError("Enemy Pool not found");
            return;
        }
        GameObject newEnemy = null;
        pool.Get(out newEnemy);
        newEnemy.GetComponent<BaseEnemy>().onDeath.AddListener(OnKillEnemy);

        enemyFactory.NewEnemyConfigure(ref newEnemy, spawnPosition);
        enemySpawned.Add(newEnemy);

    }

    private void OnKillEnemy(GameObject enemy)
    {
        BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();
        baseEnemy.onDeath.RemoveListener(OnKillEnemy);
        ObjectPool<GameObject> pool = EnemyPoolById[baseEnemy.EnemyType.id];
        pool.Release(enemy);
        enemySpawned.Remove(enemy);
    }

    private void WinGame()
    {
        Debug.Log("you win");
        activateWinScreenChannel.Invoke();
        this.gameObject.SetActive(false);
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

    private void ChangeWaveState(bool state)
    {
        wavesEnded = state;
    }

    private void KillAllEnemies()
    {
        foreach (GameObject enemy in enemySpawned)
        {
            enemy.GetComponent<BaseEnemy>().ReceiveDamage(9999999);
        }
    }
}
