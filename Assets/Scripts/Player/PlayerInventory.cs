using System.Collections;
using System.Collections.Generic;
using Item;
using Turret;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform pickableSpot;
    [SerializeField] private Pickable _pickable;
    [SerializeField] private KitchenObjectSO kitchenObject;


    public bool hasPickable()
    {
        return _pickable != null;
    }

    public void SetPickable(Item.Pickable item)
    {
        item.SetNewParent(this.pickableSpot);
        _pickable = item;
    }
    

    public void DestroyPickable()
    {
        Destroy(_pickable.gameObject);
        _pickable = null;
    }

    public void NullPickable()
    {
        _pickable = null;
    }

    public bool hasPotion() => _pickable is Potion;
    public bool hasIngredient() => _pickable is Ingredient;

    public Item.Pickable GetPickable() => _pickable;

    public BaseTurret GetTurret()
    {
        return (_pickable as Potion)._baseTurretSo.asset;
    }
}