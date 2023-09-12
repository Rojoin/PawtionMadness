using Grid;
using UnityEngine;

namespace Turret
{
    public abstract class BaseTurret : MonoBehaviour, IHealthComponent
    {
        [SerializeField] protected BaseTurretSO turretType;
        [SerializeField] private Tile tile;
        
        private float currentHealth;
        private bool isAlive;

        public float MaxHealth { get => turretType.maxHealth; }
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

        private void Awake()
        {
            CurrentHealth = MaxHealth;
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