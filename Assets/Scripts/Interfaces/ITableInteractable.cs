using Player;

namespace Interfaces
{
    public interface ITableInteractable
    {
        void OnInteraction(PlayerInventory playerInventory = null);
        bool TryInteract();
    }
}