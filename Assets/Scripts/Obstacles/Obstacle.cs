using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected bool isBroken = false;

    private void Update()
    {
        if (transform.position.z < -1.2f)
        {
            transform.localPosition -= new Vector3(0, 2 * Time.deltaTime, 0);
            //Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
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
        if (gameObject.GetComponentInChildren<Rigidbody>() != null)
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
                rb.AddForce(new Vector3(Random.Range(-6f, 6f), Random.Range(-6f, 6f), Random.Range(-6f, 6f)), ForceMode.Impulse);
            }
            Destroy(gameObject, 2);
        }
        else
            Destroy(gameObject);
    }
}
