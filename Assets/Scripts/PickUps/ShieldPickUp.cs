using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : PickUp
{
    protected override void DoAction(Collider other)
    {
        other.GetComponent<Player>().ActivateShield();
    }
}
