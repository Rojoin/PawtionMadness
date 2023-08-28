using Interfaces;
using UnityEngine;

namespace Table
{
    public class Table : MonoBehaviour , ITableInteractable
    {
        public virtual void OnInteraction()
        {
            Debug.Log("Interaction!", gameObject);
        }

        public bool TryInteract()
        {
            throw new System.NotImplementedException();
        }
    }
}