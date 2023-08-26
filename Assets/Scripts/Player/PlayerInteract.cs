using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Pickable _pickable;
        [SerializeField] private float _interactDistance;
        [SerializeField] private VoidChannelSO InteractChannel;


        private void OnEnable()
        {
            InteractChannel.Subscribe(OnInteract);
        }

        private void OnDisable()
        {
            InteractChannel.Unsubscribe(OnInteract);
        }


        public void OnInteract()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, _interactDistance) )
            {
                if (raycastHit.transform.TryGetComponent<ITableInteractable>(out var interactable))
                {
                    interactable.OnInteraction();
                }
            }


            if (_pickable == null)
            {
            }
        }
    }
}