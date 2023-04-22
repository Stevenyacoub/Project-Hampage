using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBox : MonoBehaviour
{
    // Created by Giovanni Quevedo
    // -- This class enables the player to interact with items and triggers in the world using the interact button
    // -- The InteractBox is the hitbox that handles the interaction system, and acts as the players interaction range

    //list of interactables within range
    List<Interactable> localInteractables;
    //the interactable we want to interact with (closest)
    Interactable priority;
    //current instance of UI element to manipulate
    public GameObject interactUI;
    bool UIShown = false;
    //camera reference for UI rotation
    public Camera mainCam;

    //height to spawn interact UI
    public float UIheight = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        // Instantiate interactable list
        localInteractables = new List<Interactable>();
        // Disable interact UI
        interactUI.SetActive(false);
    }

    // Update is called every frame
    // - Constantly check for interactions and show UI if needed
    void Update(){
        CheckForInteractions();
        if(UIShown)
            RotateUI();
    }
    
    // -- // Interaction Logic

    // This checks to see if there are any interactables near us
    // if so, it selects a priority based on distance
    // It also displays the UI for the priority interactable, and hides the UI if no priority exists
    void CheckForInteractions(){
        if(localInteractables.Count != 0){
            // If we have atleast one
            if(localInteractables.Count > 1){
                FindClosestInteractable();
                ShowUIForInteractable(priority);
            }else{
                priority = localInteractables[0];
                ShowUIForInteractable(priority);
            }
        }else{
            priority = null;
            if(UIShown)
                HideInteractUI();
        }
    }

    // This gets called when the user presses the interact button (PC: "E")
    // Interacts with our priority interactable
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

    // Face UI to the camera
    void RotateUI(){
        interactUI.transform.LookAt(mainCam.transform);
    }

    // Activate our interact UI at the given position
    void ShowUIForInteractable(Interactable interactable){
        // show our UI element, and take note
        interactUI.SetActive(true);  
        UIShown = true;

        // Move the UI to where the Interactable is located, with height offset
        interactUI.transform.position = interactable.transform.position + new Vector3(0, UIheight, 0);
    }

    // Hide UI
    void HideInteractUI(){
        interactUI.SetActive(false);
        UIShown = false;
    }

    // -- // Public methods allowing interactable items to add and remove themself from player's interact list 

    // Lets an interactable register
    public void RegisterInteractable(Interactable interactable){
        localInteractables.Add(interactable);
        interactable.registered = true;   
    }

    // Unregisters an interactable, while hiding it's UI
    public void UnregisterInteractable(Interactable interactable){
        localInteractables.Remove(interactable);
        interactable.registered = false;
    }

    // -- // Collision with Interactables

    //Called when a rigidbody enters the collider
    private void OnTriggerEnter(Collider other)
    {
        //Try to get an interactable out of the collider
        // !! - this only works if the collider and the script are on the same gameobject! Not a child or parent
        if(other.TryGetComponent<Interactable>(out Interactable interactable)){
            
            //We got an interactable!
            //Now try to see if we can cast a ray to it (meaning nothing is in the way)
            
            //Shoot a ray to the object
            if(Physics.Raycast(transform.position, (other.transform.position - transform.position), out RaycastHit hit)){

                //See if the hit object also has an interactable (ignore if not)
                if(hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable hitInteractable)){

                    //If we can reach it and it's not registered
                    if(hitInteractable.Equals(interactable) && !interactable.registered){
                        RegisterInteractable(interactable);
                    }
                }   
            }  
        }
    }

    //Called while rigidbody is still in collider
    private void OnTriggerStay(Collider other) {
        if(other.TryGetComponent<Interactable>(out Interactable interactable)){
            
            //Shoot a ray to the object
            if(Physics.Raycast(transform.position, (other.transform.position - transform.position), out RaycastHit hit)){

                //See if the hit object also has an interactable (ignore if not)
                if(hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable hitInteractable)){

                    //If we can reach it and it's not registered
                    if(hitInteractable.Equals(interactable) && !interactable.registered){
                        RegisterInteractable(interactable);
                    // Or if we can't reach but it is...
                    }else if(!hitInteractable.Equals(interactable) && interactable.registered){
                        UnregisterInteractable(interactable);
                    }
                }   
            }        
        }
    }

    //Called when rigidbody leaves the collider
    private void OnTriggerExit(Collider other) {
        if(other.TryGetComponent<Interactable>(out Interactable interactable)){
            if(interactable.registered){
               UnregisterInteractable(interactable);
            }
        }
    }
}
