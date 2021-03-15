using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private const int version = 6;
    public Text currentLevel;
    public Animator settingsPanel;
    public Text moneyText;
    private bool settingsOpened = false;

    private int vibroState;
    public Sprite turnOn, turnOff;
    public Image vibro;

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
        if (PlayerPrefs.GetInt("TutorialCastles") == 1)
            SceneManager.LoadScene("Level1");
        else
            SceneManager.LoadScene("Level");
    }

    public void SwitchSettings()
    {
        if (settingsOpened)
        {
            settingsPanel.SetBool("Close", true);
            StartCoroutine(CoroutineHelper.WaitFor(0.8f, delegate ()
            {
                settingsPanel.SetBool("Close", false);
                settingsPanel.enabled = false;
            }));
            settingsOpened = false;
        }
        else
        {
            settingsPanel.enabled = true;
            settingsOpened = true;
        }
    }

    /*
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
    */

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
        PlayerPrefs.SetInt("Vibro", vibroState);
    }

    private void CheckPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Version") || PlayerPrefs.GetInt("Version") != version)
            PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("Version", version);

        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("LevelWithoutHeart", 0);
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("Vibro", 1);
            PlayerPrefs.SetInt("Hearts", 3);
            PlayerPrefs.SetInt("MoneyToSpawn", 15 + PlayerPrefs.GetInt("Level") - 1);
        }
        if(PlayerPrefs.GetInt("Speed") == 0)
        {
            PlayerPrefs.SetFloat("Speed", 5);
            PlayerPrefs.SetFloat("SpeedPercent", 0);
        }
        if (!PlayerPrefs.HasKey("ObstacleProbability"))
        {
            PlayerPrefs.SetFloat("ObstacleProbability", 0.004f);
        }

        if (PlayerPrefs.GetInt("EndlessLevel") == 1) PlayerPrefs.SetInt("EndlessLevel", 0);
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.SetInt("TutorialCastles", 1);
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
