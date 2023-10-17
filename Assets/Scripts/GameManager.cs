using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private GameObject player;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private UIManager uiManager;

    [Header("Channels")]
    [SerializeField] private VoidChannelSO actionChannelSO;
    [SerializeField] private VoidChannelSO pauseChannelSO;
    [SerializeField] private VoidChannelSO showRecipesChannelSO;
    [Header("Values")]
    [SerializeField] float timeUntilGameOver = 0.2f;

    [Header("Events")]
    public UnityEvent deActivateRecipe;
    private bool isPaused;
    private bool isRecipesOn;


    private void Awake()
    {
        actionChannelSO.Subscribe(TutorialSequence);
        showRecipesChannelSO.Subscribe(ShowRecipes);
        player.SetActive(false);
        enemySpawner.gameObject.SetActive(false);
        enemySpawner.OnNewWaveAdded.AddListener(uiManager.AddWaveIcon);
        enemySpawner.OnGameBarUpdated.AddListener(uiManager.UpdateGameBar);
        enemySpawner.OnIncomingWave.AddListener(uiManager.ShowNewWaveAlert);
        enemySpawner.activateWinScreenChannel.AddListener(WinGame);
    }

    private void OnDestroy()
    {
        pauseChannelSO.Unsubscribe(PauseLevel);
        showRecipesChannelSO.Unsubscribe(ShowRecipes);
        enemySpawner.OnGameBarUpdated.RemoveListener(uiManager.UpdateGameBar);
        enemySpawner.OnNewWaveAdded.RemoveListener(uiManager.AddWaveIcon);
        enemySpawner.OnIncomingWave.RemoveListener(uiManager.ShowNewWaveAlert);
        enemySpawner.activateWinScreenChannel.RemoveListener(WinGame);
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
            Invoke(nameof(GameOver), timeUntilGameOver);
        }
    }

    private void TutorialSequence()
    {
        if (uiManager.HasTutorialEnded())
        {
            player.SetActive(true);
            enemySpawner.gameObject.SetActive(true);
            actionChannelSO.Unsubscribe(TutorialSequence);
            pauseChannelSO.Subscribe(PauseLevel);
        }
    }

    private void GameOver()
    {
        pauseChannelSO.Unsubscribe(PauseLevel);
        Time.timeScale = 0;
        uiManager.ActivateGameOverCanvas();
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
            uiManager.TogglePauseMenu(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
        }
    }

    public void WinGame()
    {
        pauseChannelSO.Unsubscribe(PauseLevel);
        uiManager.ActivateWinScreen();
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