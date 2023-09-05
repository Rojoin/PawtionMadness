using System;
using Item;
using Player;
using Turret;
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
        [SerializeField] private BaseTurret turret;

        private void Start()
        {
            ResetCauldron();
        }

        private void Update()
        {
            if (state == CauldronState.Cooking)
            {
                currentTime += Time.deltaTime;
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
                    playerInventory.SetTurret(turret);
                    ResetCauldron();
                    playerInventory.SetPickable(potion);
                    break;
                case CauldronState.Empty when playerInventory.hasIngredient():
                    AddIngredientToCook(playerInventory.GetPickable() as Ingredient);
                
                    playerInventory.DestroyPickable();
                    break;
            }
        }

        private void AddIngredientToCook(Ingredient getPickable)
        {
            if (!image.enabled)
            {
                image.enabled = true;
            }

            timerMax += getPickable.IsProcessed() ? getPickable.timeToCook / 2.0f : getPickable.timeToCook;
            turret = getPickable.turret;
            state = CauldronState.Cooking;
        }

        private void ResetCauldron()
        {
            currentTime = 0.0f;
            timerMax = 0.0f;
            image.fillAmount = 0;
            turret = null;
            state = CauldronState.Empty;
            image.enabled = false;
        }
    }
}