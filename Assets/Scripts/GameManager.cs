using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemySpawner;
    [Header("Panels")]
    [SerializeField] private CanvasGroup LoseScreen;
    [SerializeField] private CanvasGroup WinScreen;
    [SerializeField] private CanvasGroup PauseScreen;
    [SerializeField] private CanvasGroup RecipesScreen;
    [SerializeField] private CanvasGroup TutorialScreen;
    [Header("Channels")]
    [SerializeField] private VoidChannelSO actionChannelSO;
    [SerializeField] private VoidChannelSO pauseChannelSO;
    [SerializeField] private VoidChannelSO showRecipesChannelSO;
    [Header("Events")]
    public UnityEvent deActivateRecipe;
    private bool isPaused;
    private bool isRecipesOn;


    private void Awake()
    {
        actionChannelSO.Subscribe(TutorialSequence);
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
        WinScreen.alpha = 0;
        WinScreen.interactable = false;
        WinScreen.blocksRaycasts = false;
        PauseScreen.alpha = 0;
        PauseScreen.interactable = false;
        PauseScreen.blocksRaycasts = false;
        RecipesScreen.alpha = 0;
        RecipesScreen.interactable = false;
        RecipesScreen.blocksRaycasts = false;
        TutorialScreen.alpha = 1;
        TutorialScreen.interactable = true;
        TutorialScreen.blocksRaycasts = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Invoke(nameof(GameOver), 5);
        }
    }

    private void TutorialSequence()
    {
        if (TutorialScreen.GetComponent<TutorialCanvas>().NextTutorial())
        {
            player.SetActive(true);
            enemySpawner.SetActive(true);
            TutorialScreen.alpha = 0;
            TutorialScreen.interactable = false;
            TutorialScreen.blocksRaycasts = false;
            actionChannelSO.Unsubscribe(TutorialSequence);
            pauseChannelSO.Subscribe(PauseLevel);
        }
    }

    private void GameOver()
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
        if (isRecipesOn)
        {
            deActivateRecipe.Invoke();
        }
        else
        {
            isPaused = !isPaused;
            PauseScreen.alpha = isPaused ? 1 : 0;
            PauseScreen.interactable = isPaused;
            PauseScreen.blocksRaycasts = isPaused;
            Time.timeScale = isPaused ? 0 : 1;
        }
    }

    public void WinGame()
    {
        WinScreen.alpha = 1;
        WinScreen.interactable = true;
        WinScreen.blocksRaycasts = true;
        Time.timeScale = 0;
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