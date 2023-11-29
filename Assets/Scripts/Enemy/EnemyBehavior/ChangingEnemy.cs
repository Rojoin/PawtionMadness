using UnityEngine;
using Enemy;

public class ChangingEnemy : BaseEnemy
{
    public bool changedState;

    public float LifeToChangeState
    {
        get => (enemyType as ChangingEnemySO).lifeToChangeState;
    }

    public override void Init()
    {
        base.Init();
        changedState = false;
    }
    public override void ReceiveDamage(float damage)
    {
        CurrentHealth -= damage;
        onDamage.Invoke();

        if (CurrentHealth <= LifeToChangeState && !changedState)
        {
            changedState = true;
            onSpecialInteraction.Invoke();
        }

        if (CurrentHealth <= 0)
        {
            isAlive = false;
            Death();
        }
    }
}
