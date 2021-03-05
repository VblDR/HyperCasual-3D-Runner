using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public static Camera instance;

    public Vector3 finishPos;
    public float speed;
    private bool finished;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (finished && Vector3.Distance(transform.position, finishPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPos, speed * Time.deltaTime);
        }
    }


    public void SetFinish()
    {
        finished = true;
        finishPos.x = transform.position.x;
        finishPos.y = transform.position.y;
    }
}
