using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AttackStrategy
{
    ControllerCharacter contr
    {
        get { return contr; }
    }
    int damageModifier
    {
        get { return damageModifier; }
    }

    void performAttack();
}
