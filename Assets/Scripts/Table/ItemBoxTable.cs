using System;
using Item;
using Player;
using UnityEngine;


namespace Table
{
    public class ItemBoxTable : Table
    {
        [SerializeField] private Ingredient item;

        public override void OnInteraction(PlayerInventory playerInventory = null,PlayerInteract playerInteract = null)
        {
            if (!playerInventory.hasPickable())
            {
                var ingredientToGive = Instantiate(item, transform.position, Quaternion.identity);
                ingredientToGive.SetIconVisible(false);
                playerInventory.SetPickable(ingredientToGive);
            }
            else
            {
                Debug.Log("The player already has a item!");
            }
        }
    }
}