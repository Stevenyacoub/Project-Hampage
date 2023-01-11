using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Class used to manage player data not related to input
    //For input based management, see ControllerCharacter

    List<IInteractable> localInteractables;

    void Awake(){
        localInteractables = new List<IInteractable>();
    }

    // Interaction Logic:

    public void interact(){
        bool actionPerformed = false;

        //Check if we have any interactables, if so execute the first one
        if(localInteractables.Count != 0){
            actionPerformed = localInteractables[0].performAction();
        }

        //Debug.Log( actionPerformed ? "Interacted with " + localInteractables[0] : ( localInteractables.Count == 0 ? " No interactables." : "Interaction Failed"));
    }

    // Public methods allowing interactable items to add and remove themself from player's interact list 
    public void registerInteractable(IInteractable interactable){
        localInteractables.Add(interactable);
        interactable.registered = true;   
    }
    public void unregisterInteractable(IInteractable interactable){
        localInteractables.Remove(interactable);
        interactable.registered = false;   
    }

}
