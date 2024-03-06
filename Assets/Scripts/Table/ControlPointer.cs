using System;
using UnityEngine;


public class ControlPointer : MonoBehaviour
{
    [SerializeField] private IntChannelSO onControlSchemeChange;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private SpriteRenderer currentControllerImage;
    [SerializeField] private Sprite[] posibleImages;
    [SerializeField] private Sprite[] alternativeImages;
    public bool UseAlternativeImages { get; set; }


    private void OnEnable()
    {
        currentControllerImage.sprite = UseAlternativeImages
            ? alternativeImages[playerStats.ControllerInput]
            : posibleImages[playerStats.ControllerInput];
        onControlSchemeChange.Subscribe(OnControllerSchemeChanged);
    }

    private void OnDisable()
    {
        onControlSchemeChange.Unsubscribe(OnControllerSchemeChanged);
    }

    private void OnControllerSchemeChanged(int value)
    {
        currentControllerImage.sprite = UseAlternativeImages ? alternativeImages[value] : posibleImages[value];
    }
}