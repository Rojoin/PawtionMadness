using System;
using CustomSceneSwitcher.Switcher;
using CustomSceneSwitcher.Switcher.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private SceneChangeData GoToGame;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button backOptionsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button backCreditsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private CanvasGroup menuCanvas;
        [SerializeField] private CanvasGroup optionsCanvas;
        [SerializeField] private CanvasGroup creditsCanvas;
        private bool isOptionsActive;
        private bool isHowToPlayActive;

        private void Awake()
        {
            startGameButton.onClick.AddListener(StartGame);
            optionsButton.onClick.AddListener(OptionsToggle);
            backOptionsButton.onClick.AddListener(OptionsToggle);
            creditsButton.onClick.AddListener(HowToToggle);
            backCreditsButton.onClick.AddListener(HowToToggle);
            exitButton.onClick.AddListener(ExitGame);
            isOptionsActive = false;
            isHowToPlayActive = false;
        }

        private void OnDestroy()
        {
            startGameButton.onClick.RemoveListener(StartGame);
            optionsButton.onClick.RemoveListener(OptionsToggle);
            backOptionsButton.onClick.RemoveListener(OptionsToggle);
            creditsButton.onClick.RemoveListener(HowToToggle);
            backCreditsButton.onClick.RemoveListener(HowToToggle);
            exitButton.onClick.RemoveListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void OptionsToggle()
        {
            isOptionsActive = !isOptionsActive;
            SetCanvasVisibility(optionsCanvas, isOptionsActive);
            menuCanvas.blocksRaycasts = !isOptionsActive;
        }

        private void HowToToggle()
        {
            isHowToPlayActive = !isHowToPlayActive;
            SetCanvasVisibility(creditsCanvas, isHowToPlayActive);
            menuCanvas.blocksRaycasts = !isHowToPlayActive;
        }

        private void StartGame()
        {
            SceneSwitcher.ChangeScene(GoToGame);
        }

        private void SetCanvasVisibility(CanvasGroup canvas, bool state)
        {
            canvas.alpha = state ? 1 : 0;
            canvas.interactable = state;
            canvas.blocksRaycasts = state;
        }
    }
}