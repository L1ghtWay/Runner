using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : SingletonMonoBehaviour<PlayerPrefsController>
{
    public int coinsBalance;
    public float recordScore;
    public float mainVolume;
    public float musicVolume;
    public float moveVolume;
    public int muteAllSoundsInt;
    public bool muteAllSounds;

    protected override void Initialize()
    {
        LoadData();
    }

    public void LoadData ()
    {
        if (PlayerPrefs.HasKey("MuteAllSoundsInt"))
        {
            muteAllSoundsInt = PlayerPrefs.GetInt("MuteAllSoundsInt");            
        }
        else muteAllSoundsInt = 0;        

        if (PlayerPrefs.HasKey("MainVolume"))
        {
            mainVolume = PlayerPrefs.GetFloat("MainVolume");
        }
        else mainVolume = 1;     

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else musicVolume = 1;

        if (PlayerPrefs.HasKey("MoveVolume"))
        {
            moveVolume = PlayerPrefs.GetFloat("MoveVolume");
        }
        else moveVolume = 20;

        if (PlayerPrefs.HasKey("CoinsBalance"))
        {
            coinsBalance = PlayerPrefs.GetInt("CoinsBalance");
        }
        else coinsBalance = 0;

        if (PlayerPrefs.HasKey("RecordScore"))
        {
            recordScore = PlayerPrefs.GetFloat("RecordScore");
        }
        else recordScore = 0;
    }   
    
    public void SaveData ()
    {
        PlayerPrefs.SetInt("CoinsBalance", coinsBalance);
        PlayerPrefs.SetFloat("RecordScore", recordScore);
        PlayerPrefs.Save();
    }
    
    public void SaveMusicData ()
    {
        PlayerPrefs.SetFloat("MainVolume", mainVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("MoveVolume", moveVolume);
        PlayerPrefs.SetInt("MuteAllSoundsInt", muteAllSoundsInt);
        PlayerPrefs.Save();
    }
    
    public void DeleteAllData ()
    {
        PlayerPrefs.DeleteAll();
    }
}
