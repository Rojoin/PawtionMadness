using Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.WSA;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public UnityEvent activateWinScreenChannel;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyInvokeChannel onEnemySpawnChannel;
    [SerializeField] private float timeUntilWin = 5f;
    [SerializeField] private Transform enemyParent;

    [SerializeField] private int simpleEnemyPoolSize;
    [SerializeField] private int specialEnemyPoolSize;

    private List<GameObject> enemySpawned;
    private ObjectPool<GameObject> simpleEnemyPool;
    private ObjectPool<GameObject> reinforcedEnemyPool;
    private ObjectPool<GameObject> rangeEnemyPool;
    private ObjectPool<GameObject> chargeEnemyPool;
    private ObjectPool<GameObject> angryEnemyPool;
    private ObjectPool<GameObject> changingLineEnemyPool;

    [SerializeField] private EnemySO simpleEnemy;
    //[SerializeField] private EnemyChargeSO chargeEnemy;

    private EnemyFactory enemyFactory = new EnemyFactory();

    private void Awake()
    {
        enemySpawned = new List<GameObject>();
        onEnemySpawnChannel.Subscribe(addNewEnemy);

        simpleEnemyPool = new ObjectPool<GameObject>(() => Instantiate(simpleEnemy.asset, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, 5, 40);
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

        GameObject newEnemy = null;


        switch (enemyType.enemyType)
        {
            case EnemySO.EnemyTypes.Simple:
                simpleEnemyPool.Get(out newEnemy);
                newEnemy.GetComponent<BaseEnemy>().onDeath.AddListener(OnKillSimpleEnemy);
                break;
            case EnemySO.EnemyTypes.Reinforced:

                break;
            case EnemySO.EnemyTypes.Range:

                break;
            case EnemySO.EnemyTypes.Charge:

                break;
            case EnemySO.EnemyTypes.Angry:

                break;
            case EnemySO.EnemyTypes.ChangeLine:

                break;
            default:
                break;
        }

        enemyFactory.NewEnemyConfigure(ref newEnemy, spawnPosition);
        enemySpawned.Add(newEnemy);

    }

    private void OnKillSimpleEnemy(GameObject simpleEnemy)
    {
        simpleEnemy.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillSimpleEnemy);
        simpleEnemyPool.Release(simpleEnemy);
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
