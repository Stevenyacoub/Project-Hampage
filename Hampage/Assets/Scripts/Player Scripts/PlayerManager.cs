using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Class used to manage player data not related to input
    //For input based management, see ControllerCharacter

    // For Interaction:
    // - localInteractables is a list of interactables within range
    // - priority is the interactable we want (closest to us) (for use when multiple interactables are present)
    List<Interactable> localInteractables;
    Interactable priority;

    // A list of "collected" items
    // !! TODO: Items currently delete themselves, so the items themselves can't be accessed!!
    // !! We need to build a inventory version of items when they're collected, this is only a temporary demonstration!!!
    List<Item> inventory;


    // Awake gets called before the first frame update
    void Awake(){
        localInteractables = new List<Interactable>();
        inventory = new List<Item>();
    }

    // Update gets called each frame update
    void Update(){
        CheckForInteractions();
    }

    // Method to add items to inventory
    // !! This is only temporary, as items destroy themselves when collected
    // TODO: Refactor to use an inventory version of items
    public void addToInventory(Item item){
        inventory.Add(item);
        Debug.Log("Inventory Count:" + inventory.Count);
    }

    // -- // Interaction Logic

    // This checks to see if there are any interactables near us
    // if so, it selects a priority based on distance
    // It also displays the UI for the priority interactable
    void CheckForInteractions(){
        if(localInteractables.Count != 0){
            // If we have more than one
            if(localInteractables.Count > 1){
                FindClosestInteractable();
                priority.ShowUI();
                HideNotPriority();
            }else{
                // If there's only one
                priority = localInteractables[0];
                priority.ShowUI();
            }
        }else{
            priority = null;
        }
    }

    // This gets called when the user presses the interact button (PC: "E")
    // Interacts with our priority interactab;e
    public void interact(){
        bool actionPerformed = false;

        //Check if we have a priority interactable, execute if so
        if(priority){
            actionPerformed = priority.performAction();
        }
    }

    // Finds the closest interactable and sets it as our priority (when multiple are present)
    void FindClosestInteractable(){
        // Setting the leastDist to distance between us and first object
        float leastDist = Vector3.Distance(this.gameObject.transform.position, localInteractables[0].gameObject.transform.position);
        // Temporarily setting that one as the priority
        priority = localInteractables[0];
        // Searching for a closer one using a dynamic loop
        foreach (var inter in localInteractables)
        {
            float thisDist = Vector3.Distance(this.gameObject.transform.position, inter.gameObject.transform.position);
            if( thisDist < leastDist){
                priority = inter;
                leastDist = thisDist;
            }
        }
    }

    // Hide the UI of non-prioritized interactables
    void HideNotPriority(){
        foreach (var inter in localInteractables)
        {
            if (!inter.Equals(priority)){
                inter.HideUI();
            }
        }
    }

    // Public methods allowing interactable items to add and remove themself from player's interact list 

    // Lets an interactable register
    public void registerInteractable(Interactable interactable){
        localInteractables.Add(interactable);
        interactable.registered = true;   
    }

    // Unregisters an interactable, while hiding it's UI
    public void unregisterInteractable(Interactable interactable){
        localInteractables.Remove(interactable);
        interactable.registered = false;
        interactable.HideUI();
    }

    

}
