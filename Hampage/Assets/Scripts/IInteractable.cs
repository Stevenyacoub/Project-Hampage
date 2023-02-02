using UnityEngine;


public interface IInteractable
{
    // Interface for interactable items
    // all interactables perform some sort of function on interaction
    bool registered {get;set;}
    bool performAction();
}
