using Turret;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Turret", menuName = "Create Turret/Base Turret")]
public class BaseTurretSO : ScriptableObject
{
    public BaseTurret asset;
    public float maxHealth;
    public Sprite sprite;
}