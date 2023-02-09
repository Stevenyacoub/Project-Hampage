using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    // Base Interactable class
    // All Interactables "performAction" when interacted with

    // An interactable is registered if the player is within interaction distance 
    public bool registered { get; set; }
    
    
    // Abstract method for all children of Interactable
    public abstract bool performAction();

    
}
