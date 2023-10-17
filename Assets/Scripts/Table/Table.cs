using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Table
{
    public class Table : MonoBehaviour, ITableInteractable
    {
        public UnityEvent OnInteract { get; set; } = new UnityEvent();
        public virtual void OnInteraction(PlayerInventory playerInventory = null, PlayerInteract playerInteract = null)
        {
            Debug.Log("Interaction!", gameObject);
            OnInteract.Invoke();
        }

        public bool TryInteract()
        {
            throw new System.NotImplementedException();
        }
    }
}