using System;
using Item;
using Player;
using UnityEngine;
using UnityEngine.Events;


namespace Table
{
    public class ItemBoxTable : Table
    {
        [SerializeField] private KitchenObjectSO item;

        public UnityEvent OnItemPickUp = new();
        public UnityEvent OnFailedInteraction = new();
        public override void OnInteraction(PlayerInventory playerInventory = null,PlayerInteract playerInteract = null)
        {
            OnInteract.Invoke();
            if (!playerInventory.hasPickable())
            {
                var ingredientToGive = Instantiate(item.ingredientModel, transform.position, Quaternion.identity);
                ingredientToGive.SetIconVisible(false);
                playerInventory.SetPickable(ingredientToGive);
                OnItemPickUp.Invoke();
            }
            else
            {
                Debug.Log("The player already has a item!");
                OnFailedInteraction.Invoke();
            }
        }
    }
}