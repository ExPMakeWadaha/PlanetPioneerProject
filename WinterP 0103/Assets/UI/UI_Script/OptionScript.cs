using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//씬변환할려면 꼭 잇어야함

public class OptionScript : MonoBehaviour {

    GameObject UnMuteButton;
    GameObject MuteButton;
    GameObject Audio;
    GameObject SoundManager;
    GameObject OptionPanel;

    AudioSource BGM;
    AudioSource ButtonSound;

    bool PanelActivation = false;
    bool Paused = false;

    void Start()
    {
        UnMuteButton = GameObject.Find("UnMuteButton");
        MuteButton = GameObject.Find("MuteButton");
        Audio = GameObject.Find("Audio Source");
        SoundManager = GameObject.Find("SoundManager");
        OptionPanel = GameObject.Find("OptionPanel");

        OptionPanel.SetActive(false);
        UnMuteButton.SetActive(false);

        BGM = Audio.GetComponent<AudioSource>();
        ButtonSound = SoundManager.GetComponent<AudioSource>();

    }

    public void UnMute()
    {
        BGM.mute = true;
        ButtonSound.mute = true;
        MuteButton.SetActive(false);
        UnMuteButton.SetActive(true);
    }

    public void Mute()
    {
        BGM.mute = false;
        ButtonSound.mute = false;
        UnMuteButton.SetActive(false);
        MuteButton.SetActive(true);
    }

    public void OptionPanelActivation()
    {
        if (PanelActivation)
        {
            OptionPanel.SetActive(false);
            PanelActivation = false;
        }
        else
        {
            OptionPanel.SetActive(true);
            PanelActivation = true;
        }

    }


    public void GameReset()
    {
        SceneManager.LoadScene("startingScene");
        TimeManager.GameReset();
        BuildManager.GameReset();
    }
}
