using Turret;
using UnityEngine;
using UnityEngine.Pool;

public class TurretManager : MonoBehaviour
{
    private TurretFactory turretFactory = new TurretFactory();
    [SerializeField] private Transform maxDistancePoint;


    public BaseTurret AddNewTurret(BaseTurretSO newTurret, Transform position, Transform parent)
    {
        return turretFactory.NewTurretConfigure(newTurret, position, maxDistancePoint, parent);
    }
}
