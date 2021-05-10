using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeFrames : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (Player.instance != null)
        {
            if (Player.instance.transform.position.x > 0)
                Player.instance.Move(Direction.Left);
            else if (Player.instance.transform.position.x < 0)
                Player.instance.Move(Direction.Right);
        }
        else
        {
            if (PlayerTutorial.instance.transform.position.x > 0)
                PlayerTutorial.instance.Move(Direction.Left);
            else if (PlayerTutorial.instance.transform.position.x < 0)
                PlayerTutorial.instance.Move(Direction.Right);
        }
        Destroy(gameObject);
    }
}
