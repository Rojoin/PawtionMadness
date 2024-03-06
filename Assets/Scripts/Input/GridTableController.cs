using System;
using Grid;
using Player;
using UnityEngine;

namespace Table
{
    public class GridTableController : Table
    {
        [SerializeField] private VoidChannelSO ChangeToGridChannel;
        [SerializeField] public VoidChannelSO OnBackInput;
        public override void OnInteraction(PlayerInventory playerInventory = null,PlayerInteract playerInteract = null)
        {
            OnInteract.Invoke();
            ChangeToGridChannel.RaiseEvent();
        }
        
    }
}