using UnityEngine;
using Enemy;

public class AngryEnemy : ChangingEnemy
{
    private bool stopMoving;
    private bool canAttack;
    private float timer;
    private Vector3 initRayPosition;

    public float AngryAttackSpeed
    {
        get => (enemyType as AngryEnemySO).angryAttackSpeed;
    }

    public float AngryMoveSpeed
    {
        get => (enemyType as AngryEnemySO).angryMoveSpeed;
    }

    public override void Init()
    {
        base.Init();
        stopMoving = false;
        canAttack = false;
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
        if (!changedState)
        {
            transform.position += transform.forward * (Time.deltaTime * MoveSpeed);
            initRayPosition = transform.position + (-transform.forward * transform.localScale.x / 2);
        }
        else
        {
            transform.position += transform.forward * (Time.deltaTime * AngryMoveSpeed);
            initRayPosition = transform.position + (-transform.forward * transform.localScale.x / 2);
        }
    }

    private void Attack(IHealthComponent targetDamage)
    {
        targetDamage.ReceiveDamage(Damage);
        canAttack = false;

        if (!changedState)
        {
            timer -= enemyType.attackSpeed;
            onAttack.Invoke();
        }
        else
        {
            timer -= AngryAttackSpeed;
            onAttack.Invoke();
        }

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
