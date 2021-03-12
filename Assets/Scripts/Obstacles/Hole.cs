using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && active)
        {
            Player.instance.Fall();
            active = false;
        }
    }
}
