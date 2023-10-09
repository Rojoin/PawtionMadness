using UnityEngine;

public class ExplosiveTurret : InstantTurret
{
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= castTime)
        {
            ExplodeTurret();
            Destroy(gameObject);
        }
    }

    private void ExplodeTurret()
    {
        Collider[] hits;
        hits = Physics.OverlapBox(transform.position, new Vector3(range / 2, range / 2, range / 2), transform.rotation);

        for (int i = 0; i < hits.Length; i++)
        {
            Collider hit = hits[i];

            if (hit.transform.TryGetComponent<IHealthComponent>(out var entity) && hit.CompareTag("enemy"))
            {
                entity.ReceiveDamage(ExplosiveDamage);
            }
        }
    }
}
