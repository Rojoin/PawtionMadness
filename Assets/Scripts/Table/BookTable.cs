using System.Collections;
using Player;
using UnityEngine;

namespace Table
{
    public class BookTable : Table
    {
        [SerializeField] private VoidChannelSO activateRecipesCanvasChannel;
        private bool isCanvasActive = false;
        private Coroutine isPlayerOnObject;
 
        public override void OnInteraction(PlayerInventory playerInventory = null, PlayerInteract playerInteract = null)
        {
            OnInteract.Invoke();
            isCanvasActive = !isCanvasActive;
            activateRecipesCanvasChannel.RaiseEvent();
            if (!isCanvasActive)
            {
                StopCoroutine(isPlayerOnObject);
                OnItemDrop.Invoke();
            }
            else
            {
                isPlayerOnObject = StartCoroutine(CheckIfPlayerIsColliding(playerInteract));
            }

        }

        private IEnumerator CheckIfPlayerIsColliding(PlayerInteract playerInteract)
        {
            while (playerInteract.IsPlayerLookingAtObject(this.gameObject) && isCanvasActive)
            {
                yield return null;
            }
            isCanvasActive = false;
            activateRecipesCanvasChannel.RaiseEvent();
            OnItemDrop.Invoke();
            Debug.Log("Not looking");
            yield break;
        }
        public void setCanvasActiveBool(bool state = false)
        {
            isCanvasActive = state;
        }
    }
}