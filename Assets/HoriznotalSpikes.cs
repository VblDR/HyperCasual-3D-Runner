using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoriznotalSpikes : Obstacle
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null && !isBroken)
        {
            if (Player.instance.activeShield)
                Shield.instance.AcceptDamage();
            else
                other.GetComponent<IDamageable>().AcceptDamage();
            isBroken = true;
            DestroyMyself();
        }
    }
}
