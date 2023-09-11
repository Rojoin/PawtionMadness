using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public GameObject asset;

    public float moveSpeed;

    public float damage;
    public float attackSpeed;
    public float attackRange;

    public float maxHealth;
}
