using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStair : MonoBehaviour
{
    public bool up;

    private bool active = true;
   
    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            active = false;
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
    }


    private void OnTriggerExit(Collider other)
    {
        if (active)
        {
            active = false;
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
    }
}
