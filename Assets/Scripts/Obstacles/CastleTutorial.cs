using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTutorial : MonoBehaviour
{
    public float speed;
    private bool moving = false;
    private bool active = true;
    public MeshRenderer[] toDestroy;
    public GameObject toUnable;

    private void Update()
    {
        if (moving)
        {
            transform.localPosition -= new Vector3(0, 0, speed * Time.deltaTime);
        }
    }

    public void DestroyCastle()
    {
        toUnable.SetActive(false);
        TutorialController.instance.DestroyCastle();
        foreach (var c in toDestroy)
        {
            c.enabled = false;
        }
        foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(Random.Range(-6f, 6f), Random.Range(-6f, 6f), Random.Range(-6f, 6f)), ForceMode.Impulse);
        }
        Destroy(gameObject, 2);
    }

    public void MoveCastle()
    {
        moving = true;
    }

    public void StopCastle()
    {
        moving = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            active = false;
            if (collision.GetComponentInParent<PlayerTutorial>().shield.activeSelf) Shield.instance.AcceptDamage();
            else PlayerTutorial.instance.AcceptDamage();
            PlayerTutorial.instance.SetEndPose();
            DestroyCastle();
        }
    }
}