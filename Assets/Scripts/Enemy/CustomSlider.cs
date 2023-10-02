using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    [SerializeField] private Image[] backGround;
    [SerializeField] private Image bar;
    [SerializeField] private Image waveIcon;
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
        
    }
}