using AK.Wwise;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    public MusicSo musicSo;
    public int currentState = 0;

    private void Start()
    {
        AkSoundEngine.PostEvent(musicSo.callInitMusic.ToString(), gameObject);
    }

    private void Update()
    {
        Debug.Log(currentState + " = " + musicSo.SwitchStates[currentState].ToString());
    }

    public void StopMusic()
    {
        AkSoundEngine.PostEvent(musicSo.callStopMusic.ToString(), gameObject);
    }

    [ContextMenu("Change State")]
    public void SetNewState()
    {
        currentState++;
        AkSoundEngine.SetSwitch(musicSo.SwitchGroup.ToString(), musicSo.SwitchStates[currentState].ToString(), gameObject);
    }

    [ContextMenu("Change State")]
    public void callLoseState()
    {
        AkSoundEngine.SetSwitch(musicSo.SwitchGroup.ToString(), musicSo.LoseMusicState.ToString(), gameObject);
    }

    [ContextMenu("Change State")]
    public void callWinState()
    {
        AkSoundEngine.SetSwitch(musicSo.SwitchGroup.ToString(), musicSo.WinMusicState.ToString(), gameObject);
    }
}
