using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool isBroken = false;

    private void Update()
    {
        if (transform.position.z < -2.3f)
        {
            //transform.localPosition -= new Vector3(0, 2 * Time.deltaTime, 0);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null && !isBroken)
        {
            isBroken = true;
            other.GetComponent<IDamageable>().AcceptDamage();
            DestroyMyself();
        }
    }


    protected virtual void DestroyMyself()
    {
        /*
        GetComponentInChildren<MeshRenderer>().enabled = false;
        foreach(Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
        Destroy(gameObject, 1);
        */

        Destroy(gameObject);
    }
}
