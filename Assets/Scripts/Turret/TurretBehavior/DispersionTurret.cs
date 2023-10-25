using System;
using System.Collections;
using System.Collections.Generic;
using Turret;
using UnityEngine;
using UnityEngine.Events;

public class DispersionTurret : AttackTurret
{
    public UnityEvent onAttack;
    
    public void Update()
    {
        if (!isAlive) return;
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
        hits = Physics.RaycastAll(transform.position, transform.forward*AttackRange, AttackRange);

        onAttack.Invoke();
        base.Shoot();
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.transform.TryGetComponent<IHealthComponent>(out var entity)&& hit.collider.CompareTag("enemy"))
            {
                entity.ReceiveDamage(AttackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward *AttackRange);
    }
}