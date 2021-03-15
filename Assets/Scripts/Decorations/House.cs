using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField]
    public float minAngle, maxAngle;

    private void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }
}
