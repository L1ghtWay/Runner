using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject preExitPanel;
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private Text recordScore;
    [SerializeField] private Text coinsBalance;

    private void Start()
    {
        coinsBalance.text = $"Coins:\n {PlayerPrefsController.instance.coinsBalance}";
        recordScore.text = $"Best score:\n {PlayerPrefsController.instance.recordScore}";
    }
    public void StartGame()
    {        
        SceneManager.LoadScene(1, LoadSceneMode.Single);        
    }

    public void Settings ()
    {
        PlayerPrefsController.instance.SaveMusicData();
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
        mainButtons.SetActive(!mainButtons.activeInHierarchy);        
    }

    public void PreExit()
    {        
        preExitPanel.SetActive(!preExitPanel.activeInHierarchy);
        mainButtons.SetActive(!mainButtons.activeInHierarchy);
    }   

    public void Exit()
    {
        PlayerPrefsController.instance.DeleteAllData();
        Application.Quit();        
    }
}
