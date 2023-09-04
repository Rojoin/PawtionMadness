using Health;
using Turret;
using UnityEngine;

public abstract class AttackTurret : BaseTurret
{
    [SerializeField] private float shootSpeed;
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackRange;

    private float shootSpeedTimer;

    public float ShootSpeedTimer { get => shootSpeedTimer; set => shootSpeedTimer = value; }
    public float ShootSpeed { get => shootSpeed; set => shootSpeed = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }
    public int AttackRange { get => attackRange; set => attackRange = value; }

    public abstract void Shoot();

    public bool DetectEntity()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, AttackRange))
        {
            if (hit.collider.gameObject.TryGetComponent<EntityHealth>(out var entity))
            {
                return true;
            }
        }

        return false;
    }
}
