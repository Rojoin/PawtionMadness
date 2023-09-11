using UnityEngine;

[CreateAssetMenu(menuName = "Create CuttingRecipeSO", fileName = "CuttingRecipeSO", order = 0)]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO inputIngredient;
    public KitchenObjectSO processedIngredient;
}