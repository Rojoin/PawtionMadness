using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private Image foreGround;
    [SerializeField] private Image bar;
    [SerializeField] private GameObject waveIcon;
    [SerializeField] private RectTransform barEndPos;
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
    }

    public void AddWaveImage(float position)
    {
        float rectX = barEndPos.anchoredPosition.x * position;
        Debug.Log(barEndPos.rect.x);

        Vector2 newPos = new Vector2(rectX, barEndPos.anchoredPosition.y);

        GameObject newWaveIcon = Instantiate(waveIcon, foreGround.transform);
        newWaveIcon.SetActive(true);
        Debug.Log(newWaveIcon,gameObject);
        RectTransform rect = newWaveIcon.GetComponent<RectTransform>();
        rect.anchoredPosition = newPos;
        rect.sizeDelta = barEndPos.rect.size;
    }
}