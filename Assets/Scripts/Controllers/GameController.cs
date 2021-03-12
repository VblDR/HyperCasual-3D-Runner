using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameObject restartButton;

    public static GameController instance;

    [HideInInspector]
    public List<Castle> castles = new List<Castle>();

    //UI
    public GameObject lostPanel, finishPanel;
    public GameObject darkPanel;
    public Text moneyDisplay;

    //finish
    public GameObject[] particles;

    private bool castleStage = false;
    public bool bridgeHole = false;


    private void Awake()
    {
        instance = this;

        CheckPlayerPrefs();    
    }

    private void Start()
    {
        StartCoroutine(CoroutineHelper.WaitFor(1f, delegate ()
        {
            StartGame();
        }));
    }

    public void StartGame()
    {
        SwipeManager.instance.enabled = true;
        if (RoadGenerator.instance != null)
            RoadGenerator.instance.StartRoad();
        else
            LevelGenerator.instance.StartRoad();

        IslandGenerator.instance.MoveIslands();
        Player.instance.StartMoving();
    }

    public void ActivateCastles()
    {
        castleStage = true;
        foreach(var c in castles)
        {
            c.MoveCastle();
        }
    }

    public void DestroyCastle()
    {
        castles.RemoveAt(0);
        Debug.Log(castles.Count);
        if (castles.Count == 0 && Player.instance.health != 0)
            GameFinished();
    }


    public void GameOver()
    {
        SwipeManager.instance.enabled = false;
        if (RoadGenerator.instance != null)
            RoadGenerator.instance.StopRoad();
        else
            LevelGenerator.instance.StopRoad();
        IslandGenerator.instance.StopIslands();

        if(castleStage)
            foreach(Castle c in castles)
            {
                c.StopCastle();
            }

        darkPanel.SetActive(true);
        lostPanel.SetActive(true);

    }


    public void GameFinished()
    {
        Player.instance.SetVictory();
        foreach(var c in particles)
        {
            c.SetActive(true);
        }
        darkPanel.SetActive(true);
        finishPanel.SetActive(true);
        moneyDisplay.text = "+" + Player.instance.money;
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        if (PlayerPrefs.GetInt("Level") % 10 == 0)
        {
            if (!Mathf.Approximately(PlayerPrefs.GetFloat("SpeedPercent"), 0.1f))
            {
                float percent = PlayerPrefs.GetFloat("SpeedPercent");
                float speed = PlayerPrefs.GetFloat("Speed") - PlayerPrefs.GetFloat("Speed") * percent;
                speed += speed * percent;
                PlayerPrefs.SetFloat("SpeedPercent", percent);
                PlayerPrefs.SetFloat("Speed", speed);
            } 
        }
    }

    //button funcs
    public void ReturnToMainMenu()
    {
        if (PlayerPrefs.GetInt("EndlessLevel") == 1)
            PlayerPrefs.SetInt("EndlessLevel", 0);

        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void AdsButton()
    {
        ReturnPlayerToLevel();
    }

    public void ReturnPlayerToLevel()
    {

        lostPanel.SetActive(false);
        darkPanel.SetActive(false);
        if (!castleStage)
        {
            if (!bridgeHole)
            {
                if (RoadGenerator.instance != null)
                    RoadGenerator.instance.StartRoad();
                else
                    LevelGenerator.instance.StartRoad();
                IslandGenerator.instance.MoveIslands();

                StartCoroutine(CoroutineHelper.WaitFor(0.2f, delegate ()
                {
                    if (RoadGenerator.instance != null)
                        RoadGenerator.instance.StopRoad();
                    else
                        LevelGenerator.instance.StopRoad();
                    IslandGenerator.instance.StopIslands();

                    Player.instance.IncreaseHP();
                    Player.instance.transform.position = new Vector3(Player.instance.transform.position.x, 0, 1);


                    SwipeManager.instance.enabled = false;
                    Player.instance.SetImmortality();
                    StartGame();

                }));
            }
            else
            {
                Player.instance.IncreaseHP();
                Player.instance.transform.position = new Vector3(0, 0, 1);
                StartGame();
                SwipeManager.instance.enabled = false;
            }
        }
        else
        {
            if (castles.Count == 0)
            {
                foreach (var c in particles)
                {
                    c.SetActive(true);
                }
                Player.instance.IncreaseHP();
                darkPanel.SetActive(true);
                finishPanel.SetActive(true);
                moneyDisplay.text = "+" + Player.instance.money;
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            }
            else
            {
                Player.instance.IncreaseHP();
                SwipeManager.instance.enabled = true;
                ActivateCastles();
            }
        }

    }

    private void CheckPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
        }
    }
}
