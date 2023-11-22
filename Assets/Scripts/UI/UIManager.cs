using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")] [SerializeField] private CanvasGroup LoseScreen;
    [SerializeField] private CanvasGroup WinScreen;
    [SerializeField] private CanvasGroup PauseScreen;
    [SerializeField] private CanvasGroup SettingsScreen;
    [SerializeField] private CanvasGroup RecipesScreen;
    [SerializeField] private CanvasGroup TutorialScreen;

    [Header("InGame")] [SerializeField] private CustomSlider waveGameBar;
    [SerializeField] private GameObject textBeforeWave;
    [SerializeField] private float waveTextTimer;
    [SerializeField] private float textSize = 2.0f;
    private UIAnimation _uiManager = new UIAnimation();


    public void Init()
    {
        SetCanvasVisibility(LoseScreen, false);
        SetCanvasVisibility(WinScreen, false);
        SetCanvasVisibility(PauseScreen, false);
        SetCanvasVisibility(RecipesScreen, false);
        SetCanvasVisibility(SettingsScreen, false);
        Cursor.visible = false;
    }

    private void SetCanvasVisibility(CanvasGroup canvas, bool state)
    {
        canvas.alpha = state ? 1 : 0;
        canvas.interactable = state;
        canvas.blocksRaycasts = state;
        if (state)
        {
            Button currentbutton = canvas.GetComponentInChildren<Button>();
            if (currentbutton)
            {
                EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(currentbutton.gameObject);
            }
        }
    }

    public void ToggleSettingsOn()
    {
        SetCanvasVisibility(PauseScreen, false);
        SetCanvasVisibility(SettingsScreen, true);
    }

    public void GoBackToPause()
    {
        SetCanvasVisibility(PauseScreen, true);
        SetCanvasVisibility(SettingsScreen, false);
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
        SetCanvasVisibility(LoseScreen, true);
        SetCanvasVisibility(PauseScreen, false);
        SetCanvasVisibility(RecipesScreen, false);
    }

    public void ShowRecipesCanvas(bool isRecipesOn)
    {
        SetCanvasVisibility(RecipesScreen, isRecipesOn);
    }

    public void ActivateWinScreen()
    {
        Cursor.visible = true;
        SetCanvasVisibility(WinScreen, true);
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
        Cursor.visible = isPaused;
        SetCanvasVisibility(PauseScreen, isPaused);
        SetCanvasVisibility(SettingsScreen, false);
    }

    public void ShowNewWaveAlert()
    {
        StartCoroutine(SpawnWaveText());
    }

    private IEnumerator SpawnWaveText()
    {
        textBeforeWave.SetActive(true);
        StartCoroutine(_uiManager.PlayAnimation(textBeforeWave.GetComponent<RectTransform>(), textSize, waveTextTimer,
            textBeforeWave.GetComponent<TextMeshProUGUI>()));
        yield return new WaitForSeconds(waveTextTimer);
        textBeforeWave.SetActive(false);
        yield break;
    }
}