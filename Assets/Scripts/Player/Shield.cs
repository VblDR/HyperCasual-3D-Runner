using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    public void AcceptDamage()
    {
        gameObject.SetActive(false);
    }
}
