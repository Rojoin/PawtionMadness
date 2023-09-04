using UnityEngine;
using Health;

namespace Enemy
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private EntityHealth health;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float damage;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackRange;

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float Damage { get => damage; set => damage = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }

        private void Awake()
        {
            health = GetComponent<EntityHealth>();
        }
    }

}
