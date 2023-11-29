using Item;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Table
{
    public class ClearDesk : Table
    {
        private Pickable _pickable;
        [SerializeField] private Transform _itemPos;

        public override void OnInteraction(PlayerInventory playerInventory = null,PlayerInteract playerInteract = null)
        {
            OnInteract.Invoke();
            if (!_pickable && playerInventory.hasPickable())
            {
                playerInventory.GetPickable().SetNewParent(_itemPos);
                _pickable = playerInventory.GetPickable();
                playerInventory.NullPickable();
                OnItemDrop.Invoke();
            }
            else if (_pickable && !playerInventory.hasPickable())
            {
                playerInventory.SetPickable(_pickable);
                _pickable = null;
                OnItemPickUp.Invoke();
            }
            else
            {
                OnFailedInteraction.Invoke(); 
            }
        }
    }
}