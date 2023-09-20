using System;
using Grid;
using Player;
using UnityEngine;

namespace Table
{
    public class GridTableController : Table
    {
        [SerializeField] private VoidChannelSO ChangeToGridChannel;
        public override void OnInteraction(PlayerInventory playerInventory = null,PlayerInteract playerInteract = null)
        {
            ChangeToGridChannel.RaiseEvent();
        }
    }
}