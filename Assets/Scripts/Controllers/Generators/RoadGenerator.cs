using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator instance;

    [Header("Platfroms")]
    public GameObject smoothPlatformPrefab;
    public List<GameObject> platformsHolesPrefabs;
    public List<GameObject> specialPlatformPrefabs;

    [Space(10)]
    public float maxSpeed;
    private float speed;
    private float currentSpeed = 0;
    private float percent;

    private int currentStep = 0;

    private List<GameObject> roads = new List<GameObject>();
    public int maxRoads;

    private void Start()
    {
        speed = maxSpeed;
        CheckPlayerPrefs();
        instance = this;
        GenerateRoad();
    }

    void Update()
    {
        if (currentSpeed == 0) return;    

        foreach(GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, currentSpeed * Time.deltaTime);
        }

        if(roads[0].transform.position.z < -5)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }
    }

    private void GenerateRoad()
    {
        for(int i = 0; i < maxRoads; i++)
        {
            CreateNextRoad();
        }
    }

    private void CreateNextRoad()
    {
        GameObject platform;
        Vector3 pos = new Vector3(0, -0.25f, -3);
        if (roads.Count > 0)
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 3f);

        currentStep++;

        if(currentStep % 3 == 0 && currentStep > 8)
        {
            if(Random.Range(0, 10) < 6)
                platform = Instantiate(platformsHolesPrefabs[Random.Range(0, platformsHolesPrefabs.Count)], pos, Quaternion.identity);
            else
                platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);
        }
        else if(currentStep % 5 == 0)
        {
            if(Random.Range(0, 10) < 6)
                platform = Instantiate(specialPlatformPrefabs[Random.Range(0, specialPlatformPrefabs.Count)], pos, Quaternion.identity);
            else
                platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);
        }
        else
            platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);

        if (currentStep > 6)
            platform.GetComponent<Platform>().enabled = true;

        if (currentStep % 50 == 0)
        {
            if (!Mathf.Approximately(percent, 0.10f))
            {
                percent += 0.005f;
                speed = maxSpeed * (1 + percent);
                currentSpeed = speed;
            }
        }
        platform.transform.SetParent(transform);
        roads.Add(platform);
    }

    public void StopRoad()
    {
        currentSpeed = 0;
    }

    public void StartRoad()
    {
        currentSpeed = speed;
    }

    private void CheckPlayerPrefs()
    {
        PlayerPrefs.SetInt("EndlessLevel", 1);
        PlayerPrefs.SetInt("MoneyToSpawn", 2000);
    }
}