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
                _pickable = Instantiate(playerInventory.GetPickable(), _itemPos.position, Quaternion.identity,
                    _itemPos);
                playerInventory.DestroyPickable();
            }
            else if (_pickable && !playerInventory.hasPickable())
            {
                playerInventory.SetPickable(_pickable);
                Destroy(_pickable.gameObject);
            }
        }
    }
}