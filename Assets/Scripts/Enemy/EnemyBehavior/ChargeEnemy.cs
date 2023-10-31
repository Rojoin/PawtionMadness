using UnityEngine;
using Enemy;

public class ChargeEnemy : BaseEnemy
{
    private bool stopMoving;
    private bool canAttack;
    private bool isCharging;
    private float timer;
    private Vector3 initRayPosition;

    public float ChargeSpeed
    {
        get => (enemyType as EnemyChargeSO).chargeSpeed;
    }

    public float ChargeDamage
    {
        get => (enemyType as EnemyChargeSO).chargeDamage;
    }

    public override void Init()
    {
        base.Init();
        stopMoving = false;
        canAttack = false;
        isCharging = true;
    }

    //TODO:Cambiar logica update a IEnumerator
    //TODO:Hacer FST
    private void Update()
    {
        DetectEntity();

        if (!canAttack)
        {
            timer += Time.deltaTime;
        }

        if (timer > enemyType.attackSpeed)
        {
            canAttack = true;
        }

        if (!stopMoving)
        {
            Movement();
        }

    }

    public void Movement()
    {
        if (isCharging)
        {
            transform.position += transform.forward * (Time.deltaTime * ChargeSpeed);
        }
        else
        {
            transform.position += transform.forward * (Time.deltaTime * MoveSpeed);
        }

        initRayPosition = transform.position + (-transform.forward * transform.localScale.x / 2);
    }

    private void Attack(IHealthComponent targetDamage)
    {
        if (isCharging)
        {
            targetDamage.ReceiveDamage(ChargeDamage);
            isCharging = false;
        }
        else
        {
            targetDamage.ReceiveDamage(Damage);
        }

        canAttack = false;
        timer -= enemyType.attackSpeed;

        if (!targetDamage.IsAlive())
        {
            stopMoving = false;
        }

    }

    private void DetectEntity()
    {
        int layerMask = 1 << gameObject.layer;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(initRayPosition, transform.forward, out hit, AttackRange, layerMask))
        {
            if (hit.collider.gameObject.TryGetComponent<IHealthComponent>(out var entity) && canAttack)
            {
                stopMoving = true;
                if (canAttack)
                {
                    Attack(entity);
                }
            }
        }
        else if (stopMoving && canAttack)
        {
            stopMoving = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(initRayPosition, initRayPosition + transform.forward * AttackRange);
        Gizmos.DrawSphere(initRayPosition, 0.01f);
    }
}
