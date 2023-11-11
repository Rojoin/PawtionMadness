using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraLerp : MonoBehaviour
{
    [Header("Lerp Camera")]
    public Transform cameraPosition;
    public Transform player;
    public Transform filedPosition;

    [Header("Timers")]
    [SerializeField] private float initalTime;
    [SerializeField] private float watchFieldTime;
    [SerializeField] private float finalTime;

    [Header("Enemies To Show")]
    [SerializeField] private Transform SpawnPosition;
    [SerializeField] private float SpawnWidth;
    [SerializeField] private float SpawnHeigh;
    public EnemiesToShow[] enemiesToShow;
    private List<GameObject> enemiesShowedList = new List<GameObject>();

    public UnityEvent endCameraLerp;

    private float lerpMove = 0;
    private Transform startPosition;
    private Transform finalPosition;

    private void Awake()
    {
        StartCoroutine(LerpSequenze());
    }

    private IEnumerator LerpSequenze()
    {
        SpawnEnemies();

        yield return new WaitForSeconds(initalTime);

        startPosition = player;
        finalPosition = filedPosition;
        yield return new WaitWhile(LerpPosition);

        yield return new WaitForSeconds(watchFieldTime);

        startPosition = filedPosition;
        finalPosition = player;
        yield return new WaitWhile(LerpPosition);

        yield return new WaitForSeconds(finalTime);
        endCameraLerp.Invoke();

        DeleteEnemies();
    }

    private bool LerpPosition()
    {
        lerpMove += Time.deltaTime;
        cameraPosition.position = Vector3.Lerp(startPosition.position, finalPosition.position, lerpMove);

        if (lerpMove > 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToShow.Length; i++)
        {
            for (int j = 0; j < enemiesToShow[i].cuantity; j++)
            {
                GameObject newEnemy = Instantiate(enemiesToShow[i].Type.asset,
                    new Vector3(
                    Random.Range(SpawnPosition.transform.position.x, SpawnPosition.transform.position.x - SpawnHeigh), 
                    SpawnPosition.transform.position.y, 
                    Random.Range(SpawnPosition.transform.position.z, SpawnPosition.transform.position.z + SpawnWidth)),
                    transform.rotation);

                enemiesShowedList.Add(newEnemy);
            }
        }
    }

    private void DeleteEnemies()
    {
        foreach (var enemy in enemiesShowedList)
        {
            Destroy(enemy);
        }

        enemiesShowedList.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(SpawnPosition.position.x - SpawnHeigh / 2, SpawnPosition.position.y, SpawnPosition.position.z + SpawnWidth / 2), new Vector3(SpawnHeigh, 1, SpawnWidth));
    }
}
