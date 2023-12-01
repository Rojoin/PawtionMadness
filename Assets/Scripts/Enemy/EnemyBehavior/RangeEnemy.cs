using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class RangeEnemy : SimpleEnemy
{
    [SerializeField] private ParticleSystem attackParticle;


    protected override IEnumerator Attack(IHealthComponent targetDamage)
    {
        canAttack = false;
        timer -= enemyType.attackSpeed;
        _animator?.SetTrigger(AttackTrigger);
        yield return new WaitForSeconds(enemyType.attackDelay);

        var spell = Instantiate(attackParticle, currentObjective.position, Quaternion.identity);
        targetDamage.ReceiveDamage(Damage);
        onAttack.Invoke();
        if (!targetDamage.IsAlive())
        {
            stopMoving = false;
        }
        Destroy(spell,spell.totalTime);
        yield break;
    }
}