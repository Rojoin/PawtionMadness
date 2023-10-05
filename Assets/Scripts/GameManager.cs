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
    [SerializeField] private UIManager uiManager;

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
        if (uiManager.HasTutorialEnded())
        {
            player.SetActive(true);
            enemySpawner.SetActive(true);
            actionChannelSO.Unsubscribe(TutorialSequence);
            pauseChannelSO.Subscribe(PauseLevel);
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        uiManager.activateGameOverCanvas();
    }

    private void ShowRecipes()
    {
        isRecipesOn = !isRecipesOn;
        uiManager.ShowRecipesCanvas(isRecipesOn);
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
            uiManager.togglePauseMenu(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
        }
    }

    public void WinGame()
    {
        uiManager.activateWinScreen();
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