using UnityEngine;

[CreateAssetMenu(fileName ="New Base Enemy", menuName = "Create Enemy/Base Enemy")]
public class EnemySO : ScriptableObject
{
    public enum EnemyTypes
    {
        Simple,
        Reinforced,
        Range,
        Charge,
        Angry,
        ChangeLine,
    }

    public EnemyTypes enemyType;
    public GameObject asset;

    public float moveSpeed;

    public float damage;
    public float attackSpeed;
    public float attackRange;

    public float maxHealth;
    public float deathTime;
    public float attackDelay = 0.2f;
}
