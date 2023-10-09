using UnityEngine;
using Health;
using UnityEngine.Events;

namespace Enemy
{
    public class BaseEnemy : MonoBehaviour, IHealthComponent
    {
        [SerializeField] protected EnemySO enemyType;
        [SerializeField] private UnityEvent onDamage;
        private float currentHealth;
        private bool isAlive;

        public float MoveSpeed { get => enemyType.moveSpeed; }
        public float Damage { get => enemyType.damage;  }
        public float AttackSpeed { get => enemyType.attackSpeed;  }
        public float AttackRange { get => enemyType.attackRange;  }
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

        private void Start()
        {
            CurrentHealth = enemyType.maxHealth;
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
            onDamage.Invoke();
            if (CurrentHealth <= 0)
            {
                isAlive = false;
                Death();
            }
        }
    }

}
