using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    public float speed;
    private bool moving = false;

    public MeshRenderer[] toDestroy;

    private void Update()
    {
        if (moving)
        {
            transform.localPosition -= new Vector3(0, 0, speed * Time.deltaTime);
        }
    }

    public void DestroyCastle()
    {
        foreach(var c in toDestroy)
        {
            c.enabled = false;
        }
        foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
        Destroy(gameObject, 1);
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
        if (collision.CompareTag("Player") || collision.CompareTag("Shield"))
        {
            if (collision.CompareTag("Shield")) Shield.instance.AcceptDamage();
            if (collision.CompareTag("Player")) Player.instance.AcceptDamage();

            Player.instance.SetEndPose();
            DestroyCastle();
        }
    }
}
