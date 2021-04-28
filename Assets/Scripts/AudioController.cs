using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider moveSlider;
    [SerializeField] private Toggle muteAllSounds;
    
    private void Start()
    {       
        masterSlider.value = PlayerPrefsController.instance.mainVolume;
       
        musicSlider.value = PlayerPrefsController.instance.musicVolume;
        
        moveSlider.value = PlayerPrefsController.instance.moveVolume;       

        if (PlayerPrefsController.instance.muteAllSoundsInt == 1)
        {
            PlayerPrefsController.instance.muteAllSounds = true;
        }
        else PlayerPrefsController.instance.muteAllSounds = false;

        muteAllSounds.isOn = PlayerPrefsController.instance.muteAllSounds;
    }

    public void MuteAllSounds(bool enabled)
    {
        if (enabled)
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
            PlayerPrefsController.instance.muteAllSoundsInt = 1;            
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, PlayerPrefsController.instance.mainVolume));
            PlayerPrefsController.instance.muteAllSoundsInt = 0;
        }
    }

    public void MainVolumeSettings(float volume)
    {        
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
        PlayerPrefsController.instance.mainVolume = volume;        
    }
    
    public void BackgroundVolumeSettings (float volume)
    {
        mixer.audioMixer.SetFloat("BackgroundSoundsVolume", Mathf.Lerp(-80, 0, volume));
        PlayerPrefsController.instance.musicVolume = volume;
    }

    public void MoveVolumeSettings (float volume)
    {
        mixer.audioMixer.SetFloat("MoveSoundsVolume", Mathf.Lerp(-80, 20, volume));
        PlayerPrefsController.instance.moveVolume = volume;
    }
}
