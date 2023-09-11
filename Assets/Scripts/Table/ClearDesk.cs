using Item;
using UnityEngine;

namespace Table
{
    public class ClearDesk : Table
    {
        private Pickable _pickable;
        [SerializeField] private Transform _itemPos;

        public override void OnInteraction(PlayerInventory playerInventory = null)
        {
            if (!_pickable && playerInventory.hasPickable())
            {
                playerInventory.GetPickable().SetNewParent(_itemPos);
                _pickable = playerInventory.GetPickable();
                playerInventory.NullPickable();
            }
            else if (_pickable && !playerInventory.hasPickable())
            {
                playerInventory.SetPickable(_pickable);
                _pickable = null;
            }
        }
    }
}