using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create KitchenObjectSO", fileName = "KitchenObjectSO", order = 0)]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
    public float timeToCook;
    public int interactionToProcesses;
}