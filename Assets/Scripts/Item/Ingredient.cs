
using Turret;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    public class Ingredient : Pickable
    {
        [SerializeField] private KitchenObjectSO ingredientSO;
        public float TimeToCook => ingredientSO.timeToCook;
        public int InteractionToProcesses => ingredientSO.interactionToProcesses;
        private bool isProcessed = false;
         public BaseTurret turret;

        public bool IsProcessed() => isProcessed;

        public void SetProcessed(bool state = true)
        {
            isProcessed = state;
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