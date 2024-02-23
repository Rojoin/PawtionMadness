using UnityEngine;

[CreateAssetMenu(menuName = "Create TimeController", fileName = "TimeController", order = 0)]
public class TimeController : ScriptableObject
{
    public float defaultTimeScale = 1.0f;
    [Range(0.25f,1.0f)]
    public float slowTimeScale = 0.5f;
}