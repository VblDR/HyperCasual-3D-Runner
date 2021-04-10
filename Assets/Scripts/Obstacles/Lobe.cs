using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobe : MonoBehaviour
{
    public float rotateSpeed;
    public bool rotateable;

    private void Update()
    {
        if(rotateable)
            transform.Rotate(0, 0, rotateSpeed);
        if (transform.position.z < -2.2f)
        {
            //transform.localPosition -= new Vector3(0, 2 * Time.deltaTime, 0);
            Destroy(gameObject);
        }
    }

}
