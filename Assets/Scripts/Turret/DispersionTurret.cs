using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispersionTurret : AttackTurret
{ 
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
        hits = Physics.RaycastAll(transform.position, transform.forward, AttackRange);

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
