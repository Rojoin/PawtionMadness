
using System;
using Turret;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Item
{
    public class Ingredient : Pickable
    {
        [SerializeField] private KitchenObjectSO ingredientSO;
        [SerializeField] private Image imageIcon;
        public float TimeToCook => ingredientSO.timeToCook;
        public int InteractionToProcesses => ingredientSO.interactionToProcesses;
        private bool isProcessed = false;
        private bool _isItemIconVisible = false;
        public BaseTurret turret;

        private void Awake()
        {
            SetIconVisible(false);
        }

        public bool IsProcessed() => isProcessed;
        public bool IsItemIconVisible() => _isItemIconVisible;

        public void SetProcessed(bool state = true)
        {
            isProcessed = state;
        }

        public void SetIconVisible(bool state = true)
        {
            _isItemIconVisible = state;
            imageIcon.sprite = _isItemIconVisible ? ingredientSO.sprite : null;
            imageIcon.enabled = state;
        }
    
        public Sprite GetIngredientImage()
        {
            return ingredientSO.sprite;
        } 
        public KitchenObjectSO GetSO()
        {
            return ingredientSO;
        }
    }
}