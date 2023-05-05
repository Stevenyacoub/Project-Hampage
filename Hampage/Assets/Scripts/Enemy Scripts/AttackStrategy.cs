using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AttackStrategy
{
    int damageAmount
    {
        get { return damageAmount; }
    }

    void performAttack();
}
