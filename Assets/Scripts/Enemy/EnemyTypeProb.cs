using UnityEngine;

[System.Serializable]
public class EnemyTypeProb
{
    public EnemySO Type;
    [Range(1.0f, 10.0f)]
    public int probability;
}