using JunEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MMSingleton<AudioSystem>
{
    private bool _isPlay
    {
        get
        {
            return PlayerPrefsExtension.GetBool("isPlay", true); 
        }
        set
        {
            PlayerPrefsExtension.SetBool("isPlay", value); 
        }
    }

    public AudioSource BGM;

    private void Start()
    {
        BGM.mute = _isPlay;
    }

    public void ButtonSound()
    {
        _isPlay = !_isPlay; // luu gia tri 
        BGM.mute = _isPlay;
    }
}
