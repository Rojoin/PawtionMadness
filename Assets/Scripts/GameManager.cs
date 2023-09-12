using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup LoseScreen;
    [SerializeField] private CanvasGroup PauseScreen;
    [SerializeField] private VoidChannelSO pauseChannelSO;

    private bool isPaused;

    private void Awake()
    {
        pauseChannelSO.Subscribe(PauseLevel);
    }

    private void OnDestroy()
    {
        pauseChannelSO.Unsubscribe(PauseLevel);
    }

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
        LoseScreen.alpha = 0;
        LoseScreen.interactable = false;
        LoseScreen.blocksRaycasts = false;
        PauseScreen.alpha = 0;
        PauseScreen.interactable = false;
        PauseScreen.blocksRaycasts = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            LoseScreen.alpha = 1;
            LoseScreen.interactable = true;
            LoseScreen.blocksRaycasts = true;
            Time.timeScale = 0;
        }
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