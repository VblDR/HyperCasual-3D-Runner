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
            transform.Rotate(rotateSpeed, 0, 0);
        if (transform.position.z < -2.2f)
        {
            //transform.localPosition -= new Vector3(0, 2 * Time.deltaTime, 0);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().AcceptDamage();
            gameObject.SetActive(false);
        }
    }

}
