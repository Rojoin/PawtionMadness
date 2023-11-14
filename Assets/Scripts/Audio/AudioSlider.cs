using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public AK.Wwise.RTPC RTPC;
    public Slider slider;

    private void Start()
    {
        AkSoundEngine.SetRTPCValue(RTPC.Name, slider.value);
    }
    public void SetNewSliderValue()
    {
        AkSoundEngine.SetRTPCValue(RTPC.Name, slider.value);
        Debug.Log(slider.value);

    }
}
