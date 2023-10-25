using System;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Create PotionRecipeSO", fileName = "PotionRecipeSO", order = 0)]
public class PotionRecipeSO : ScriptableObject
{
    public Potion potion;
    public List<IngredientData> recipe;

    void OnValidate()
    {
        foreach (IngredientData data1 in recipe)
        {
            foreach (IngredientData data2 in recipe)
            {
                if (data1 == data2)
                {
                    continue;
                }

                if (data1.item == data2.item)
                {
                    data2.item = null;
                    data2.amount = 1;
                }
            }
        }
    }

    public bool IsSameRecipe(List<IngredientData> otherRecipe)
    {
        for (int i = 0; i < recipe.Count; i++)
        {
            IngredientData data01 = recipe[i];
            bool isItemFound = false;
            for (var j = 0; j < otherRecipe.Count; j++)
            {
                var data02 = otherRecipe[j];

                if (data01.item == data02.item)
                {
                    isItemFound = true;
                    if (data01.amount != data02.amount)
                    {
                        return false;
                    }
                    break;
                }
            }

            if (!isItemFound)
            {
                return false;
            }
        }

        return true;
    }
}

[Serializable]
public class IngredientData
{
    public KitchenObjectSO item;
    public int amount = 1;
}