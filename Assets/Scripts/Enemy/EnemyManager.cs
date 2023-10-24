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
    [SerializeField] private BoolChannelSO OnWaveFinishChannel;
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
    [SerializeField] private EnemySO reinforcedEnemy;
    [SerializeField] private EnemySO rangeEnemy;
    [SerializeField] private EnemyChargeSO chargeEnemy;
    [SerializeField] private AngryEnemySO angryEnemy;
    [SerializeField] private ChangingEnemySO changinLineEnemy;

    private bool wavesEnded;

    private EnemyFactory enemyFactory = new EnemyFactory();

    private void Awake()
    {
        wavesEnded = false;
        OnWaveFinishChannel.Subscribe(ChangeWaveState);

        enemySpawned = new List<GameObject>();
        onEnemySpawnChannel.Subscribe(addNewEnemy);

        simpleEnemyPool = new ObjectPool<GameObject>(() => Instantiate(simpleEnemy.asset, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, simpleEnemyPoolSize, 100);

        reinforcedEnemyPool = new ObjectPool<GameObject>(() => Instantiate(reinforcedEnemy.asset, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, simpleEnemyPoolSize, 100);

        rangeEnemyPool = new ObjectPool<GameObject>(() => Instantiate(rangeEnemy.asset, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, specialEnemyPoolSize, 100);

        chargeEnemyPool = new ObjectPool<GameObject>(() => Instantiate(chargeEnemy.asset, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, specialEnemyPoolSize, 100);

        angryEnemyPool = new ObjectPool<GameObject>(() => Instantiate(angryEnemy.asset, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, specialEnemyPoolSize, 100);

        changingLineEnemyPool = new ObjectPool<GameObject>(() => Instantiate(changinLineEnemy.asset, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, specialEnemyPoolSize, 100);
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

        GameObject newEnemy = null;


        switch (enemyType.enemyType)
        {
            case EnemySO.EnemyTypes.Simple:
                simpleEnemyPool.Get(out newEnemy);
                newEnemy.GetComponent<BaseEnemy>().onDeath.AddListener(OnKillSimpleEnemy);
                break;
            case EnemySO.EnemyTypes.Reinforced:
                reinforcedEnemyPool.Get(out newEnemy);
                newEnemy.GetComponent<BaseEnemy>().onDeath.AddListener(OnKillReinforcedEnemy);
                break;
            case EnemySO.EnemyTypes.Range:
                rangeEnemyPool.Get(out newEnemy);
                newEnemy.GetComponent<BaseEnemy>().onDeath.AddListener(OnKillRangeEnemy);
                break;
            case EnemySO.EnemyTypes.Charge:
                chargeEnemyPool.Get(out newEnemy);
                newEnemy.GetComponent<BaseEnemy>().onDeath.AddListener(OnKillChargeEnemy);
                break;
            case EnemySO.EnemyTypes.Angry:
                angryEnemyPool.Get(out newEnemy);
                newEnemy.GetComponent<BaseEnemy>().onDeath.AddListener(OnKillAngryEnemy);
                break;
            case EnemySO.EnemyTypes.ChangeLine:
                changingLineEnemyPool.Get(out newEnemy);
                newEnemy.GetComponent<BaseEnemy>().onDeath.AddListener(OnKillChangingLineEnemy);
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
        enemySpawned.Remove(simpleEnemy);
    }

    private void OnKillReinforcedEnemy(GameObject reinforcedEnemy)
    {
        reinforcedEnemy.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillReinforcedEnemy);
        reinforcedEnemyPool.Release(reinforcedEnemy);
        enemySpawned.Remove(reinforcedEnemy);
    }

    private void OnKillRangeEnemy(GameObject rangeEnemy)
    {
        rangeEnemy.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillRangeEnemy);
        rangeEnemyPool.Release(rangeEnemy);
        enemySpawned.Remove(rangeEnemy);
    }

    private void OnKillChargeEnemy(GameObject chargeEnemy)
    {
        chargeEnemy.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillChargeEnemy);
        chargeEnemyPool.Release(chargeEnemy);
        enemySpawned.Remove(chargeEnemy);
    }

    private void OnKillAngryEnemy(GameObject angryEnemy)
    {
        angryEnemy.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillAngryEnemy);
        angryEnemyPool.Release(angryEnemy);
        enemySpawned.Remove(angryEnemy);
    }

    private void OnKillChangingLineEnemy(GameObject changeLineEnemy)
    {
        changeLineEnemy.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillChangingLineEnemy);
        changingLineEnemyPool.Release(changeLineEnemy);
        enemySpawned.Remove(changeLineEnemy);
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
}
