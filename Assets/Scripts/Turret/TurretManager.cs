using Enemy;
using System;
using System.Collections.Generic;
using Turret;
using UnityEngine;
using UnityEngine.Pool;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private Transform maxDistancePoint;
    [SerializeField] private int TurretPoolSize;

    [SerializeField] private List<BaseTurretSO> turretByIds = new List<BaseTurretSO>();
    private Dictionary<string, ObjectPool<GameObject>> pools = new();

    private TurretFactory turretFactory = new TurretFactory();

    private void Awake()
    {
        foreach (var t in turretByIds)
        {
            pools.Add(t.id, new ObjectPool<GameObject>(() => Instantiate(t.asset.gameObject, transform),
            turret => { turret.gameObject.SetActive(true); }, turret => { turret.gameObject.SetActive(false); },
            turret => { Destroy(turret.gameObject); }, false, TurretPoolSize, 100));
        }
    }


    public BaseTurret AddNewTurret(BaseTurretSO newTurretSO, Transform position, Transform parent)
    {
        var pool = pools[newTurretSO.id];
        if(pool == null)
        {
            Debug.LogError("Pool not found");
            return null;
        }
        GameObject newTurret = null;
        pool.Get(out newTurret);
        newTurret.GetComponent<BaseTurret>().onDeath.AddListener(OnKillTurret);

        turretFactory.NewTurretConfigure(newTurret, position, maxDistancePoint);
        newTurret.transform.SetParent(parent);
        return newTurret.GetComponent<BaseTurret>();

    }

    private void OnKillTurret(GameObject Turret)
    {
        BaseTurret baseTurret = Turret.GetComponent<BaseTurret>();
        baseTurret.onDeath.RemoveListener(OnKillTurret);
        ObjectPool<GameObject> pool = pools[baseTurret.TurretType.id];
        pool.Release(Turret);
    }
}
