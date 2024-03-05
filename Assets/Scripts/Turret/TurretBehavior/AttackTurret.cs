using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Turret
{
    public abstract class AttackTurret : BaseTurret
    {
        public UnityEvent onAttack;
        private static readonly int ShootTriggerAnim = Animator.StringToHash("Shoot");
        protected float shootSpeedTimer = 0;
        private float attackRange;
        public float AttackAnimDelay { get => (turretType as AttackTurretSO).attackDelay; }
        public float ShootSpeed
        {
            get => (turretType as AttackTurretSO).shootSpeed;
        }
        public int AttackDamage
        {
            get => (turretType as AttackTurretSO).attackDamage;
        }
        public float AttackRangeSO
        {
            get => (turretType as AttackTurretSO).attackRange;
        }
        public override void Init()
        {
            attackRange = AttackRangeSO;
            base.Init();
        }
     
        public virtual void Shoot()
        {
            if (animator)
            {
                animator.SetTrigger(ShootTriggerAnim);
            }
        }

        public bool DetectEntity()
        {
            int layerMask = 1 << gameObject.layer;
            layerMask = ~layerMask;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward , hitInfo: out hit, maxDistance: attackRange, layerMask))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("enemy"))
                {
                    return true;
                }
            }

            return false;
        }

        public void SetAttackRange(float newAttackRange)
        {
            attackRange = newAttackRange;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward * attackRange));
        }

        protected abstract IEnumerator AttackEnemies();
    }
}