using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator instance;

    [Header("Platfroms")]
    public GameObject smoothPlatformPrefab;
    public List<GameObject> platformsHolesPrefabs;
    public List<GameObject> specialPlatformPrefabs;
    public GameObject stairsUp;
    public GameObject stairsDown;

    [Space(10)]
    public float maxSpeed;
    public float stairsLength;
    private float speed;
    private float currentSpeed = 0;
    private float percent;
    private Vector3 direction;
    private float posAddition = 0;

    private int currentStep = 0;

    private List<GameObject> roads = new List<GameObject>();
    public int maxRoads;


    public bool startRec = false;
    public Text score;
    public Text bestScore;
    private float scoreRiched = 0;
    private int bestScoreRiched;

    private void Start()
    {
        speed = maxSpeed;
        CheckPlayerPrefs();
        instance = this;
        GenerateRoad();
        direction = new Vector3(0, 0, currentSpeed);
    }

    void Update()
    {
        if (currentSpeed == 0) return;    

        foreach(GameObject road in roads)
        {
            road.transform.position -= direction*Time.deltaTime;
        }

        if(roads[0].transform.position.z < -11)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }

        if (startRec)
            WriteScore();
    }

    private void GenerateRoad()
    {
        for(int i = 0; i < maxRoads; i++)
        {
            CreateNextRoad();
        }
    }



    private void WriteScore()
    {
        scoreRiched += speed * Time.deltaTime;
        score.text = System.Convert.ToString((int)scoreRiched);
        if (scoreRiched > bestScoreRiched)
        {
            bestScore.text = System.Convert.ToString((int)scoreRiched);
            PlayerPrefs.SetInt("BestScore", (int)scoreRiched);
        }
    }

    private void CreateNextRoad()
    {
        GameObject platform;
        Vector3 pos = new Vector3(0, -0.25f, 0);
        if (roads.Count > 1)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, posAddition, 3.1f);
            posAddition = 0;
        }
        currentStep++;

        if(currentStep % 5 == 0 && currentStep > 8)
        {
            if(Random.Range(0, 10) < 6)
                platform = Instantiate(platformsHolesPrefabs[Random.Range(0, platformsHolesPrefabs.Count)], pos, Quaternion.identity);
            else
                platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);
        }
        else if(currentStep % 3 == 0 && currentStep > 8)
        {
            if(Random.Range(0, 10) < 6)
                platform = Instantiate(specialPlatformPrefabs[Random.Range(0, specialPlatformPrefabs.Count)], pos, Quaternion.identity);
            else
                platform = Instantiate(smoothPlatformPrefab, pos, Quaternion.identity);
        }
        else if (currentStep % 7 == 0)
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
        
        if (currentStep > 6)
            platform.GetComponent<Platform>().enabled = true;

        if (currentStep % 50 == 0)
        {
            if (!Mathf.Approximately(percent, 0.10f))
            {
                percent += 0.005f;
                speed = maxSpeed * (1 + percent);
                currentSpeed = speed;
                direction.z = currentSpeed;
            }
        }
        platform.transform.SetParent(transform);
        roads.Add(platform);
    }

    public void StopRoad()
    {
        currentSpeed = 0;
        direction.z = 0;
        startRec = false; 
    }

    public void StartRoad()
    {
        currentSpeed = speed;
        direction.z = currentSpeed;
        startRec = true;
    }

    private void CheckPlayerPrefs()
    {
        PlayerPrefs.SetInt("EndlessLevel", 1);
        PlayerPrefs.SetInt("MoneyToSpawn", 2000);
        bestScoreRiched = PlayerPrefs.GetInt("BestScore");
        bestScore.text = System.Convert.ToString(bestScoreRiched);
    }

    public void MoveUp()
    {
        direction.y -= 1.9f;
    }

    public void MoveDown()
    {
        direction.y += 1.9f;
    }
}
