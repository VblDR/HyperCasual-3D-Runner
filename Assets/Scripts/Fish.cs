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
        anim = GetComponent<Animation>();
    }
}
