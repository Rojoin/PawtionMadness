using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create New Music Containder", menuName = "MusicContainer")]
public class MusicSo : ScriptableObject
{
    public AK.Wwise.Event callInitMusic;
    public AK.Wwise.Event callStopMusic;

    public AK.Wwise.Switch LoseMusicState;
    public AK.Wwise.Switch WinMusicState;

    public string SwitchGroup;
    public AK.Wwise.Switch[] SwitchStates;
}