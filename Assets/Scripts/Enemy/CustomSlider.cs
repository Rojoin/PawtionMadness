using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private Image foreGround;
    [SerializeField] private Image bar;
    [SerializeField] private GameObject waveIcon;
    [SerializeField] private RectTransform barEndPos;
    [SerializeField] private RectTransform icon;
    private Vector2 handleInitialPos = Vector2.zero;
    private float fillAmount;
    private int waveCounter = 0;

    public float FillAmount
    {
        get => fillAmount;
        set
        {
            fillAmount = value;
            UpdateSlider();
        }
    }

    private void UpdateSlider()
    {
        bar.fillAmount = FillAmount;
        UpdateHandleIcon();
    }

    public void AddWaveImage(float position)
    {
        float rectX = barEndPos.anchoredPosition.x * position;

        Vector2 newPos = new Vector2(rectX, barEndPos.anchoredPosition.y);

        GameObject newWaveIcon = Instantiate(waveIcon, foreGround.transform);
        newWaveIcon.SetActive(true);
        RectTransform rect = newWaveIcon.GetComponent<RectTransform>();
        rect.anchoredPosition = newPos;
        rect.sizeDelta = barEndPos.rect.size;
    }

    private void UpdateHandleIcon()
    {
        if (handleInitialPos == Vector2.zero)
        {
            handleInitialPos = new Vector2(icon.anchoredPosition.x,icon.anchoredPosition.y);
        }
        float rectX = handleInitialPos.x * bar.fillAmount;

        Vector2 newPos = new Vector2(rectX, handleInitialPos.y);

        icon.anchoredPosition = newPos;
        icon.sizeDelta = barEndPos.rect.size;
    }
}