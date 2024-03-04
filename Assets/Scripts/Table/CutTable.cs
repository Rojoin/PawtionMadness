using System;
using Item;
using Player;
using UnityEngine;
using UnityEngine.Events;
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
        public UnityEvent OnCut;


        public void Awake()
        {
            ResetCount();
        }

        public override void OnInteraction(PlayerInventory playerInventory = null, PlayerInteract playerInteract = null)
        {
            OnInteract.Invoke();
            if (!playerInventory.hasPickable() && ingredient)
            {
                if (cutCounter < ingredient.InteractionToProcesses)
                {
                    CutIngredient();
                    OnCut.Invoke();
                }
                else
                {
                    ingredient.SetIconVisible(true);
                    playerInventory.SetPickable(ingredient);
                    ingredient = null;
                    ResetCount();
                    OnItemPickUp.Invoke();
                    Debug.Log("OnItemPickUp:Invoked");
                }
            }
            else if (playerInventory.hasIngredient() && !(playerInventory.GetPickable() as Ingredient).IsProcessed() &&
                     !ingredient)
            {
                playerInventory.GetPickable().SetNewParent(_itemPos);
                ingredient = playerInventory.GetPickable() as Ingredient;
                playerInventory.NullPickable();
                progressBar.enabled = true;
                ingredient.SetIconVisible(true);
                progressBar.fillAmount = 0;
                OnItemDrop.Invoke();
            }
            else
            {
                OnFailedInteraction.Invoke();
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
                var cutIngredient = Instantiate(ingredient.GetIngredientSO().cutIngredient);
                cutIngredient.SetNewParent(_itemPos);
                Destroy(ingredient.gameObject);
                ingredient = cutIngredient;
                ingredient.SetProcessed();
            }
        }
    }
}