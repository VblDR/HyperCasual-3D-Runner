using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTutorial : MonoBehaviour
{
    private bool active = true;
    public Pose pose;

    public void DestroyCastle()
    {
        active = false;
        PlayerTutorial.instance.SetEndPose();
        SendGameController();
        Destroy(gameObject);
    }

    private void SendGameController()
    {
        TutorialController.instance.DestroyCastle();
        if(gameObject != null) Destroy(gameObject, 1.2f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            active = false;
            Debug.Log("Player enterted castle");
            switch (pose)
            {
                case Pose.DOWN:
                    if (!PlayerTutorial.instance.DownPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.UP:
                    if (!PlayerTutorial.instance.UpPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.LEFT:
                    if (!PlayerTutorial.instance.LeftPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.RIGHT:
                    if (!PlayerTutorial.instance.RightPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            active = false;
            Debug.Log("Player enterted castle");
            switch (pose)
            {
                case Pose.DOWN:
                    if (!PlayerTutorial.instance.DownPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.UP:
                    if (!PlayerTutorial.instance.UpPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.LEFT:
                    if (!PlayerTutorial.instance.LeftPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.RIGHT:
                    if (!PlayerTutorial.instance.RightPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
            }
        }
    }
}
