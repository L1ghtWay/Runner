using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject preExitPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Text coinsBalance;
    [SerializeField] private Text score;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Text finalScore;
    [SerializeField] private Text finalCoins;
    [SerializeField] private Timer timer;

    private AudioSource playerAudioSource;

    

    private void Start()
    {
        playerAudioSource = PlayerController.instance.GetComponent<AudioSource>();
        PlayerController.instance.PlayerIsNotAlive += OpenGameOverPanel;
    }
    void Update()
    {
        if (PlayerController.instance.newRecord)
        {
            finalScore.text = $"Congratulations! You have new score record: {PlayerController.instance.score}";
        }

        else
        {
            finalScore.text = $"You score: {PlayerController.instance.score}. Try again!";
        }

        finalCoins.text = $"Coin earned: {PlayerController.instance.coinsBalance}";
        coinsBalance.text = $"Coins: {PlayerController.instance.coinsBalance}";
        score.text = $"Score: {timer.GetTime()}";

        if (Input.GetKeyDown(KeyCode.Escape)) Menu();
    }    

    public void Menu ()
    {
        menuPanel.SetActive(!menuPanel.activeInHierarchy);

        if (menuPanel.activeSelf == true) playerAudioSource.Stop();
        else
        {            
            playerAudioSource.Play();
        }
    }

    public void PreEitPanel ()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
        preExitPanel.SetActive(!preExitPanel.activeInHierarchy);
    }

    public void ExitToTheMainMenu ()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void OpenGameOverPanel ()
    {        
        GameOverPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        if (PlayerController.instance != null)
            PlayerController.instance.PlayerIsNotAlive -= OpenGameOverPanel;
    }

}
