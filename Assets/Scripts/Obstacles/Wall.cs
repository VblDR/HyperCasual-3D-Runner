using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{

    protected override void DestroyMyself()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(Random.Range(-7f, 7f), Random.Range(-7f, 7f), Random.Range(-7f, 7f)), ForceMode.Impulse);
        }
        Destroy(gameObject, 2);
    }
}
