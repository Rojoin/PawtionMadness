using UnityEngine;

public class AudioSlider : MonoBehaviour
{
    public AK.Wwise.RTPC SetRTPC;
    public void SetNewSliderValue(float sliderValue)
    {
        AkSoundEngine.SetRTPCValue(SetRTPC.Name, sliderValue);
    }
}
