using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup LoseScreen;
    [SerializeField] private CanvasGroup PauseScreen;
    [SerializeField] private CanvasGroup RecipesScreen;
    [SerializeField] private VoidChannelSO pauseChannelSO;
    [SerializeField] private VoidChannelSO showRecipesChannelSO;

    private bool isPaused;
    private bool isRecipesOn;

    private void Awake()
    {
        pauseChannelSO.Subscribe(PauseLevel);
        showRecipesChannelSO.Subscribe(ShowRecipes);
    }

    private void OnDestroy()
    {
        pauseChannelSO.Unsubscribe(PauseLevel);
        showRecipesChannelSO.Unsubscribe(ShowRecipes);
    }

    private void Start()
    {
        isPaused = false;
        isRecipesOn = false;
        Time.timeScale = 1;
        LoseScreen.alpha = 0;
        LoseScreen.interactable = false;
        LoseScreen.blocksRaycasts = false;
        PauseScreen.alpha = 0;
        PauseScreen.interactable = false;
        PauseScreen.blocksRaycasts = false;
        RecipesScreen.alpha = 0;
        RecipesScreen.interactable = false;
        RecipesScreen.blocksRaycasts = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            LoseScreen.alpha = 1;
            LoseScreen.interactable = true;
            LoseScreen.blocksRaycasts = true;
            Time.timeScale = 0;
            PauseScreen.alpha = 0;
            PauseScreen.interactable = false;
            PauseScreen.blocksRaycasts = false;
            RecipesScreen.alpha = 0;
            RecipesScreen.interactable = false;
            RecipesScreen.blocksRaycasts = false;
        }
    }

    private void ShowRecipes()
    {
        isRecipesOn = !isRecipesOn;
        RecipesScreen.alpha = isRecipesOn ? 1 : 0;
        RecipesScreen.interactable = isRecipesOn;
        RecipesScreen.blocksRaycasts = isRecipesOn;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseLevel()
    {
        isPaused = !isPaused;
        PauseScreen.alpha = isPaused ? 1 : 0;
        PauseScreen.interactable = isPaused;
        PauseScreen.blocksRaycasts = isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}