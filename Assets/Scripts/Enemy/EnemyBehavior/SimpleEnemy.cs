using System.Collections;
using UnityEngine;
using Enemy;

public class SimpleEnemy : BaseEnemy
{
    protected bool stopMoving;
    protected bool canAttack;
    protected float timer;
    protected Vector3 initRayPosition;
    protected static readonly int AttackTrigger = Animator.StringToHash("Attack");
    protected Transform currentObjective;

    public override void Init()
    {
        base.Init();
        stopMoving = false;
        canAttack = false;
    }

    //TODO:Cambiar logica update a IEnumerator
    //TODO:Hacer FST
    protected void Update()
    {
        if (!isAlive) return;

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
        transform.position += transform.forward * (Time.deltaTime * MoveSpeed);
        initRayPosition = transform.position + (-transform.forward * transform.localScale.x / 2);
    }

    protected virtual IEnumerator Attack(IHealthComponent targetDamage)
    {
        canAttack = false;
        timer -= enemyType.attackSpeed;
        _animator?.SetTrigger(AttackTrigger);
        yield return new WaitForSeconds(enemyType.attackDelay);
        targetDamage.ReceiveDamage(Damage);
        onAttack.Invoke();
        if (!targetDamage.IsAlive())
        {
            stopMoving = false;
        }

        yield break;
    }

    protected void DetectEntity()
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
                    isAttacking = StartCoroutine(Attack(entity));
                    currentObjective = hit.collider.gameObject.transform;
                }
            }
        }
        else if (stopMoving && canAttack)
        {
            stopMoving = false;
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(initRayPosition, initRayPosition + transform.forward * AttackRange);
        Gizmos.DrawSphere(initRayPosition, 0.01f);
    }
}