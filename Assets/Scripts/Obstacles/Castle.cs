using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private bool active = true;
    public Finish finish;

    public void DestroyCastle()
    {
        GameController.instance.DestroyCastle();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            finish.Unable();
            active = false;
            GameController.instance.SlowMotionOff();
            if (collision.GetComponentInParent<Player>().shield.activeSelf)
            {
                Shield.instance.AcceptDamage();
                Player.instance.SetEndPose();
            }
            else
            {
                Player.instance.Death();
                Player.instance.SetEndPose();
            }
            DestroyCastle();
        
        }
    }
}
