using UnityEngine;

public class Button : Interactable, ITrigger
{
    // Pair-Programmed by Giovanni Quevedo and Devin Elmore
    
    // -- Buttons are ITriggers that can be interacted with
    
    // For ITrigger:
    [SerializeField]
    private Activatable active;
    
    // Accessors for Activatable
    public Activatable activatable {
        get {return active;} 
        set {active = value;} 
    }

    // Activate our activatable if we have one, if not return false
    public bool activate(){
        if(active != null){
            return active.startActivation(); 
        }else{
            Debug.Log("No activatable to activate!");
            return false;
        }
    }

    // Button's unique implementation, activates it's activatable and notifies via console
    public override bool performAction(){
        //Since this button is also a trigger, it's perform action is to activate its activatable
        Debug.Log("Button pressed!");
        return activate();
    }

}
