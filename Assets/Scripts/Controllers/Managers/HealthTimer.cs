using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HealthTimer : MonoBehaviour
{

    private DateTime RewardedHealthDT;
    private float timeBetweenAds = 60; //minutes
    private bool shouldHeal;


    public Sprite healthOn;
    public Image[] healthCount;
    public Text timer;


    private void Start()
    {
        GetRewardHealthDT();
    }


    private void Update()
    {
        if (shouldHeal)
        {
            DateTime nowTime = DateTime.Now;
            if (RewardedHealthDT <= nowTime)
            {
                PlayerPrefs.SetInt("Hearts", PlayerPrefs.GetInt("Hearts") + 1);
                if (PlayerPrefs.GetInt("Hearts") < 3)
                {
                    healthCount[PlayerPrefs.GetInt("Hearts") - 1].sprite = healthOn;
                    RewardedHealthDT = DateTime.Now.AddMinutes(timeBetweenAds);
                    PlayerPrefs.SetInt("RewardedVideoYear", RewardedHealthDT.Year);
                    PlayerPrefs.SetInt("RewardedVideoMonth", RewardedHealthDT.Month);
                    PlayerPrefs.SetInt("RewardedVideoDay", RewardedHealthDT.Day);
                    PlayerPrefs.SetInt("RewardedVideoHour", RewardedHealthDT.Hour);
                    PlayerPrefs.SetInt("RewardedVideoMinute", RewardedHealthDT.Minute);
                    PlayerPrefs.SetInt("RewardedVideoSecond", RewardedHealthDT.Second);
                }
                else
                {
                    shouldHeal = false;
                    timer.gameObject.SetActive(false);
                    PlayerPrefs.SetInt("RewardedVideoYear", 0);
                }
            }
            else
            {
                timer.text = (RewardedHealthDT - nowTime).ToString(@"mm\:ss");
            }
        }
    }


    private void GetRewardHealthDT()
    {
        if (PlayerPrefs.GetInt("Hearts") < 3)
        {
            if (PlayerPrefs.HasKey("RewardedVideoYear") && PlayerPrefs.GetInt("RewardedVideoYear") != 0)
            {
                RewardedHealthDT = new DateTime(
                        PlayerPrefs.GetInt("RewardedVideoYear", DateTime.Now.Year),
                        PlayerPrefs.GetInt("RewardedVideoMonth", DateTime.Now.Month),
                        PlayerPrefs.GetInt("RewardedVideoDay", DateTime.Now.Day),
                        PlayerPrefs.GetInt("RewardedVideoHour", DateTime.Now.Hour),
                        PlayerPrefs.GetInt("RewardedVideoMinute", DateTime.Now.Minute),
                        PlayerPrefs.GetInt("RewardedVideoSecond", DateTime.Now.Second));
            }
            else
            {
                RewardedHealthDT = DateTime.Now.AddMinutes(timeBetweenAds);
                PlayerPrefs.SetInt("RewardedVideoYear", RewardedHealthDT.Year);
                PlayerPrefs.SetInt("RewardedVideoMonth", RewardedHealthDT.Month);
                PlayerPrefs.SetInt("RewardedVideoDay", RewardedHealthDT.Day);
                PlayerPrefs.SetInt("RewardedVideoHour", RewardedHealthDT.Hour);
                PlayerPrefs.SetInt("RewardedVideoMinute", RewardedHealthDT.Minute);
                PlayerPrefs.SetInt("RewardedVideoSecond", RewardedHealthDT.Second);
            }
            timer.gameObject.SetActive(true);
            shouldHeal = true;
        }
        else
        {
            shouldHeal = false;
        }
        for(int i = 0; i < PlayerPrefs.GetInt("Hearts"); i++)
        {
            healthCount[i].sprite = healthOn;
        }
    }
}
