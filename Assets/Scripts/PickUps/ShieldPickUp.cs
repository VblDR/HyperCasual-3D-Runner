using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : PickUp
{



    protected override void DoAction()
    {
        if(Player.instance != null)
            Player.instance.ActivateShield();
        else
            PlayerTutorial.instance.ActivateShield();
    }
}
