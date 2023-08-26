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
    }
    private void Update()
    {
        if (!canAttack) 
        {
            timer += Time.deltaTime;
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
        Rigidbody.velocity = transform.forward * MoveSpeed;
    }

    private void Attack(EntityHealth targetDamage)
    {
        targetDamage.ReceiveDamage(Damage);
        canAttack = false;
        timer -= timerCooldown;

        if (!targetDamage.IsAlive)
        {
            stopMoving = false;
        }

        Debug.Log("Attack");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<EntityHealth>(out var entity))
        {
            stopMoving = true;
            if (canAttack)
            {
                Attack(entity);
            }
        }
    }
}
