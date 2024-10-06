using JunEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    public AudioMixerGroup Master;

    private void Start()
    {
        SetVolume();
    }

    public void ButtonSound()
    {
        _isPlay = !_isPlay; 
        SetVolume();
    }

    public void SetVolume()
    {
        if (!_isPlay)
        {
            Master.audioMixer.SetFloat("Master", 0);
        }
        else
        {
            Master.audioMixer.SetFloat("Master", -80);
        }
    }
}
