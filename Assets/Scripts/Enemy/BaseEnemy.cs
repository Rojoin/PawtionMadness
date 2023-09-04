using UnityEngine;
using Health;

namespace Enemy
{
    public class BaseEnemy : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float damage;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackRange;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private bool isAlive;

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float Damage { get => damage; set => damage = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }

        private void Awake()
        {
            CurrentHealth = maxHealth;
            isAlive = true;
        }
        public virtual void Death()
        {
            Destroy(gameObject);
        }

        public virtual bool IsAlive()
        {
            return isAlive;
        }

        public virtual void ReceiveDamage(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                isAlive = false;
                Death();
            }
        }
    }

}
