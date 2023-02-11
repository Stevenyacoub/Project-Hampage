using UnityEngine;

public class Button : Interactable, ITrigger
{
    //Example of an Interactable and ITrigger Object
    // This button can be interacted with and trigger an activatable
    
    // For ITrigger:
    [SerializeField]
    private Activatable active;
    
    public Activatable activatable {
        get {return active;} 
        set {active = value;} 
    }

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
