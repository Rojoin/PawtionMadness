using Enemy;
using UnityEngine;

public class EnemyFactory
{
    public void NewEnemyConfigure(ref GameObject enemy ,Transform position)
    {
        enemy.transform.rotation = position.rotation;
        enemy.transform.position = position.position;
        enemy.GetComponent<BaseEnemy>().Init();
    }
}
