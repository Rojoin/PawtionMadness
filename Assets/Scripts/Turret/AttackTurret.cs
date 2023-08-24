using System;
using Turret;
using UnityEngine;

public abstract class AttackTurret : BaseTurret
{
    [SerializeField] private float shootSpeed;
    [SerializeField] private int AttackDamage;

    private float shootSpeedTimer;

    public float ShootSpeedTimer { get => shootSpeedTimer; set => shootSpeedTimer = value; }
    public float ShootSpeed { get => shootSpeed; set => shootSpeed = value; }

    public abstract void Shoot();
}
