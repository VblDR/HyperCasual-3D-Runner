using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public GameObject banner, banner2;

    public List<GameObject> palms;
    public int minNumer = 7;
    public int maxNumber = 11;

    public GameObject spawnHolder;
    private List<Vector3> spawnPoints = new List<Vector3>();

    public int fishMaxSpawn, fishMinSpawn;
    public GameObject fishHolder;
    public float timeBetweenFish = 1f;
    private float curTime;
    private List<GameObject> enabledFish = new List<GameObject>();
    private List<GameObject> fish = new List<GameObject>();

    private List<Vector3> used = new List<Vector3>();

    private void Start()
    {
        curTime = timeBetweenFish;

        //palms
        foreach (var c in spawnHolder.GetComponentsInChildren<Transform>())
        {
            spawnPoints.Add(c.position);
        }

        int rand = Random.Range(minNumer, maxNumber + 1);
        for (int i = 0; i < rand; i++)
        {
            Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Count)];
            while (used.Contains(pos))
                pos = spawnPoints[Random.Range(0, spawnPoints.Count)];
            used.Add(pos);

            Quaternion euler = Quaternion.Euler(0, Random.Range(30f, 50f), 0);

            GameObject palm = Instantiate(palms[Random.Range(0, palms.Count)], pos, euler);
            palm.transform.SetParent(transform);
        }
        EnableFish();
    }

    private void Update()
    {
        /*
        if (curTime > 0)
            curTime -= Time.deltaTime;
        else
        {
            DisableFish();
            EnableFish();
            curTime = timeBetweenFish;
        }
        */
    }

    public void SpawnBanner(bool isRight)
    {
        if(isRight)
            banner2.SetActive(true);
        else   
            banner.SetActive(true);
    }

    private void EnableFish()
    {
        foreach (Transform c in fishHolder.GetComponent<Transform>())
        {
            fish.Add(c.transform.gameObject);
        }

        int rand = Random.Range(fishMinSpawn, fishMaxSpawn + 1);
        for (int i = 0; i < rand; i++)
        {
            GameObject curFish = fish[Random.Range(0, fish.Count)];
            while (curFish.activeSelf)
                curFish = fish[Random.Range(0, fish.Count)];

            curFish.SetActive(true);
            enabledFish.Add(curFish);
        }
    }

    private void DisableFish()
    {
        for(int i = 0; i < enabledFish.Count; i++)
        {
            enabledFish[i].SetActive(false);
            enabledFish.RemoveAt(i);
        }
    }
}
