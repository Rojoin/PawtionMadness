using System;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Table
{
    public class Table : MonoBehaviour, ITableInteractable
    {
        private BoxCollider boxCollider;
        protected bool canInteract = true;

        public UnityEvent OnItemPickUp = new();
        public UnityEvent OnItemDrop = new();
        public UnityEvent OnFailedInteraction = new();
        public void OnEnable()
        {
            TryGetComponent<BoxCollider>(out boxCollider);
        }

        public UnityEvent OnInteract { get; set; } = new UnityEvent();
        public virtual void OnInteraction(PlayerInventory playerInventory = null, PlayerInteract playerInteract = null)
        {
            Debug.Log("Interaction!", gameObject);
            OnInteract.Invoke();
        }

        public void TryInteract(PlayerInventory playerInventory = null, PlayerInteract playerInteract = null)
        {
            if (canInteract)
            {
                OnInteraction(playerInventory, playerInteract);
            }
        }
        public virtual void InteractionState(bool state = true)
        {
            canInteract = state;
        }
    }
}