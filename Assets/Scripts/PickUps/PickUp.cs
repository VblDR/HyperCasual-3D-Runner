using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, PickUpObject
{

    public float rotateSpeed = 2;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoAction(other);
            Destroy(gameObject);
        }
    }

    protected virtual void DoAction(Collider other)
    {

    }
}


interface PickUpObject
{
    delegate void DoAction(Collider other);
}