using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Channel/VoidChannelSO",fileName ="VoidChannelSO")]

public class VoidChannelSO : ScriptableObject
{
    private Action voidEvent;
    public void Subscribe(in Action handler)
    {
        voidEvent += handler;
    }
    public void Unsubscribe(in Action handler)
    {
        voidEvent -= handler;
    }

    public void RaiseEvent()
    {
        if (voidEvent == null)
        {
            Debug.LogWarning($"{name}:No subscribers to this event.");
        }
        else
        {
            voidEvent?.Invoke();
        }
    }
}