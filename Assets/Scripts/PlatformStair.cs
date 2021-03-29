using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStair : MonoBehaviour
{
    public bool up;
    private bool entered = false;

    private void OnTriggerEnter(Collider other)
    {

        if (entered)
        {
            if (RoadGenerator.instance != null)
            {
                if (up) RoadGenerator.instance.MoveUp();
                else RoadGenerator.instance.MoveDown();
            }
            else
            {
                if (up) LevelGenerator.instance.MoveUp();
                else LevelGenerator.instance.MoveDown();
            }
        }
        else
        {
            entered = true;
            if (RoadGenerator.instance != null)
            {
                if (up) RoadGenerator.instance.MoveDown();
                else RoadGenerator.instance.MoveUp();
            }
            else
            {
                if (up) LevelGenerator.instance.MoveDown();
                else LevelGenerator.instance.MoveUp();
            }
        }
    }
}
