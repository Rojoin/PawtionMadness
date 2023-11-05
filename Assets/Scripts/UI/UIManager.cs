using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private CanvasGroup LoseScreen;
    [SerializeField] private CanvasGroup WinScreen;
    [SerializeField] private CanvasGroup PauseScreen;
    [SerializeField] private CanvasGroup SettingsScreen;
    [SerializeField] private CanvasGroup RecipesScreen;
    [SerializeField] private CanvasGroup TutorialScreen;

    [Header("InGame")]
    [SerializeField] private CustomSlider waveGameBar;
    [SerializeField] private GameObject textBeforeWave;
    [SerializeField] private float waveTextTimer;

    public void Start()
    {
        SetCanvasVisibility(LoseScreen, false);
        SetCanvasVisibility(WinScreen, false);
        SetCanvasVisibility(PauseScreen, false);
        SetCanvasVisibility(RecipesScreen, false);
        SetCanvasVisibility(SettingsScreen, false);
    //    SetCanvasVisibility(TutorialScreen, true);
    }

    private void SetCanvasVisibility(CanvasGroup canvas, bool state)
    {
        canvas.alpha = state ? 1 : 0;
        canvas.interactable = state;
        canvas.blocksRaycasts = state;
    }

    public void ToggleSettingsOn()
    {
        SetCanvasVisibility(PauseScreen,false);
        SetCanvasVisibility(SettingsScreen,true);
    } 
    public void GoBackToPause()
    {
        SetCanvasVisibility(PauseScreen,true);
        SetCanvasVisibility(SettingsScreen,false);
    }
    public bool HasTutorialEnded()
    {
        if (TutorialScreen.GetComponent<TutorialCanvas>().NextTutorial())
        {
            SetCanvasVisibility(TutorialScreen, false);
            return true;
        }

        return false;
    }

    public void ActivateGameOverCanvas()
    {
        SetCanvasVisibility(LoseScreen,true);
        SetCanvasVisibility(PauseScreen,false);
        SetCanvasVisibility(RecipesScreen,false);
    }

    public void ShowRecipesCanvas(bool isRecipesOn)
    {
        SetCanvasVisibility(RecipesScreen,isRecipesOn);
    }

    public void ActivateWinScreen()
    {
        SetCanvasVisibility(WinScreen,true);
    }

    public void UpdateGameBar(float value)
    {
        waveGameBar.FillAmount = value;
    }

    public void AddWaveIcon(float normalizePosition)
    {
        waveGameBar.AddWaveImage(normalizePosition);
    }

    public void TogglePauseMenu(bool isPaused)
    {
        SetCanvasVisibility(PauseScreen,isPaused);
        SetCanvasVisibility(SettingsScreen,false);
    }

    public void ShowNewWaveAlert()
    {
        StartCoroutine(SpawnWaveText());
    }

    private IEnumerator SpawnWaveText()
    {
        textBeforeWave.SetActive(true);
        yield return new WaitForSeconds(waveTextTimer);
        textBeforeWave.SetActive(false);
        yield break;
    }
}