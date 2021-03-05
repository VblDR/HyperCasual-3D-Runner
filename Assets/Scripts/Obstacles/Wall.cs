using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{

    protected override void DestroyMyself()
    {
        Destroy(gameObject);
    }
}
