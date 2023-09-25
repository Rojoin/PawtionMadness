using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class WaveSO : ScriptableObject
{
    public EnemyTypeProb[] enemyTypes;

    public int enemyCount;
    public bool activeWave;

    [Header("Before Wave")]
    public int totalEnemyBeforeWave;
    public int newSpawnTime;

    [Header("Wave")]
    public int totalEnemyWave;
    public float reductionSpawnTime;
    public float delayAfterWave;
}
