using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class WaveSO : ScriptableObject
{
    public EnemyTypeProb[] enemyTypes;

    [Header("Basic Info Wave")]
    public float TimeBeforeNewPreWaveStart;

    [Header("Pre Wave")]
    public float newSpawnTime;
    public int totalEnemyBeforeWave;
    public float delayBeforeWave;

    [Header("Wave")]
    public int totalEnemyWave;
    public float SpawnTimeInWave;
}
