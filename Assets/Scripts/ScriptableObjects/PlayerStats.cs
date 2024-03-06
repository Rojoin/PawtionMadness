using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerStats", fileName = "PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject
{
    public float speed;
    public float interactDistance;
    public bool shouldGridControllerReset = false;
    public bool isTutorialOn = false;
    public int ControllerInput { get; private set; } = 0;
    public void ChangeControllerInput(int value)
    {
        ControllerInput = value;
    }
}