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
        protected bool isAlive;
        private static readonly int Death1 = Animator.StringToHash("Death");
        private BoxCollider boxCollider;
        public float MaxHealth { get => turretType.maxHealth; }
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

        public float DeathTime { get => turretType.deathTime; }
        private void Awake()
        {
            CurrentHealth = MaxHealth;
            boxCollider = GetComponent<BoxCollider>();
            isAlive = true;
        }
        public virtual void Death()
        {
            animator?.SetTrigger(Death1);
            boxCollider.enabled = false;
            Invoke(nameof(DestroyObject),DeathTime);
        }
        private void DestroyObject()
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