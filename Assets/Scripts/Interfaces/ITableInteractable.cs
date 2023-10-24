using Player;

namespace Interfaces
{
    public interface ITableInteractable
    {
        void OnInteraction(PlayerInventory playerInventory = null, PlayerInteract playerInteract = null);
        void TryInteract(PlayerInventory playerInventory = null, PlayerInteract playerInteract = null);
    }
}