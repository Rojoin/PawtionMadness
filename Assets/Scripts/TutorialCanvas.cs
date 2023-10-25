using UnityEngine;

public class TutorialCanvas : MonoBehaviour
{
    [SerializeField] private GameObject[] images;
    private int currentImage;

    private void Awake()
    {
        foreach (GameObject tutorialScreen in images)
        {
            tutorialScreen.SetActive(false);
        }

        currentImage = 0;
        images[currentImage].SetActive(true);
    }

    public bool NextTutorial()
    {
        images[currentImage].SetActive(false);
        currentImage++;
        if (currentImage < images.Length)
        {
            images[currentImage].SetActive(true);
            return false;
        }

        return true;
    }
}