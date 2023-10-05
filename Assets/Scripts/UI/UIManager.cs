using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private CanvasGroup LoseScreen;
    [SerializeField] private CanvasGroup WinScreen;
    [SerializeField] private CanvasGroup PauseScreen;
    [SerializeField] private CanvasGroup RecipesScreen;
    [SerializeField] private CanvasGroup TutorialScreen;
    
    
    public void Start()
    {
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
    public bool HasTutorialEnded()
    {
        if (TutorialScreen.GetComponent<TutorialCanvas>().NextTutorial())
        {
            TutorialScreen.alpha = 0;
            TutorialScreen.interactable = false;
            TutorialScreen.blocksRaycasts = false;
            return true;
        }

        return false;
    }

    public void activateGameOverCanvas()
    {
        LoseScreen.alpha = 1;
        LoseScreen.interactable = true;
        LoseScreen.blocksRaycasts = true;
        PauseScreen.alpha = 0;
        PauseScreen.interactable = false;
        PauseScreen.blocksRaycasts = false;
        RecipesScreen.alpha = 0;
        RecipesScreen.interactable = false;
        RecipesScreen.blocksRaycasts = false;
    }

    public void ShowRecipesCanvas(bool isRecipesOn)
    {
        RecipesScreen.alpha = isRecipesOn ? 1 : 0;
        RecipesScreen.interactable = isRecipesOn;
        RecipesScreen.blocksRaycasts = isRecipesOn;
    }

    public void activateWinScreen()
    {
        WinScreen.alpha = 1;
        WinScreen.interactable = true;
        WinScreen.blocksRaycasts = true;
    }

    public void togglePauseMenu(bool isPaused)
    {
        PauseScreen.alpha = isPaused ? 1 : 0;
        PauseScreen.interactable = isPaused;
        PauseScreen.blocksRaycasts = isPaused;
    }
}