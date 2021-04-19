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

    [SerializeField] private SaveData saveData;



    private void Start()
    {

        mixer.audioMixer.SetFloat("MasterVolume", saveData.mainVolume);
        masterSlider.value = saveData.mainVolume;

        mixer.audioMixer.SetFloat("BackgroundSoundsVolume", saveData.musicVolume);
        musicSlider.value = saveData.musicVolume;

        mixer.audioMixer.SetFloat("MoveSoundsVolume", saveData.moveVolume);
        moveSlider.value = saveData.moveVolume;

        muteAllSounds.isOn = saveData.muteAllSounds;

    }

    public void MuteAllSounds(bool enabled)
    {
        if (enabled)
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
            saveData.muteAllSounds = true;
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, saveData.mainVolume));
            saveData.muteAllSounds = false;
        }
    }

    public void MainVolumeSettings(float volume)
    {        
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
        saveData.mainVolume = volume;        
    }
    
    public void BackgroundVolumeSettings (float volume)
    {
        mixer.audioMixer.SetFloat("BackgroundSoundsVolume", Mathf.Lerp(-80, 0, volume));
        saveData.musicVolume = volume;
    }

    public void MoveVolumeSettings (float volume)
    {
        mixer.audioMixer.SetFloat("MoveSoundsVolume", Mathf.Lerp(-80, 20, volume));
        saveData.moveVolume = volume;
    }
}
