using System.Collections;
using System.Collections.Generic;
using Item;
using Turret;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform pickableSpot;
    [SerializeField] private Item.Pickable _pickable;
    [SerializeField] private BaseTurret _turret;


    public bool hasPickable()
    {
        return _pickable != null;
    }

    public void SetPickable(Item.Pickable item)
    {
            _pickable = Instantiate(item, pickableSpot.position, Quaternion.identity, pickableSpot);
            _pickable.TransferData(item);
    }

    public void SetPickableExt(Item.Pickable item)
    {
        item.transform.SetParent(pickableSpot);
        _pickable = item;
    }

    public void DestroyPickable()
    {
        Destroy(_pickable.gameObject);
        _pickable = null;
    }

    public bool hasPotion() => _pickable is Potion;
    public bool hasIngredient() => _pickable is Ingredient;

    public Item.Pickable GetPickable() => _pickable;

    public void SetTurret(BaseTurret turret)
    {
        this._turret = turret;
    }

    public BaseTurret GetTurret()
    {
        return _turret;
    }
}