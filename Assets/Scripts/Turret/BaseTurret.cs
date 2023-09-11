using Grid;
using UnityEngine;

namespace Turret
{
    public abstract class BaseTurret : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private Tile tile;

        private float maxHealth;
        private float currentHealth;
        private bool isAlive;

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

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