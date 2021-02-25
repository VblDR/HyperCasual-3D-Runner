using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool isBroken = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null && !isBroken)
        {
            isBroken = true;
            other.GetComponent<IDamageable>().AcceptDamage();
            DestroyMyself();
        }
    }


    private void DestroyMyself()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        foreach(Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
        Destroy(gameObject, 1);
    }
}
