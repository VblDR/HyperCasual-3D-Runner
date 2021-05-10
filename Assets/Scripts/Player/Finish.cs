using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{

    public bool active;
    private void OnTriggerEnter(Collider other)
    {

        if (active)
        {

            active = false;
            if (Player.instance != null)
            {
                GameController.instance.GameFinished();
            }
            else
            {
                TutorialController.instance.GameFinished();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active)
        {

            active = false;
            if (Player.instance != null)
            {
                GameController.instance.GameFinished();
            }
            else
            {
                TutorialController.instance.GameFinished();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (active)
        {
            active = false;
            if (Player.instance != null)
            {
                GameController.instance.GameFinished();
            }
            else
            {
                TutorialController.instance.GameFinished();
            }
        }
    }   

    public void Activate()
    {
        active = true;
    }
}
