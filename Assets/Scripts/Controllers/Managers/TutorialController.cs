using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public List<CastleTutorial> castles;

    public static TutorialController instance;

    //UI
    public GameObject leftCastleInfo, rigthCastleInfo, UpCastleInfo, movementInfo;
    public GameObject lostPanel, finishPanel;
    public GameObject blur;
    public Text moneyDisplay;

    //finish
    public GameObject[] particles;

    private bool castleStage = false;
    public bool bridgeHole = false;
    private bool tutorial = false;

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
        SwipeManagerTutorial.instance.enabled = true;
        TutorialGenerator.instance.StartRoad();

        IslandGenerator.instance.MoveIslands();
        PlayerTutorial.instance.StartMoving();
    }

    public void ActivateCastles()
    {
        castleStage = true;
        blur.SetActive(true);
        leftCastleInfo.SetActive(true);

        StartCoroutine(CoroutineHelper.WaitFor(0.7f, delegate ()
        {
            blur.SetActive(false);
            leftCastleInfo.SetActive(false);
        }));

        StartCoroutine(CoroutineHelper.WaitFor(1.2f, delegate ()
        {
            foreach (var c in castles)
            {
                c.MoveCastle();
            }
        }));

    }

    public void DestroyCastle()
    {
        castles.RemoveAt(0);

        if(castles.Count > 0)
        {
            foreach (CastleTutorial c in castles)
                c.StopCastle();

            blur.SetActive(true);
            if (castles.Count == 2)
                rigthCastleInfo.SetActive(true);
            else
                UpCastleInfo.SetActive(true);


            StartCoroutine(CoroutineHelper.WaitFor(0.7f, delegate ()
            {
                blur.SetActive(false);
                if (castles.Count == 2)
                    rigthCastleInfo.SetActive(false);
                else
                    UpCastleInfo.SetActive(false);
            }));

            StartCoroutine(CoroutineHelper.WaitFor(1.2f, delegate ()
            {
                foreach (var c in castles)
                {
                    c.MoveCastle();
                }
            }));
        }
        else if (castles.Count == 0 && PlayerTutorial.instance.health != 0)
            GameFinished();
    }


    public void GameOver()
    {
        SwipeManagerTutorial.instance.enabled = false;
        TutorialGenerator.instance.StopRoad();
        IslandGenerator.instance.StopIslands();

        if (castleStage)
            foreach (CastleTutorial c in castles)
            {
                c.StopCastle();
            }

        blur.SetActive(true);
        lostPanel.SetActive(true);

    }


    public void GameFinished()
    {
        PlayerTutorial.instance.SetVictory();
        foreach (var c in particles)
        {
            c.SetActive(true);
        }
        StartCoroutine(CoroutineHelper.WaitFor(1.6f, delegate ()
        {
            blur.SetActive(true);
            finishPanel.SetActive(true);
            moneyDisplay.text = "+" + PlayerTutorial.instance.money;
        }));
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

        if (PlayerPrefs.GetFloat("ObstacleProbability") < 0.4f)
        {
            PlayerPrefs.SetFloat("ObstacleProbability", PlayerPrefs.GetFloat("ObstacleProbability") + 0.008f);
        }
        PlayerPrefs.SetInt("TutorialCastles", 0);
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
        blur.SetActive(false);
        PlayerTutorial.instance.StopFallingCoroutine();
        if (!castleStage)
        {
            if (!bridgeHole)
            {
                TutorialGenerator.instance.StartRoad();

                StartCoroutine(CoroutineHelper.WaitFor(0.2f, delegate ()
                {
                    TutorialGenerator.instance.StopRoad();

                    PlayerTutorial.instance.IncreaseHP();
                    PlayerTutorial.instance.transform.position = new Vector3(PlayerTutorial.instance.transform.position.x, 0, 1);


                    SwipeManagerTutorial.instance.enabled = false;
                    PlayerTutorial.instance.SetImmortality();
                    StartGame();

                }));
            }
            else
            {
                PlayerTutorial.instance.IncreaseHP();
                PlayerTutorial.instance.transform.position = new Vector3(0, 0, 1);
                StartGame();
                SwipeManagerTutorial.instance.enabled = true;
                SwipeManagerTutorial.instance.SetFinish(true);
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
                PlayerTutorial.instance.IncreaseHP();
                blur.SetActive(true);
                finishPanel.SetActive(true);
                moneyDisplay.text = "+" + Player.instance.money;
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            }
            else
            {
                PlayerTutorial.instance.IncreaseHP();
                SwipeManagerTutorial.instance.enabled = true;
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
        if (PlayerPrefs.GetInt("Tutorial") == 1)
        {
            tutorial = true;
            PlayerPrefs.SetInt("Tutorial", 0);
        }
    }
}
