using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Table
{
    public class TrashCan : Table
    {
     
        public override void OnInteraction(PlayerInventory playerInventory = null,PlayerInteract playerInteract = null)
        {
            OnInteract.Invoke();
            if (playerInventory.hasPickable())
            {
                playerInventory.DestroyPickable();
            }
            else
            {
                Debug.Log("The player doesn´t have an Item");
            }
        }
    }
}