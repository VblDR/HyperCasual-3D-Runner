using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, PickUpObject
{

    public float rotateSpeed = 2;
    protected bool active = true;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }

    protected void OnTriggerEnter(Collider collision)
    {
        if (active)
        {
            active = false;
            DoAction();
            Destroy(gameObject);
        }
    }

    protected void OnTriggerStay(Collider collision)
    {
        if (active)
        {
            active = false;
            DoAction();
            Destroy(gameObject);
        }
    }

    protected void OnTriggerExit(Collider collision)
    {
        if (active)
        {
            active = false;
            DoAction();
            Destroy(gameObject);
        }
    }

    protected virtual void DoAction()
    {

    }
}


interface PickUpObject
{
    delegate void DoAction();
}