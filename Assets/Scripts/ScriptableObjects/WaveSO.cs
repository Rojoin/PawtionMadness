using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class WaveSO : ScriptableObject
{
    public EnemyTypeProb[] enemyTypes;

    public float newSpawnTime;

    [Header("Before Wave")]
    public int totalEnemyBeforeWave;
    public float delayBeforeWave;

    [Header("Wave")]
    public int totalEnemyWave;
    public float SpawnTimeInWave;
    public float delayAfterWave;
}
