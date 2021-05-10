using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUp : PickUp
{
    public int money;
    protected override void DoAction()
    {
        if(Player.instance != null)
            Player.instance.IncreaseMoney(money);
        else
            PlayerTutorial.instance.IncreaseMoney(money);
    }

}
