using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{

    public static Shield instance;

    public float rotateSpeed = 2;


    private void Start()
    {
        instance = this;    
    }

    private void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }

    public void AcceptDamage()
    {
        gameObject.SetActive(false);
    }
}
