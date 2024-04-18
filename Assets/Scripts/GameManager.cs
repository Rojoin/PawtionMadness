using System;
using System.Collections;
using CustomSceneSwitcher.Switcher;
using CustomSceneSwitcher.Switcher.Data;
using Player;
using Table;
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
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager uiManager;

    [Header("Channels")]
    [SerializeField] private VoidChannelSO actionChannelSO;
    [SerializeField] private VoidChannelSO pauseChannelSO;
    [SerializeField] private VoidChannelSO gridToggleChannelSO;
    [SerializeField] private VoidChannelSO changeToPlayerChannelSO;
    [SerializeField] private VoidChannelSO backInputChannel;
    [SerializeField] private VoidChannelSO showRecipesChannelSO;
    [SerializeField] private VoidChannelSO initialCounterChannelSO;
    [SerializeField] private VoidChannelSO OnCheatWinGame;
    [SerializeField] private VoidChannelSO OnCheatLoseGame;
    [FormerlySerializedAs("OnStartEnemySpawner")] [SerializeField]
    private VoidChannelSO OnEndLerpChannel;

    [Header("Values")]
    [SerializeField] float timeUntilGameOver = 0.2f;
    [SerializeField] float timeUntilActivateEvents = 10f;
    [SerializeField] bool isTutorialScene = false;
    [Header("Data")]
    [SerializeField] private PlayerStats playerStats;
    [Header("SceneChanger")]
    [SerializeField] private SceneChangeData mainMenu;

    [SerializeField] private SceneChangeData currentScene;
    [SerializeField] private SceneChangeData nextScene;

    [Header("Events")]
    public UnityEvent deActivateRecipe;
    public UnityEvent callWinGame;
    public UnityEvent callLoseGame;
    private bool isPaused;
    private bool isGridActivated;
    private bool isRecipesOn;


    private void Awake()
    {
        player.SetActive(true);
        InitUI();
        pauseChannelSO.Subscribe(PauseLevel);
        OnCheatWinGame.Subscribe(WinGame);
        OnCheatLoseGame.Subscribe(GameOver);
        _cameraManager.gameObject.SetActive(true);
        _cameraManager.OnLerpEndChannel.Subscribe(InitializeGameMode);


        enemySpawner.StartEnemySpawner();
        enemySpawner.enabled = false;
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
        changeToPlayerChannelSO.Unsubscribe(GridToggle);
        _cameraManager.OnLerpEndChannel.Unsubscribe(InitializeGameMode);
        initialCounterChannelSO.Unsubscribe(InitCountdown);
        initialCounterChannelSO.Unsubscribe(InitPlayerAndEnemy);
        if (tutorialManager)
        {
            tutorialManager.OnTutorialEnd.RemoveAllListeners();
        }
        StopAllCoroutines();
        pauseChannelSO.Unsubscribe(PauseLevel);
        OnCheatWinGame.Unsubscribe(WinGame);
        OnCheatLoseGame.Unsubscribe(GameOver);
    }

    private void InitializeGameMode()
    {
        if (playerStats.isTutorialOn && tutorialManager)
        {
            tutorialManager.gameObject.SetActive(true);
            InitGame();
            InitPlayer();
            tutorialManager.OnTutorialEnd.AddListener(InitCountdown);
        }
        else
        {
            InitGame();
            InitCountdown();
        }
    }

    private void InitCountdown()
    {
        if (tutorialManager)
        {
            tutorialManager.OnTutorialEnd.RemoveAllListeners();
        }

        initialCounterChannelSO.RaiseEvent();
        initialCounterChannelSO.Subscribe(InitPlayerAndEnemy);
    }

    private void InitPlayerAndEnemy()
    {
        enemySpawner.gameObject.SetActive(true);
        enemySpawner.enabled = true;
        initialCounterChannelSO.Unsubscribe(InitPlayerAndEnemy);
        InitPlayer();
    }

    private void InitGame()
    {
        uiManager.Init();
        showRecipesChannelSO.Subscribe(ShowRecipes);

        gridToggleChannelSO.Subscribe(GridToggle);
        changeToPlayerChannelSO.Subscribe(GridToggle);
    }

    private void InitUI()
    {
        enemySpawner.OnNewWaveAdded.AddListener(uiManager.AddWaveIcon);
        enemySpawner.OnGameBarUpdated.AddListener(uiManager.UpdateGameBar);
        enemySpawner.OnIncomingWave.AddListener(uiManager.ShowNewWaveAlert);
        enemyManager.activateWinScreenChannel.AddListener(WinGame);
    }

    private void InitPlayer()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
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
        _cameraManager.ChangeToGridCamera(isGridActivated);
        uiManager.ToggleFocusPanel(isGridActivated);
    }


    [ContextMenu("Lose Game")]
    private void GameOver()
    {
        pauseChannelSO.Unsubscribe(PauseLevel);
        enemyManager.activateWinScreenChannel.RemoveAllListeners();
        Time.timeScale = 0;
        uiManager.ActivateGameOverCanvas();
        Cursor.visible = true;
        callLoseGame.Invoke();
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
        }
        else
        {
            isPaused = !isPaused;
            uiManager.TogglePauseMenu(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
            player.GetComponent<PlayerInteract>().enabled = !isPaused;
            player.GetComponent<PlayerController>().enabled = !isPaused;
        }
    }

    [ContextMenu("Win Game")]
    public void WinGame()
    {
        Cursor.visible = true;
        pauseChannelSO.Unsubscribe(PauseLevel);
        enemyManager.activateWinScreenChannel.RemoveAllListeners();
        uiManager.ActivateWinScreen();
        callWinGame.Invoke();
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
