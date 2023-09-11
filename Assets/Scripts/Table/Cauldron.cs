using System;
using Item;
using Player;
using Turret;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private Ingredient[] ingredientsInCauldron;
        [SerializeField] private int maxIngredientInCauldron = 3;
        private int currentIngredientCounter = 0;
        [SerializeField] private Image[] ingredientsImages;

        private void Start()
        {
            ResetCauldron();
        }

        private void Update()
        {
            if (state == CauldronState.Cooking)
            {
                if (currentTime > timerMax)
                {
                    if (currentIngredientCounter == maxIngredientInCauldron)
                    {
                        state = CauldronState.Done;
                    }
                }
                else
                {
                    currentTime += Time.deltaTime;
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
                    var potionToGive = Instantiate(potion, transform.position, Quaternion.identity);
                    playerInventory.SetPickable(potionToGive);
                    break;
                case CauldronState.Empty when playerInventory.hasIngredient():
                case CauldronState.Cooking when playerInventory.hasIngredient():
                    if (currentIngredientCounter < ingredientsInCauldron.Length)
                    {
                        AddIngredientToCook(playerInventory.GetPickable() as Ingredient);
                        playerInventory.NullPickable();
                    }

                    break;
            }
        }

        private void AddIngredientToCook(Ingredient ingredient)
        {
            if (!image.enabled)
            {
                image.enabled = true;
            }

            ingredientsInCauldron[currentIngredientCounter] = ingredient;
            ingredient.SetNewParent(this.transform);
            ingredient.gameObject.SetActive(false);
            ingredientsImages[currentIngredientCounter].sprite = ingredient.GetIngredientImage();
            ingredientsImages[currentIngredientCounter].enabled = true;
            timerMax += ingredient.IsProcessed() ? ingredient.TimeToCook / 2.0f : ingredient.TimeToCook;
            turret = ingredient.turret;
            state = CauldronState.Cooking;
            currentIngredientCounter++;
        }

        private void ResetCauldron()
        {
            ingredientsInCauldron = new Ingredient[maxIngredientInCauldron];
            currentTime = 0.0f;
            timerMax = 0.0f;
            image.fillAmount = 0;
            turret = null;
            state = CauldronState.Empty;
            image.enabled = false;
            currentIngredientCounter = 0;
            foreach (var uiImages in ingredientsImages)
            {
                uiImages.sprite = null;
                uiImages.enabled = false;
            }
        }
    }
}