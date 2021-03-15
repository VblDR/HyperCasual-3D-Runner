using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public float maxTime;
    private float timer = 0;
    public GameObject[] boats;

    private void Update()
    {
        if (timer <= 0)
        {
            timer = maxTime;
            Instantiate(boats[Random.Range(0, boats.Length)], spawnPoint);
        }
        else
            timer -= Time.deltaTime;
    }
}
