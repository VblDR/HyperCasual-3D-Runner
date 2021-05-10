using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private bool active = true;

    public Pose pose;
    public Finish finish;
    public void DestroyCastle()
    {
        Player.instance.Death();
        SendGameController();
    }

    private void SendGameController()
    {
        GameController.instance.DestroyCastle();
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
                    if (!Player.instance.DownPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.UP:
                    if (!Player.instance.UpPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.LEFT:
                    if (!Player.instance.LeftPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.RIGHT:
                    if (!Player.instance.RightPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
            }
        
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && active)
        {
            active = false;
            Debug.Log("Player enterted castle");
            switch (pose)
            {
                case Pose.DOWN:
                    if (!Player.instance.DownPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.UP:
                    if (!Player.instance.UpPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.LEFT:
                    if (!Player.instance.LeftPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.RIGHT:
                    if (!Player.instance.RightPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && active)
        {
            active = false;
            Debug.Log("Player enterted castle");
            switch (pose)
            {
                case Pose.DOWN:
                    if (!Player.instance.DownPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.UP:
                    if (!Player.instance.UpPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.LEFT:
                    if (!Player.instance.LeftPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
                case Pose.RIGHT:
                    if (!Player.instance.RightPose)
                        DestroyCastle();
                    else
                        SendGameController();
                    break;
            }

        }
    }

    public void ActivateFinish()
    {
        finish.gameObject.SetActive(true);
        finish.Activate();
    }
}

public enum Pose
{
    UP, 
    DOWN, 
    RIGHT,
    LEFT
}
