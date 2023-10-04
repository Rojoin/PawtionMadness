using System;
using System.Collections.Generic;
using System.Linq;
using Item;
using Player;
using Turret;
using Unity.VisualScripting;
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

        [SerializeField] private Potion defaultPotion;
        [SerializeField] private Potion potionFromRecipe;
        [SerializeField] private KitchenObjectSO[] ingredientsInCauldron;
        [SerializeField] private PotionRecipeSO[] posiblePotions;
        [SerializeField] private int maxIngredientInCauldron = 3;
        private int currentIngredientCounter = 0;
        [SerializeField] private Image[] ingredientsImages;
        private List<IngredientData> cauldronStuff = new List<IngredientData>();

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
                        PotionRecipeSO potionToGet = GetRecipePotion();
                        potionFromRecipe = InstantiatePotion(potionToGet);
                        cauldronStuff.Clear();
                    }
                }
                else
                {
                    currentTime += Time.deltaTime;
                }

                image.fillAmount = currentTime / timerMax;
            }
        }

        public override void OnInteraction(PlayerInventory playerInventory = null, PlayerInteract playerInteract = null)
        {
            switch (state)
            {
                case CauldronState.Done when !playerInventory.hasPickable():
                    playerInventory.SetPickable(potionFromRecipe);
                    ResetCauldron();
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

            ingredientsInCauldron[currentIngredientCounter] = ingredient.GetSO();
            ingredient.SetNewParent(this.transform);
            ingredient.gameObject.SetActive(false);
            ingredientsImages[currentIngredientCounter].sprite = ingredient.GetIngredientImage();
            ingredientsImages[currentIngredientCounter].enabled = true;
            timerMax += ingredient.IsProcessed() ? ingredient.TimeToCook / 2.0f : ingredient.TimeToCook;
            state = CauldronState.Cooking;
            currentIngredientCounter++;

            KitchenObjectSO so = ingredient.GetSO();

            bool addNewIngredient = true;
            for (var i = 0; i < cauldronStuff.Count; i++)
            {
                var ingredientData = cauldronStuff[i];
                if (ingredientData.item == so)
                {
                    ingredientData.amount++;
                    addNewIngredient = false;
                }
            }

            if (addNewIngredient)
                cauldronStuff.Add(new IngredientData { item = so, amount = 1 });
        }

        private void ResetCauldron()
        {
            ingredientsInCauldron = new KitchenObjectSO[maxIngredientInCauldron];
            currentTime = 0.0f;
            timerMax = 0.0f;
            image.fillAmount = 0;
            state = CauldronState.Empty;
            image.enabled = false;
            currentIngredientCounter = 0;
            foreach (var uiImages in ingredientsImages)
            {
                uiImages.sprite = null;
                uiImages.enabled = false;
            }
        }
        
        

        private PotionRecipeSO GetRecipePotion()
        {
            PotionRecipeSO nextPotion = null;

            for (var i = 0; i < posiblePotions.Length; i++)
            {
                PotionRecipeSO recipe = posiblePotions[i];

                if (recipe.IsSameRecipe(cauldronStuff))
                    return recipe;
            }
            return null;
            
        }

        private Potion InstantiatePotion(PotionRecipeSO recipe)
        {
            Potion potion;
            if (recipe)
                potion = Instantiate(recipe.potion, transform.position, Quaternion.identity);
            else
                potion = Instantiate(defaultPotion, transform.position, Quaternion.identity);

            potion.SetIconVisible(true);
            return potion;
        }

    }
}