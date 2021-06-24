using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    public static IslandGenerator instance;

    public GameObject[] islands;
    public float maxSpeed = 5;
    public int maxIslands = 3;
    public Vector3 startPos;


    private bool isRight;
    private float speed;
    private List<GameObject> spawned = new List<GameObject>();
    private List<Transform> spawnedTransform = new List<Transform>();

    private float timeToSecret = 300;
    private bool secretSpawned = false;
    private bool isEndless = false;
    public Vector3 direction;

    private void Start()
    {
        if (PlayerPrefs.GetInt("EndlessLevel") == 1) isEndless = true;
        instance = this;
        GenerateIslands();
    }

    private void Update()
    {
        if (isEndless && timeToSecret > 0)
            timeToSecret -= Time.deltaTime;

        if (timeToSecret > 0)
            timeToSecret -= Time.deltaTime;

        if (speed == 0) return;

        for(int i = 0; i < spawnedTransform.Count; i++)
        {
            spawnedTransform[i].position -= direction * Time.deltaTime;
        }

        if (spawned[0].transform.position.z < -10)
        {
            Destroy(spawned[0]);
            spawned.RemoveAt(0);
            spawnedTransform.RemoveAt(0);
            CreateNextIsland();
        }
    }

    private void GenerateIslands()
    {
        for (int i = 0; i < maxIslands; i++)
        {
            CreateNextIsland();
        }
    }

    private void CreateNextIsland()
    {
        Vector3 pos = startPos;
        if (spawned.Count > 0)
            pos.z = spawnedTransform[spawnedTransform.Count - 1].position.z + 60;

        Quaternion euler = isRight ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        pos.x += isRight ? 11 : -11;
        isRight = !isRight;

        GameObject curIsland = Instantiate(islands[Random.Range(0, islands.Length)], pos, euler);
        curIsland.transform.SetParent(transform);
        if(timeToSecret < 0 && !secretSpawned)
        {
            if (curIsland.GetComponent<Island>() != null)
            {
                secretSpawned = true;
                curIsland.GetComponent<Island>().SpawnBanner(!isRight);
            }
        }
        spawned.Add(curIsland);
        spawnedTransform.Add(curIsland.transform);
    }

    public void MoveIslands()
    {
        speed = maxSpeed;
        direction = new Vector3(0, 0, speed);
    }

    public void StopIslands()
    {
        speed = 0;
        direction = new Vector3(0, 0, speed);
    }
}


