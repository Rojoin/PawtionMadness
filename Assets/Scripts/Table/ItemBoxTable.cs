using System;
using Item;
using Player;
using UnityEngine;


namespace Table
{
    public class ItemBoxTable : Table
    {
        [SerializeField] private Ingredient item;

        public override void OnInteraction(PlayerInventory playerInventory = null)
        {
            if (!playerInventory.hasPickable())
            {
                playerInventory.SetPickable(item);
            }
            else
            {
                Debug.Log("The player already has a item!");
            }
        }
    }
}