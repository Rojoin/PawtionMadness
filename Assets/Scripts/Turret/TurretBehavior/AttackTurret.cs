using UnityEngine;

namespace Turret
{
    public abstract class AttackTurret : BaseTurret
    {
        private static readonly int ShootTriggerAnim = Animator.StringToHash("Shoot");
        protected float shootSpeedTimer = 0;

        public float ShootSpeed
        {
            get => (turretType as AttackTurretSO).shootSpeed;
        }
        public int AttackDamage
        {
            get => (turretType as AttackTurretSO).attackDamage;
        }
        public int AttackRange
        {
            get => (turretType as AttackTurretSO).attackRange;
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
            if (Physics.Raycast(transform.position, transform.forward , hitInfo: out hit, maxDistance: AttackRange,layerMask))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("enemy"))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, transform.position + (transform.forward * AttackRange));
        }
    }
}