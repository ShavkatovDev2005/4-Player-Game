using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class setting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    void Awake()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            load_sound_volume();
        }
        else 
        {
            ses_kontrol();
        }
    }

    public void ses_kontrol()
    {
        float volume = slider.value;
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void load_sound_volume()
    {
        slider.value=PlayerPrefs.GetFloat("musicVolume");

        ses_kontrol();
    }
}
