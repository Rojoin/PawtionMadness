using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory;

        [SerializeField] private float _interactDistance;
        [SerializeField] private VoidChannelSO InteractChannel;
        
        public UnityEvent<RaycastHit> OnInteractableAtRange { get; set; } = new UnityEvent<RaycastHit>();


        private void OnEnable()
        {
            InteractChannel.Subscribe(OnInteract);
        }

        private void OnDisable()
        {
            InteractChannel.Unsubscribe(OnInteract);
        }

        public void Update()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, _interactDistance))
            {
                OnInteractableAtRange.Invoke(raycastHit);
            }
            else
            {
                RaycastHit hit = new RaycastHit();
                OnInteractableAtRange.Invoke(hit);
            }
        }

        public void OnInteract()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, _interactDistance))
            {
                if (raycastHit.transform.TryGetComponent<ITableInteractable>(out var interactable))
                {
                    interactable.OnInteraction(_playerInventory,this);
                }
            }
        }

        public bool IsPlayerLookingAtObject(GameObject thisObject)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, _interactDistance))
            {
                return raycastHit.collider.gameObject == thisObject;
            }

            return false;
        }
    }
}