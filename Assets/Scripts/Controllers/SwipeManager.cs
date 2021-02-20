using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;

    private Vector2 startPos, deltaPos;
    private bool moving;

    const float swipeTreshold = 0.1f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
#if UNITY_EDITOR
        PCInput();
#elif UNITY_ANDROID
        AndroidInput();
#endif
    }

    private void PCInput()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            moving = true;
        }
        else if(Input.GetMouseButtonUp(0) && moving)
        {
            moving = false;
        }

        deltaPos = Vector2.zero;
        if(moving && Input.GetMouseButton(0))
        {
            deltaPos = (Vector2)Input.mousePosition - startPos;
        }


        if(deltaPos.magnitude > swipeTreshold)
        {
            if (deltaPos.x > 0)
                Player.instance.Move(Direction.Right);
            else
                Player.instance.Move(Direction.Left);
           
            startPos = Input.mousePosition;
        }
        */

        if (Input.GetKeyDown(KeyCode.D))
            Player.instance.Move(Direction.Right);
        if (Input.GetKeyDown(KeyCode.A))
            Player.instance.Move(Direction.Left);
    }

    private void AndroidInput()
    {
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startPos = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                startPos = Input.GetTouch(0).position;
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                deltaPos = Input.GetTouch(0).position - startPos;

                if(deltaPos.magnitude > swipeTreshold)
                {
                    if (deltaPos.x > 0)
                        Player.instance.Move(Direction.Right);
                    else
                        Player.instance.Move(Direction.Left);
                }
            }

        }
    }
}

public enum Direction
{
    Left,
    Right,
}