using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] obstacles;

    private void OnEnable()
    {
        if (spawnPoints.Length != 0)
        {
            if (Random.Range(0, 10) > 3)
            {
                Vector3 pos1 = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                GameObject obstacle1 = Instantiate(obstacles[Random.Range(0, obstacles.Length)], pos1, Quaternion.identity);
                obstacle1.transform.SetParent(transform);

                if (spawnPoints.Length > 1)
                {
                    if (Random.Range(0, 10) > 8)
                    {
                        Vector3 pos2 = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                        while (pos2 == pos1)
                            pos2 = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                        GameObject obstacle2 = Instantiate(obstacles[Random.Range(0, obstacles.Length)], pos2, Quaternion.identity);
                        obstacle2.transform.SetParent(transform);
                    }
                }
            }
        }
    }
}
