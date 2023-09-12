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
        [SerializeField] private KitchenObjectSO[] ingredientsInCauldron;
        [SerializeField] private PotionRecipeSO[] posiblePotions;
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
                    playerInventory.SetPickable(GetRecipePotion());
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

        private Potion GetRecipePotion()
        {
            PotionRecipeSO nextPotion = null;

            foreach (var recipe in posiblePotions)
            {
                var dict = CreateCoockingDictionary();
                nextPotion = CheckRecipe(dict, recipe);
                if (nextPotion)
                {
                    break;
                }
            }

            return nextPotion
                ? Instantiate(nextPotion.potion, transform.position, Quaternion.identity)
                : Instantiate(defaultPotion, transform.position, Quaternion.identity);
        }

        private PotionRecipeSO CheckRecipe(Dictionary<ScriptableObject, int> dict, PotionRecipeSO recipe)
        {
            foreach (var item in recipe.itemsNeeded)
            {
                if (!dict.ContainsKey(item))
                {
                    return null;
                }
                else
                {
                    dict[item]--;
                    if (dict[item] == 0)
                    {
                        dict.Remove(item);
                    }
                }
            }

            return dict.Count == 0 ? recipe : null;
        }

        private Dictionary<ScriptableObject, int> CreateCoockingDictionary()
        {
            var dict = new Dictionary<ScriptableObject, int>();

            foreach (var item in ingredientsInCauldron)
            {
                if (!dict.TryAdd(item, 1))
                    dict[item]++;
            }

            return dict;
        }
    }
}