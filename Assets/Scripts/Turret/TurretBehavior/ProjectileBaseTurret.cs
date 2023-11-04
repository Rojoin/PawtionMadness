using System.Collections;
using Turret;
using UnityEngine;

public class ProjectileBaseTurret : AttackTurret
{   
    public Projectile Projectile
    {
        get => (turretType as ProyectileTurretSO).projectile;
    }
    public Transform InitialShootPosition
    {
        get => (turretType as ProyectileTurretSO).initialShootPosition;
    }
    public float ProjectileSpeed
    {
        get => (turretType as ProyectileTurretSO).projectileSpeed;
    }
    public float ProjectileQuantity
    {
        get => (turretType as ProyectileTurretSO).projectileQuantity;
    }

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
        for (int i = 0; i < ProjectileQuantity; i++)
        {
            base.Shoot();
            StartCoroutine(AttackEnemies());
        }
    }

    protected override IEnumerator AttackEnemies()
    {
        yield return new WaitForSeconds(AttackAnimDelay);
        onAttack.Invoke();
        Projectile newProjectile = Instantiate(Projectile, transform.position, transform.rotation);
        newProjectile.ProjectileSpeed = ProjectileSpeed;
        newProjectile.Damage = AttackDamage;
        yield break;
    }
}