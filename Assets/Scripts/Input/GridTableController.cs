using System;
using Grid;
using UnityEngine;

namespace Table
{
    public class GridTableController : Table
    {
        [SerializeField] private VoidChannelSO ChangeToGridChannel;
        public override void OnInteraction()
        {
            ChangeToGridChannel.RaiseEvent();
        }
    }
}