using System;
using Table;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UICauldron : MonoBehaviour
{
    private Cauldron _cauldron;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image[] backGroundImage;
    [SerializeField] private Image[] ingredientsImages;
    private int currentDisplayImages;
    [SerializeField] private int maxDisplayImages = 3;

    private void Awake()
    {
        _cauldron = GetComponent<Cauldron>();
        _cauldron.OnIngredientAdded.AddListener(AddIngredientImage);
        _cauldron.OnCookingFinished.AddListener(Reset);
        _cauldron.OnFillAmountUpdated.AddListener(SetFillAmount);
        Reset();
    }

    private void OnDestroy()
    {
        _cauldron.OnCookingFinished.RemoveListener(Reset);
        _cauldron.OnIngredientAdded.RemoveListener(AddIngredientImage);
        _cauldron.OnFillAmountUpdated.RemoveListener(SetFillAmount);
    }

    private void Reset()
    {
        _fillImage.fillAmount = 0;
        _fillImage.enabled = false;
        foreach (Image image in backGroundImage)
        {
            image.enabled = false;
        }

        foreach (Image ingredientsImage in ingredientsImages)
        {
            ingredientsImage.enabled = false;
        }

        currentDisplayImages = 0;
    }

    private void AddIngredientImage(Sprite newIngredientImage)
    {
        if (!_fillImage.enabled)
        {
            _fillImage.enabled = true;
            foreach (Image image in backGroundImage)
            {
                image.enabled = true;
            }

        }

        ingredientsImages[currentDisplayImages].sprite = newIngredientImage;
        ingredientsImages[currentDisplayImages].enabled = true;
        currentDisplayImages++;
    }

    private void SetFillAmount(float fillAmount)
    {
        _fillImage.fillAmount = fillAmount;
    }
}