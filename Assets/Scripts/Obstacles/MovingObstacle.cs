using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : Obstacle
{
    public float speed;
    private bool moving = false;
    public float rotateSpeed;


    private void Update()
    {
        if (transform.localPosition.x == 1 && !moving)
            StartCoroutine(Moving(Direction.Left));
        else if (transform.localPosition.x == -1 && !moving)
            StartCoroutine(Moving(Direction.Right));

        transform.Rotate(0, rotateSpeed, 0);
    }

    private IEnumerator Moving(Direction dir)
    {
        moving = true;
        Vector3 move;
        float x;
        if (dir.Equals(Direction.Left))
        {
            move = new Vector3(-speed, 0, 0);
            x = -1;
        }
        else 
        {
            move = new Vector3(speed, 0, 0);
            x = 1;
        }

        while(Mathf.Abs(x - transform.localPosition.x) > 0.01f)
        {
            transform.localPosition += move * Time.deltaTime;
            if (transform.localPosition.x > 1 || transform.localPosition.x < -1)
                break;
            yield return null;
        }
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        moving = false;
    }
}
