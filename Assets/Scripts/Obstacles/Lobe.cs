using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobe : MonoBehaviour
{
    public float rotateSpeed;

    private void Update()
    {
        transform.Rotate(rotateSpeed, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().AcceptDamage();
        }
    }

}
