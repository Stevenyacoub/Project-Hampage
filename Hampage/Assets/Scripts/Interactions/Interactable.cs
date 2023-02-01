using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    // Base Interactable class
    // Interactables can surface a UI to let the player know they're pressable
    // All Interactables "performAction" when interacted with

    // An interactable is registered if the player is within interaction distance 
    public bool registered { get; set; }
    public Canvas interactUI;
    public Camera mainCam;
    bool UIShown;
    
    // Awake gets called before the first frame update
    void Awake() {
        // Turn off our "E" UI and take note
        interactUI.enabled = false;
        UIShown = false;
    }

    // Update gets called every frame update
    void Update() {
        
        // If UI is being shown, have it face the camera
        if(UIShown){
            interactUI.transform.LookAt(mainCam.transform);
        }
    }

    // This is called by the playermanager when this Interactable becomes priority
    public void ShowUI(){
        interactUI.enabled = true;
        UIShown = true;
    }

    // This is called when this Interactable is no longer priority, or when it's unregistered as an interactable
    public void HideUI(){
        interactUI.enabled = false;
        UIShown = false;
    }

    // Abstract method for all children of Interactable
    public abstract bool performAction();

    
}
