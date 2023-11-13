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
    private ObjectPool<GameObject> badDefenseTurretPool;

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

        dispersionTurretPool = new ObjectPool<GameObject>(() => Instantiate(DispersionTurret.asset.gameObject, transform),
        turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
        turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100);

        explosiveTurretPool = new ObjectPool<GameObject>(() => Instantiate(ExplosiveTurret.asset.gameObject, transform),
        turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
        turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100);

        defenseTurretPool = new ObjectPool<GameObject>(() => Instantiate(DefenseTurret.asset.gameObject, transform),
        turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
        turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100);

        badDefenseTurretPool = new ObjectPool<GameObject>(() => Instantiate(badDefenseTurret.asset.gameObject, transform),
        turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
        turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100);
    }


    public BaseTurret AddNewTurret(BaseTurretSO newTurretSO, Transform position, Transform parent)
    {
        GameObject newTurret = null;

        Debug.Log(newTurretSO.turretType);

        switch (newTurretSO.turretType)
        {
            case BaseTurretSO.TurretTypes.Proyectile:
                proyectileTurretPool.Get(out newTurret);
                newTurret.GetComponent<BaseTurret>().onTurretDeath.AddListener(OnKillProyectileTurret);
                break;

            case BaseTurretSO.TurretTypes.Dispersion:
                dispersionTurretPool.Get(out newTurret);
                newTurret.GetComponent<BaseTurret>().onTurretDeath.AddListener(OnKillDispersionTurret);
                break;

            case BaseTurretSO.TurretTypes.Explosive:
                explosiveTurretPool.Get(out newTurret);
                newTurret.GetComponent<BaseTurret>().onTurretDeath.AddListener(OnKillExplosiveTurret);
                break;

            case BaseTurretSO.TurretTypes.Defense:
                defenseTurretPool.Get(out newTurret);
                newTurret.GetComponent<BaseTurret>().onTurretDeath.AddListener(OnKillDefenseTurret);
                break;

            case BaseTurretSO.TurretTypes.BadDefense:
                badDefenseTurretPool.Get(out newTurret);
                newTurret.GetComponent<BaseTurret>().onTurretDeath.AddListener(OnKillBadDefenseTurret);
                break;

            default:
                break;
        }

        turretFactory.NewTurretConfigure(newTurret, position, maxDistancePoint);
        newTurret.transform.SetParent(parent);
        return newTurret.GetComponent<BaseTurret>();

    }

    private void OnKillProyectileTurret(GameObject proyectileTurret)
    {
        proyectileTurret.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillProyectileTurret);
        proyectileTurretPool.Release(proyectileTurret);
    }

    private void OnKillDispersionTurret(GameObject dispersionTurret)
    {
        dispersionTurret.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillDispersionTurret);
        dispersionTurretPool.Release(dispersionTurret);
    }

    private void OnKillExplosiveTurret(GameObject explosiveTurret)
    {
        explosiveTurret.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillExplosiveTurret);
        explosiveTurretPool.Release(explosiveTurret);
    }

    private void OnKillDefenseTurret(GameObject defenseTurret)
    {
        defenseTurret.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillDefenseTurret);
        defenseTurretPool.Release(defenseTurret);
    }

    private void OnKillBadDefenseTurret(GameObject badDefenseTurret)
    {
        badDefenseTurret.GetComponent<BaseEnemy>().onDeath.RemoveListener(OnKillBadDefenseTurret);
        badDefenseTurretPool.Release(badDefenseTurret);
    }
}
