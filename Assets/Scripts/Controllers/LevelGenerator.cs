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
    
    [Header("Finish Prefs")]
    public GameObject bridge;
    public GameObject transporter;
    public List<GameObject> castles;

    [Space(10)]
    public float maxSpeed;
    public float maxSpeedOnFinish;
    private float speed = 0;

    private List<GameObject> roads = new List<GameObject>();
    public int roadsNumber;

    private void Awake()
    {
        instance = this;
        CheckPlayerPrefs();
    }

    private void Start()
    {
        CreateLevel();
    }

    private void Update()
    {
        if (speed != 0)
            MovePlatforms();

        if(roads[0].transform.position.z < -5)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
    }

    private void CreateLevel()
    {
        Vector3 pos = new Vector3(0, -0.25f, -3);

        for(int i = 1; i <= roadsNumber; i++)
        {
            CreatePlatfrom(pos, i);
            pos += new Vector3(0, 0, 3);
        }

        GameObject platform;
        platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);
        platform.transform.SetParent(transform);
        roads.Add(platform);

        pos += new Vector3(0, 0, 3.775f);
        GameObject bridgePlatform = Instantiate(bridge, pos, Quaternion.identity);
        bridgePlatform.transform.SetParent(transform);
        roads.Add(bridgePlatform);

        pos += new Vector3(0, 0, 6.188f);
        GameObject transporterPlatform = Instantiate(transporter, pos, Quaternion.identity);
        transporterPlatform.transform.SetParent(transform);
        roads.Add(transporterPlatform);

        pos += new Vector3(0, 0, 7.826f);
        GameObject castle = Instantiate(castles[Random.Range(0, 3)], pos, Quaternion.identity);
        castle.transform.SetParent(transform);
        roads.Add(castle);
        GameController.instance.castles.Add(castle.GetComponentInChildren<Castle>());

        pos += new Vector3(0, 0, 7.826f);
        GameObject castle2 = Instantiate(castles[Random.Range(0, 3)], pos, Quaternion.identity);
        castle2.transform.SetParent(transform);
        roads.Add(castle2);
        GameController.instance.castles.Add(castle2.GetComponentInChildren<Castle>());

        pos += new Vector3(0, 0, 7.826f);
        GameObject castle3 = Instantiate(castles[Random.Range(0, 3)], pos, Quaternion.identity);
        castle3.transform.SetParent(transform);
        roads.Add(castle3);
        GameController.instance.castles.Add(castle3.GetComponentInChildren<Castle>());
    }

    private void CreatePlatfrom(Vector3 pos, int step)
    {
        GameObject platform;

        if (step % 3 == 0 && step > 6)
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

         roadsNumber += PlayerPrefs.GetInt("Level");
            
         if(PlayerPrefs.GetInt("LevelWithoutHeart") == 4)
         {
              PlayerPrefs.SetInt("MustHeartSpawn", 1);
              PlayerPrefs.SetInt("LevelWithoutHeart", 0);
         }
         else
            PlayerPrefs.SetInt("LevelWithoutHeart", PlayerPrefs.GetInt("LevelWithoutHeart") + 1);

        PlayerPrefs.SetInt("MoneyToSpawn", 15 + PlayerPrefs.GetInt("Level") - 1);
        PlayerPrefs.SetInt("MustShieldSpawn", 1);
    }
}
