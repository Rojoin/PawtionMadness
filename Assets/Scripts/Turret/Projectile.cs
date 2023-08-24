using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float lifeTime;
    private float lifeTimer;

    private void Update()
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
