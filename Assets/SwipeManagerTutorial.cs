using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManagerTutorial : MonoBehaviour
{
    public static SwipeManagerTutorial instance;

    private Vector2 startPos, deltaPos;
    private bool moving;
    private bool finish = false;
    const float swipeTreshold = 120f;

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
        if (!finish)
        {
            if (Input.GetKeyDown(KeyCode.D))
                PlayerTutorial.instance.Move(Direction.Right);
            if (Input.GetKeyDown(KeyCode.A))
                PlayerTutorial.instance.Move(Direction.Left);
            if (Input.GetKeyDown(KeyCode.W))
                PlayerTutorial.instance.Jump();
            if (Input.GetKeyDown(KeyCode.S))
                PlayerTutorial.instance.Slide();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
                PlayerTutorial.instance.SetRightPose();
            if (Input.GetKeyDown(KeyCode.D))
                PlayerTutorial.instance.SetLeftPose();
            if (Input.GetKeyDown(KeyCode.W))
                PlayerTutorial.instance.SetUpPose();
            if (Input.GetKeyDown(KeyCode.S))
                PlayerTutorial.instance.SetDownPose();
        }
    }

    private void AndroidInput()
    {
        if (!finish)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    startPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {

                    deltaPos = Input.GetTouch(0).position - startPos;

                    if (deltaPos.magnitude > 2 * swipeTreshold)
                    {
                        if (Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
                        {
                            if (deltaPos.x > 0)
                                PlayerTutorial.instance.Move(Direction.Right);
                            else
                                PlayerTutorial.instance.Move(Direction.Left);
                        }
                        else
                        {
                            if (deltaPos.y > 0)
                                PlayerTutorial.instance.Jump();
                            if (deltaPos.y < 0)
                                PlayerTutorial.instance.Slide();
                        }
                        startPos = Input.GetTouch(0).position;
                    }

                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    deltaPos = Input.GetTouch(0).position - startPos;

                    if (deltaPos.magnitude > swipeTreshold/2f)
                    {
                        if (Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
                        {
                            if (deltaPos.x > 0)
                                PlayerTutorial.instance.Move(Direction.Right);
                            else
                                PlayerTutorial.instance.Move(Direction.Left);
                        }
                        else
                        {
                            if (deltaPos.y > 0)
                                PlayerTutorial.instance.Jump();
                            if (deltaPos.y < 0)
                                PlayerTutorial.instance.Slide();
                        }
                    }

                }

            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    startPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    deltaPos = Input.GetTouch(0).position - startPos;

                    if (deltaPos.magnitude > swipeTreshold)
                    {
                        if (Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
                        {
                            if (deltaPos.x < 0)
                                PlayerTutorial.instance.SetRightPose();
                            else
                                PlayerTutorial.instance.SetLeftPose();
                        }
                        else
                        {
                            if (deltaPos.y > 0)
                                PlayerTutorial.instance.SetUpPose();
                            if (deltaPos.y < 0)
                                PlayerTutorial.instance.SetDownPose();
                        }
                    }

                }

            }
        }
    }

    public void SetFinish(bool value)
    {
        finish = value;
    }
}
