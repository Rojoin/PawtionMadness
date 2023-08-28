using System;
using Grid;
using Player;
using UnityEngine;

namespace Table
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private VoidChannelSO ChangeToGridChannel;
        [SerializeField] private VoidChannelSO ChangeToPlayerChannel;
        [SerializeField] private GridController _grid;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerInteract _playerInteract;

        private void OnEnable()
        {
            ChangeToGridChannel.Subscribe(ChangeControllerToGrid);
            ChangeToPlayerChannel.Subscribe(ChangeControllerToMovement);
        }

        private void OnDisable()
        {
            ChangeToGridChannel.Unsubscribe(ChangeControllerToGrid);
            ChangeToPlayerChannel.Unsubscribe(ChangeControllerToMovement);
        }

        public void ChangeControllerToGrid()
        {
            _grid.enabled = true;
            _playerMovement.enabled = false;
            _playerInteract.enabled = false;
        }

        public void ChangeControllerToMovement()
        {
            _grid.enabled = false;
            _playerMovement.enabled = true;
            _playerInteract.enabled = true;
        }
    }
}