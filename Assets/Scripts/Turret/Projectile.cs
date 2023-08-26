using Health;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float lifeTime;
    private float lifeTimer;
    private float damage;

    public float Damage { get => damage; set => damage = value; }

    private void Update()
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy") && collision.gameObject.TryGetComponent<EntityHealth>(out var entity))
        {
            entity.ReceiveDamage(Damage);
            Destroy(gameObject);
        }
    }
}
