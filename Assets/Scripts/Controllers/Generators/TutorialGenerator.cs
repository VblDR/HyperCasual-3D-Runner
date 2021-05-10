using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGenerator : MonoBehaviour
{
    public static TutorialGenerator instance;

    [Space(10)]
    public float maxSpeed;
    private float speed = 0;

    Vector3 direction;
    public List<GameObject> roads = new List<GameObject>();

    private void Awake()
    {
        direction = new Vector3(0, 0, speed);
        instance = this;
        CheckPlayerPrefs();
        if (roads.Count == 0)
            foreach (Transform c in GetComponentInChildren<Transform>())
                roads.Add(c.gameObject);
    }

    private void Update()
    {
        if (roads[0].transform.position.z < -16)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
    }

    private void FixedUpdate()
    {
        if (speed != 0)
            MovePlatforms();
    }

    private void MovePlatforms()
    {
        for(int i = 0; i < roads.Count; i++)
        {
            roads[i].GetComponentInParent<Rigidbody>().velocity = -direction * Time.fixedDeltaTime * 70;
        }
    }

    public void StopRoad()
    {
        speed = 0;
        for(int i = 0; i < roads.Count; i++)
        {
            roads[i].GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void StartRoad()
    {
        speed = maxSpeed;
        direction.z = speed;
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
