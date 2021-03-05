using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text currentLevel;
    public GameObject darkPanel, settingsPanel;
    public Text moneyText;


    private int soundState, vibroState;
    public Sprite turnOn, turnOff;
    public Image sound, vibro;

    private int level;

    private void Awake()
    {
        CheckPlayerPrefs();
    }


    public void LoadEndlessLevel()
    {
        SceneManager.LoadScene("EndlessLEvel");
    }


    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void SwitchSettings()
    {
        darkPanel.SetActive(!darkPanel.activeSelf);
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void SwitchSound()
    {
        if(soundState == 0)
        {
            soundState = 1;
            sound.sprite = turnOn;
        }
        else
        {
            soundState = 0;
            sound.sprite = turnOff;
        }
        PlayerPrefs.SetInt("Sound", soundState);
    }

    public void SwitchVibro()
    {
        if (vibroState == 0)
        {
            vibroState = 1;
            vibro.sprite = turnOn;
        }
        else
        {
            vibroState = 0;
            vibro.sprite = turnOff;
        }
        PlayerPrefs.SetInt("Vibro", soundState);
    }

    public void OpenStore()
    {
        Debug.Log("Open store button");
    }

    private void CheckPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("LevelWithoutHeart", 0);
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("Vibro", 1);
            PlayerPrefs.SetInt("Hearts", 3);
        }

        soundState = PlayerPrefs.GetInt("Sound");
        if(soundState == 0)
        {
            sound.sprite = turnOff;
        }
        
        vibroState = PlayerPrefs.GetInt("Vibro");
        if(vibroState == 0)
        {
            vibro.sprite = turnOff;
        }

        moneyText.text = PlayerPrefs.GetInt("Money").ToString();
        level = PlayerPrefs.GetInt("Level");
        currentLevel.text = level.ToString();
    }
}
