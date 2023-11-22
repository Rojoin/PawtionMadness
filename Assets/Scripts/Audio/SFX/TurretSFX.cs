using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSFX : MonoBehaviour
{
    public AK.Wwise.Event Shoot;
    public AK.Wwise.Event Death;
    public AK.Wwise.Event SpecialSound;

    public void PlayShoot()
    {
        AkSoundEngine.PostEvent(Shoot.Id, gameObject);
    }

    public void PlayDeath()
    {
        AkSoundEngine.PostEvent(Death.Id, gameObject);
    }

    public void PlaySpecialSound()
    {
        AkSoundEngine.PostEvent(SpecialSound.Id, gameObject);
    }
}
