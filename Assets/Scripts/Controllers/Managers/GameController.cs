using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public GameObject movementInfo;
    private bool tutorial = false;

    public static GameController instance;

    [HideInInspector]
    public List<Castle> castles = new List<Castle>();

    //UI
    public GameObject lostPanel, finishPanel, lostPanelCastleStage;
    public GameObject blur;
    public Text moneyDisplay;

    //finish
    public GameObject[] particles;

    private bool castleStage = false;
    public bool bridgeHole = false;
    private bool gameOver = false;

    private void Awake()
    {
        instance = this;

        CheckPlayerPrefs();    
    }

    private void Start()
    {
        if (tutorial)
        {
            blur.SetActive(true);
            movementInfo.SetActive(true);

            StartCoroutine(CoroutineHelper.WaitFor(1.8f, delegate ()
            {
                blur.SetActive(false);
                movementInfo.SetActive(false);
            }));

            StartCoroutine(CoroutineHelper.WaitFor(2.6f, delegate ()
            {

                StartGame();
            }));

        }
        else
            StartCoroutine(CoroutineHelper.WaitFor(1.2f, delegate ()
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
        if (Player.instance.shield.activeSelf)
            Shield.instance.AcceptDamage();
    }

    public void DestroyCastle()
    {
        castles.RemoveAt(0);
        Debug.Log(castles.Count);
        LevelGenerator.instance.IncreaseSpeed();
        if (castles.Count == 0 && !gameOver)
            GameFinished();
    }


    public void GameOver()
    {
        gameOver = true;
        StopRoad();

        if(RoadGenerator.instance != null)
        {
            moneyDisplay.text = "+" + Player.instance.money;
        }

        blur.SetActive(true);
        if (castleStage)
        {
            lostPanelCastleStage.SetActive(true);
            StartCoroutine(CoroutineHelper.WaitFor(1, delegate ()
            {
                SceneManager.LoadScene("MainMenu");
            }));
        }
        else
        {
            lostPanel.SetActive(true);
        }
    }



    public void GameFinished()
    {
        StartCoroutine(CoroutineHelper.WaitFor(1, delegate ()
        {
            StopRoad();
            Player.instance.SetVictory();
        }));


        foreach(var c in particles)
        {
            c.SetActive(true);
        }
        StartCoroutine(CoroutineHelper.WaitFor(1.6f, delegate ()
        {
            blur.SetActive(true);
            finishPanel.SetActive(true);
            moneyDisplay.text = "+" + Player.instance.money;
        }));


        if (PlayerPrefs.GetInt("CastlesNumber") != 10 && PlayerPrefs.GetInt("Level") % 5 == 0)
            PlayerPrefs.SetInt("CastlesNumber", PlayerPrefs.GetInt("CastlesNumber") + 1);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        if (PlayerPrefs.GetInt("Level") % 10 == 0)
        {
            if (!Mathf.Approximately(PlayerPrefs.GetFloat("SpeedPercent"), 0.1f))
            {
                float percent = PlayerPrefs.GetFloat("SpeedPercent");
                float speed = PlayerPrefs.GetFloat("Speed") - PlayerPrefs.GetFloat("Speed") * percent;
                percent += 0.05f;
                speed += speed * percent;
                PlayerPrefs.SetFloat("SpeedPercent", percent);
                PlayerPrefs.SetFloat("Speed", speed);
            } 
        }

        if (PlayerPrefs.GetFloat("ObstacleProbability") < 0.4f)
        {
            PlayerPrefs.SetFloat("ObstacleProbability", PlayerPrefs.GetFloat("ObstacleProbability") + 0.006f);
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
        gameOver = false;
        lostPanel.SetActive(false);
        blur.SetActive(false);
        Player.instance.StopFallingCoroutine();
        Player.instance.SetDefaultState();
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

                    if(Player.instance.health == 0)
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
                SwipeManager.instance.enabled = true;
                SwipeManager.instance.SetFinish(true);
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
                blur.SetActive(true);
                finishPanel.SetActive(true);
                moneyDisplay.text = "+" + Player.instance.money;
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            }
            else
            {
                Player.instance.IncreaseHP();
                SwipeManager.instance.enabled = true;
                StartGame();
            }
        }

    }

    private void CheckPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
        }
        if(PlayerPrefs.GetInt("Tutorial") == 1)
        {
            tutorial = true;
            PlayerPrefs.SetInt("Tutorial", 0);
        }

        if(PlayerPrefs.GetInt("ShieldBought") == 1)
        {
            Player.instance.ActivateShield();
        }
    }

    private void StopRoad()
    {
        SwipeManager.instance.enabled = false;
        if (RoadGenerator.instance != null) RoadGenerator.instance.StopRoad();
        else LevelGenerator.instance.StopRoad();
        IslandGenerator.instance.StopIslands();
    }

    public void SlowMotionOn()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.2F * Time.timeScale;
    }

    public void SlowMotionOff()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.2F * Time.timeScale;
    }
}
