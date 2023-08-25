using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthComponent
{
   public void ReceiveDamage();
   public void Death();
}
