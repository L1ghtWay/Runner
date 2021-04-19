using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    public int coinsBalance;
    public float recordScore;
    public float mainVolume;
    public float musicVolume;
    public float moveVolume;
    public int muteAllSoundsInt;
    public bool muteAllSounds;
    public static PlayerPrefsController instance;

    private void Awake()
    {
        if (PlayerPrefsController.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        PlayerPrefsController.instance = this;

        LoadData();        
    }
    
    public void LoadData ()
    {
        if (PlayerPrefs.HasKey("MuteAllSoundsInt"))
        {
            muteAllSoundsInt = PlayerPrefs.GetInt("MuteAllSoundsInt");            
        }
        else muteAllSoundsInt = 0;

        if (muteAllSoundsInt == 1) muteAllSounds = true;
        else muteAllSounds = false;

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
        else moveVolume = 1;

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
    
    public void SaveMusicSettings ()
    {
        PlayerPrefs.SetFloat("MainVolume", mainVolume);
        PlayerPrefs.SetFloat("MusicVolume",musicVolume);
        PlayerPrefs.SetFloat("MoveVolume", moveVolume);
        PlayerPrefs.SetInt("MuteAllSoundsInt", muteAllSoundsInt);
        PlayerPrefs.Save();
    }

    public void DeleteAllData ()
    {
        PlayerPrefs.DeleteAll();
    }
    
    private void OnDestroy()
    {
        PlayerPrefsController.instance = null;
    }

}
