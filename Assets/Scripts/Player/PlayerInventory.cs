using System.Collections;
using System.Collections.Generic;
using Pickable;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform pickableSpot;
    [SerializeField] private Pickable.Pickable _pickable;


    public bool hasPickable()
    {
        return _pickable != null;
    }

    public void SetPickable(Pickable.Pickable item)
    {
        _pickable = Instantiate(item, pickableSpot.position, Quaternion.identity, pickableSpot);
    }

    public void DestroyPickable()
    {
        Destroy(_pickable.gameObject);
        _pickable = null;
    }

    public bool hasPotion() => _pickable is Potion;

    public Pickable.Pickable GetPickable() => _pickable;
}