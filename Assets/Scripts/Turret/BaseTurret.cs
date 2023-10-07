using Grid;
using UnityEngine;
using UnityEngine.Events;

namespace Turret
{
    public abstract class BaseTurret : MonoBehaviour, IHealthComponent
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected BaseTurretSO turretType;
        [SerializeField] private Tile tile;
        [SerializeField] private UnityEvent _onHit;
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
            
            _onHit.Invoke();
            if (CurrentHealth <= 0)
            {
                isAlive = false;
                Death();
            }
        }

    }
}