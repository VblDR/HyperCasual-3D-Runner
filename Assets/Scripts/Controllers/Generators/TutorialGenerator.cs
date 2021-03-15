using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGenerator : MonoBehaviour
{
    public static TutorialGenerator instance;

    [Space(10)]
    public float maxSpeed;
    private float speed = 0;

    public List<GameObject> roads = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        CheckPlayerPrefs();
        if (roads.Count == 0)
            foreach (Transform c in GetComponentInChildren<Transform>())
                roads.Add(c.gameObject);
    }

    private void Update()
    {
        if (speed != 0)
            MovePlatforms();

        if (roads[0].transform.position.z < -5)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
    }
    private void MovePlatforms()
    {
        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }
    }

    public void StopRoad()
    {
        speed = 0;
    }

    public void StartRoad()
    {
        speed = maxSpeed;
    }

    private void CheckPlayerPrefs()
    {

        if (PlayerPrefs.GetInt("LevelWithoutHeart") == 4)
        {
            PlayerPrefs.SetInt("MustHeartSpawn", 1);
            PlayerPrefs.SetInt("LevelWithoutHeart", 0);
        }
        else
            PlayerPrefs.SetInt("LevelWithoutHeart", PlayerPrefs.GetInt("LevelWithoutHeart") + 1);

        maxSpeed = PlayerPrefs.GetFloat("Speed");
        PlayerPrefs.SetInt("MoneyToSpawn", 15 + PlayerPrefs.GetInt("Level") - 1);
        PlayerPrefs.SetInt("MustShieldSpawn", 1);
    }
}
