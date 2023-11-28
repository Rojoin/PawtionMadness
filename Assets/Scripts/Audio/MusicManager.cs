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

    public void StopMusic()
    {
        AkSoundEngine.PostEvent(musicSo.callStopMusic.ToString(), gameObject);
    }

    [ContextMenu("Change to next State")]
    public void SetNewState()
    {
        currentState++;
        AkSoundEngine.SetSwitch(musicSo.SwitchGroup.ToString(), musicSo.SwitchStates[currentState].ToString(), gameObject);
    }

    [ContextMenu("Change to Lose State")]
    public void callLoseState()
    {
        AkSoundEngine.SetSwitch(musicSo.SwitchGroup.ToString(), musicSo.LoseMusicState.ToString(), gameObject);
    }

    [ContextMenu("Change to Win State")]
    public void callWinState()
    {
        AkSoundEngine.SetSwitch(musicSo.SwitchGroup.ToString(), musicSo.WinMusicState.ToString(), gameObject);
    }
}
