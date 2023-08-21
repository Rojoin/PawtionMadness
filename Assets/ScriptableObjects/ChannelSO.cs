using System;
using UnityEngine;

public abstract class ChannelSO<T> : ScriptableObject
{
    private Action<T> dataEvent;
    public void Subscribe(in Action<T> handler)
    {
        Debug.Log($"{name}:Suscribed.");
        dataEvent += handler;
    }
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    public void Unsubscribe(in Action<T> handler)
    {
        Debug.Log($"{name}:Unsuscribed.");
        dataEvent -= handler;
    }

    public void RaiseEvent(T data)
    {
        dataEvent?.Invoke(data);
    }
}

public abstract class ChannelSO<T1, T2> : ScriptableObject
{
    private Action<T1, T2> dataEvent;
    public void Subscribe(in Action<T1, T2> handler)
    {
        dataEvent += handler;
    }
    public void Unsubscribe(in Action<T1, T2> handler)
    {
        dataEvent -= handler;
    }

    public void RaiseEvent(T1 data1, T2 data2)
    {
        if (dataEvent == null)
        {
            Debug.LogWarning($"{name}:No subscribers to this event.");
        }
        else
        {
            dataEvent?.Invoke(data1, data2);
        }
    }
}

public abstract class ChannelSO<T1, T2, T3> : ScriptableObject
{
    private Action<T1, T2, T3> dataEvent;
    public void Subscribe(Action<T1, T2, T3> handler)
    {
        dataEvent += handler;
    }
    public void Unsubscribe(in Action<T1, T2, T3> handler)
    {
        dataEvent -= handler;
    }

    public void RaiseEvent(T1 data1, T2 data2, T3 data3)
    {
        if (dataEvent == null)
        {
            Debug.LogWarning($"{name}:No subscribers to this event.");
        }
        else
        {
            dataEvent?.Invoke(data1, data2, data3);
        }
    }
}
public abstract class ChannelSO<T1, T2, T3, T4> : ScriptableObject
{
    private Action<T1, T2, T3, T4> dataEvent;
    public void Subscribe(Action<T1, T2, T3, T4> handler)
    {
        dataEvent += handler;
    }
    public void Unsubscribe(in Action<T1, T2, T3, T4> handler)
    {

        dataEvent -= handler;
    }

    public void RaiseEvent(T1 data1, T2 data2, T3 data3, T4 data4)
    {
        if (dataEvent == null)
        {
            Debug.LogWarning($"{name}:No subscribers to this event.");
        }
        else
        {
            dataEvent?.Invoke(data1, data2, data3, data4);
        }
    }
}
public abstract class ChannelSO<T1, T2, T3, T4, T5> : ScriptableObject
{
    private Action<T1, T2, T3, T4, T5> dataEvent;
    public void Subscribe(Action<T1, T2, T3, T4, T5> handler)
    {
        dataEvent += handler;
    }
    public void Unsubscribe(in Action<T1, T2, T3, T4, T5> handler)
    {
        dataEvent -= handler;
    }

    public void RaiseEvent(T1 data1, T2 data2, T3 data3, T4 data4, T5 data5)
    {
        if (dataEvent == null)
        {
            Debug.LogWarning($"{name}:No subscribers to this event.");
        }
        else
        {
            dataEvent?.Invoke(data1, data2, data3, data4, data5);
        }
    }
}

