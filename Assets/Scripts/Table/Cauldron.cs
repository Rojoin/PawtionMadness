using System;
using Pickable;
using Player;
using UnityEngine;
using UnityEngine.UI;


namespace Table
{
    internal enum CauldronState
    {
        Empty,
        Cooking,
        Done
    }
    public class Cauldron : Table
    {
        [SerializeField] private Image image;
        [SerializeField] private float timerMax;
        [SerializeField] private float currentTime = 0.0f;
        private CauldronState state;
        [SerializeField] private Potion potion;

        private void Start()
        {
            ResetCauldron();
        }

        private void Update()
        {
            if (state == CauldronState.Cooking)
            {
                currentTime += Time.deltaTime;
                Debug.Log(currentTime);
                if (currentTime > timerMax)
                {
                    state = CauldronState.Done;
                }

                image.fillAmount = currentTime / timerMax;
            }

            
        }

        public override void OnInteraction(PlayerInventory playerInventory = null)
        {
            switch (state)
            {
                case CauldronState.Done when !playerInventory.hasPickable():
                    ResetCauldron();
                    playerInventory.SetPickable(potion);
                    break;
                case CauldronState.Done when playerInventory.hasPickable():
                case CauldronState.Cooking when playerInventory.hasPickable():
                case CauldronState.Empty when playerInventory.hasPickable():
                    AddIngredientToCook(playerInventory.GetPickable());
                    playerInventory.DestroyPickable();
                    break;
                case CauldronState.Cooking when !playerInventory.hasPickable():
                case CauldronState.Empty when !playerInventory.hasPickable():
                    break;
            }
        }

        private void AddIngredientToCook(Pickable.Pickable getPickable)
        {
            if (!image.enabled)
            {
                image.enabled = true;
            }
            timerMax += getPickable.timeToCook;
            state = CauldronState.Cooking;
        }
        private void ResetCauldron()
        {
            currentTime = 0.0f;
            timerMax = 0.0f;
            image.fillAmount = 0;
            state = CauldronState.Empty;
            image.enabled = false;
        }
    }
}