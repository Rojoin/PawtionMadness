using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DispersionTurret : AttackTurret
{
    public UnityEvent onAttack;

    public void Update()
    {
        shootSpeedTimer += Time.deltaTime;

        if (shootSpeedTimer > ShootSpeed)
        {
            shootSpeedTimer = 0;
            if (DetectEntity())
            {
                Shoot();
            }
        }
    }

    public override void Shoot()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, AttackRange,LayerMask.NameToLayer("Enemy"));

        onAttack.Invoke();
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.transform.TryGetComponent<IHealthComponent>(out var entity))
            {
                entity.ReceiveDamage(AttackDamage);
                Destroy(gameObject);
            }
        }
    }
}