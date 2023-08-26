using System;
using Turret;
using UnityEngine;

public abstract class AttackTurret : BaseTurret
{
    [SerializeField] private float shootSpeed;
    [SerializeField] private int attackDamage;

    private float shootSpeedTimer;

    public float ShootSpeedTimer { get => shootSpeedTimer; set => shootSpeedTimer = value; }
    public float ShootSpeed { get => shootSpeed; set => shootSpeed = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }

    public abstract void Shoot();
}
