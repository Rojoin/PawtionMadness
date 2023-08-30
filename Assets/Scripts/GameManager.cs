using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup LoseScreen;

    private void Start()
    {
        LoseScreen.alpha = 0;
        LoseScreen.interactable = false;
        LoseScreen.blocksRaycasts = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            LoseScreen.alpha = 1;
            LoseScreen.interactable = true;
            LoseScreen.blocksRaycasts = true;
        }
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
