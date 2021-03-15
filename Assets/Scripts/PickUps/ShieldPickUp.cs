using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : PickUp
{
    protected override void DoAction(Collider other)
    {
        if(Player.instance != null)
            other.GetComponent<Player>().ActivateShield();
        else
            other.GetComponent<PlayerTutorial>().ActivateShield();
    }
}
