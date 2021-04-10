using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameController.instance != null)
                GameController.instance.SlowMotionOn();
            else
                TutorialController.instance.SlowMotionOn();
        }
    }
}
