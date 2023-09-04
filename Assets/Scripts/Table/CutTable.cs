using System;
using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Table
{
    public class CutTable : Table
    {
        
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
            if (!playerInventory.hasPickable())
            {
                if (cutCounter < ingredient.interactionToProcesses)
                {
                    CutIngredient();
                }
                else
                {
                    playerInventory.SetPickable(ingredient);
                    Destroy(ingredient.gameObject);
                    ResetCount();
                }
            }
            else if (playerInventory.hasIngredient())
            {
                ingredient = Instantiate(playerInventory.GetPickable() as Ingredient, _itemPos.position, Quaternion.identity,
                    _itemPos);
                playerInventory.DestroyPickable();
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
            float progressBarFillAmount = (cutCounter / ingredient.interactionToProcesses);
            progressBar.fillAmount = progressBarFillAmount;
            if (progressBarFillAmount == 1)
            {
                ingredient.SetProcessed();
            }
        }
    }
}