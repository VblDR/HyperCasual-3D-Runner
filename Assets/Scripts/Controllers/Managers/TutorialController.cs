using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public List<GameObject> castles;

    public static TutorialController instance;

    //UI
    public GameObject leftCastleInfo, rigthCastleInfo, UpCastleInfo, movementInfo, downCastleInfo;
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
        if (PlayerTutorial.instance.shield.activeSelf)
            Shield.instance.AcceptDamage();
        
        StartCoroutine(CoroutineHelper.WaitFor(0.8f, delegate ()
        {
            blur.SetActive(true);
            leftCastleInfo.SetActive(true);
        }));

        StartCoroutine(CoroutineHelper.WaitFor(1.6f, delegate ()
        {
            blur.SetActive(false);
            leftCastleInfo.SetActive(false);
        }));
    }

    public void DestroyCastle()
    {
        castles.RemoveAt(0);

        if (castles.Count > 0)
        {

            if (castles.Count == 3)
                StartCoroutine(CoroutineHelper.WaitFor(0.2f, delegate ()
                {
                    StopRoad();
                    blur.SetActive(true);
                    rigthCastleInfo.SetActive(true);
                }));
            else if (castles.Count == 2) 
                StartCoroutine(CoroutineHelper.WaitFor(0.2f, delegate ()
                {
                    StopRoad();
                    blur.SetActive(true);
                    UpCastleInfo.SetActive(true);
                }));
            else
                StartCoroutine(CoroutineHelper.WaitFor(0.2f, delegate ()
                {
                    StopRoad();
                    blur.SetActive(true);
                    downCastleInfo.SetActive(true);
                }));


            StartCoroutine(CoroutineHelper.WaitFor(1.3f, delegate ()
            {
                if (castles.Count == 3)
                    rigthCastleInfo.SetActive(false);
                else if (castles.Count == 2)
                    UpCastleInfo.SetActive(false);
                else
                    downCastleInfo.SetActive(false);
                blur.SetActive(false);

                StartCoroutine(CoroutineHelper.WaitFor(0.5f, delegate () 
                { 
                    StartGame(); 
                }));
            }));

        }
            
    }


    public void GameOver()
    {
        StopRoad();

        blur.SetActive(true);
        lostPanel.SetActive(true);

    }


    public void GameFinished()
    {
        StopRoad();
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

    private void StopRoad()
    {
        PlayerTutorial.instance.SetEndPose();
        SwipeManagerTutorial.instance.enabled = false;
        TutorialGenerator.instance.StopRoad();
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
