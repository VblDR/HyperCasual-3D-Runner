using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.localPosition += new Vector3(1, 0, 0) * speed * Time.deltaTime;

        if(transform.localPosition.x > 78)
        {
            Destroy(gameObject);
        }
    }
}
