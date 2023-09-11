﻿using System;
using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Table
{
    public class CutTable : Table
    {
        [SerializeField] private CuttingRecipeSO[] _cuttingRecipeArray;
        [SerializeField] private Image progressBar;
        [SerializeField] private Ingredient ingredient;
        [SerializeField] private Transform _itemPos;
         private float cutCounter;

         public void Awake()
         {
             ResetCount();
         }

         public override void OnInteraction(PlayerInventory playerInventory = null)
        {
            
            if (!playerInventory.hasPickable() && ingredient)
            {
                if (cutCounter < ingredient.InteractionToProcesses)
                {
                    CutIngredient();
                }
                else
                {
                    playerInventory.SetPickable(ingredient);
                    ingredient = null;
                    ResetCount();
                }
            } 
            else if (playerInventory.hasIngredient())
            {
                playerInventory.GetPickable().SetNewParent(_itemPos);
                ingredient = playerInventory.GetPickable() as Ingredient;
                playerInventory.NullPickable();
                progressBar.enabled = true;
                progressBar.fillAmount = 0;
            }
        }

        private void ResetCount()
        {
            cutCounter = 0;
            progressBar.enabled = false;
        }

        
        private void CutIngredient()
        {
            if (!progressBar.enabled)
            {
                progressBar.enabled = true;
            }
            cutCounter++;
            float progressBarFillAmount = (cutCounter / ingredient.InteractionToProcesses);
            progressBar.fillAmount = progressBarFillAmount;
            if (progressBarFillAmount == 1)
            {
                ingredient.SetProcessed();
            }
        }
    }
}