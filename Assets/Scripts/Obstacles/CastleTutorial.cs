using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTutorial : MonoBehaviour
{
    private bool active = true;
    public Finish finish;


    public void DestroyCastle()
    { 
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            TutorialController.instance.SlowMotionOff();
            finish.Unable();
            active = false;
            if (collision.GetComponentInParent<PlayerTutorial>().shield.activeSelf) Shield.instance.AcceptDamage();
            else PlayerTutorial.instance.AcceptDamage();
            PlayerTutorial.instance.SetEndPose();
            DestroyCastle();
        }
    }
}
