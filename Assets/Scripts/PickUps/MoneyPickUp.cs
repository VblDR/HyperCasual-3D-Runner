using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUp : PickUp
{
    public int money;

    protected override void DoAction(Collider other)
    {
        if(Player.instance != null)
            other.GetComponent<Player>().IncreaseMoney(money);
        else
            other.GetComponent<PlayerTutorial>().IncreaseMoney(money);
    }

}
