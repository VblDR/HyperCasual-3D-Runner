using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    
    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        //GameController.instance.SlowMotionOff();

        if (other.CompareTag("Player") && active)
        {

            active = false;
            if(Player.instance != null)
            {
                Player.instance.SetEndPose();
                GameController.instance.DestroyCastle();
            }
            else
            {
                PlayerTutorial.instance.SetEndPose();
                TutorialController.instance.DestroyCastle();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && active)
        {

            active = false;
            if (Player.instance != null)
            {
                Player.instance.SetEndPose();
                GameController.instance.DestroyCastle();
            }
            else
            {
                PlayerTutorial.instance.SetEndPose();
                TutorialController.instance.DestroyCastle();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && active)
        {

            active = false;
            if (Player.instance != null)
            {
                Player.instance.SetEndPose();
                GameController.instance.DestroyCastle();
            }
            else
            {
                PlayerTutorial.instance.SetEndPose();
                TutorialController.instance.DestroyCastle();
            }
        }
    }

    public void Unable()
    {
        Debug.Log("unabled");
        active = false;
    }
    
}
