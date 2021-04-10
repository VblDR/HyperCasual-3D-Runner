using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    [Header("Platfroms")]
    public GameObject smoothPlatformPrefab;
    public List<GameObject> platformsHolesPrefabs;
    public List<GameObject> specialPlatformPrefabs;
    public GameObject stairsUp;
    public GameObject stairsDown;

    [Header("Finish Prefs")]
    public GameObject bridge;
    public GameObject transporter;
    public List<GameObject> castles;

    [Space(10)]
    public float maxSpeed;
    public float stairsLength;
    private float speed = 0;

    private List<GameObject> roads = new List<GameObject>();
    public int roadsNumber;
    private Vector3 direction;
    private int castleNumber = 4;
    private float posAddition;

    private void Awake()
    {
        instance = this;
        CheckPlayerPrefs();

    }

    private void Start()
    {
        direction = new Vector3(0, 0, speed);
        CreateLevel();
    }

    private void Update()
    {
        if (speed != 0)
            MovePlatforms();

        if(roads[0].transform.position.z < -16)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
    }

    private void CreateLevel()
    {
        Vector3 pos = new Vector3(0, -0.25f, 0);

        for(int i = 1; i <= roadsNumber; i++)
        {
            CreatePlatfrom(pos, i);
            pos += new Vector3(0, posAddition, 3.1f);
            posAddition = 0;
        }

        GameObject platform;
        platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);
        platform.transform.SetParent(transform);
        roads.Add(platform);

        pos += new Vector3(0, 0, 4.6005f);
        GameObject bridgePlatform = Instantiate(bridge, pos, Quaternion.identity);
        bridgePlatform.transform.SetParent(transform);
        roads.Add(bridgePlatform);

        pos += new Vector3(0, 0, 7f);

        for(int i = 0; i < castleNumber; i++)
        {
            GameObject castle = Instantiate(castles[Random.Range(0, 4)], pos, Quaternion.identity);
            castle.transform.SetParent(transform);
            roads.Add(castle);
            GameController.instance.castles.Add(castle.GetComponentInChildren<Castle>());
            pos += new Vector3(0, 0, 15.6f);
        }
    }

    private void CreatePlatfrom(Vector3 pos, int step)
    {
        GameObject platform;

        if (step % 3 == 0 && step > 8)
        {
            if (Random.Range(0, 10) < 4)
                platform = Instantiate(platformsHolesPrefabs[Random.Range(0, platformsHolesPrefabs.Count)], pos, Quaternion.identity);
            else
                platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);
        }
        else if (step % 5 == 0)
        {
            if (Random.Range(0, 10) < 6)
                platform = Instantiate(specialPlatformPrefabs[Random.Range(0, specialPlatformPrefabs.Count)], pos, Quaternion.identity);
            else
                platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);
        }
        else if (step % 7 == 0)
        {
            if (Random.Range(0, 10) > 6)
            {

                if (Random.Range(0, 2) > 0)
                {
                    platform = Instantiate(stairsDown, pos, Quaternion.identity);
                    posAddition -= stairsLength;
                }
                else
                {
                    platform = Instantiate(stairsUp, pos, Quaternion.identity);
                    posAddition += stairsLength;
                }
            }
            else
            {
                platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);
                if (Random.Range(0, 2) == 1)
                    platform.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
            platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);

        if (step > 6)
            platform.GetComponent<Platform>().enabled = true;

        platform.transform.SetParent(transform);
        roads.Add(platform);
    }

    private void MovePlatforms()
    {
        foreach (GameObject road in roads)
        {
            road.transform.position -= direction * Time.deltaTime;
        }
    }

    public void StopRoad()
    {
        speed = 0;
        direction.z = 0;
    }

    public void StartRoad()
    {
        speed = maxSpeed;
        direction.z = maxSpeed;
    }

    private void CheckPlayerPrefs()
    {

        roadsNumber += PlayerPrefs.GetInt("CastlesNumber");

        maxSpeed = PlayerPrefs.GetFloat("Speed");
        PlayerPrefs.SetInt("MoneyToSpawn", 15 + PlayerPrefs.GetInt("Level") - 1);
        PlayerPrefs.SetInt("MustShieldSpawn", 1);
        castleNumber = PlayerPrefs.GetInt("CastlesNumber");
    }
    public void MoveUp()
    {
        direction.y -= 1.9f;
    }

    public void MoveDown()
    {
        direction.y += 1.9f;
    }


    public void IncreaseSpeed()
    {
        speed += 0.2f * speed;
        maxSpeed = speed;
    }
}
