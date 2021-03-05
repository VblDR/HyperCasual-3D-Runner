using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] obstacles;
    public GameObject heart, shield, coin;
    public Transform[] coinSpawn, heartSpawn, shieldSpawn;

    private void OnEnable()
    {
        if (spawnPoints.Length != 0)
        {
            if (Random.Range(0, 10) > 5)
            {
                Vector3 pos1 = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                GameObject obstacle1 = Instantiate(obstacles[Random.Range(0, obstacles.Length)], pos1, Quaternion.identity);
                obstacle1.transform.SetParent(transform);
            }

            if (PlayerPrefs.GetInt("EndlessLevel") == 0)
            {
                if (PlayerPrefs.GetInt("MustHeartSpawn") == 1 && Random.Range(0, 10) > 5)
                {
                    GameObject heartObj = Instantiate(heart, heartSpawn[Random.Range(0, 3)].position, Quaternion.identity);
                    heartObj.transform.SetParent(transform);
                    PlayerPrefs.SetInt("MustHeartSpawn", 0);
                }
                else if (PlayerPrefs.GetInt("MustShieldSpawn") == 1)
                {
                    PlayerPrefs.SetInt("MustShieldSpawn", 0);
                    GameObject shieldObj = Instantiate(shield, shieldSpawn[Random.Range(0, 3)].position, Quaternion.identity);
                    shieldObj.transform.SetParent(transform);
                }

                if (PlayerPrefs.GetInt("MoneyToSpawn") > 0 && Random.Range(0f, 10f) > 0.5f)
                {
                    GameObject coinObj = Instantiate(coin, coinSpawn[Random.Range(0, 3)].position, Quaternion.identity);
                    coinObj.transform.SetParent(transform);
                }
            }
            else
            {
                if (Random.Range(0f, 10f) > 9.8f)
                {
                    GameObject heartObj = Instantiate(heart, heartSpawn[Random.Range(0, 3)].position, Quaternion.identity);
                    heartObj.transform.SetParent(transform);
                    PlayerPrefs.SetInt("MustHeartSpawn", 0);
                }
                else if (Random.Range(0f, 10f) > 9.6f)
                {
                    PlayerPrefs.SetInt("MustShieldSpawn", 0);
                    GameObject shieldObj = Instantiate(shield, shieldSpawn[Random.Range(0, 3)].position, Quaternion.identity);
                    shieldObj.transform.SetParent(transform);
                }

                if (Random.Range(0f, 10f) > 6f)
                {
                    GameObject coinObj = Instantiate(coin, coinSpawn[Random.Range(0, 3)].position, Quaternion.identity);
                    coinObj.transform.SetParent(transform);
                }
            }
        }
    }
}
