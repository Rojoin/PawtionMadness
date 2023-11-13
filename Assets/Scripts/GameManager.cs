using System;
using System.Collections;
using CustomSceneSwitcher.Switcher;
using CustomSceneSwitcher.Switcher.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Entities")] 
    
    [SerializeField] private GameObject player;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager uiManager;

    [Header("Channels")] 
    
    [SerializeField] private VoidChannelSO actionChannelSO;
    [SerializeField] private VoidChannelSO pauseChannelSO;
    [SerializeField] private VoidChannelSO gridToggleChannelSO;
    [SerializeField] private VoidChannelSO backInputChannel;
    [SerializeField] private VoidChannelSO showRecipesChannelSO;
    [SerializeField] private VoidChannelSO initialCounterChannelSO;
    [Header("Values")] 
    
    [SerializeField] float timeUntilGameOver = 0.2f;
    [SerializeField] float timeUntilActivateEvents = 10f;
    [SerializeField] bool isTutorialScene = false;

    [Header("SceneChanger")] 
    
    [SerializeField] private SceneChangeData mainMenu;

    [SerializeField] private SceneChangeData currentScene;
    [SerializeField] private SceneChangeData nextScene;

    [Header("Events")] 
    
    public UnityEvent deActivateRecipe;
    
    private bool isPaused;
    private bool isGridActivated;
    private bool isRecipesOn;



    private void Awake()
    {
        pauseChannelSO.Subscribe(PauseLevel);
        if (isTutorialScene)
        {
            InitGame();
        }
        else
        {
            _cameraManager.gameObject.SetActive(true);
            Invoke(nameof(InitGame), timeUntilActivateEvents);
        }
    }

    private void InitGame()
    {
        uiManager.Init();
        showRecipesChannelSO.Subscribe(ShowRecipes);
        enemySpawner.OnNewWaveAdded.AddListener(uiManager.AddWaveIcon);
        enemySpawner.OnGameBarUpdated.AddListener(uiManager.UpdateGameBar);
        enemySpawner.OnIncomingWave.AddListener(uiManager.ShowNewWaveAlert);
        enemyManager.activateWinScreenChannel.AddListener(WinGame);
        gridToggleChannelSO.Subscribe(GridToggle);
        player.SetActive(true);
        initialCounterChannelSO.RaiseEvent();
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
        gridToggleChannelSO.Unsubscribe(GridToggle);
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

    private void GridToggle()
    {
        isGridActivated = !isGridActivated;
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
        enemyManager.activateWinScreenChannel.RemoveAllListeners();
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
        Time.timeScale = 1;
        SceneSwitcher.ChangeScene(currentScene);
    }

    public void PauseLevel()
    {
        if (isRecipesOn)
        {
            deActivateRecipe.Invoke();
        }
        else if (isGridActivated)
        {
            backInputChannel.RaiseEvent();
            GridToggle();
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
    }

    [ContextMenu("Go To Menu")]
    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneSwitcher.ChangeScene(mainMenu);
    }

    public void GoToNextLevel()
    {
        Time.timeScale = 1;
        SceneSwitcher.ChangeScene(nextScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}