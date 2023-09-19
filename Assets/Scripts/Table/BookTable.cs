using UnityEngine;

namespace Table
{
    public class BookTable : Table
    {
        [SerializeField] private VoidChannelSO activateRecipesCanvasChannel;
        private bool isCanvasActive = false;
        public override void OnInteraction(PlayerInventory playerInventory = null)
        {
            isCanvasActive = !isCanvasActive;
            activateRecipesCanvasChannel.RaiseEvent();
        }
    }
}