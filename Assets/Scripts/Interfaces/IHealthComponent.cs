using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthComponent
{
    public void ReceiveDamage(float damage);
    public void Death();
    public bool IsAlive();
}
