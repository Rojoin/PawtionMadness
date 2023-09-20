using Health;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private float lifeTime;
    public UnityEvent onCollision;
    private float lifeTimer;
    private float projectileSpeed;
    private float damage;

    public float Damage
    {
        get => damage;
        set => damage = value;
    }
    public float ProjectileSpeed
    {
        get => projectileSpeed;
        set => projectileSpeed = value;
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer > lifeTime)
        {
            Destroy(gameObject);
        }

        transform.position += transform.forward * (Time.deltaTime * ProjectileSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IHealthComponent>(out var entity))
        {
            entity.ReceiveDamage(Damage);
            var hit = Instantiate(hitParticle);
            hit.Play();
            Destroy(gameObject);
        }
    }
}