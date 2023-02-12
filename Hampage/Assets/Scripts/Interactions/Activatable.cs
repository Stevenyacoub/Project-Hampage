using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Giovanni Quevedo
// -- Activatables can perform a function when activated by an ITrigger

public abstract class Activatable : MonoBehaviour
{
    // Abstract method for activating an item
    public abstract bool startActivation();
}
