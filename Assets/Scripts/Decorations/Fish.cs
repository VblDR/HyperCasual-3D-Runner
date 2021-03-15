using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Animation anim;


    private void Update()
    {
        anim.Play();
    }

    private void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        anim = GetComponent<Animation>();
    }
}
