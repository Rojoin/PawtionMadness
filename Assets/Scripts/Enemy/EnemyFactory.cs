using UnityEngine;

public class EnemyFactory
{
    public GameObject SpawnNewEnemy(Transform position, EnemySO enemyType, Transform parent)
    {
        GameObject newEnemy = GameObject.Instantiate(enemyType.asset, position.position, position.rotation, parent);
        return newEnemy;
    }
}
