using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerStats", fileName = "PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject
{
    public float speed;
    public float interactDistance;
    public bool shouldGridControllerReset = false;
}