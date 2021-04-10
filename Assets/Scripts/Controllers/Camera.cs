using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public static Camera instance;

    public Vector3 finishPos;
    public float speed;
    private bool finished;

    public Vector3 startPos;
    public Quaternion startRot;
    public float rotationSpeed;
    private bool start = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (start && (Vector3.Distance(transform.position, startPos) > 0.01f || Quaternion.Angle(transform.rotation, startRot) > Mathf.Epsilon))
        {
            if (Vector3.Distance(transform.position, startPos) > 0.01f)
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            else
                transform.position = startPos;

            if (Quaternion.Angle(transform.rotation, startRot) > Mathf.Epsilon)
                transform.rotation = Quaternion.Slerp(transform.rotation, startRot, rotationSpeed * Time.deltaTime);
            else
                transform.rotation = startRot;

            if (Vector3.Distance(transform.position, startPos) < Mathf.Epsilon && Quaternion.Angle(transform.rotation, startRot) < Mathf.Epsilon/2)
                start = false;

        }
        

        if (finished && Vector3.Distance(transform.position, finishPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPos, speed * Time.deltaTime);
        }
    }

    public void SetStart()
    {
        start = true;
    }


    public void SetFinish()
    {
        finished = true;
        finishPos.x = transform.position.x;
    }
}
