using System.Collections;
using System.Collections.Generic;
using Item;
using Turret;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform pickableSpot;
    [SerializeField] private Pickable _pickable;
    [SerializeField] private KitchenObjectSO kitchenObject;
    public UnityEvent<bool> OnItemPickUp;


    public bool hasPickable()
    {
        return _pickable != null;
    }

    public void SetPickable(Item.Pickable item)
    {
        item.SetNewParent(this.pickableSpot);
        _pickable = item;
        OnItemPickUp.Invoke(true);
    }
    

    public void DestroyPickable()
    {
        Destroy(_pickable.gameObject);
        _pickable = null;
        OnItemPickUp.Invoke(false);
    }

    public void NullPickable()
    {
        _pickable = null;
        OnItemPickUp.Invoke(false);
    }

    public bool hasPotion() => _pickable is Potion;
    public bool hasIngredient() => _pickable is Ingredient;

    public Item.Pickable GetPickable() => _pickable;

    public BaseTurretSO GetTurret()
    {
        return _pickable is Potion potion ? potion._baseTurretSo : null;
    }
}