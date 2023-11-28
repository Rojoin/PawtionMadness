using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class ExplosiveTurret : InstantTurret
{
    [SerializeField] private ParticleSystem explotionFX;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float explotionShakeForce;
    private float timer = 0;
    private bool canAttack = true;
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    public UnityEvent onSpawn;
    public UnityEvent onExplosion;

    private void Start()
    {
        onSpawn.Invoke();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        animator?.SetTrigger(Shoot);
        if (timer >= castTime && canAttack)
        {
            ExplodeTurret();
            canAttack = false;
        }
    }

    private void ExplodeTurret()
    {
        Collider[] hits;
        hits = Physics.OverlapBox(transform.position, new Vector3(range / 2, range / 2, range / 2), transform.rotation);

        var explotion = Instantiate(explotionFX,transform.position,Quaternion.identity);
        explotion.Play();
        onExplosion.Invoke();
        impulseSource.GenerateImpulseWithForce(explotionShakeForce);
        for (int i = 0; i < hits.Length; i++)
        {
            Collider hit = hits[i];

            if (hit.transform.TryGetComponent<IHealthComponent>(out var entity) && hit.CompareTag("enemy"))
            {
                entity.ReceiveDamage(ExplosiveDamage);
            }
        }
        Death();
    }
}