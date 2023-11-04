using UnityEngine;
using UnityEngine.Pool;

public class TurretManager : MonoBehaviour
{
    private TurretFactory turretFactory = new TurretFactory();
    [SerializeField] private Transform maxDistancePoint;
    [SerializeField] private Transform parent;

    public void AddNewTurret(BaseTurretSO newTurret, Transform position)
    {
        turretFactory.NewTurretConfigure(newTurret, position, maxDistancePoint, parent);
    }
}
