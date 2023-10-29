using System;
using CustomSceneSwitcher.Switcher;
using CustomSceneSwitcher.Switcher.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private GameObject player;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager uiManager;

    [Header("Channels")]
    [SerializeField] private VoidChannelSO actionChannelSO;
    [SerializeField] private VoidChannelSO pauseChannelSO;
    [SerializeField] private VoidChannelSO showRecipesChannelSO;
    [Header("Values")]
    [SerializeField] float timeUntilGameOver = 0.2f;
    [SerializeField] bool isTutorialScene = false;
    [Header("SceneChanger")]
    [SerializeField] private SceneChangeData mainMenu;
    [SerializeField] private SceneChangeData currentScene;
    [SerializeField] private SceneChangeData nextScene;

    [Header("Events")]
    public UnityEvent deActivateRecipe;
    private bool isPaused;
    private bool isRecipesOn;


    private void Awake()
    {
        pauseChannelSO.Subscribe(PauseLevel);
        showRecipesChannelSO.Subscribe(ShowRecipes);
        enemySpawner.OnNewWaveAdded.AddListener(uiManager.AddWaveIcon);
        enemySpawner.OnGameBarUpdated.AddListener(uiManager.UpdateGameBar);
        enemySpawner.OnIncomingWave.AddListener(uiManager.ShowNewWaveAlert);
        enemyManager.activateWinScreenChannel.AddListener(WinGame);

        player.SetActive(true);

        enemySpawner.gameObject.SetActive(!isTutorialScene);
    }

    private void OnDestroy()
    {
        pauseChannelSO.Unsubscribe(PauseLevel);
        showRecipesChannelSO.Unsubscribe(ShowRecipes);
        enemySpawner.OnGameBarUpdated.RemoveListener(uiManager.UpdateGameBar);
        enemySpawner.OnNewWaveAdded.RemoveListener(uiManager.AddWaveIcon);
        enemySpawner.OnIncomingWave.RemoveListener(uiManager.ShowNewWaveAlert);
        enemyManager.activateWinScreenChannel.RemoveAllListeners();
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
            actionChannelSO.Unsubscribe(TutorialSequence);
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
        SceneSwitcher.ChangeScene(currentScene);
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
        enemyManager.activateWinScreenChannel.RemoveAllListeners();
        uiManager.ActivateWinScreen();
        Time.timeScale = 1;
    }
    [ContextMenu("Go To Menu")]
    public void GoToMenu()
    {
        SceneSwitcher.ChangeScene(mainMenu);
    }

    public void GoToNextLevel()
    {
       SceneSwitcher.ChangeScene(nextScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}