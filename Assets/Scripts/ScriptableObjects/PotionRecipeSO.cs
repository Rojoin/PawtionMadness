using Item;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PotionRecipeSO", fileName = "PotionRecipeSO", order = 0)]
public class PotionRecipeSO: ScriptableObject
{
    public KitchenObjectSO[] itemsNeeded;
    public Potion potion;
}