using UnityEngine;

public class TimeController : ScriptableObject
{
    public const float defaultTimeScale = 1.0f;
    [Range(0.25f,1.0f)]
    public float slowTimeScale = 0.5f;
}