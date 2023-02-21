using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AttackStrategy
{
    int damageModifier
    {
        get { return damageModifier; }
    }

    void performAttack();
}
