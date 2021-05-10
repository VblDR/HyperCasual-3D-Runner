using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUp
{
    protected override void DoAction()
    {
        Player.instance.IncreaseHP();
    }
}
