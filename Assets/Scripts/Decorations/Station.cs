using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public int fishMaxSpawn, fishMinSpawn;
    public GameObject fishHolder;
    public float timeBetweenFish = 1f;
    private float curTime;
    private List<GameObject> enabledFish = new List<GameObject>();
    private List<GameObject> fish = new List<GameObject>();


    private void OnEnable()
    {
        curTime = timeBetweenFish;

        foreach (Transform c in fishHolder.GetComponent<Transform>())
        {
            fish.Add(c.transform.gameObject);
        }

        EnableFish();
    }

    private void Update()
    {

        if (curTime > 0)
            curTime -= Time.deltaTime;
        else
        {
            DisableFish();
            EnableFish();
            curTime = timeBetweenFish;
        }

    }

    private void EnableFish()
    {
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
        for (int i = 0; i < enabledFish.Count; i++)
        {
            enabledFish[i].SetActive(false);
            enabledFish.RemoveAt(i);
        }
    }
}
