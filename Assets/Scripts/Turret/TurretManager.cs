using Enemy;
using Turret;
using UnityEngine;
using UnityEngine.Pool;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private Transform maxDistancePoint;

    private ObjectPool<GameObject> proyectileTurretPool;
    private ObjectPool<GameObject> dispersionTurretPool;
    private ObjectPool<GameObject> explosiveTurretPool;
    private ObjectPool<GameObject> defenseTurretPool;
    private ObjectPool<GameObject> dadDefenseTurretPool;

    [SerializeField] private ProyectileTurretSO ProyectileTurret;
    [SerializeField] private AttackTurretSO DispersionTurret;
    [SerializeField] private InstantTurretSO ExplosiveTurret;
    [SerializeField] private BaseTurretSO DefenseTurret;
    [SerializeField] private BaseTurretSO badDefenseTurret;

    [SerializeField] private int TurretPoolSize;

    private TurretFactory turretFactory = new TurretFactory();

    private void Awake()
    {
        proyectileTurretPool = new ObjectPool<GameObject>(() => Instantiate(ProyectileTurret.asset.gameObject, transform),
        turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
        turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100);

        dispersionTurretPool = new ObjectPool<GameObject>(() => Instantiate(ProyectileTurret.asset.gameObject, transform),
        turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
        turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100);

        explosiveTurretPool = new ObjectPool<GameObject>(() => Instantiate(ProyectileTurret.asset.gameObject, transform),
        turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
        turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100);

        defenseTurretPool = new ObjectPool<GameObject>(() => Instantiate(ProyectileTurret.asset.gameObject, transform),
        turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
        turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100);

        dadDefenseTurretPool = new ObjectPool<GameObject>(() => Instantiate(ProyectileTurret.asset.gameObject, transform),
        turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
        turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100);
    }


    public BaseTurret AddNewTurret(BaseTurretSO newTurret, Transform position, Transform parent)
    {
        return turretFactory.NewTurretConfigure(newTurret, position, maxDistancePoint, parent);
    }

}
