using UnityEngine;
using Enemy;
using Health;

public class SimpleEnemy : BaseEnemy
{
    private bool stopMoving;
    private bool canAttack;
    private float timerCooldown;
    private float timer;

    private void Awake()
    {
        stopMoving = false;
        canAttack = false;
        timerCooldown = AttackSpeed;
    }

    //TODO:Cambiar logica update a IEnumerator
    //TODO:Hacer FST
    private void Update()
    {
        DetectEntity();

        if (!canAttack)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }

        if (timer > timerCooldown)
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
    }

    private void Attack(IHealthComponent targetDamage)
    {
        if (canAttack)
        {
            targetDamage.ReceiveDamage(Damage);
            canAttack = false;
            timer -= timerCooldown;
        }

        if (!targetDamage.IsAlive())
        {
            stopMoving = false;
        }

        Debug.Log("Attack");
    }

    private void DetectEntity()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, AttackRange))
        {
            if (hit.collider.gameObject.TryGetComponent<IHealthComponent>(out var entity))
            {
                stopMoving = true;
                if (canAttack)
                {
                    Attack(entity);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * AttackRange);
    }
}
