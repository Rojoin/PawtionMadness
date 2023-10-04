using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject baseEnemy;
    [SerializeField] private UnityEvent activateWinScreenChannel;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private WaveSO[] waveList;
    [SerializeField] private CustomSlider gameBar;
    [SerializeField] private GameObject textBeforeWave;
    [SerializeField] private float waveTextTimer;
    
    private List<EnemySO> probList = new List<EnemySO>();
    private List<GameObject> enemySpawned;
    private float maxGameBarTimer = 0;
    private float timerGameBar = 0;

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

        foreach (var wave in waveList)
        {
            float waveTimerBeforeWave = wave.newSpawnTime * wave.totalEnemyBeforeWave + wave.delayBeforeWave;

            maxGameBarTimer += waveTimerBeforeWave +
                               wave.delayAfterWave;
        }

        gameBar.FillAmount = timerGameBar / maxGameBarTimer;
        
        float waveTimer = 0;
        foreach (var wave in waveList)
        {
            float imageNormalizePosition =
                (waveTimer + wave.newSpawnTime * wave.totalEnemyBeforeWave + wave.delayBeforeWave) / maxGameBarTimer;
            
            waveTimer += wave.newSpawnTime * wave.totalEnemyBeforeWave + wave.delayBeforeWave + wave.delayAfterWave;
            Debug.Log(imageNormalizePosition);
            gameBar.AddWaveImage(imageNormalizePosition);
        }
    }

    private void Update()
    {
        if (timerGameBar < maxGameBarTimer)
        {
            timerGameBar += Time.deltaTime;
        }

        gameBar.FillAmount = timerGameBar / maxGameBarTimer;
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

                SpawnNewEnemy();
                enemyCount++;

                spawnTimer = 0;
                if (activeWave && enemyCount >= waveList[actualWave].totalEnemyWave)
                {
                    spawnTime = waveList[actualWave].delayAfterWave;
                    delayTimer = true;
                    enemyCount = 0;
                }
                else if (!activeWave && enemyCount >= waveList[actualWave].totalEnemyBeforeWave)
                {
                    activeWave = true;
                    spawnTime = waveList[actualWave].delayBeforeWave;
                    enemyCount = 0;
                    StartCoroutine(SpawnWaveText());
                }
            }
        }
        else if (!AreEnemiesAlive())
        {
            Invoke(nameof(WinGame), 5);
        }
    }

    private void WinGame()
    {
        activateWinScreenChannel.Invoke();
    }

    private bool AreEnemiesAlive()
    {
        return enemySpawned.Count >0;
    }

    private void SpawnNewEnemy()
    {
        Transform spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var type = probList[Random.Range(0, probList.Count)];
        GameObject newEnemy = Instantiate(type.asset, spawnPosition.transform.position, spawnPoints[0].rotation);
        enemySpawned.Add(newEnemy);
    }
    private IEnumerator SpawnWaveText()
    {
        textBeforeWave.SetActive(true);
        yield return new WaitForSeconds(waveTextTimer);
        textBeforeWave.SetActive(false);
        yield break;
    }
}