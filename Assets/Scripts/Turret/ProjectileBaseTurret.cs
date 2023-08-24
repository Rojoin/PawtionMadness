using UnityEngine;

public class ProjectileBaseTurret : AttackTurret
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform initialShootPosition;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileQuantity;

    public void Update()
    {
        ShootSpeedTimer += Time.deltaTime;

        if (ShootSpeedTimer > ShootSpeed)
        {
            ShootSpeedTimer = 0;
            Shoot();
        }
    }
    public override void Shoot()
    {
        for (int i = 0; i < projectileQuantity; i++)
        {
            Projectile newProjectile = Instantiate(projectile, initialShootPosition.position, initialShootPosition.rotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(initialShootPosition.forward * projectileSpeed, ForceMode.Acceleration);
        }
    }
}
