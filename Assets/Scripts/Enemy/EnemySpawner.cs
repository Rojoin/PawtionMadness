using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveSO[] waveList;

    private List<EnemySO> probList = new List<EnemySO>();
    private float maxGameBarTimer = 0;
    private float timerGameBar = 0;

    private float spawnTimer;
    private float spawnPeriod;
    private bool delayTimer;

    private float currentTimer;

    private int enemyCount;
    private bool activeWave;
    private int actualWave = 0;

    [Header("Events")]
    public UnityEvent<float> OnGameBarUpdated = new UnityEvent<float>();
    public UnityEvent<float> OnNewWaveAdded = new UnityEvent<float>();
    public UnityEvent OnIncomingWave = new UnityEvent();
    public EnemyInvokeChannel invokeEnemyChannel = new EnemyInvokeChannel();

    private void Awake()
    {
        spawnPeriod = waveList[actualWave].newSpawnTime;

        foreach (var wave in waveList)
        {
            float waveTimerBeforeWave = wave.newSpawnTime * wave.totalEnemyBeforeWave + wave.delayBeforeWave;

            maxGameBarTimer += waveTimerBeforeWave +
                               wave.delayAfterWave;
        }

        currentTimer = timerGameBar / maxGameBarTimer;
        OnGameBarUpdated.Invoke(currentTimer);

        float waveTimer = 0;
        foreach (var wave in waveList)
        {
            float imageNormalizePosition =
                (waveTimer + wave.newSpawnTime * wave.totalEnemyBeforeWave + wave.delayBeforeWave) / maxGameBarTimer;

            waveTimer += wave.newSpawnTime * wave.totalEnemyBeforeWave + wave.delayBeforeWave + wave.delayAfterWave;
            OnNewWaveAdded.Invoke(imageNormalizePosition);
        }
    }

    private void Update()
    {
        if (timerGameBar < maxGameBarTimer)
        {
            timerGameBar += Time.deltaTime;
        }

        currentTimer = timerGameBar / maxGameBarTimer;
        OnGameBarUpdated.Invoke(currentTimer);

        if (actualWave < waveList.Length)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer > spawnPeriod)
            {
                if (activeWave)
                {
                    spawnPeriod = waveList[actualWave].SpawnTimeInWave;
                }

                if (delayTimer)
                {
                    delayTimer = false;
                    activeWave = false;
                    actualWave++;
                    if (actualWave < waveList.Length)
                    {
                        spawnPeriod = waveList[actualWave].newSpawnTime;
                    }

                    enemyCount = 0;
                }

                SortEnemies();

                invokeEnemyChannel.RaiseEvent(probList);
                enemyCount++;

                spawnTimer = 0;
                if (activeWave && enemyCount >= waveList[actualWave].totalEnemyWave)
                {
                    spawnPeriod = waveList[actualWave].delayAfterWave;
                    delayTimer = true;
                    enemyCount = 0;
                }
                else if (!activeWave && actualWave < waveList.Length &&
                         enemyCount >= waveList[actualWave].totalEnemyBeforeWave)
                {
                    activeWave = true;
                    spawnPeriod = waveList[actualWave].delayBeforeWave;
                    enemyCount = 0;
                    OnIncomingWave.Invoke();
                }
            }
        }
    }

    private void SortEnemies()
    {
        if (actualWave < waveList.Length)
        {
            foreach (EnemyTypeProb newEnemyType in waveList[actualWave].enemyTypes)
            {
                for (int i = 0; i < newEnemyType.probability; i++)
                {
                    probList.Add(newEnemyType.Type);
                }
            }
        }
    }

}