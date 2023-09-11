using UnityEngine;
using Health;

namespace Enemy
{
    public class BaseEnemy : MonoBehaviour, IHealthComponent
    {
        public EnemySO type;
        private float moveSpeed;
        private float damage;
        private float attackSpeed;
        private float attackRange;
        private float maxHealth;
        private float currentHealth;
        private bool isAlive;

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float Damage { get => damage; set => damage = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

        private void Start()
        {
            moveSpeed = type.moveSpeed;
            damage = type.damage;
            attackSpeed = type.attackSpeed;
            attackRange = type.attackRange;
            maxHealth = type.maxHealth;

            CurrentHealth = maxHealth;
            isAlive = true;

            Instantiate(type.asset, transform.position, transform.rotation, transform);
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
