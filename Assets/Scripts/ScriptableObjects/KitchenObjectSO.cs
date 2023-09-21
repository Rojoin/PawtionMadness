using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

[CreateAssetMenu(menuName = "Create KitchenObjectSO", fileName = "KitchenObjectSO", order = 0)]
public class KitchenObjectSO : ScriptableObject
{
    public Ingredient prefab;
    public Sprite sprite;
    public string objectName;
    public float timeToCook;
    public int interactionToProcesses;
}