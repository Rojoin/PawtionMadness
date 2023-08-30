using Interfaces;
using Player;
using UnityEngine;

namespace Table
{
    public class Table : MonoBehaviour, ITableInteractable
    {
        public virtual void OnInteraction(PlayerInventory playerInventory = null)
        {
            Debug.Log("Interaction!", gameObject);
        }

        public bool TryInteract()
        {
            throw new System.NotImplementedException();
        }
    }
}