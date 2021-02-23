using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameObject startButton;
    public GameObject restartButton;

    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        SwipeManager.instance.enabled = true;
        startButton.SetActive(false);
        RoadGenerator.instance.StartRoad();
        Player.instance.StartMoving();
    }

    public void GameOver()
    {
        RoadGenerator.instance.StopRoad();
        SwipeManager.instance.enabled = false;
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
