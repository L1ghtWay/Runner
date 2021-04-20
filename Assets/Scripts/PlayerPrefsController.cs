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
    public SaveData saveData;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadData();        
    }
    
    public void LoadData ()
    {
        if (PlayerPrefs.HasKey("MuteAllSoundsInt"))
        {
            muteAllSoundsInt = PlayerPrefs.GetInt("MuteAllSoundsInt");            
        }
        else muteAllSoundsInt = 0;

        if (muteAllSoundsInt == 1)
        {
            muteAllSounds = true;            
        }
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

        GetDataToScriptableObject();
    }   
    
    public void SaveData ()
    {
        SetDataFromScriptableObject();

        PlayerPrefs.SetInt("CoinsBalance", coinsBalance);
        PlayerPrefs.SetFloat("RecordScore", recordScore);
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

    private void GetDataToScriptableObject()
    {
        saveData.muteAllSounds = muteAllSounds;
        saveData.mainVolume = mainVolume;
        saveData.musicVolume = musicVolume;
        saveData.moveVolume = moveVolume;
        saveData.coinsBalance = coinsBalance;
        saveData.recordScore = recordScore;
    }

    private void SetDataFromScriptableObject()
    {
        muteAllSounds = saveData.muteAllSounds;
        mainVolume = saveData.mainVolume;
        musicVolume = saveData.musicVolume;
        moveVolume = saveData.moveVolume;
        coinsBalance = saveData.coinsBalance;
        recordScore = saveData.recordScore;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
