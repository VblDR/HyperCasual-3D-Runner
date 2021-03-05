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

    private void Awake()
    {
        instance = this;

        CheckPlayerPrefs();

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
        foreach(var c in castles)
        {
            c.MoveCastle();
        }
    }

    public void DestroyCastle()
    {
        Destroy(castles[0], 1f);
        castles.RemoveAt(0);

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

        foreach(Castle c in castles)
        {
            c.StopCastle();
        }

        darkPanel.SetActive(true);
        lostPanel.SetActive(true);

        if (PlayerPrefs.GetInt("EndlessLevel") == 1)
            PlayerPrefs.SetInt("EndlessLevel", 0);
    }


    public void GameFinished()
    {
        foreach(var c in particles)
        {
            c.SetActive(true);
        }
        darkPanel.SetActive(true);
        finishPanel.SetActive(true);
        moneyDisplay.text = "+" + Player.instance.money;
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
    }

    //button funcs
    public void ReturnToMainMenu()
    {
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
        if (RoadGenerator.instance != null)
            RoadGenerator.instance.StartRoad();
        else
            LevelGenerator.instance.StartRoad();
        IslandGenerator.instance.MoveIslands();

        StartCoroutine(CoroutineHelper.WaitFor(0.5f, delegate ()
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

    private void CheckPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
        }
    }
}
